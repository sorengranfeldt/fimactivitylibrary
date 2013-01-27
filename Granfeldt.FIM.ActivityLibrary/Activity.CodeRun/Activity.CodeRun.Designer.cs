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
    public partial class CodeRunActivity
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
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            this.NullOrInvalidGrammar = new System.Workflow.Activities.CodeActivity();
            this.faultHandlerActivity2 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.UpdateTargetIfNeeded = new Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity();
            this.faultHandlersActivity2 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.SaveResolvedParameterValue = new System.Workflow.Activities.CodeActivity();
            this.ResolveParameterValue = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.UpdateWorkflowData = new System.Workflow.Activities.IfElseBranchActivity();
            this.ConditionalUpdateTarget = new System.Workflow.Activities.IfElseBranchActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.ResolveAndSave = new System.Workflow.Activities.SequenceActivity();
            this.ExitGracefully = new System.Workflow.Activities.CodeActivity();
            this.ShouldUpdateTargetOrWorkflowData = new System.Workflow.Activities.IfElseActivity();
            this.ExecuteCode = new System.Workflow.Activities.CodeActivity();
            this.CompileCode = new System.Workflow.Activities.CodeActivity();
            this.ResolveAllParameters = new System.Workflow.Activities.WhileActivity();
            // 
            // NullOrInvalidGrammar
            // 
            this.NullOrInvalidGrammar.Name = "NullOrInvalidGrammar";
            this.NullOrInvalidGrammar.ExecuteCode += new System.EventHandler(this.NonExistingGrammarException_ExecuteCode);
            // 
            // faultHandlerActivity2
            // 
            this.faultHandlerActivity2.Activities.Add(this.NullOrInvalidGrammar);
            this.faultHandlerActivity2.FaultType = typeof(System.ArgumentException);
            this.faultHandlerActivity2.Name = "faultHandlerActivity2";
            // 
            // UpdateTargetIfNeeded
            // 
            this.UpdateTargetIfNeeded.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateTargetIfNeeded.AttributeName = null;
            this.UpdateTargetIfNeeded.Name = "UpdateTargetIfNeeded";
            this.UpdateTargetIfNeeded.NewValue = null;
            this.UpdateTargetIfNeeded.TargetId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateTargetIfNeeded.TargetResource = null;
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
            activitybind1.Name = "CodeRunActivity";
            activitybind1.Path = "ResolvedParameterExpression";
            this.ResolveParameterValue.WorkflowDictionaryKey = null;
            this.ResolveParameterValue.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // UpdateWorkflowData
            // 
            this.UpdateWorkflowData.Name = "UpdateWorkflowData";
            // 
            // ConditionalUpdateTarget
            // 
            this.ConditionalUpdateTarget.Activities.Add(this.UpdateTargetIfNeeded);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ShouldUpdateTarget_Condition);
            this.ConditionalUpdateTarget.Condition = codecondition1;
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
            // ExitGracefully
            // 
            this.ExitGracefully.Name = "ExitGracefully";
            this.ExitGracefully.ExecuteCode += new System.EventHandler(this.ExitGracefully_ExecuteCode);
            // 
            // ShouldUpdateTargetOrWorkflowData
            // 
            this.ShouldUpdateTargetOrWorkflowData.Activities.Add(this.ConditionalUpdateTarget);
            this.ShouldUpdateTargetOrWorkflowData.Activities.Add(this.UpdateWorkflowData);
            this.ShouldUpdateTargetOrWorkflowData.Name = "ShouldUpdateTargetOrWorkflowData";
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
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.MoreParametersToResolve_Condition);
            this.ResolveAllParameters.Condition = codecondition2;
            this.ResolveAllParameters.Name = "ResolveAllParameters";
            // 
            // CodeRunActivity
            // 
            this.Activities.Add(this.ResolveAllParameters);
            this.Activities.Add(this.CompileCode);
            this.Activities.Add(this.ExecuteCode);
            this.Activities.Add(this.ShouldUpdateTargetOrWorkflowData);
            this.Activities.Add(this.ExitGracefully);
            this.Name = "CodeRunActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity ExitGracefully;

        private UpdateSingleValueAttributeAsNeededActivity UpdateTargetIfNeeded;

        private IfElseBranchActivity UpdateWorkflowData;

        private IfElseBranchActivity ConditionalUpdateTarget;

        private IfElseActivity ShouldUpdateTargetOrWorkflowData;

        private FaultHandlersActivity faultHandlersActivity2;

        private CodeActivity NullOrInvalidGrammar;

        private FaultHandlerActivity faultHandlerActivity2;

        private CodeActivity ExecuteCode;

        private FaultHandlersActivity faultHandlersActivity1;

        private CodeActivity CompileCode;

        private CodeActivity SaveResolvedParameterValue;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveParameterValue;

        private SequenceActivity ResolveAndSave;

        private WhileActivity ResolveAllParameters;











































    }
}
