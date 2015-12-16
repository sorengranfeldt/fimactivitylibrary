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
    public partial class AddRemoveFromMultiValueActivity
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
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_11 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            this.LogException = new System.Workflow.Activities.CodeActivity();
            this.LogArgumentException = new System.Workflow.Activities.CodeActivity();
            this.faultHandlerGeneralException = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.faultHandlerArgumentException = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.UpdateMVOnTarget = new Granfeldt.FIM.ActivityLibrary.AddRemoveMultiValueActivityAsNeeded();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.LoopAndUpdateAllTargets = new System.Workflow.Activities.WhileActivity();
            this.Enumerate = new Granfeldt.FIM.ActivityLibrary.FindResourcesActivity();
            this.SetupVariablesActivity = new System.Workflow.Activities.CodeActivity();
            this.ResolveValueToAdd = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.ResolveLookupXPath = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.GetCurrentRequest = new Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity();
            // 
            // LogException
            // 
            this.LogException.Name = "LogException";
            this.LogException.ExecuteCode += new System.EventHandler(this.LogException_ExecuteCode);
            // 
            // LogArgumentException
            // 
            this.LogArgumentException.Name = "LogArgumentException";
            this.LogArgumentException.ExecuteCode += new System.EventHandler(this.LogArgumentException_ExecuteCode);
            // 
            // faultHandlerGeneralException
            // 
            this.faultHandlerGeneralException.Activities.Add(this.LogException);
            this.faultHandlerGeneralException.FaultType = typeof(System.Exception);
            this.faultHandlerGeneralException.Name = "faultHandlerGeneralException";
            // 
            // faultHandlerArgumentException
            // 
            this.faultHandlerArgumentException.Activities.Add(this.LogArgumentException);
            this.faultHandlerArgumentException.FaultType = typeof(System.ArgumentException);
            this.faultHandlerArgumentException.Name = "faultHandlerArgumentException";
            // 
            // UpdateMVOnTarget
            // 
            this.UpdateMVOnTarget.Action = null;
            this.UpdateMVOnTarget.Destination = null;
            this.UpdateMVOnTarget.Name = "UpdateMVOnTarget";
            this.UpdateMVOnTarget.TargetIdToUpdate = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateMVOnTarget.TargetResource = null;
            this.UpdateMVOnTarget.ValueToAddOrRemove = null;
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerArgumentException);
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerGeneralException);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // LoopAndUpdateAllTargets
            // 
            this.LoopAndUpdateAllTargets.Activities.Add(this.UpdateMVOnTarget);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.MoreTargetsToUpdate_Condition);
            this.LoopAndUpdateAllTargets.Condition = codecondition1;
            this.LoopAndUpdateAllTargets.Name = "LoopAndUpdateAllTargets";
            // 
            // Enumerate
            // 
            this.Enumerate.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.Enumerate.Attributes = null;
            this.Enumerate.EnumeratedResourceIDs = null;
            this.Enumerate.EnumeratedResources = list_11;
            this.Enumerate.Name = "Enumerate";
            this.Enumerate.PageSize = 0;
            this.Enumerate.SortingAttributes = null;
            this.Enumerate.TotalResultsCount = 0;
            this.Enumerate.XPathFilter = null;
            // 
            // SetupVariablesActivity
            // 
            this.SetupVariablesActivity.Name = "SetupVariablesActivity";
            this.SetupVariablesActivity.ExecuteCode += new System.EventHandler(this.SetupVariablesActivity_ExecuteCode);
            // 
            // ResolveValueToAdd
            // 
            activitybind1.Name = "AddRemoveFromMultiValueActivity";
            activitybind1.Path = "ValueToAddRemove";
            this.ResolveValueToAdd.Name = "ResolveValueToAdd";
            activitybind2.Name = "AddRemoveFromMultiValueActivity";
            activitybind2.Path = "ValueToAddRemoveResolved";
            this.ResolveValueToAdd.WorkflowDictionaryKey = null;
            this.ResolveValueToAdd.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.GrammarExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.ResolveValueToAdd.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // ResolveLookupXPath
            // 
            activitybind3.Name = "AddRemoveFromMultiValueActivity";
            activitybind3.Path = "LookupXPath";
            this.ResolveLookupXPath.Name = "ResolveLookupXPath";
            activitybind4.Name = "AddRemoveFromMultiValueActivity";
            activitybind4.Path = "ResolvedLookupXPath";
            this.ResolveLookupXPath.WorkflowDictionaryKey = null;
            this.ResolveLookupXPath.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.GrammarExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.ResolveLookupXPath.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // GetCurrentRequest
            // 
            activitybind5.Name = "AddRemoveFromMultiValueActivity";
            activitybind5.Path = "CurrentRequest";
            this.GetCurrentRequest.Name = "GetCurrentRequest";
            this.GetCurrentRequest.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity.CurrentRequestProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // AddRemoveFromMultiValueActivity
            // 
            this.Activities.Add(this.GetCurrentRequest);
            this.Activities.Add(this.ResolveLookupXPath);
            this.Activities.Add(this.ResolveValueToAdd);
            this.Activities.Add(this.SetupVariablesActivity);
            this.Activities.Add(this.Enumerate);
            this.Activities.Add(this.LoopAndUpdateAllTargets);
            this.Activities.Add(this.faultHandlersActivity1);
            this.Name = "AddRemoveFromMultiValueActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity LogException;

        private FaultHandlerActivity faultHandlerGeneralException;

        private CodeActivity LogArgumentException;

        private FaultHandlerActivity faultHandlerArgumentException;

        private FaultHandlersActivity faultHandlersActivity1;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveValueToAdd;

        private AddRemoveMultiValueActivityAsNeeded UpdateMVOnTarget;

        private WhileActivity LoopAndUpdateAllTargets;

        private Microsoft.ResourceManagement.Workflow.Activities.CurrentRequestActivity GetCurrentRequest;

        private FindResourcesActivity Enumerate;

        private CodeActivity SetupVariablesActivity;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveLookupXPath;

























    }
}
