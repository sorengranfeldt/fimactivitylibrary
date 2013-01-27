using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Collections.Generic;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

// The workflow was triggered, and I get Denied on the Group creation. I've populated all of the required attribute required to create a group (ObjectType = Group, Type = Distribution, Scope = Universal, DisplayName = SomeUniqueName, MembershipLocked = false, MembershipAddWorkflow = Owner Approval, Domain = MyDomain, DisplayedOwner = containingWorkflow.ActorId, Owner = containingWorkflow.ActorId, MailNickname = SomeUniqueName, AccountName = SomeUniqueName). 
// http://www.fimspecialist.com/fim-portal/custom-workflow-examples/createresourceactivity-example/

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class CreateObjectActivity : SequenceActivity
    {

        #region Properties

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CreateObjectActivity));
        [Description("Title")]
        [Category("Title Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get
            {
                return ((string)(base.GetValue(CreateObjectActivity.TitleProperty)));
            }
            set
            {
                base.SetValue(CreateObjectActivity.TitleProperty, value);
            }
        }

        public static DependencyProperty InitialValuePairsProperty = DependencyProperty.Register("InitialValuePairs", typeof(string[]), typeof(CreateObjectActivity));
        [Description("InitialValuePairs")]
        [Category("InitialValuePairs Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string[] InitialValuePairs
        {
            get
            {
                return ((string[])(base.GetValue(CreateObjectActivity.InitialValuePairsProperty)));
            }
            set
            {
                base.SetValue(CreateObjectActivity.InitialValuePairsProperty, value);
            }
        }

        public static DependencyProperty NewObjectTypeProperty = DependencyProperty.Register("NewObjectType", typeof(string), typeof(CreateObjectActivity));
        [Description("NewObjectType")]
        [Category("NewObjectType Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string NewObjectType
        {
            get
            {
                return ((string)(base.GetValue(CreateObjectActivity.NewObjectTypeProperty)));
            }
            set
            {
                base.SetValue(CreateObjectActivity.NewObjectTypeProperty, value);
            }
        }

        public static DependencyProperty ExistenceLookupFilterProperty = DependencyProperty.Register("ExistenceLookupFilter", typeof(string), typeof(CreateObjectActivity));
        [Description("ExistenceLookupFilter")]
        [Category("ExistenceLookupFilter Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ExistenceLookupFilter
        {
            get
            {
                return ((string)(base.GetValue(CreateObjectActivity.ExistenceLookupFilterProperty)));
            }
            set
            {
                base.SetValue(CreateObjectActivity.ExistenceLookupFilterProperty, value);
            }
        }

        public static DependencyProperty ResolvedInitialValueGrammerProperty = DependencyProperty.Register("ResolvedInitialValueGrammer", typeof(string), typeof(CreateObjectActivity));
        [Description("ResolvedInitialValueGrammer")]
        [Category("ResolvedInitialValueGrammer Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ResolvedInitialValueGrammer
        {
            get
            {
                return ((string)(base.GetValue(CreateObjectActivity.ResolvedInitialValueGrammerProperty)));
            }
            set
            {
                base.SetValue(CreateObjectActivity.ResolvedInitialValueGrammerProperty, value);
            }
        }

        public static DependencyProperty CreatedObjectIdProperty = DependencyProperty.Register("CreatedObjectId", typeof(System.Guid), typeof(Granfeldt.FIM.ActivityLibrary.CreateObjectActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public Guid CreatedObjectId
        {
            get
            {
                return ((System.Guid)(base.GetValue(Granfeldt.FIM.ActivityLibrary.CreateObjectActivity.CreatedObjectIdProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.CreateObjectActivity.CreatedObjectIdProperty, value);
            }
        }

        #endregion


        public CreateObjectActivity()
        {
            InitializeComponent();
        }

        private void DoExistenceCheck_Condition(object sender, ConditionalEventArgs e)
        {
            Debugging.Log("Activity initialized");
            e.Result = false;
            if (string.IsNullOrEmpty(this.ExistenceLookupFilter))
            {
                Debugging.Log("Existence filter is empty. No existence check will be done.");
            }
            else
            {
                e.Result = true;
                Debugging.Log("Resolving existence test filer", this.ExistenceLookupFilter);
                ResolveExistenceTestFilter.GrammarExpression = this.ExistenceLookupFilter;
            }
        }

        private void PrepareExistenceCheckLookup_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Preparing lookup of", ResolveExistenceTestFilter.ResolvedExpression);
            ExistenceLookup.ActorId = WellKnownGuids.FIMServiceAccount;
            ExistenceLookup.PageSize = 100;
            ExistenceLookup.Attributes = new string[] { "ObjectID" };
            ExistenceLookup.XPathFilter = ResolveExistenceTestFilter.ResolvedExpression;
        }

        private void VerifyExistenceLookupResult_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Found results #", ExistenceLookup.TotalResultsCount);

        }

        private void ShouldCreateObject_Condition(object sender, ConditionalEventArgs e)
        {
            e.Result = !(ExistenceLookup.TotalResultsCount > 0);
            Debugging.Log(string.Format("Object will {0}be created", e.Result ? "" : "NOT "));
        }

        public class AttributeValue
        {
            public object Value { get; set; }
            public string TargetAttributeName { get; set; }
            public bool ShouldResolve = false;
        }
        List<AttributeValue> AttributeValues = new List<AttributeValue>();
        AttributeValue ResolvedAttributeValue;

        private void PrepareResolveInitialValues_ExecuteCode(object sender, EventArgs e)
        {
            // parse attribute pairs and build hashtable for easy processing
            foreach (string attribute in this.InitialValuePairs)
            {
                string[] s = attribute.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length == 2)
                {
                    Debugging.Log("Adding initial value expression", attribute);
                    AttributeValues.Add(new AttributeValue { ShouldResolve = s[0].Trim().Contains("[//"), Value = s[0].Trim(), TargetAttributeName = s[1].Trim() });
                }
                else
                {
                    Debugging.Log("Invalid initial value entry", attribute);
                }
            }
        }

        private void MoreToResolve_Condition(object sender, ConditionalEventArgs e)
        {
            // return true if there are any more source values to resolve
            e.Result = AttributeValues.Where(av => av.ShouldResolve).Count() > 0;
            if (e.Result)
            {
                ResolvedAttributeValue = AttributeValues.Where(a => a.ShouldResolve).FirstOrDefault();
                Debugging.Log("Resolving", ResolvedAttributeValue.Value);

                // prepare ResolveGrammer activity
                ResolveInitialValueGrammar.GrammarExpression = (string)ResolvedAttributeValue.Value;
                //ResolveInitialValueGrammar.ResolvedExpression = this.ResolvedInitialValueGrammer;
            }
            else
            {
                Debugging.Log("No more source values to resolve");
            }
        }

        private void FetchResolvedGrammar_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Resolved value", ResolveInitialValueGrammar.ResolvedExpression);
            Debugging.Log("Resolved value (this)", this.ResolvedInitialValueGrammer);
            ResolvedAttributeValue.Value = ResolveInitialValueGrammar.ResolvedExpression;
            ResolvedAttributeValue.ShouldResolve = false;
        }

        private void NonExistingGrammarException_ExecuteCode(object sender, EventArgs e)
        {
            // if we get here, the resolve has failed. If the value that we're
            // trying to resolve doesn't exist, we also get here. We
            // assume that there is no value and set source value to null
            // which effectively results in a 'Delete' operation on
            // the target attribute value (if present)
            Debugging.Log("Missing value or invalid XPath reference", this.ResolveInitialValueGrammar.GrammarExpression);
            ResolvedAttributeValue.Value = null;
            ResolvedAttributeValue.ShouldResolve = false;
        }

        private void PrepareCreateObject_ExecuteCode(object sender, EventArgs e)
        {
            List<CreateRequestParameter> RequestParameters = new List<CreateRequestParameter>();
            Debugging.Log("Preparing to create object type", this.NewObjectType);
            RequestParameters.Add(new CreateRequestParameter("ObjectType", this.NewObjectType));
            foreach (AttributeValue value in AttributeValues)
            {
                if (value.Value == null)
                {
                    Debugging.Log("Skip adding NULL parameter", value.TargetAttributeName);
                }
                else
                {
                    object formattedValue = FIMAttributeUtilities.FormatValue(value.Value);
                    Debugging.Log(string.Format("Adding {0} (formatted value)", value.TargetAttributeName), formattedValue);
                    RequestParameters.Add(new CreateRequestParameter(value.TargetAttributeName, formattedValue));
                }
            }
            CreateNewObject.ActorId = WellKnownGuids.FIMServiceAccount;
            CreateNewObject.CreateParameters = RequestParameters.ToArray();
        }

        private void ExitGracefully_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Activity exited");
        }

    }
}
