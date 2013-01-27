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
    public partial class CreateObjectActivity
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
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType> list_11 = new System.Collections.Generic.List<Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType>();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            this.NonExistingGrammarException = new System.Workflow.Activities.CodeActivity();
            this.InvalidGrammerOrArgument = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.FetchResolvedGrammar = new System.Workflow.Activities.CodeActivity();
            this.ResolveInitialValueGrammar = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.ResolveAndFetchSequence = new System.Workflow.Activities.SequenceActivity();
            this.CreateNewObject = new Microsoft.ResourceManagement.Workflow.Activities.CreateResourceActivity();
            this.PrepareCreateObject = new System.Workflow.Activities.CodeActivity();
            this.ResolveInitialValueWhile = new System.Workflow.Activities.WhileActivity();
            this.PrepareResolveInitialValues = new System.Workflow.Activities.CodeActivity();
            this.VerifyExistenceLookupResult = new System.Workflow.Activities.CodeActivity();
            this.ExistenceLookup = new Granfeldt.FIM.ActivityLibrary.FindResourcesActivity();
            this.PrepareExistenceCheckLookup = new System.Workflow.Activities.CodeActivity();
            this.ResolveExistenceTestFilter = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.NoCreateObject = new System.Workflow.Activities.IfElseBranchActivity();
            this.YesCreateObject = new System.Workflow.Activities.IfElseBranchActivity();
            this.DontCheckExistence = new System.Workflow.Activities.IfElseBranchActivity();
            this.CheckExistence = new System.Workflow.Activities.IfElseBranchActivity();
            this.ExitGracefully = new System.Workflow.Activities.CodeActivity();
            this.ShouldCreateObject = new System.Workflow.Activities.IfElseActivity();
            this.DoExistenceTest = new System.Workflow.Activities.IfElseActivity();
            // 
            // NonExistingGrammarException
            // 
            this.NonExistingGrammarException.Name = "NonExistingGrammarException";
            this.NonExistingGrammarException.ExecuteCode += new System.EventHandler(this.NonExistingGrammarException_ExecuteCode);
            // 
            // InvalidGrammerOrArgument
            // 
            this.InvalidGrammerOrArgument.Activities.Add(this.NonExistingGrammarException);
            this.InvalidGrammerOrArgument.FaultType = typeof(System.ArgumentException);
            this.InvalidGrammerOrArgument.Name = "InvalidGrammerOrArgument";
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.InvalidGrammerOrArgument);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // FetchResolvedGrammar
            // 
            this.FetchResolvedGrammar.Name = "FetchResolvedGrammar";
            this.FetchResolvedGrammar.ExecuteCode += new System.EventHandler(this.FetchResolvedGrammar_ExecuteCode);
            // 
            // ResolveInitialValueGrammar
            // 
            this.ResolveInitialValueGrammar.GrammarExpression = null;
            this.ResolveInitialValueGrammar.Name = "ResolveInitialValueGrammar";
            activitybind1.Name = "CreateObjectActivity";
            activitybind1.Path = "ResolvedInitialValueGrammer";
            this.ResolveInitialValueGrammar.WorkflowDictionaryKey = null;
            this.ResolveInitialValueGrammar.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // ResolveAndFetchSequence
            // 
            this.ResolveAndFetchSequence.Activities.Add(this.ResolveInitialValueGrammar);
            this.ResolveAndFetchSequence.Activities.Add(this.FetchResolvedGrammar);
            this.ResolveAndFetchSequence.Activities.Add(this.faultHandlersActivity1);
            this.ResolveAndFetchSequence.Name = "ResolveAndFetchSequence";
            // 
            // CreateNewObject
            // 
            this.CreateNewObject.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.CreateNewObject.ApplyAuthorizationPolicy = false;
            activitybind2.Name = "CreateObjectActivity";
            activitybind2.Path = "CreatedObjectId";
            this.CreateNewObject.CreateParameters = null;
            this.CreateNewObject.Name = "CreateNewObject";
            this.CreateNewObject.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.CreateResourceActivity.CreatedResourceIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // PrepareCreateObject
            // 
            this.PrepareCreateObject.Name = "PrepareCreateObject";
            this.PrepareCreateObject.ExecuteCode += new System.EventHandler(this.PrepareCreateObject_ExecuteCode);
            // 
            // ResolveInitialValueWhile
            // 
            this.ResolveInitialValueWhile.Activities.Add(this.ResolveAndFetchSequence);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.MoreToResolve_Condition);
            this.ResolveInitialValueWhile.Condition = codecondition1;
            this.ResolveInitialValueWhile.Name = "ResolveInitialValueWhile";
            // 
            // PrepareResolveInitialValues
            // 
            this.PrepareResolveInitialValues.Name = "PrepareResolveInitialValues";
            this.PrepareResolveInitialValues.ExecuteCode += new System.EventHandler(this.PrepareResolveInitialValues_ExecuteCode);
            // 
            // VerifyExistenceLookupResult
            // 
            this.VerifyExistenceLookupResult.Name = "VerifyExistenceLookupResult";
            this.VerifyExistenceLookupResult.ExecuteCode += new System.EventHandler(this.VerifyExistenceLookupResult_ExecuteCode);
            // 
            // ExistenceLookup
            // 
            this.ExistenceLookup.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ExistenceLookup.Attributes = null;
            this.ExistenceLookup.EnumeratedResourceIDs = null;
            this.ExistenceLookup.EnumeratedResources = list_11;
            this.ExistenceLookup.Name = "ExistenceLookup";
            this.ExistenceLookup.PageSize = 0;
            this.ExistenceLookup.SortingAttributes = null;
            this.ExistenceLookup.TotalResultsCount = 0;
            this.ExistenceLookup.XPathFilter = null;
            // 
            // PrepareExistenceCheckLookup
            // 
            this.PrepareExistenceCheckLookup.Name = "PrepareExistenceCheckLookup";
            this.PrepareExistenceCheckLookup.ExecuteCode += new System.EventHandler(this.PrepareExistenceCheckLookup_ExecuteCode);
            // 
            // ResolveExistenceTestFilter
            // 
            this.ResolveExistenceTestFilter.GrammarExpression = null;
            this.ResolveExistenceTestFilter.Name = "ResolveExistenceTestFilter";
            this.ResolveExistenceTestFilter.ResolvedExpression = null;
            this.ResolveExistenceTestFilter.WorkflowDictionaryKey = null;
            // 
            // NoCreateObject
            // 
            this.NoCreateObject.Name = "NoCreateObject";
            // 
            // YesCreateObject
            // 
            this.YesCreateObject.Activities.Add(this.PrepareResolveInitialValues);
            this.YesCreateObject.Activities.Add(this.ResolveInitialValueWhile);
            this.YesCreateObject.Activities.Add(this.PrepareCreateObject);
            this.YesCreateObject.Activities.Add(this.CreateNewObject);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ShouldCreateObject_Condition);
            this.YesCreateObject.Condition = codecondition2;
            this.YesCreateObject.Name = "YesCreateObject";
            // 
            // DontCheckExistence
            // 
            this.DontCheckExistence.Name = "DontCheckExistence";
            // 
            // CheckExistence
            // 
            this.CheckExistence.Activities.Add(this.ResolveExistenceTestFilter);
            this.CheckExistence.Activities.Add(this.PrepareExistenceCheckLookup);
            this.CheckExistence.Activities.Add(this.ExistenceLookup);
            this.CheckExistence.Activities.Add(this.VerifyExistenceLookupResult);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.DoExistenceCheck_Condition);
            this.CheckExistence.Condition = codecondition3;
            this.CheckExistence.Name = "CheckExistence";
            // 
            // ExitGracefully
            // 
            this.ExitGracefully.Name = "ExitGracefully";
            this.ExitGracefully.ExecuteCode += new System.EventHandler(this.ExitGracefully_ExecuteCode);
            // 
            // ShouldCreateObject
            // 
            this.ShouldCreateObject.Activities.Add(this.YesCreateObject);
            this.ShouldCreateObject.Activities.Add(this.NoCreateObject);
            this.ShouldCreateObject.Name = "ShouldCreateObject";
            // 
            // DoExistenceTest
            // 
            this.DoExistenceTest.Activities.Add(this.CheckExistence);
            this.DoExistenceTest.Activities.Add(this.DontCheckExistence);
            this.DoExistenceTest.Name = "DoExistenceTest";
            // 
            // CreateObjectActivity
            // 
            this.Activities.Add(this.DoExistenceTest);
            this.Activities.Add(this.ShouldCreateObject);
            this.Activities.Add(this.ExitGracefully);
            this.Name = "CreateObjectActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity ExitGracefully;

        private CodeActivity PrepareCreateObject;

        private Microsoft.ResourceManagement.Workflow.Activities.CreateResourceActivity CreateNewObject;

        private CodeActivity NonExistingGrammarException;

        private FaultHandlerActivity InvalidGrammerOrArgument;

        private FaultHandlersActivity faultHandlersActivity1;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveInitialValueGrammar;

        private SequenceActivity ResolveAndFetchSequence;

        private CodeActivity FetchResolvedGrammar;

        private WhileActivity ResolveInitialValueWhile;

        private IfElseBranchActivity NoCreateObject;

        private IfElseBranchActivity YesCreateObject;

        private IfElseActivity ShouldCreateObject;

        private CodeActivity PrepareResolveInitialValues;

        private CodeActivity VerifyExistenceLookupResult;

        private FindResourcesActivity ExistenceLookup;

        private CodeActivity PrepareExistenceCheckLookup;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveExistenceTestFilter;

        private IfElseBranchActivity DontCheckExistence;

        private IfElseBranchActivity CheckExistence;

        private IfElseActivity DoExistenceTest;





























    }
}
