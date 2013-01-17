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
    public partial class UpdateSingleValueAttributeAsNeededActivity
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
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            this.UpdateResource = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
            this.NoBranch = new System.Workflow.Activities.IfElseBranchActivity();
            this.YesBranch = new System.Workflow.Activities.IfElseBranchActivity();
            this.IsUpdateNeeded = new System.Workflow.Activities.IfElseActivity();
            this.ReadResource = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.SetupReadTarget = new System.Workflow.Activities.CodeActivity();
            // 
            // UpdateResource
            // 
            this.UpdateResource.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateResource.ApplyAuthorizationPolicy = false;
            this.UpdateResource.Name = "UpdateResource";
            this.UpdateResource.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateResource.UpdateParameters = null;
            // 
            // NoBranch
            // 
            this.NoBranch.Name = "NoBranch";
            // 
            // YesBranch
            // 
            this.YesBranch.Activities.Add(this.UpdateResource);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.TargetUpdateNeeded_Condition);
            this.YesBranch.Condition = codecondition1;
            this.YesBranch.Name = "YesBranch";
            // 
            // IsUpdateNeeded
            // 
            this.IsUpdateNeeded.Activities.Add(this.YesBranch);
            this.IsUpdateNeeded.Activities.Add(this.NoBranch);
            this.IsUpdateNeeded.Name = "IsUpdateNeeded";
            // 
            // ReadResource
            // 
            this.ReadResource.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadResource.Description = "Reads the current target";
            this.ReadResource.Name = "ReadResource";
            activitybind1.Name = "UpdateSingleValueAttributeAsNeededActivity";
            activitybind1.Path = "TargetResource";
            activitybind2.Name = "UpdateSingleValueAttributeAsNeededActivity";
            activitybind2.Path = "TargetId";
            this.ReadResource.SelectionAttributes = null;
            this.ReadResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.ReadResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // SetupReadTarget
            // 
            this.SetupReadTarget.Name = "SetupReadTarget";
            this.SetupReadTarget.ExecuteCode += new System.EventHandler(this.SetupReadTarget_ExecuteCode);
            // 
            // UpdateSingleValueAttributeAsNeededActivity
            // 
            this.Activities.Add(this.SetupReadTarget);
            this.Activities.Add(this.ReadResource);
            this.Activities.Add(this.IsUpdateNeeded);
            this.Name = "UpdateSingleValueAttributeAsNeededActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity SetupReadTarget;

        private IfElseBranchActivity NoBranch;

        private IfElseBranchActivity YesBranch;

        private IfElseActivity IsUpdateNeeded;

        private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity UpdateResource;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadResource;








    }
}
