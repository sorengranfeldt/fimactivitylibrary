using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.ResourceManagement.Workflow.Activities;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{

	public partial class ClearSingleValuedAttributeActivity : SequenceActivity
	{

		#region Properties

		public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(System.String), typeof(ClearSingleValuedAttributeActivity));
		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
		[BrowsableAttribute(true)]
		[CategoryAttribute("Misc")]
		public String Title
		{
			get
			{
				return ((string)(base.GetValue(TitleProperty)));
			}
			set
			{
				base.SetValue(TitleProperty, value);
			}
		}

		public static DependencyProperty CurrentRequestProperty = DependencyProperty.Register("CurrentRequest", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.RequestType), typeof(ClearSingleValuedAttributeActivity));
		[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
		[BrowsableAttribute(true)]
		[CategoryAttribute("Misc")]
		public Microsoft.ResourceManagement.WebServices.WSResourceManagement.RequestType CurrentRequest
		{
			get
			{
				return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.RequestType)(base.GetValue(CurrentRequestProperty)));
			}
			set
			{
				base.SetValue(CurrentRequestProperty, value);
			}
		}

		public static DependencyProperty AttributeNameProperty = DependencyProperty.Register("AttributeName", typeof(string), typeof(ClearSingleValuedAttributeActivity));
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Properties")]
		public string AttributeName
		{
			get
			{
				return (string)base.GetValue(AttributeNameProperty);
			}
			set
			{
				base.SetValue(AttributeNameProperty, value);
			}
		}

		public static DependencyProperty TargetIdProperty = DependencyProperty.Register("TargetId", typeof(Guid), typeof(ClearSingleValuedAttributeActivity));
		[Description("TargetId")]
		[Category("TargetId Category")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Guid TargetId
		{
			get
			{
				return ((Guid)(base.GetValue(ClearSingleValuedAttributeActivity.TargetIdProperty)));
			}
			set
			{
				base.SetValue(ClearSingleValuedAttributeActivity.TargetIdProperty, value);
			}
		}

		#endregion

		public ClearSingleValuedAttributeActivity()
		{
			InitializeComponent();
		}

		void ActivityStatus(object sender, ActivityExecutionStatusChangedEventArgs e)
		{
			Tracer.TraceInformation("execution-status name: {0}, status: {1}, result: {2}", e.Activity.Name, e.ExecutionStatus, e.ExecutionResult);
		}

		private void PrepareReading_ExecuteCode(object sender, EventArgs e)
		{
			Tracer.Enter("preparereading_executecode");
			try
			{
				ReadResource.ActorId = WellKnownGuids.FIMServiceAccount;
				Tracer.TraceInformation("actor-objectid: {0}", ReadResource.ActorId);
				ReadResource.ResourceId = CurrentRequest.Target.GetGuid();
				Tracer.TraceInformation("target-objectid: {0}", ReadResource.ResourceId);
				ReadResource.SelectionAttributes = new string[] { this.AttributeName, "DisplayName" };
				Tracer.TraceInformation("attribute: {0}", ReadResource.SelectionAttributes.FirstOrDefault());

				ReadResource.Executing += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
				ReadResource.Faulting += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
				ReadResource.StatusChanged += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
				ReadResource.Canceling += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
				ReadResource.Compensating += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
				ReadResource.Closed += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
			}
			catch (Exception ex)
			{
				Tracer.TraceError("preparereading_executecode", ex);
			}
			Tracer.Exit("preparereading_executecode");
		}

		private void ThereWasValue_Condition(object sender, ConditionalEventArgs e)
		{
			try
			{
				Tracer.Enter("therewasvalue_condition");
				object displayName = ReadResource.Resource["DisplayName"];
				object myValue = ReadResource.Resource[this.AttributeName];
				Tracer.TraceInformation("target-displayname {0}", displayName);
				if (null != myValue)
				{
					Tracer.TraceInformation("current-value: {0}", ReadResource.Resource[this.AttributeName]);
					UpdateResource.ActorId = WellKnownGuids.FIMServiceAccount;
					Tracer.TraceInformation("actor-objectid: {0}", UpdateResource.ActorId);
					UpdateResource.ResourceId = CurrentRequest.Target.GetGuid();
					Tracer.TraceInformation("target-objectid: {0}", UpdateResource.ResourceId);
					UpdateRequestParameter[] UpdateParameters = new UpdateRequestParameter[] {
						new UpdateRequestParameter(
							this.AttributeName,
							UpdateMode.Remove,
							myValue)};
					UpdateResource.UpdateParameters = UpdateParameters;

					UpdateResource.Executing += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
					UpdateResource.Faulting += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
					UpdateResource.StatusChanged += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
					UpdateResource.Canceling += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
					UpdateResource.Compensating += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);
					UpdateResource.Closed += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ActivityStatus);

					e.Result = true;
				}
				else
				{
					Tracer.TraceInformation("no-current-value-to-remove");
					e.Result = false;
				}
			}
			catch (Exception ex)
			{
				Tracer.TraceError("therewasvalue_condition'", ex);
			}
			Tracer.Exit("therewasvalue_condition");
		}

		private void EndGracefully_ExecuteCode(object sender, EventArgs e)
		{
			Tracer.TraceInformation("done");
		}


	}

}
