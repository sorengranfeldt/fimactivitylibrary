using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
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
	public partial class ClearSingleValuedAttributeActivity
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
			System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
			System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
			this.UpdateResource = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
			this.NoActionNeeded = new System.Workflow.Activities.IfElseBranchActivity();
			this.ThereWasValue = new System.Workflow.Activities.IfElseBranchActivity();
			this.EndGracefully = new System.Workflow.Activities.CodeActivity();
			this.IsRemovalNeeded = new System.Workflow.Activities.IfElseActivity();
			this.ReadResource = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
			this.PrepareReading = new System.Workflow.Activities.CodeActivity();
			this.GetCurrentRequest = new Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity();
			// 
			// UpdateResource
			// 
			this.UpdateResource.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
			this.UpdateResource.ApplyAuthorizationPolicy = false;
			this.UpdateResource.Name = "UpdateResource";
			this.UpdateResource.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
			this.UpdateResource.UpdateParameters = null;
			// 
			// NoActionNeeded
			// 
			this.NoActionNeeded.Name = "NoActionNeeded";
			// 
			// ThereWasValue
			// 
			this.ThereWasValue.Activities.Add(this.UpdateResource);
			codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ThereWasValue_Condition);
			this.ThereWasValue.Condition = codecondition1;
			this.ThereWasValue.Name = "ThereWasValue";
			// 
			// EndGracefully
			// 
			this.EndGracefully.Name = "EndGracefully";
			this.EndGracefully.ExecuteCode += new System.EventHandler(this.EndGracefully_ExecuteCode);
			// 
			// IsRemovalNeeded
			// 
			this.IsRemovalNeeded.Activities.Add(this.ThereWasValue);
			this.IsRemovalNeeded.Activities.Add(this.NoActionNeeded);
			this.IsRemovalNeeded.Name = "IsRemovalNeeded";
			// 
			// ReadResource
			// 
			this.ReadResource.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
			this.ReadResource.Name = "ReadResource";
			this.ReadResource.Resource = null;
			this.ReadResource.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
			this.ReadResource.SelectionAttributes = null;
			// 
			// PrepareReading
			// 
			this.PrepareReading.Name = "PrepareReading";
			this.PrepareReading.ExecuteCode += new System.EventHandler(this.PrepareReading_ExecuteCode);
			// 
			// GetCurrentRequest
			// 
			activitybind1.Name = "ClearSingleValuedAttributeActivity";
			activitybind1.Path = "CurrentRequest";
			this.GetCurrentRequest.Name = "GetCurrentRequest";
			this.GetCurrentRequest.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity.CurrentRequestProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
			// 
			// ClearSingleValuedAttributeActivity
			// 
			this.Activities.Add(this.GetCurrentRequest);
			this.Activities.Add(this.PrepareReading);
			this.Activities.Add(this.ReadResource);
			this.Activities.Add(this.IsRemovalNeeded);
			this.Activities.Add(this.EndGracefully);
			this.Name = "ClearSingleValuedAttributeActivity";
			this.CanModifyActivities = false;

		}





















		#endregion

		private Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity GetCurrentRequest;
		private CodeActivity EndGracefully;
		private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadResource;
		private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity UpdateResource;
		private IfElseBranchActivity ThereWasValue;
		private IfElseActivity IsRemovalNeeded;
		private IfElseBranchActivity NoActionNeeded;
		private CodeActivity PrepareReading;
	}
}
