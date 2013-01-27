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
    public partial class DeleteObjectActivity
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
            this.ExitGracefully = new System.Workflow.Activities.CodeActivity();
            this.DeleteObject = new Microsoft.ResourceManagement.Workflow.Activities.DeleteResourceActivity();
            this.PrepareDeleteObject = new System.Workflow.Activities.CodeActivity();
            this.ResolveObjectIDToDeleteGrammar = new Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity();
            this.PrepareResolveDeleteObjectID = new System.Workflow.Activities.CodeActivity();
            // 
            // ExitGracefully
            // 
            this.ExitGracefully.Name = "ExitGracefully";
            this.ExitGracefully.ExecuteCode += new System.EventHandler(this.ExitGracefully_ExecuteCode);
            // 
            // DeleteObject
            // 
            this.DeleteObject.ActorId = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.DeleteObject.ApplyAuthorizationPolicy = false;
            this.DeleteObject.Name = "DeleteObject";
            this.DeleteObject.ResourceId = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // PrepareDeleteObject
            // 
            this.PrepareDeleteObject.Name = "PrepareDeleteObject";
            this.PrepareDeleteObject.ExecuteCode += new System.EventHandler(this.PrepareDeleteObject_ExecuteCode);
            // 
            // ResolveObjectIDToDeleteGrammar
            // 
            this.ResolveObjectIDToDeleteGrammar.GrammarExpression = null;
            this.ResolveObjectIDToDeleteGrammar.Name = "ResolveObjectIDToDeleteGrammar";
            activitybind1.Name = "DeleteObjectActivity";
            activitybind1.Path = "ResolvedObjectIDToDelete";
            this.ResolveObjectIDToDeleteGrammar.WorkflowDictionaryKey = null;
            this.ResolveObjectIDToDeleteGrammar.SetBinding(Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity.ResolvedExpressionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // PrepareResolveDeleteObjectID
            // 
            this.PrepareResolveDeleteObjectID.Name = "PrepareResolveDeleteObjectID";
            this.PrepareResolveDeleteObjectID.ExecuteCode += new System.EventHandler(this.PrepareResolveDeleteObjectID_ExecuteCode);
            // 
            // DeleteObjectActivity
            // 
            this.Activities.Add(this.PrepareResolveDeleteObjectID);
            this.Activities.Add(this.ResolveObjectIDToDeleteGrammar);
            this.Activities.Add(this.PrepareDeleteObject);
            this.Activities.Add(this.DeleteObject);
            this.Activities.Add(this.ExitGracefully);
            this.Name = "DeleteObjectActivity";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.ResourceManagement.Workflow.Activities.DeleteResourceActivity DeleteObject;

        private CodeActivity PrepareDeleteObject;

        private Microsoft.ResourceManagement.Workflow.Activities.ResolveGrammarActivity ResolveObjectIDToDeleteGrammar;

        private CodeActivity ExitGracefully;

        private CodeActivity PrepareResolveDeleteObjectID;






    }
}
