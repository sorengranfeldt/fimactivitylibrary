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
    public partial class CopyValuesActivity
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
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_11 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            this.FaultArgumentNullException = new System.Workflow.Activities.CodeActivity();
            this.codeActivity1 = new System.Workflow.Activities.CodeActivity();
            this.faultHandlerNullException = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.faultHandlerActivity2 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.faultHandlersActivity2 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.UpdateTarget = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
            this.NonExistingGrammarException = new System.Workflow.Activities.CodeActivity();
            this.faultHandlersActivity3 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.NoTargetUpdate = new System.Workflow.Activities.IfElseBranchActivity();
            this.YesUpdateTarget = new System.Workflow.Activities.IfElseBranchActivity();
            this.faultHandlerActivity1 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.ShouldUpdateTargetObject = new System.Workflow.Activities.IfElseActivity();
            this.SetTargetOnAttributeValueCollection = new System.Workflow.Activities.CodeActivity();
            this.ReadTarget = new Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity();
            this.ExtractTargetObjectID = new System.Workflow.Activities.CodeActivity();
            this.FindTargetResource = new Granfeldt.FIM.ActivityLibrary.FindResourcesActivity();
            this.PrepareFindTargetResource = new System.Workflow.Activities.CodeActivity();
            this.ResolveLookupGrammar = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.FetchResolvedGrammar = new System.Workflow.Activities.CodeActivity();
            this.ResolveSourceGrammar = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.UpdateSequence = new System.Workflow.Activities.SequenceActivity();
            this.NoGetTargetID = new System.Workflow.Activities.IfElseBranchActivity();
            this.YesLookup = new System.Workflow.Activities.IfElseBranchActivity();
            this.ResolveValueGrammars = new System.Workflow.Activities.SequenceActivity();
            this.ExitGracefully = new System.Workflow.Activities.CodeActivity();
            this.LoopUpdateAllTargets = new System.Workflow.Activities.WhileActivity();
            this.ShouldLookup = new System.Workflow.Activities.IfElseActivity();
            this.ResolveSourceValues = new System.Workflow.Activities.WhileActivity();
            this.PrepareResolveValues = new System.Workflow.Activities.CodeActivity();
            // 
            // FaultArgumentNullException
            // 
            this.FaultArgumentNullException.Name = "FaultArgumentNullException";
            this.FaultArgumentNullException.ExecuteCode += new System.EventHandler(this.FaultArgumentNullException_ExecuteCode);
            // 
            // codeActivity1
            // 
            this.codeActivity1.Name = "codeActivity1";
            this.codeActivity1.ExecuteCode += new System.EventHandler(this.FaultArgumentNullException_ExecuteCode);
            // 
            // faultHandlerNullException
            // 
            this.faultHandlerNullException.Activities.Add(this.FaultArgumentNullException);
            this.faultHandlerNullException.FaultType = typeof(System.ArgumentNullException);
            this.faultHandlerNullException.Name = "faultHandlerNullException";
            // 
            // faultHandlerActivity2
            // 
            this.faultHandlerActivity2.Activities.Add(this.codeActivity1);
            this.faultHandlerActivity2.FaultType = typeof(System.ArgumentNullException);
            this.faultHandlerActivity2.Name = "faultHandlerActivity2";
            // 
            // faultHandlersActivity2
            // 
            this.faultHandlersActivity2.Activities.Add(this.faultHandlerNullException);
            this.faultHandlersActivity2.Name = "faultHandlersActivity2";
            // 
            // UpdateTarget
            // 
            this.UpdateTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateTarget.ApplyAuthorizationPolicy = false;
            this.UpdateTarget.Name = "UpdateTarget";
            this.UpdateTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.UpdateTarget.UpdateParameters = null;
            // 
            // NonExistingGrammarException
            // 
            this.NonExistingGrammarException.Name = "NonExistingGrammarException";
            this.NonExistingGrammarException.ExecuteCode += new System.EventHandler(this.NonExistingGrammarException_ExecuteCode);
            // 
            // faultHandlersActivity3
            // 
            this.faultHandlersActivity3.Activities.Add(this.faultHandlerActivity2);
            this.faultHandlersActivity3.Name = "faultHandlersActivity3";
            // 
            // NoTargetUpdate
            // 
            this.NoTargetUpdate.Name = "NoTargetUpdate";
            // 
            // YesUpdateTarget
            // 
            this.YesUpdateTarget.Activities.Add(this.UpdateTarget);
            this.YesUpdateTarget.Activities.Add(this.faultHandlersActivity2);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ShouldUpdate_Condition);
            this.YesUpdateTarget.Condition = codecondition1;
            this.YesUpdateTarget.Name = "YesUpdateTarget";
            // 
            // faultHandlerActivity1
            // 
            this.faultHandlerActivity1.Activities.Add(this.NonExistingGrammarException);
            this.faultHandlerActivity1.FaultType = typeof(System.ArgumentException);
            this.faultHandlerActivity1.Name = "faultHandlerActivity1";
            // 
            // ShouldUpdateTargetObject
            // 
            this.ShouldUpdateTargetObject.Activities.Add(this.YesUpdateTarget);
            this.ShouldUpdateTargetObject.Activities.Add(this.NoTargetUpdate);
            this.ShouldUpdateTargetObject.Activities.Add(this.faultHandlersActivity3);
            this.ShouldUpdateTargetObject.Name = "ShouldUpdateTargetObject";
            // 
            // SetTargetOnAttributeValueCollection
            // 
            this.SetTargetOnAttributeValueCollection.Name = "SetTargetOnAttributeValueCollection";
            this.SetTargetOnAttributeValueCollection.ExecuteCode += new System.EventHandler(this.SetTargetResource_ExecuteCode);
            // 
            // ReadTarget
            // 
            this.ReadTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.Name = "ReadTarget";
            activitybind1.Name = "CopyValuesActivity";
            activitybind1.Path = "Target";
            this.ReadTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ReadTarget.SelectionAttributes = null;
            this.ReadTarget.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity.ResourceProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // ExtractTargetObjectID
            // 
            this.ExtractTargetObjectID.Name = "ExtractTargetObjectID";
            this.ExtractTargetObjectID.ExecuteCode += new System.EventHandler(this.ExtractTargetObjectID_ExecuteCode);
            // 
            // FindTargetResource
            // 
            this.FindTargetResource.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.FindTargetResource.Attributes = new string[] {
        "ObjectID"};
            this.FindTargetResource.EnumeratedResourceIDs = null;
            this.FindTargetResource.EnumeratedResources = list_11;
            this.FindTargetResource.Name = "FindTargetResource";
            this.FindTargetResource.PageSize = 0;
            this.FindTargetResource.SortingAttributes = null;
            this.FindTargetResource.TotalResultsCount = 0;
            activitybind2.Name = "CopyValuesActivity";
            activitybind2.Path = "ResolvedAlternativeTargetObject";
            this.FindTargetResource.SetBinding(Granfeldt.FIM.ActivityLibrary.FindResourcesActivity.XPathFilterProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // PrepareFindTargetResource
            // 
            this.PrepareFindTargetResource.Name = "PrepareFindTargetResource";
            this.PrepareFindTargetResource.ExecuteCode += new System.EventHandler(this.PrepareFindTargetResource_ExecuteCode);
            // 
            // ResolveLookupGrammar
            // 
            activitybind3.Name = "CopyValuesActivity";
            activitybind3.Path = "AlternativeTargetObject";
            this.ResolveLookupGrammar.Name = "ResolveLookupGrammar";
            activitybind4.Name = "CopyValuesActivity";
            activitybind4.Path = "ResolvedAlternativeTargetObject";
            this.ResolveLookupGrammar.WorkflowDictionaryKey = null;
            this.ResolveLookupGrammar.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.GrammarExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.ResolveLookupGrammar.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerActivity1);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // FetchResolvedGrammar
            // 
            this.FetchResolvedGrammar.Name = "FetchResolvedGrammar";
            this.FetchResolvedGrammar.ExecuteCode += new System.EventHandler(this.FetchResolvedGrammar_ExecuteCode);
            // 
            // ResolveSourceGrammar
            // 
            this.ResolveSourceGrammar.GrammarExpression = null;
            this.ResolveSourceGrammar.Name = "ResolveSourceGrammar";
            activitybind5.Name = "CopyValuesActivity";
            activitybind5.Path = "ResolvedSourceExpression";
            this.ResolveSourceGrammar.WorkflowDictionaryKey = null;
            this.ResolveSourceGrammar.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // UpdateSequence
            // 
            this.UpdateSequence.Activities.Add(this.ReadTarget);
            this.UpdateSequence.Activities.Add(this.SetTargetOnAttributeValueCollection);
            this.UpdateSequence.Activities.Add(this.ShouldUpdateTargetObject);
            this.UpdateSequence.Name = "UpdateSequence";
            // 
            // NoGetTargetID
            // 
            this.NoGetTargetID.Activities.Add(this.ExtractTargetObjectID);
            this.NoGetTargetID.Name = "NoGetTargetID";
            // 
            // YesLookup
            // 
            this.YesLookup.Activities.Add(this.ResolveLookupGrammar);
            this.YesLookup.Activities.Add(this.PrepareFindTargetResource);
            this.YesLookup.Activities.Add(this.FindTargetResource);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ShouldDoLookup_Condition);
            this.YesLookup.Condition = codecondition2;
            this.YesLookup.Name = "YesLookup";
            // 
            // ResolveValueGrammars
            // 
            this.ResolveValueGrammars.Activities.Add(this.ResolveSourceGrammar);
            this.ResolveValueGrammars.Activities.Add(this.FetchResolvedGrammar);
            this.ResolveValueGrammars.Activities.Add(this.faultHandlersActivity1);
            this.ResolveValueGrammars.Name = "ResolveValueGrammars";
            // 
            // ExitGracefully
            // 
            this.ExitGracefully.Name = "ExitGracefully";
            this.ExitGracefully.ExecuteCode += new System.EventHandler(this.ExitGracefully_ExecuteCode);
            // 
            // LoopUpdateAllTargets
            // 
            this.LoopUpdateAllTargets.Activities.Add(this.UpdateSequence);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.MoreTargetsToUpdate_Condition);
            this.LoopUpdateAllTargets.Condition = codecondition3;
            this.LoopUpdateAllTargets.Name = "LoopUpdateAllTargets";
            // 
            // ShouldLookup
            // 
            this.ShouldLookup.Activities.Add(this.YesLookup);
            this.ShouldLookup.Activities.Add(this.NoGetTargetID);
            this.ShouldLookup.Name = "ShouldLookup";
            // 
            // ResolveSourceValues
            // 
            this.ResolveSourceValues.Activities.Add(this.ResolveValueGrammars);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.MoreToResolve_Condition);
            this.ResolveSourceValues.Condition = codecondition4;
            this.ResolveSourceValues.Name = "ResolveSourceValues";
            // 
            // PrepareResolveValues
            // 
            this.PrepareResolveValues.Name = "PrepareResolveValues";
            this.PrepareResolveValues.ExecuteCode += new System.EventHandler(this.PrepareResolveValues_ExecuteCode);
            // 
            // CopyValuesActivity
            // 
            this.Activities.Add(this.PrepareResolveValues);
            this.Activities.Add(this.ResolveSourceValues);
            this.Activities.Add(this.ShouldLookup);
            this.Activities.Add(this.LoopUpdateAllTargets);
            this.Activities.Add(this.ExitGracefully);
            this.Name = "CopyValuesActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity SetTargetOnAttributeValueCollection;

        private CodeActivity codeActivity1;

        private FaultHandlerActivity faultHandlerActivity2;

        private FaultHandlersActivity faultHandlersActivity3;

        private CodeActivity FaultArgumentNullException;

        private FaultHandlerActivity faultHandlerNullException;

        private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity UpdateTarget;

        private FaultHandlersActivity faultHandlersActivity2;

        private WhileActivity LoopUpdateAllTargets;

        private SequenceActivity UpdateSequence;

        private CodeActivity ExtractTargetObjectID;

        private CodeActivity ExitGracefully;

        private CodeActivity PrepareResolveValues;

        private CodeActivity PrepareFindTargetResource;

        private FindResourcesActivity FindTargetResource;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveLookupGrammar;

        private IfElseBranchActivity NoGetTargetID;

        private IfElseBranchActivity YesLookup;

        private IfElseActivity ShouldLookup;

        private CodeActivity NonExistingGrammarException;

        private FaultHandlerActivity faultHandlerActivity1;

        private FaultHandlersActivity faultHandlersActivity1;

        private CodeActivity FetchResolvedGrammar;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveSourceGrammar;

        private WhileActivity ResolveSourceValues;

        private SequenceActivity ResolveValueGrammars;

        private IfElseBranchActivity NoTargetUpdate;

        private IfElseBranchActivity YesUpdateTarget;

        private IfElseActivity ShouldUpdateTargetObject;

        private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadTarget;




















































































    }
}
