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
    public partial class AddRemoveMultiValueActivityAsNeeded
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
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            this.UpdateTarget = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
            this.NothingToDo = new System.Workflow.Activities.IfElseBranchActivity();
            this.DoUpdate = new System.Workflow.Activities.IfElseBranchActivity();
            this.ShouldUpdate = new System.Workflow.Activities.IfElseActivity();
            this.ReadTarget = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.PrepareReadResource = new System.Workflow.Activities.CodeActivity();
            // 
            // UpdateTarget
            // 
            this.UpdateTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateTarget.ApplyAuthorizationPolicy = false;
            this.UpdateTarget.Name = "UpdateTarget";
            this.UpdateTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateTarget.UpdateParameters = null;
            // 
            // NothingToDo
            // 
            this.NothingToDo.Name = "NothingToDo";
            // 
            // DoUpdate
            // 
            this.DoUpdate.Activities.Add(this.UpdateTarget);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ShouldUpdate_Condition);
            this.DoUpdate.Condition = codecondition1;
            this.DoUpdate.Name = "DoUpdate";
            // 
            // ShouldUpdate
            // 
            this.ShouldUpdate.Activities.Add(this.DoUpdate);
            this.ShouldUpdate.Activities.Add(this.NothingToDo);
            this.ShouldUpdate.Name = "ShouldUpdate";
            // 
            // ReadTarget
            // 
            this.ReadTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.Name = "ReadTarget";
            activitybind1.Name = "AddRemoveMultiValueActivityAsNeeded";
            activitybind1.Path = "TargetResource";
            this.ReadTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.SelectionAttributes = null;
            this.ReadTarget.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // PrepareReadResource
            // 
            this.PrepareReadResource.Name = "PrepareReadResource";
            this.PrepareReadResource.ExecuteCode += new System.EventHandler(this.PrepareReadResource_ExecuteCode);
            // 
            // AddRemoveMultiValueActivityAsNeeded
            // 
            this.Activities.Add(this.PrepareReadResource);
            this.Activities.Add(this.ReadTarget);
            this.Activities.Add(this.ShouldUpdate);
            this.Name = "AddRemoveMultiValueActivityAsNeeded";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity PrepareReadResource;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadTarget;

        private IfElseBranchActivity NothingToDo;

        private IfElseBranchActivity DoUpdate;

        private IfElseActivity ShouldUpdate;

        private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity UpdateTarget;

















    }
}
