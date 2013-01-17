// January 17, 2013 | Soren Granfeldt
//  - code revised and partially rewritten before CodePlex release

using System;
using System.ComponentModel;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{

    public partial class LookupAttributeValueActivity : SequenceActivity
    {

        #region Public Properties

        public static DependencyProperty LookupActorProperty = DependencyProperty.Register("LookupActor", typeof(string), typeof(LookupAttributeValueActivity));
        [Description("LookupActor")]
        [Category("LookupActor Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string LookupActor
        {
            get
            {
                return ((string)(base.GetValue(LookupAttributeValueActivity.LookupActorProperty)));
            }
            set
            {
                base.SetValue(LookupAttributeValueActivity.LookupActorProperty, value);
            }
        }

        public static DependencyProperty AttributeNameProperty = DependencyProperty.Register("AttributeName", typeof(System.String), typeof(LookupAttributeValueActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String AttributeName
        {
            get
            {
                return ((string)(base.GetValue(LookupAttributeValueActivity.AttributeNameProperty)));
            }
            set
            {
                base.SetValue(LookupAttributeValueActivity.AttributeNameProperty, value);
            }
        }

        public static DependencyProperty XPathFilterProperty = DependencyProperty.Register("XPathFilter", typeof(System.String), typeof(LookupAttributeValueActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String XPathFilter
        {
            get
            {
                return ((string)(base.GetValue(LookupAttributeValueActivity.XPathFilterProperty)));
            }
            set
            {
                base.SetValue(LookupAttributeValueActivity.XPathFilterProperty, value);
            }
        }

        public static DependencyProperty DestinationProperty = DependencyProperty.Register("Destination", typeof(System.String), typeof(LookupAttributeValueActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String Destination
        {
            get
            {
                return ((string)(base.GetValue(LookupAttributeValueActivity.DestinationProperty)));
            }
            set
            {
                base.SetValue(LookupAttributeValueActivity.DestinationProperty, value);
            }
        }

        public static DependencyProperty ResolvedXPathFilterProperty = DependencyProperty.Register("ResolvedXPathFilter", typeof(System.String), typeof(LookupAttributeValueActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String ResolvedXPathFilter
        {
            get
            {
                return ((string)(base.GetValue(LookupAttributeValueActivity.ResolvedXPathFilterProperty)));
            }
            set
            {
                base.SetValue(LookupAttributeValueActivity.ResolvedXPathFilterProperty, value);
            }
        }

        public static DependencyProperty NonUniqueValueActionProperty = DependencyProperty.Register("NonUniqueValueAction", typeof(string), typeof(LookupAttributeValueActivity));
        [Description("NonUniqueValueAction")]
        [Category("NonUniqueValueAction Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string NonUniqueValueAction
        {
            get
            {
                return ((string)(base.GetValue(LookupAttributeValueActivity.NonUniqueValueActionProperty)));
            }
            set
            {
                base.SetValue(LookupAttributeValueActivity.NonUniqueValueActionProperty, value);
            }
        }

        #endregion

        #region Code

        public LookupAttributeValueActivity()
        {
            InitializeComponent();
        }

        private void SetupVariablesActivity_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Enter SetupVariablesActivity_ExecuteCode");
            Enumerate.ActorId = new Guid(this.LookupActor);
            Debugging.Log("XPath (resolved)", this.ResolvedXPathFilter);
            Enumerate.XPathFilter = this.ResolvedXPathFilter;
            Enumerate.Attributes = new string[] { this.AttributeName };
            Enumerate.PageSize = 100;
            Debugging.Log("Leave SetupVariablesActivity_ExecuteCode");
        }

        private void UpdateTargetOrWorkflowData_Condition(object sender, ConditionalEventArgs e)
        {
            Debugging.Log("Enter UpdateTargetOrWorkflowData_Condition");
            e.Result = false; // go in workflowdata direction (we'll update workflowdata here, so actually nothing to do)

            object newValue = null;
            ResourceType r;

            Debugging.Log("Results #", Enumerate.TotalResultsCount);
            if (Enumerate.TotalResultsCount > 1)
            {
                switch (this.NonUniqueValueAction)
                {
                    case "RETURNLAST":
                        // get the first object in the resultset and grab the
                        // value from the requested attribute
                        r = Enumerate.EnumeratedResources[Enumerate.TotalResultsCount - 1];
                        newValue = r[this.AttributeName];
                        break;
                    case "ERROR":
                        Debugging.Log(new System.ArgumentException("Too many results returned from lookup"));
                        break;
                    default:
                        // get the first object in the resultset and grab the
                        // value from the requested attribute
                        r = Enumerate.EnumeratedResources[0];
                        newValue = r[this.AttributeName];
                        break;
                }
            }

            SequentialWorkflow containingWorkflow = null;
            if (!SequentialWorkflow.TryGetContainingWorkflow(this, out containingWorkflow))
            {
                throw new InvalidOperationException("Could not get parent workflow!");
            }

            // separate destination object and destination attribute.
            string destination = null;
            string destinationAttribute = null;
            StringUtilities.ExtractWorkflowExpression(this.Destination, out destination, out destinationAttribute);

            if (!string.IsNullOrEmpty(destinationAttribute))
            {
                if (destination.Equals("workflowdata", StringComparison.OrdinalIgnoreCase))
                {
                    // write output value to WorkflowDictionary.
                    if ((containingWorkflow != null) && (newValue != null))
                    {
                        containingWorkflow.WorkflowDictionary.Add(destinationAttribute, newValue);
                    }
                }
                else
                {
                    if (destination.Equals("target", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Result = true;
                        UpdateTargetIfNeeded.ActorId = new Guid(this.LookupActor);
                        UpdateTargetIfNeeded.AttributeName = destinationAttribute;
                        UpdateTargetIfNeeded.NewValue = newValue;
                        UpdateTargetIfNeeded.TargetId = containingWorkflow.TargetId;
                    }
                }
            }
            Debugging.Log("Exit UpdateTargetOrWorkflowData_Condition");
        }

        #endregion

    }

}
