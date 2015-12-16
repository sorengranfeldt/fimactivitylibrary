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
using Microsoft.ResourceManagement.Workflow.Activities;
using System.Collections.Generic;
using Microsoft.ResourceManagement.WebServices;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class ReconcileSoftwareGroupMembersActivity : SequenceActivity
    {

        #region Properties

        // ExplicitMemberComputerOwners
        public static DependencyProperty UserMembersAttributeNameProperty = DependencyProperty.Register("UserMembersAttributeName", typeof(string), typeof(ReconcileSoftwareGroupMembersActivity));
        [Description("UserMembersAttributeName")]
        [Category("UserMembersAttributeName Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserMembersAttributeName
        {
            get
            {
                return ((string)(base.GetValue(ReconcileSoftwareGroupMembersActivity.UserMembersAttributeNameProperty)));
            }
            set
            {
                base.SetValue(ReconcileSoftwareGroupMembersActivity.UserMembersAttributeNameProperty, value);
            }
        }

        // ExplicitMember
        public static DependencyProperty ComputerMembersAttributeNameProperty = DependencyProperty.Register("ComputerMembersAttributeName", typeof(string), typeof(ReconcileSoftwareGroupMembersActivity));
        [Description("ComputerMembersAttributeName")]
        [Category("ComputerMembersAttributeName Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ComputerMembersAttributeName
        {
            get
            {
                return ((string)(base.GetValue(ReconcileSoftwareGroupMembersActivity.ComputerMembersAttributeNameProperty)));
            }
            set
            {
                base.SetValue(ReconcileSoftwareGroupMembersActivity.ComputerMembersAttributeNameProperty, value);
            }
        }

        public static DependencyProperty TargetResourceProperty = DependencyProperty.Register("TargetResource", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType), typeof(Granfeldt.FIM.ActivityLibrary.ReconcileSoftwareGroupMembersActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType TargetResource
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType)(base.GetValue(Granfeldt.FIM.ActivityLibrary.ReconcileSoftwareGroupMembersActivity.TargetResourceProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.ReconcileSoftwareGroupMembersActivity.TargetResourceProperty, value);
            }
        }

        #endregion

        SequentialWorkflow containingWorkflow = null;
        public List<Guid> usermembers = new List<Guid>();
        public List<Guid> currentcomputermembers = new List<Guid>();
        public List<Guid> expectedcomputermembers = new List<Guid>();

        public ReconcileSoftwareGroupMembersActivity()
        {
            InitializeComponent();
        }

        void ReadTarget_Closed(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            Debugging.Log(string.Format("Activity {0} result", e.Activity.Name), e.ExecutionResult);
            try
            {
                // pick up the value of the multivalue attributes read from the target
                if (this.ReadTarget.Resource[this.UserMembersAttributeName] != null)
                {
                    foreach (UniqueIdentifier u in this.ReadTarget.Resource[this.UserMembersAttributeName] as List<UniqueIdentifier>)
                    {
                        Debugging.Log("current user member objectid", u);
                        usermembers.Add(u.GetGuid());
                    }
                }
                else
                {
                    Debugging.Log("no current user members");
                }

                if (this.ReadTarget.Resource[this.ComputerMembersAttributeName] != null)
                {
                    foreach (UniqueIdentifier u in this.ReadTarget.Resource[this.ComputerMembersAttributeName] as List<UniqueIdentifier>)
                    {
                        Debugging.Log("current computer member objectid", u);
                        currentcomputermembers.Add(u.GetGuid());
                    }
                }
                else
                {
                    Debugging.Log("no current computer members");
                }
            }
            catch (Exception ex)
            {
                Debugging.Log(ex);
            }
        }

        void EnumerateComputersClosed(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            Debugging.Log(string.Format("{0} {1} with result: {2}", e.ExecutionStatus, e.Activity.Name, e.ExecutionResult));
        }

        private void PrepareReadTarget_ExecuteCode(object sender, EventArgs e)
        {
            if (!SequentialWorkflow.TryGetContainingWorkflow(this, out containingWorkflow))
            {
                throw new InvalidOperationException("Could not get parent workflow!");
            }
            ReadTarget.ActorId = WellKnownGuids.FIMServiceAccount;
            ReadTarget.ResourceId = containingWorkflow.TargetId;
            ReadTarget.SelectionAttributes = new string[] { this.UserMembersAttributeName, this.ComputerMembersAttributeName };
            ReadTarget.Closed += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ReadTarget_Closed);
        }

        private void FindUsersComputers_ChildInitialized(object sender, ReplicatorChildEventArgs e)
        {
            Debugging.Log("child initialized");
            Debugging.Log("activity", e.Activity.Name);
            Debugging.Log("instancedata", e.InstanceData);

            // get the correct instance of the child activity
            FindResourcesActivity fc = e.Activity as FindResourcesActivity;

            fc.ActorId = WellKnownGuids.FIMServiceAccount;
            fc.XPathFilter = string.Format("/Computer[ComputerLocalAdministrator='{0}']", e.InstanceData as Guid?);
            Debugging.Log("enumeration filter", fc.XPathFilter);
            fc.Attributes = new string[] { "ObjectID" };
            fc.PageSize = 100;
            fc.StatusChanged += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(EnumerateComputersClosed);
            fc.Faulting += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(EnumerateComputersClosed);
        }

        private void FindUsersComputers_ChildCompleted(object sender, ReplicatorChildEventArgs e)
        {
            Debugging.Log("child completed");
            // get the correct instance of the child activity
            FindResourcesActivity fc = e.Activity as FindResourcesActivity;
            if (fc != null)
            {
                Debugging.Log("enumerate computers result count", fc.TotalResultsCount);
                if (fc.EnumeratedResourceIDs != null)
                {
                    foreach (Guid computer in fc.EnumeratedResourceIDs.ToList())
                    {
                        Debugging.Log("expected member", computer);
                        expectedcomputermembers.Add(computer);
                    }
                }
            }
        }

        private void FindUsersComputers_Completed(object sender, EventArgs e)
        {
            Debugging.Log("completed");
        }

        private void FindUsersComputers_Initialized(object sender, EventArgs e)
        {
            Debugging.Log("initialized");
        }

        private void UpdateTarget_Condition(object sender, ConditionalEventArgs e)
        {
            List<UpdateRequestParameter> updateparameters = new List<UpdateRequestParameter>();

            e.Result = false;
            foreach (Guid member in currentcomputermembers.Except(expectedcomputermembers))
            {
                Debugging.Log("remove currentmember", member);
                e.Result = true;
                updateparameters.Add(
                    new UpdateRequestParameter(this.ComputerMembersAttributeName, UpdateMode.Remove, member)
                    );
            }
            foreach (Guid member in expectedcomputermembers.Except(currentcomputermembers))
            {
                Debugging.Log("add expected member", member);
                e.Result = true;
                updateparameters.Add(
                    new UpdateRequestParameter(this.ComputerMembersAttributeName, UpdateMode.Insert, member)
                    );
            }
            UpdateExplicitComputerMembers.ActorId = WellKnownGuids.FIMServiceAccount;
            UpdateExplicitComputerMembers.ResourceId = containingWorkflow.TargetId;
            UpdateExplicitComputerMembers.UpdateParameters = updateparameters.ToArray();
            UpdateExplicitComputerMembers.StatusChanged += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(EnumerateComputersClosed);
        }

    }
}
