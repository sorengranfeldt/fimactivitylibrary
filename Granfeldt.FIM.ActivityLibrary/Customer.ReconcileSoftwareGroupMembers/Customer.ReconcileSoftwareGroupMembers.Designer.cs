using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class ReconcileSoftwareGroupMembersActivity
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("", "")]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_11 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            this.UpdateExplicitComputerMembers = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
            this.NothingToDo = new System.Workflow.Activities.IfElseBranchActivity();
            this.UpdateExplicitMember = new System.Workflow.Activities.IfElseBranchActivity();
            this.EnumerateComputers = new Granfeldt.FIM.ActivityLibrary.FindResourcesActivity();
            this.UpdateMembers = new System.Workflow.Activities.IfElseActivity();
            this.FindUsersComputers = new System.Workflow.Activities.ReplicatorActivity();
            this.ReadTarget = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.PrepareReadTarget = new System.Workflow.Activities.CodeActivity();
            // 
            // UpdateExplicitComputerMembers
            // 
            this.UpdateExplicitComputerMembers.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateExplicitComputerMembers.ApplyAuthorizationPolicy = false;
            this.UpdateExplicitComputerMembers.Name = "UpdateExplicitComputerMembers";
            this.UpdateExplicitComputerMembers.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateExplicitComputerMembers.UpdateParameters = null;
            // 
            // NothingToDo
            // 
            this.NothingToDo.Name = "NothingToDo";
            // 
            // UpdateExplicitMember
            // 
            this.UpdateExplicitMember.Activities.Add(this.UpdateExplicitComputerMembers);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.UpdateTarget_Condition);
            this.UpdateExplicitMember.Condition = codecondition1;
            this.UpdateExplicitMember.Name = "UpdateExplicitMember";
            // 
            // EnumerateComputers
            // 
            this.EnumerateComputers.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.EnumerateComputers.Attributes = null;
            this.EnumerateComputers.EnumeratedResourceIDs = null;
            this.EnumerateComputers.EnumeratedResources = list_11;
            this.EnumerateComputers.Name = "EnumerateComputers";
            this.EnumerateComputers.PageSize = 0;
            this.EnumerateComputers.SortingAttributes = null;
            this.EnumerateComputers.TotalResultsCount = 0;
            this.EnumerateComputers.XPathFilter = null;
            // 
            // UpdateMembers
            // 
            this.UpdateMembers.Activities.Add(this.UpdateExplicitMember);
            this.UpdateMembers.Activities.Add(this.NothingToDo);
            this.UpdateMembers.Name = "UpdateMembers";
            activitybind1.Name = "ReconcileSoftwareGroupMembersActivity";
            activitybind1.Path = "usermembers";
            // 
            // FindUsersComputers
            // 
            this.FindUsersComputers.Activities.Add(this.EnumerateComputers);
            this.FindUsersComputers.ExecutionType = System.Workflow.Activities.ExecutionType.Sequence;
            this.FindUsersComputers.Name = "FindUsersComputers";
            this.FindUsersComputers.ChildInitialized += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.FindUsersComputers_ChildInitialized);
            this.FindUsersComputers.ChildCompleted += new System.EventHandler<System.Workflow.Activities.ReplicatorChildEventArgs>(this.FindUsersComputers_ChildCompleted);
            this.FindUsersComputers.Completed += new System.EventHandler(this.FindUsersComputers_Completed);
            this.FindUsersComputers.Initialized += new System.EventHandler(this.FindUsersComputers_Initialized);
            this.FindUsersComputers.SetBinding(System.Workflow.Activities.ReplicatorActivity.InitialChildDataProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // ReadTarget
            // 
            this.ReadTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.Name = "ReadTarget";
            activitybind2.Name = "ReconcileSoftwareGroupMembersActivity";
            activitybind2.Path = "TargetResource";
            this.ReadTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.SelectionAttributes = null;
            this.ReadTarget.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // PrepareReadTarget
            // 
            this.PrepareReadTarget.Name = "PrepareReadTarget";
            this.PrepareReadTarget.ExecuteCode += new System.EventHandler(this.PrepareReadTarget_ExecuteCode);
            // 
            // ReconcileSoftwareGroupMembersActivity
            // 
            this.Activities.Add(this.PrepareReadTarget);
            this.Activities.Add(this.ReadTarget);
            this.Activities.Add(this.FindUsersComputers);
            this.Activities.Add(this.UpdateMembers);
            this.Name = "ReconcileSoftwareGroupMembersActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity UpdateExplicitComputerMembers;

        private IfElseBranchActivity NothingToDo;

        private IfElseBranchActivity UpdateExplicitMember;

        private IfElseActivity UpdateMembers;

        private FindResourcesActivity EnumerateComputers;

        private ReplicatorActivity FindUsersComputers;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadTarget;

        private CodeActivity PrepareReadTarget;
















    }
}
