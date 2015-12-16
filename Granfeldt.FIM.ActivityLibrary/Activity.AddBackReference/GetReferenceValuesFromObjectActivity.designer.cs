using System.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class GetReferenceValuesFromObjectActivity
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
            this.ProcessResults = new System.Workflow.Activities.CodeActivity();
            this.ReadResource = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.PrepareReading = new System.Workflow.Activities.CodeActivity();
            // 
            // ProcessResults
            // 
            this.ProcessResults.Name = "ProcessResults";
            this.ProcessResults.ExecuteCode += new System.EventHandler(this.ProcessResults_ExecuteCode);
            // 
            // ReadResource
            // 
            activitybind1.Name = "GetReferenceValuesFromObjectActivity";
            activitybind1.Path = "ActorId";
            this.ReadResource.Name = "ReadResource";
            activitybind2.Name = "GetReferenceValuesFromObjectActivity";
            activitybind2.Path = "CurrentResource";
            activitybind3.Name = "GetReferenceValuesFromObjectActivity";
            activitybind3.Path = "ObjectID";
            activitybind4.Name = "GetReferenceValuesFromObjectActivity";
            activitybind4.Path = "SelectionAttributes";
            this.ReadResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.ReadResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            //this.ReadResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.SelectionAttributesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.ReadResource.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // PrepareReading
            // 
            this.PrepareReading.Name = "PrepareReading";
            this.PrepareReading.ExecuteCode += new System.EventHandler(this.PrepareReading_ExecuteCode);
            // 
            // GetReferenceValuesFromObjectActivity
            // 
            this.Activities.Add(this.PrepareReading);
            this.Activities.Add(this.ReadResource);
            this.Activities.Add(this.ProcessResults);
            this.Name = "GetReferenceValuesFromObjectActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity PrepareReading;
        private CodeActivity ProcessResults;
        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadResource;








    }
}