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
    public partial class FindResourcesActivity
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
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            this.ReadEnumeratedResource = new System.Workflow.Activities.CodeActivity();
            this.GenerateResourceIds = new System.Workflow.Activities.CodeActivity();
            this.Enumerate = new Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity();
            // 
            // readEnumeratedResource
            // 
            this.ReadEnumeratedResource.Name = "ReadEnumeratedResource";
            this.ReadEnumeratedResource.ExecuteCode += new System.EventHandler(this.ReadEnumeratedResource_ExecuteCode);
            // 
            // GenerateResourceIds
            // 
            this.GenerateResourceIds.Name = "GenerateResourceIds";
            this.GenerateResourceIds.ExecuteCode += new System.EventHandler(this.GenerateResourceIds_ExecuteCode);
            // 
            // Enumerate
            // 
            this.Enumerate.Activities.Add(this.ReadEnumeratedResource);
            activitybind1.Name = "FindResourcesActivity";
            activitybind1.Path = "ActorId";
            this.Enumerate.Name = "Enumerate";
            activitybind2.Name = "FindResourcesActivity";
            activitybind2.Path = "PageSize";
            activitybind3.Name = "FindResourcesActivity";
            activitybind3.Path = "Attributes";
            activitybind4.Name = "FindResourcesActivity";
            activitybind4.Path = "SortingAttributes";
            activitybind5.Name = "FindResourcesActivity";
            activitybind5.Path = "TotalResultsCount";
            activitybind6.Name = "FindResourcesActivity";
            activitybind6.Path = "XPathFilter";
            this.Enumerate.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.ActorIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.Enumerate.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.PageSizeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.Enumerate.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.SelectionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.Enumerate.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.SortingAttributesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.Enumerate.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.TotalResultsCountProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.Enumerate.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // FindResourcesActivity
            // 
            this.Activities.Add(this.Enumerate);
            this.Activities.Add(this.GenerateResourceIds);
            this.Name = "FindResourcesActivity";
            this.CanModifyActivities = false;

		}

		#endregion

        private CodeActivity GenerateResourceIds;
        private CodeActivity ReadEnumeratedResource;
        private Microsoft.ResourceManagement.Workflow.Activities.EnumerateResourcesActivity Enumerate;

    }
}
