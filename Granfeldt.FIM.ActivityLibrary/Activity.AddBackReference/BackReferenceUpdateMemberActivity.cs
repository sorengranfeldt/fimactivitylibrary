using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class BackReferenceUpdateMemberActivity : SequenceActivity
    {
        public BackReferenceUpdateMemberActivity()
        {
            InitializeComponent();
        }

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(BackReferenceUpdateMemberActivity));

        [Description("Title")]
        [Category("Title Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get
            {
                return ((string)(base.GetValue(BackReferenceUpdateMemberActivity.TitleProperty)));
            }
            set
            {
                base.SetValue(BackReferenceUpdateMemberActivity.TitleProperty, value);
            }
        }

        public static DependencyProperty ActionProperty = DependencyProperty.Register("Action", typeof(string), typeof(BackReferenceUpdateMemberActivity));
        [Description("Action")]
        [Category("Action Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Action
        {
            get
            {
                return ((string)(base.GetValue(BackReferenceUpdateMemberActivity.ActionProperty)));
            }
            set
            {
                base.SetValue(BackReferenceUpdateMemberActivity.ActionProperty, value);
            }
        }

        public static DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(BackReferenceUpdateMemberActivity));
        [Description("Source")]
        [Category("Source Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Source
        {
            get
            {
                return ((string)(base.GetValue(BackReferenceUpdateMemberActivity.SourceProperty)));
            }
            set
            {
                base.SetValue(BackReferenceUpdateMemberActivity.SourceProperty, value);
            }
        }

        public static DependencyProperty DestinationProperty = DependencyProperty.Register("Destination", typeof(string), typeof(BackReferenceUpdateMemberActivity));
        [Description("Destination")]
        [Category("Destination Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Destination
        {
            get
            {
                return ((string)(base.GetValue(BackReferenceUpdateMemberActivity.DestinationProperty)));
            }
            set
            {
                base.SetValue(BackReferenceUpdateMemberActivity.DestinationProperty, value);
            }
        }

        public List<Guid> GroupToUpdate = null;
        public Guid CurrentGroupToUpdate;

        private void ForEveryAdd_Condition(object sender, ConditionalEventArgs e)
        {
            Debugging.Log("Enter::ForEveryAdd_Condition");
            // if not initialized, then initialize and load value into List
            if (GroupToUpdate == null)
            {
                GroupToUpdate = new List<Guid>(this.GetReferenceUpdates.AddedReferences);
            }

            Debugging.Log(string.Format("Count: {0}", GroupToUpdate.Count));
            if (GroupToUpdate.Count > 0)
            {
                // removed processed item
                CurrentGroupToUpdate = GroupToUpdate[0];
                GroupToUpdate.RemoveAt(0);
                e.Result = true;
            }
            else
            {
                // no more items to process
                e.Result = false;
            }
            Debugging.Log("Exit::ForEveryAdd_Condition");
        }

        private void PrepareGroupOperation_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Enter::PrepareGroupOperation_ExecuteCode");
            try
            {
                // since we're in a while loop, we have to grab the instantiated workflow activity UpdateResourceActivity
                SequenceActivity currentParentSequenceActivity = (SequenceActivity)((CodeActivity)sender).Parent;

                UpdateReferenceAttributesAsNeededActivity currentUpdateResourceActivity = currentParentSequenceActivity.Activities.OfType<UpdateReferenceAttributesAsNeededActivity>().First();

                SequentialWorkflow containingWorkflow = null;
                if (!SequentialWorkflow.TryGetContainingWorkflow(this, out containingWorkflow))
                {
                    throw new InvalidOperationException("Could not get parent workflow!");
                }

                Debugging.Log(string.Format("Resource Id: {0}", CurrentGroupToUpdate));
                currentUpdateResourceActivity.ActorId = WellKnownGuids.BuiltInSynchronizationAccount;
                currentUpdateResourceActivity.ResourceId = CurrentGroupToUpdate;
                currentUpdateResourceActivity.AttributeName = this.Destination;
                if (this.Action.ToLower() == "add")
                {
                    Debugging.Log(string.Format("Add member: {0}", this.GetReferenceUpdates.ResourceId.ToString()));
                    currentUpdateResourceActivity.UpdateMode = UpdateMode.Insert;
                }
                else
                {
                    Debugging.Log(string.Format("Remove member: {0}", this.GetReferenceUpdates.ResourceId.ToString()));
                    currentUpdateResourceActivity.UpdateMode = UpdateMode.Remove;
                }
                currentUpdateResourceActivity.Values = new Guid[] { this.GetReferenceUpdates.ResourceId };
            }
            catch (Exception ex)
            {
                Debugging.Log(string.Format("Error: {0}", ex.Message));
            }
            Debugging.Log("Exit::PrepareGroupOperation_ExecuteCode");
        }

        private void SetupClearMV_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Enter::SetupClearMV_ExecuteCode");
            try
            {
                SequentialWorkflow containingWorkflow = null;
                if (!SequentialWorkflow.TryGetContainingWorkflow(this, out containingWorkflow))
                {
                    throw new InvalidOperationException("Could not get parent workflow!");
                }
                ClearMVOnResource.ActorId = WellKnownGuids.BuiltInSynchronizationAccount;
                ClearMVOnResource.ResourceId = containingWorkflow.TargetId;
                int numOfElements = this.GetReferenceUpdates.AddedReferences.ToList().Count;
                Debugging.Log(string.Format("List count: {0}", this.GetReferenceUpdates.AddedReferences.ToList().Count));
                Debugging.Log(string.Format("Remove elements: {0}", numOfElements));
                
                List<UpdateRequestParameter> ps = new List<UpdateRequestParameter>();
                foreach (Guid value in this.GetReferenceUpdates.AddedReferences.ToList())
                {
                    Debugging.Log(string.Format("Removing: {0}", value));
                    UpdateRequestParameter p = new UpdateRequestParameter();
                    p.PropertyName = this.Source;
                    p.Mode = UpdateMode.Remove;
                    p.Value = value;
                    ps.Add(p);
                }
                ClearMVOnResource.UpdateParameters = ps.ToArray();
            }
            catch (Exception ex)
            {
                Debugging.Log(string.Format("Error: {0}", ex.Message));
            }
            Debugging.Log("Exit::SetupClearMV_ExecuteCode");
        }
    }
}
