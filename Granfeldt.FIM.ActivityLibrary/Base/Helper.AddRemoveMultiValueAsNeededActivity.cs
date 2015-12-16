using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.ResourceManagement.Workflow.Activities;
using System.Collections.ObjectModel;
using Microsoft.ResourceManagement.WebServices;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class AddRemoveMultiValueActivityAsNeeded : SequenceActivity
    {
        public static DependencyProperty ActionProperty = DependencyProperty.Register("Action", typeof(System.String), typeof(AddRemoveMultiValueActivityAsNeeded));
        public static DependencyProperty TargetResourceProperty = DependencyProperty.Register("TargetResource", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType), typeof(AddRemoveMultiValueActivityAsNeeded));
        public static DependencyProperty DestinationProperty = DependencyProperty.Register("Destination", typeof(string), typeof(AddRemoveMultiValueActivityAsNeeded));

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String Action
        {
            get
            {
                return ((string)(base.GetValue(ActionProperty)));
            }
            set
            {
                base.SetValue(ActionProperty, value);
            }
        }

        public static DependencyProperty TargetIdToUpdateProperty = DependencyProperty.Register("TargetIdToUpdate", typeof(Guid), typeof(AddRemoveMultiValueActivityAsNeeded));
        [Description("TargetIdToUpdate")]
        [Category("TargetIdToUpdate Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid TargetIdToUpdate
        {
            get
            {
                return ((Guid)(base.GetValue(AddRemoveMultiValueActivityAsNeeded.TargetIdToUpdateProperty)));
            }
            set
            {
                base.SetValue(AddRemoveMultiValueActivityAsNeeded.TargetIdToUpdateProperty, value);
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Properties")]
        public string Destination
        {
            get
            {
                return (string)base.GetValue(DestinationProperty);
            }
            set
            {
                base.SetValue(DestinationProperty, value);
            }
        }

        public static DependencyProperty ValueToAddOrRemoveProperty = DependencyProperty.Register("ValueToAddOrRemove", typeof(object), typeof(AddRemoveMultiValueActivityAsNeeded));
        [Description("ValueToAddOrRemove")]
        [Category("ValueToAddOrRemove Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object ValueToAddOrRemove
        {
            get
            {
                return ((object)(base.GetValue(AddRemoveMultiValueActivityAsNeeded.ValueToAddOrRemoveProperty)));
            }
            set
            {
                base.SetValue(AddRemoveMultiValueActivityAsNeeded.ValueToAddOrRemoveProperty, value);
            }
        }

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType TargetResource
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType)(base.GetValue(TargetResourceProperty)));
            }
            set
            {
                base.SetValue(TargetResourceProperty, value);
            }
        }

        SequentialWorkflow containingWorkflow;
        List<object> NewValues = new List<object>();

        public AddRemoveMultiValueActivityAsNeeded()
        {
            InitializeComponent();
        }

        private void PrepareReadResource_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Enter AddRemoveMultiValue-PrepareReadResource_ExecuteCode");
            try
            {
                if (!SequentialWorkflow.TryGetContainingWorkflow(this, out containingWorkflow))
                {
                    throw new InvalidOperationException("Could not get parent workflow!");
                }
                ReadTarget.ActorId = WellKnownGuids.FIMServiceAccount;
                ReadTarget.ResourceId = this.TargetIdToUpdate;
                ReadTarget.SelectionAttributes = new string[] { this.Destination };
            }
            catch (Exception ex)
            {
                Debugging.Log(string.Format("Error: '{0}'", ex.Message));
            }
            Debugging.Log("Exit AddRemoveMultiValue-PrepareReadResource_ExecuteCode");
        }

        private void ShouldUpdate_Condition(object sender, ConditionalEventArgs e)
        {
            Debugging.Log("Enter :: AddRemoveMultiValue-ShouldUpdate_Condition");
            NewValues.Add(this.ValueToAddOrRemove);
            Debugging.Log("New value", this.NewValues == null ? "(null)" : this.NewValues.FirstOrDefault().ToString());
            
            Debugging.Log(string.Format("Action: {0}", Action));
            try
            {
                // convert list of unique identifiers to list of guids
                Debugging.Log("Begin converting to list");
                List<object> currentvalues = new List<object>();
                if (this.ReadTarget.Resource[this.Destination] != null)
                {
                    Debugging.Log("Type", this.ReadTarget.Resource[this.Destination].GetType());
                    foreach (UniqueIdentifier u in (List<UniqueIdentifier>)this.ReadTarget.Resource[this.Destination])
                    {
                        Debugging.Log(string.Format("Guid: {0}", u.ToString()));
                        currentvalues.Add(u.GetGuid());
                    }
                }
                Debugging.Log("End converting to list");

                e.Result = false;
                if (this.Action.Equals("add", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (currentvalues == null)
                    {
                        Debugging.Log("There is an add and no current values");
                        e.Result = true;
                    }
                    else
                    {
                        if (NewValues.Except(currentvalues).Count() > 0)
                        {
                            Debugging.Log("There is an add");
                            e.Result = true;
                        }
                        else
                        {
                            Debugging.Log("There is a add but its already included");
                            e.Result = false;
                        }
                    }
                }
                else
                {
                    if (currentvalues == null)
                    {
                        Debugging.Log("There is a remove but target value is already empty");
                        e.Result = false;
                    }
                    else
                    {
                        if (currentvalues.Intersect(NewValues).Count() > 0)
                        {
                            Debugging.Log("There is a remove");
                            e.Result = true;
                        }
                        else
                        {
                            Debugging.Log("There is a remove but target value is currently included");
                            e.Result = false;
                        }
                    }
                }
                if (e.Result)
                {
                    // prepare update request
                    UpdateTarget.ActorId = WellKnownGuids.FIMServiceAccount;
                    UpdateTarget.ResourceId = this.TargetIdToUpdate;
                    UpdateRequestParameter[] UpdateParameters = new UpdateRequestParameter[] {
                    new Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateRequestParameter(
                        this.Destination,
                        this.Action.Equals("add", StringComparison.OrdinalIgnoreCase) ? UpdateMode.Insert : UpdateMode.Remove,
                        NewValues.First())};
                    UpdateTarget.UpdateParameters = UpdateParameters;
                }
            }
            catch (Exception ex)
            {
                Debugging.Log(string.Format("AddRemoveMultiValue-ShouldUpdate_Condition, error: '{0}'", ex.Message));
            }
            Debugging.Log("Exit AddRemoveMultiValue-ShouldUpdate_Condition");
        }

    }
}
