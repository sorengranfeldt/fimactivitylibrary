using System.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class UpdateReferenceAttributeActivity
    {
        #region Designer generated code
		
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            this.UpdateResource = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
            this.PrepareUpdate = new System.Workflow.Activities.CodeActivity();
            this.ValuesAreNotPresent = new System.Workflow.Activities.IfElseBranchActivity();
            this.ValuesPresent = new System.Workflow.Activities.IfElseBranchActivity();
            this.CheckIfAnyValuesPresent = new System.Workflow.Activities.IfElseActivity();
            // 
            // UpdateResource
            // 
            activitybind1.Name = "UpdateReferenceAttributeActivity";
            activitybind1.Path = "ActorId";
            this.UpdateResource.Name = "UpdateResource";
            activitybind2.Name = "UpdateReferenceAttributeActivity";
            activitybind2.Path = "ResourceId";
            activitybind3.Name = "UpdateReferenceAttributeActivity";
            activitybind3.Path = "UpdateParameters";
            this.UpdateResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.UpdateResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.UpdateResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity.UpdateParametersProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // PrepareUpdate
            // 
            this.PrepareUpdate.Name = "PrepareUpdate";
            this.PrepareUpdate.ExecuteCode += new System.EventHandler(this.PrepareUpdate_ExecuteCode);
            // 
            // ValuesAreNotPresent
            // 
            this.ValuesAreNotPresent.Name = "ValuesAreNotPresent";
            // 
            // ValuesPresent
            // 
            this.ValuesPresent.Activities.Add(this.PrepareUpdate);
            this.ValuesPresent.Activities.Add(this.UpdateResource);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.AreValuesPresent);
            this.ValuesPresent.Condition = codecondition1;
            this.ValuesPresent.Name = "ValuesPresent";
            // 
            // CheckIfAnyValuesPresent
            // 
            this.CheckIfAnyValuesPresent.Activities.Add(this.ValuesPresent);
            this.CheckIfAnyValuesPresent.Activities.Add(this.ValuesAreNotPresent);
            this.CheckIfAnyValuesPresent.Name = "CheckIfAnyValuesPresent";
            // 
            // UpdateReferenceAttributeActivity
            // 
            this.Activities.Add(this.CheckIfAnyValuesPresent);
            this.Name = "UpdateReferenceAttributeActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private IfElseBranchActivity ValuesAreNotPresent;
        private IfElseBranchActivity ValuesPresent;
        private IfElseActivity CheckIfAnyValuesPresent;
        private CodeActivity PrepareUpdate;
        private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity UpdateResource;











    }
}