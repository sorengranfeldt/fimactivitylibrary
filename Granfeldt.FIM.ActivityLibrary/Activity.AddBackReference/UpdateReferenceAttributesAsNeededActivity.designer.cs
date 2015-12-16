using System.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class UpdateReferenceAttributesAsNeededActivity
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
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            this.UpdateReferences = new UpdateReferenceAttributeActivity();
            this.NotAtAll = new System.Workflow.Activities.IfElseBranchActivity();
            this.Probably = new System.Workflow.Activities.IfElseBranchActivity();
            this.IsTherePointInDoingAnything = new System.Workflow.Activities.IfElseActivity();
            this.GetCurrentReferences = new Granfeldt.FIM.ActivityLibrary.GetReferenceValuesFromObjectActivity();
            // 
            // UpdateReferences
            // 
            activitybind1.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind1.Path = "ActorId";
            activitybind2.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind2.Path = "AttributeName";
            this.UpdateReferences.Name = "UpdateReferences";
            activitybind3.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind3.Path = "ResourceId";
            activitybind4.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind4.Path = "UpdateMode";
            activitybind5.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind5.Path = "ValuesToSet";
            this.UpdateReferences.SetBinding(UpdateReferenceAttributeActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.UpdateReferences.SetBinding(UpdateReferenceAttributeActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.UpdateReferences.SetBinding(UpdateReferenceAttributeActivity.UpdateModeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.UpdateReferences.SetBinding(UpdateReferenceAttributeActivity.ValuesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.UpdateReferences.SetBinding(UpdateReferenceAttributeActivity.AttributeNameProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // NotAtAll
            // 
            this.NotAtAll.Name = "NotAtAll";
            // 
            // Probably
            // 
            this.Probably.Activities.Add(this.UpdateReferences);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.FindOutIfThereIsAPointInDoingAnything);
            this.Probably.Condition = codecondition1;
            this.Probably.Name = "Probably";
            // 
            // IsTherePointInDoingAnything
            // 
            this.IsTherePointInDoingAnything.Activities.Add(this.Probably);
            this.IsTherePointInDoingAnything.Activities.Add(this.NotAtAll);
            this.IsTherePointInDoingAnything.Name = "IsTherePointInDoingAnything";
            // 
            // GetCurrentReferences
            // 
            activitybind6.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind6.Path = "ActorId";
            activitybind7.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind7.Path = "AttributeName";
            activitybind8.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind8.Path = "CurrentValues";
            this.GetCurrentReferences.Name = "GetCurrentReferences";
            activitybind9.Name = "UpdateReferenceAttributesAsNeededActivity";
            activitybind9.Path = "ResourceId";
            this.GetCurrentReferences.SetBinding(Granfeldt.FIM.ActivityLibrary.GetReferenceValuesFromObjectActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.GetCurrentReferences.SetBinding(Granfeldt.FIM.ActivityLibrary.GetReferenceValuesFromObjectActivity.AttributeNameProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.GetCurrentReferences.SetBinding(Granfeldt.FIM.ActivityLibrary.GetReferenceValuesFromObjectActivity.CurrentValuesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.GetCurrentReferences.SetBinding(Granfeldt.FIM.ActivityLibrary.GetReferenceValuesFromObjectActivity.ObjectIDProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // UpdateReferenceAttributesAsNeededActivity
            // 
            this.Activities.Add(this.GetCurrentReferences);
            this.Activities.Add(this.IsTherePointInDoingAnything);
            this.Name = "UpdateReferenceAttributesAsNeededActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private GetReferenceValuesFromObjectActivity GetCurrentReferences;
        private IfElseBranchActivity NotAtAll;
        private IfElseBranchActivity Probably;
        private IfElseActivity IsTherePointInDoingAnything;
        private UpdateReferenceAttributeActivity UpdateReferences;
    }
}