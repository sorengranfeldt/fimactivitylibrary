using System.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class GetUpdatesToReferenceAttributeActivity
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
            this.ExtractDataFromRequest = new System.Workflow.Activities.CodeActivity();
            this.GetCurrentRequest = new Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity();
            // 
            // ExtractDataFromRequest
            // 
            this.ExtractDataFromRequest.Name = "ExtractDataFromRequest";
            this.ExtractDataFromRequest.ExecuteCode += new System.EventHandler(this.ExtractDataFromRequest_ExecuteCode);
            // 
            // GetCurrentRequest
            // 
            activitybind1.Name = "GetUpdatesToReferenceAttributeActivity";
            activitybind1.Path = "CurrentRequest";
            this.GetCurrentRequest.Name = "GetCurrentRequest";
            this.GetCurrentRequest.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity.CurrentRequestProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // GetUpdatesToReferenceAttributeActivity
            // 
            this.Activities.Add(this.GetCurrentRequest);
            this.Activities.Add(this.ExtractDataFromRequest);
            this.Name = "GetUpdatesToReferenceAttributeActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity ExtractDataFromRequest;
        private Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity GetCurrentRequest;

    }
}