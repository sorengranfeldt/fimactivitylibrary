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
    public partial class LookupAttributeValueActivity
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
            this.UpdateTargetIfNeeded = new Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity();
            this.UpdateWorkflowDataBranch = new System.Workflow.Activities.IfElseBranchActivity();
            this.UpdateTargetBranch = new System.Workflow.Activities.IfElseBranchActivity();
            this.UpdateTargetOrWorkflowData = new System.Workflow.Activities.IfElseActivity();
            this.Enumerate = new Granfeldt.FIM.ActivityLibrary.FindResourcesActivity();
            this.SetupVariablesActivity = new System.Workflow.Activities.CodeActivity();
            this.ResolveGrammarForXPathFilter = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
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
            // UpdateWorkflowDataBranch
            // 
            this.UpdateWorkflowDataBranch.Name = "UpdateWorkflowDataBranch";
            // 
            // UpdateTargetBranch
            // 
            this.UpdateTargetBranch.Activities.Add(this.UpdateTargetIfNeeded);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.UpdateTargetOrWorkflowData_Condition);
            this.UpdateTargetBranch.Condition = codecondition1;
            this.UpdateTargetBranch.Name = "UpdateTargetBranch";
            // 
            // UpdateTargetOrWorkflowData
            // 
            this.UpdateTargetOrWorkflowData.Activities.Add(this.UpdateTargetBranch);
            this.UpdateTargetOrWorkflowData.Activities.Add(this.UpdateWorkflowDataBranch);
            this.UpdateTargetOrWorkflowData.Name = "UpdateTargetOrWorkflowData";
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
            // ResolveGrammarForXPathFilter
            // 
            activitybind1.Name = "LookupAttributeValueActivity";
            activitybind1.Path = "XPathFilter";
            this.ResolveGrammarForXPathFilter.Name = "ResolveGrammarForXPathFilter";
            activitybind2.Name = "LookupAttributeValueActivity";
            activitybind2.Path = "ResolvedXPathFilter";
            this.ResolveGrammarForXPathFilter.WorkflowDictionaryKey = null;
            this.ResolveGrammarForXPathFilter.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.GrammarExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.ResolveGrammarForXPathFilter.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // LookupAttributeValueActivity
            // 
            this.Activities.Add(this.ResolveGrammarForXPathFilter);
            this.Activities.Add(this.SetupVariablesActivity);
            this.Activities.Add(this.Enumerate);
            this.Activities.Add(this.UpdateTargetOrWorkflowData);
            this.Name = "LookupAttributeValueActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        //private Microsoft.ResourceManagement.Workflow.Activities.ReadResourceActivity ReadResource;
        private IfElseBranchActivity UpdateWorkflowDataBranch;
        private IfElseBranchActivity UpdateTargetBranch;
        private IfElseActivity UpdateTargetOrWorkflowData;
        private UpdateSingleValueAttributeAsNeededActivity UpdateTargetIfNeeded;
        private FindResourcesActivity Enumerate;
        private CodeActivity SetupVariablesActivity;
        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveGrammarForXPathFilter;


        #region "Special workflows"

        // October 22, 2010 | Søren Granfeldt
        // this.ReadEnumeratedResources = new System.Workflow.Activities.CodeActivity();
        // this.ReadEnumeratedResources.Name = "ReadEnumeratedResources";
        // this.ReadEnumeratedResources.ExecuteCode += new System.EventHandler(this.ReadEnumeratedResources_ExecuteCode);

        // October 22, 2010 | Søren Granfeldt
        // this.Enumerate.Activities.Add(this.ReadEnumeratedResources);

        #endregion

    }
}
