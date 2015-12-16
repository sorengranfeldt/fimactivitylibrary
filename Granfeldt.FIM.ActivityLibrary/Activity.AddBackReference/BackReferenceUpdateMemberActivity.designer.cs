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
    public partial class BackReferenceUpdateMemberActivity
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
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            this.RemoveFromTarget = new Granfeldt.FIM.ActivityLibrary.UpdateReferenceAttributesAsNeededActivity();
            this.PrepareGroupRemoveOperation = new System.Workflow.Activities.CodeActivity();
            this.AddToTarget = new Granfeldt.FIM.ActivityLibrary.UpdateReferenceAttributesAsNeededActivity();
            this.PrepareGroupAddOperation = new System.Workflow.Activities.CodeActivity();
            this.RemoveActivity = new System.Workflow.Activities.SequenceActivity();
            this.AddingActivity = new System.Workflow.Activities.SequenceActivity();
            this.ForEachMemberToRemove = new System.Workflow.Activities.WhileActivity();
            this.ForEachMemberToAdd = new System.Workflow.Activities.WhileActivity();
            this.ClearMVOnResource = new Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity();
            this.SetupClearMV = new System.Workflow.Activities.CodeActivity();
            this.Remove = new System.Workflow.Activities.IfElseBranchActivity();
            this.Add = new System.Workflow.Activities.IfElseBranchActivity();
            this.ClearMultivalueAttribute = new System.Workflow.Activities.SequenceActivity();
            this.ifElseActivity1 = new System.Workflow.Activities.IfElseActivity();
            this.GetReferenceUpdates = new Granfeldt.FIM.ActivityLibrary.GetUpdatesToReferenceAttributeActivity();
            // 
            // RemoveFromTarget
            // 
            this.RemoveFromTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.RemoveFromTarget.AttributeName = null;
            this.RemoveFromTarget.Name = "RemoveFromTarget";
            this.RemoveFromTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.RemoveFromTarget.UpdateMode = Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode.Modify;
            this.RemoveFromTarget.Values = null;
            // 
            // PrepareGroupRemoveOperation
            // 
            this.PrepareGroupRemoveOperation.Name = "PrepareGroupRemoveOperation";
            this.PrepareGroupRemoveOperation.ExecuteCode += new System.EventHandler(this.PrepareGroupOperation_ExecuteCode);
            // 
            // AddToTarget
            // 
            this.AddToTarget.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AddToTarget.AttributeName = null;
            this.AddToTarget.Name = "AddToTarget";
            this.AddToTarget.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AddToTarget.UpdateMode = Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode.Modify;
            this.AddToTarget.Values = null;
            // 
            // PrepareGroupAddOperation
            // 
            this.PrepareGroupAddOperation.Name = "PrepareGroupAddOperation";
            this.PrepareGroupAddOperation.ExecuteCode += new System.EventHandler(this.PrepareGroupOperation_ExecuteCode);
            // 
            // RemoveActivity
            // 
            this.RemoveActivity.Activities.Add(this.PrepareGroupRemoveOperation);
            this.RemoveActivity.Activities.Add(this.RemoveFromTarget);
            this.RemoveActivity.Name = "RemoveActivity";
            // 
            // AddingActivity
            // 
            this.AddingActivity.Activities.Add(this.PrepareGroupAddOperation);
            this.AddingActivity.Activities.Add(this.AddToTarget);
            this.AddingActivity.Name = "AddingActivity";
            // 
            // ForEachMemberToRemove
            // 
            this.ForEachMemberToRemove.Activities.Add(this.RemoveActivity);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ForEveryAdd_Condition);
            this.ForEachMemberToRemove.Condition = codecondition1;
            this.ForEachMemberToRemove.Name = "ForEachMemberToRemove";
            // 
            // ForEachMemberToAdd
            // 
            this.ForEachMemberToAdd.Activities.Add(this.AddingActivity);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.ForEveryAdd_Condition);
            this.ForEachMemberToAdd.Condition = codecondition2;
            this.ForEachMemberToAdd.Name = "ForEachMemberToAdd";
            // 
            // ClearMVOnResource
            // 
            this.ClearMVOnResource.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ClearMVOnResource.Name = "ClearMVOnResource";
            this.ClearMVOnResource.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ClearMVOnResource.UpdateParameters = null;
            // 
            // SetupClearMV
            // 
            this.SetupClearMV.Name = "SetupClearMV";
            this.SetupClearMV.ExecuteCode += new System.EventHandler(this.SetupClearMV_ExecuteCode);
            // 
            // Remove
            // 
            this.Remove.Activities.Add(this.ForEachMemberToRemove);
            this.Remove.Name = "Remove";
            // 
            // Add
            // 
            this.Add.Activities.Add(this.ForEachMemberToAdd);
            ruleconditionreference1.ConditionName = "Condition1";
            this.Add.Condition = ruleconditionreference1;
            this.Add.Name = "Add";
            // 
            // ClearMultivalueAttribute
            // 
            this.ClearMultivalueAttribute.Activities.Add(this.SetupClearMV);
            this.ClearMultivalueAttribute.Activities.Add(this.ClearMVOnResource);
            this.ClearMultivalueAttribute.Name = "ClearMultivalueAttribute";
            // 
            // ifElseActivity1
            // 
            this.ifElseActivity1.Activities.Add(this.Add);
            this.ifElseActivity1.Activities.Add(this.Remove);
            this.ifElseActivity1.Name = "ifElseActivity1";
            // 
            // GetReferenceUpdates
            // 
            this.GetReferenceUpdates.AddedReferences = null;
            activitybind1.Name = "BackReferenceUpdateMemberActivity";
            activitybind1.Path = "Source";
            this.GetReferenceUpdates.ModifiedReferences = null;
            this.GetReferenceUpdates.Name = "GetReferenceUpdates";
            this.GetReferenceUpdates.OperationType = Microsoft.ResourceManagement.WebServices.WSResourceManagement.OperationType.Create;
            this.GetReferenceUpdates.RemovedReferences = null;
            this.GetReferenceUpdates.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.GetReferenceUpdates.SetBinding(Granfeldt.FIM.ActivityLibrary.GetUpdatesToReferenceAttributeActivity.AttributeNameProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // BackReferenceUpdateMemberActivity
            // 
            this.Activities.Add(this.GetReferenceUpdates);
            this.Activities.Add(this.ifElseActivity1);
            this.Activities.Add(this.ClearMultivalueAttribute);
            this.Name = "BackReferenceUpdateMemberActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity SetupClearMV;

        private SequenceActivity ClearMultivalueAttribute;

        private Microsoft.ResourceManagement.Workflow.Activities.UpdateResourceActivity ClearMVOnResource;

        private IfElseBranchActivity Remove;

        private IfElseBranchActivity Add;

        private IfElseActivity ifElseActivity1;

        private UpdateReferenceAttributesAsNeededActivity RemoveFromTarget;

        private CodeActivity PrepareGroupRemoveOperation;

        private SequenceActivity RemoveActivity;

        private WhileActivity ForEachMemberToRemove;

        private UpdateReferenceAttributesAsNeededActivity AddToTarget;

        private CodeActivity PrepareGroupAddOperation;

        private SequenceActivity AddingActivity;

        private WhileActivity ForEachMemberToAdd;

        private GetUpdatesToReferenceAttributeActivity GetReferenceUpdates;













    }
}
