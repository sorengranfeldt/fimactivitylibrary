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
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class AddRemoveFromMultiValueActivity : SequenceActivity
    {

        #region Properties

        public AddRemoveFromMultiValueActivity()
        {
            InitializeComponent();
        }

		public static DependencyProperty HandleDuplicateProperty = DependencyProperty.Register("HandleDuplicate", typeof(string), typeof(AddRemoveFromMultiValueActivity));
		[Description("HandleDuplicate")]
		[Category("HandleDuplicate Category")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string HandleDuplicate
		{
			get
			{
				return ((string)(base.GetValue(AddRemoveFromMultiValueActivity.HandleDuplicateProperty)));
			}
			set
			{
				base.SetValue(AddRemoveFromMultiValueActivity.HandleDuplicateProperty, value);
			}
		}

        public static DependencyProperty ActorProperty = DependencyProperty.Register("Actor", typeof(string), typeof(AddRemoveFromMultiValueActivity));
        [Description("Actor")]
        [Category("Actor Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Actor
        {
            get
            {
                return ((string)(base.GetValue(AddRemoveFromMultiValueActivity.ActorProperty)));
            }
            set
            {
                base.SetValue(AddRemoveFromMultiValueActivity.ActorProperty, value);
            }
        }

        public static DependencyProperty LookupXPathProperty = DependencyProperty.Register("LookupXPath", typeof(System.String), typeof(AddRemoveFromMultiValueActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String LookupXPath
        {
            get
            {
                return ((string)(base.GetValue(LookupXPathProperty)));
            }
            set
            {
                base.SetValue(LookupXPathProperty, value);
            }
        }

        public static DependencyProperty ResolvedLookupXPathProperty = DependencyProperty.Register("ResolvedLookupXPath", typeof(System.String), typeof(AddRemoveFromMultiValueActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String ResolvedLookupXPath
        {
            get
            {
                return ((string)(base.GetValue(AddRemoveFromMultiValueActivity.ResolvedLookupXPathProperty)));
            }
            set
            {
                base.SetValue(AddRemoveFromMultiValueActivity.ResolvedLookupXPathProperty, value);
            }
        }

        public static DependencyProperty ValueToAddRemoveProperty = DependencyProperty.Register("ValueToAddRemove", typeof(string), typeof(AddRemoveFromMultiValueActivity));
        [Description("ValueToAddRemove")]
        [Category("ValueToAddRemove Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ValueToAddRemove
        {
            get
            {
                return ((string)(base.GetValue(AddRemoveFromMultiValueActivity.ValueToAddRemoveProperty)));
            }
            set
            {
                base.SetValue(AddRemoveFromMultiValueActivity.ValueToAddRemoveProperty, value);
            }
        }

        public static DependencyProperty MultivalueAttributeNameProperty = DependencyProperty.Register("MultivalueAttributeName", typeof(string), typeof(AddRemoveFromMultiValueActivity));
        [Description("MultivalueAttributeName")]
        [Category("MultivalueAttributeName Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string MultivalueAttributeName
        {
            get
            {
                return ((string)(base.GetValue(AddRemoveFromMultiValueActivity.MultivalueAttributeNameProperty)));
            }
            set
            {
                base.SetValue(AddRemoveFromMultiValueActivity.MultivalueAttributeNameProperty, value);
            }
        }

        public static DependencyProperty AddRemoveActionProperty = DependencyProperty.Register("AddRemoveAction", typeof(string), typeof(AddRemoveFromMultiValueActivity));
        [Description("AddRemoveAction")]
        [Category("AddRemoveAction Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AddRemoveAction
        {
            get
            {
                return ((string)(base.GetValue(AddRemoveFromMultiValueActivity.AddRemoveActionProperty)));
            }
            set
            {
                base.SetValue(AddRemoveFromMultiValueActivity.AddRemoveActionProperty, value);
            }
        }


        public static DependencyProperty CurrentRequestProperty = DependencyProperty.Register("CurrentRequest", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.RequestType), typeof(AddRemoveFromMultiValueActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public RequestType CurrentRequest
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.RequestType)(base.GetValue(CurrentRequestProperty)));
            }
            set
            {
                base.SetValue(CurrentRequestProperty, value);
            }
        }

        #endregion

        private void SetupVariablesActivity_ExecuteCode(object sender, EventArgs e)
        {
            foreach (CreateRequestParameter u in this.CurrentRequest.ParseParameters<CreateRequestParameter>())
            {
            }

            Debugging.Log("Enter SetupVariablesActivity_ExecuteCode");
            Enumerate.ActorId = new Guid(this.Actor);
            Debugging.Log("XPath (resolved)", this.ResolvedLookupXPath);
            Enumerate.XPathFilter = this.ResolvedLookupXPath;
            Enumerate.Attributes = new string[] { this.MultivalueAttributeName };
            Enumerate.PageSize = 100;
            Debugging.Log("Leave SetupVariablesActivity_ExecuteCode");
        }

        private void MoreTargetsToUpdate_Condition(object sender, ConditionalEventArgs e)
        {
            Debugging.Log("Enter SetupVariablesActivity_ExecuteCode");
            // return true if there are any more source values to resolve
            e.Result = Enumerate.EnumeratedResources != null ? Enumerate.EnumeratedResources.Count() > 0 : false;

            if (e.Result)
            {
                Debugging.Log("Update target(s) left", Enumerate.EnumeratedResources.Count());
                ResourceType resource = Enumerate.EnumeratedResources[0];
                Enumerate.EnumeratedResources.RemoveAt(0);

                UpdateMVOnTarget.Action = this.AddRemoveAction;
                UpdateMVOnTarget.Destination = this.MultivalueAttributeName;
                UpdateMVOnTarget.ValueToAddOrRemove = this.ValueToAddRemoveResolved;
                UpdateMVOnTarget.TargetIdToUpdate = resource.ObjectID.GetGuid();
            }
            else
            {
                Debugging.Log("No more source values to resolve");
            }
        }

        public static DependencyProperty ValueToAddRemoveResolvedProperty = DependencyProperty.Register("ValueToAddRemoveResolved", typeof(object), typeof(Granfeldt.FIM.ActivityLibrary.AddRemoveFromMultiValueActivity));

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String ValueToAddRemoveResolved
        {
            get
            {
                return ((string)(base.GetValue(Granfeldt.FIM.ActivityLibrary.AddRemoveFromMultiValueActivity.ValueToAddRemoveResolvedProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.AddRemoveFromMultiValueActivity.ValueToAddRemoveResolvedProperty, value);
            }
        }

        private void LogArgumentException_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log(sender.ToString());
            Debugging.Log(faultHandlerArgumentException.Fault);
        }

        private void LogException_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log(sender.ToString());
            Debugging.Log(faultHandlerGeneralException.Fault);
        }
    }
}
