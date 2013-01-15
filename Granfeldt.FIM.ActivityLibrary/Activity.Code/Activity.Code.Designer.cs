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
    public partial class CodeActivity
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
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            this.CatchAndArgumentException = new System.Workflow.Activities.CodeActivity();
            this.faultHandlerActivity1 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.faultHandlersActivity3 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.UpdateTargetResource = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
            this.NoUpdateNeeded = new System.Workflow.Activities.IfElseBranchActivity();
            this.UpdateTarget = new System.Workflow.Activities.IfElseBranchActivity();
            this.NullOrInvalidGrammar = new System.Workflow.Activities.CodeActivity();
            this.CompareAndPrepareUpdate = new System.Workflow.Activities.IfElseActivity();
            this.ReadTarget = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.faultHandlerActivity2 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.ExitGracefully = new System.Workflow.Activities.CodeActivity();
            this.UpdateIfNecessary = new System.Workflow.Activities.SequenceActivity();
            this.faultHandlersActivity2 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.SaveResolvedParameterValue = new System.Workflow.Activities.CodeActivity();
            this.ResolveParameterValue = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.UpdateWorkflowData = new System.Workflow.Activities.IfElseBranchActivity();
            this.ConditionalUpdateTarget = new System.Workflow.Activities.IfElseBranchActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.ResolveAndSave = new System.Workflow.Activities.SequenceActivity();
            this.ShouldUpdateTarget = new System.Workflow.Activities.IfElseActivity();
            this.ExecuteCode = new System.Workflow.Activities.CodeActivity();
            this.CompileCode = new System.Workflow.Activities.CodeActivity();
            this.ResolveAllParameters = new System.Workflow.Activities.WhileActivity();
            // 
            // CatchAndArgumentException
            // 
            this.CatchAndArgumentException.Name = "CatchAndArgumentException";
            this.CatchAndArgumentException.ExecuteCode += new System.EventHandler(this.CatchArgumentException_ExecuteCode);
            // 
            // faultHandlerActivity1
            // 
            this.faultHandlerActivity1.Activities.Add(this.CatchAndArgumentException);
            this.faultHandlerActivity1.FaultType = typeof(System.ArgumentNullException);
            this.faultHandlerActivity1.Name = "faultHandlerActivity1";
            // 
            // faultHandlersActivity3
            // 
            this.faultHandlersActivity3.Activities.Add(this.faultHandlerActivity1);
            this.faultHandlersActivity3.Name = "faultHandlersActivity3";
            // 
            // UpdateTargetResource
            // 
            this.UpdateTargetResource.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateTargetResource.ApplyAuthorizationPolicy = false;
            this.UpdateTargetResource.Name = "UpdateTargetResource";
            this.UpdateTargetResource.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateTargetResource.UpdateParameters = null;
            // 
            // NoUpdateNeeded
            // 
            this.NoUpdateNeeded.Name = "NoUpdateNeeded";
            // 
            // UpdateTarget
            // 
            this.UpdateTarget.Activities.Add(this.UpdateTargetResource);
            this.UpdateTarget.Activities.Add(this.faultHandlersActivity3);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isTargetUpdateNeeded_Condition);
            this.UpdateTarget.Condition = codecondition1;
            this.UpdateTarget.Name = "UpdateTarget";
            // 
            // NullOrInvalidGrammar
            // 
            this.NullOrInvalidGrammar.Name = "NullOrInvalidGrammar";
            this.NullOrInvalidGrammar.ExecuteCode += new System.EventHandler(this.NonExistingGrammarException_ExecuteCode);
            // 
            // CompareAndPrepareUpdate
            // 
            this.CompareAndPrepareUpdate.Activities.Add(this.UpdateTarget);
            this.CompareAndPrepareUpdate.Activities.Add(this.NoUpdateNeeded);
            this.CompareAndPrepareUpdate.Name = "CompareAndPrepareUpdate";
            // 
            // ReadTarget
            // 
            this.ReadTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.Name = "ReadTarget";
            activitybind1.Name = "CodeActivity";
            activitybind1.Path = "TargetResource";
            this.ReadTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.SelectionAttributes = null;
            this.ReadTarget.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // faultHandlerActivity2
            // 
            this.faultHandlerActivity2.Activities.Add(this.NullOrInvalidGrammar);
            this.faultHandlerActivity2.FaultType = typeof(System.ArgumentException);
            this.faultHandlerActivity2.Name = "faultHandlerActivity2";
            // 
            // ExitGracefully
            // 
            this.ExitGracefully.Name = "ExitGracefully";
            this.ExitGracefully.ExecuteCode += new System.EventHandler(this.ExitGracefully_ExecuteCode);
            // 
            // UpdateIfNecessary
            // 
            this.UpdateIfNecessary.Activities.Add(this.ReadTarget);
            this.UpdateIfNecessary.Activities.Add(this.CompareAndPrepareUpdate);
            this.UpdateIfNecessary.Name = "UpdateIfNecessary";
            // 
            // faultHandlersActivity2
            // 
            this.faultHandlersActivity2.Activities.Add(this.faultHandlerActivity2);
            this.faultHandlersActivity2.Name = "faultHandlersActivity2";
            // 
            // SaveResolvedParameterValue
            // 
            this.SaveResolvedParameterValue.Name = "SaveResolvedParameterValue";
            this.SaveResolvedParameterValue.ExecuteCode += new System.EventHandler(this.SaveResolvedValue_ExecuteCode);
            // 
            // ResolveParameterValue
            // 
            this.ResolveParameterValue.GrammarExpression = null;
            this.ResolveParameterValue.Name = "ResolveParameterValue";
            activitybind2.Name = "CodeActivity";
            activitybind2.Path = "ResolvedParameterExpression";
            this.ResolveParameterValue.WorkflowDictionaryKey = null;
            this.ResolveParameterValue.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // UpdateWorkflowData
            // 
            this.UpdateWorkflowData.Name = "UpdateWorkflowData";
            // 
            // ConditionalUpdateTarget
            // 
            this.ConditionalUpdateTarget.Activities.Add(this.UpdateIfNecessary);
            this.ConditionalUpdateTarget.Activities.Add(this.ExitGracefully);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ShouldUpdateTarget_Condition);
            this.ConditionalUpdateTarget.Condition = codecondition2;
            this.ConditionalUpdateTarget.Name = "ConditionalUpdateTarget";
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // ResolveAndSave
            // 
            this.ResolveAndSave.Activities.Add(this.ResolveParameterValue);
            this.ResolveAndSave.Activities.Add(this.SaveResolvedParameterValue);
            this.ResolveAndSave.Activities.Add(this.faultHandlersActivity2);
            this.ResolveAndSave.Name = "ResolveAndSave";
            // 
            // ShouldUpdateTarget
            // 
            this.ShouldUpdateTarget.Activities.Add(this.ConditionalUpdateTarget);
            this.ShouldUpdateTarget.Activities.Add(this.UpdateWorkflowData);
            this.ShouldUpdateTarget.Name = "ShouldUpdateTarget";
            // 
            // ExecuteCode
            // 
            this.ExecuteCode.Name = "ExecuteCode";
            this.ExecuteCode.ExecuteCode += new System.EventHandler(this.ExecuteCode_ExecuteCode);
            // 
            // CompileCode
            // 
            this.CompileCode.Name = "CompileCode";
            this.CompileCode.ExecuteCode += new System.EventHandler(this.CompileCode_ExecuteCode);
            // 
            // ResolveAllParameters
            // 
            this.ResolveAllParameters.Activities.Add(this.ResolveAndSave);
            this.ResolveAllParameters.Activities.Add(this.faultHandlersActivity1);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.MoreParametersToResolve_Condition);
            this.ResolveAllParameters.Condition = codecondition3;
            this.ResolveAllParameters.Name = "ResolveAllParameters";
            // 
            // CodeActivity
            // 
            this.Activities.Add(this.ResolveAllParameters);
            this.Activities.Add(this.CompileCode);
            this.Activities.Add(this.ExecuteCode);
            this.Activities.Add(this.ShouldUpdateTarget);
            this.Name = "CodeActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private System.Workflow.Activities.CodeActivity CatchAndArgumentException;

        private FaultHandlerActivity faultHandlerActivity1;

        private FaultHandlersActivity faultHandlersActivity3;

        private System.Workflow.Activities.CodeActivity ExitGracefully;

        private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity UpdateTargetResource;

        private IfElseBranchActivity NoUpdateNeeded;

        private IfElseBranchActivity UpdateTarget;

        private IfElseActivity CompareAndPrepareUpdate;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadTarget;

        private SequenceActivity UpdateIfNecessary;

        private IfElseBranchActivity UpdateWorkflowData;

        private IfElseBranchActivity ConditionalUpdateTarget;

        private IfElseActivity ShouldUpdateTarget;

        private FaultHandlersActivity faultHandlersActivity2;

        private System.Workflow.Activities.CodeActivity NullOrInvalidGrammar;

        private FaultHandlerActivity faultHandlerActivity2;

        private System.Workflow.Activities.CodeActivity ExecuteCode;

        private FaultHandlersActivity faultHandlersActivity1;

        private System.Workflow.Activities.CodeActivity CompileCode;

        private System.Workflow.Activities.CodeActivity SaveResolvedParameterValue;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveParameterValue;

        private SequenceActivity ResolveAndSave;

        private WhileActivity ResolveAllParameters;


































    }
}
