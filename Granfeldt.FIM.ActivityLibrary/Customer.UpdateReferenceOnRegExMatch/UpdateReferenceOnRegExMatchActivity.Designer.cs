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
    public partial class UpdateReferenceOnRegExMatchActivity
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
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_11 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            this.PrepareClearingReference = new System.Workflow.Activities.CodeActivity();
            this.PrepareUpdateReference = new System.Workflow.Activities.CodeActivity();
            this.FindUser = new Granfeldt.FIM.ActivityLibrary.FindResourcesActivity();
            this.NoClearReference = new System.Workflow.Activities.IfElseBranchActivity();
            this.YesUpdateReference = new System.Workflow.Activities.IfElseBranchActivity();
            this.UpdateUserReference = new Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity();
            this.DoesComputerNameContainUsername = new System.Workflow.Activities.IfElseActivity();
            this.ReadTarget = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.PrepareReadTarget = new System.Workflow.Activities.CodeActivity();
            this.GetCurrentRequest = new Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity();
            // 
            // PrepareClearingReference
            // 
            this.PrepareClearingReference.Name = "PrepareClearingReference";
            this.PrepareClearingReference.ExecuteCode += new System.EventHandler(this.PrepareClearingReferenceCode);
            // 
            // PrepareUpdateReference
            // 
            this.PrepareUpdateReference.Name = "PrepareUpdateReference";
            this.PrepareUpdateReference.ExecuteCode += new System.EventHandler(this.PrepareUpdateReferenceCode);
            // 
            // FindUser
            // 
            this.FindUser.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.FindUser.Attributes = null;
            this.FindUser.EnumeratedResourceIDs = null;
            this.FindUser.EnumeratedResources = list_11;
            this.FindUser.Name = "FindUser";
            this.FindUser.PageSize = 0;
            this.FindUser.SortingAttributes = null;
            this.FindUser.TotalResultsCount = 0;
            this.FindUser.XPathFilter = null;
            // 
            // NoClearReference
            // 
            this.NoClearReference.Activities.Add(this.PrepareClearingReference);
            this.NoClearReference.Name = "NoClearReference";
            // 
            // YesUpdateReference
            // 
            this.YesUpdateReference.Activities.Add(this.FindUser);
            this.YesUpdateReference.Activities.Add(this.PrepareUpdateReference);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IfComputerNameContainsUsername);
            this.YesUpdateReference.Condition = codecondition1;
            this.YesUpdateReference.Name = "YesUpdateReference";
            // 
            // UpdateUserReference
            // 
            this.UpdateUserReference.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateUserReference.AttributeName = null;
            this.UpdateUserReference.Name = "UpdateUserReference";
            this.UpdateUserReference.NewValue = null;
            this.UpdateUserReference.TargetId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateUserReference.TargetResource = null;
            // 
            // DoesComputerNameContainUsername
            // 
            this.DoesComputerNameContainUsername.Activities.Add(this.YesUpdateReference);
            this.DoesComputerNameContainUsername.Activities.Add(this.NoClearReference);
            this.DoesComputerNameContainUsername.Name = "DoesComputerNameContainUsername";
            // 
            // ReadTarget
            // 
            this.ReadTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.Name = "ReadTarget";
            activitybind1.Name = "UpdateReferenceOnRegExMatchActivity";
            activitybind1.Path = "TargetResource";
            this.ReadTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.SelectionAttributes = null;
            this.ReadTarget.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // PrepareReadTarget
            // 
            this.PrepareReadTarget.Name = "PrepareReadTarget";
            this.PrepareReadTarget.ExecuteCode += new System.EventHandler(this.PrepareReadTargetCode);
            // 
            // GetCurrentRequest
            // 
            activitybind2.Name = "UpdateReferenceOnRegExMatchActivity";
            activitybind2.Path = "CurrentRequest";
            this.GetCurrentRequest.Name = "GetCurrentRequest";
            this.GetCurrentRequest.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity.CurrentRequestProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // UpdateReferenceOnRegExMatchActivity
            // 
            this.Activities.Add(this.GetCurrentRequest);
            this.Activities.Add(this.PrepareReadTarget);
            this.Activities.Add(this.ReadTarget);
            this.Activities.Add(this.DoesComputerNameContainUsername);
            this.Activities.Add(this.UpdateUserReference);
            this.Name = "UpdateReferenceOnRegExMatchActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity PrepareUpdateReference;

        private CodeActivity PrepareClearingReference;

        private UpdateSingleValueAttributeAsNeededActivity UpdateUserReference;

        private FindResourcesActivity FindUser;

        private IfElseBranchActivity NoClearReference;

        private IfElseBranchActivity YesUpdateReference;

        private IfElseActivity DoesComputerNameContainUsername;

        private CodeActivity PrepareReadTarget;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadTarget;

        private Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity GetCurrentRequest;














    }
}
