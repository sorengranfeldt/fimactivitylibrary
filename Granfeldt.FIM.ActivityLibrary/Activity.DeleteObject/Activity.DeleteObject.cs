// January 24, 2013 | Soren Granfeldt
//  - initial version

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class DeleteObjectActivity : SequenceActivity
    {

        #region Properties

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DeleteObjectActivity));
        [Description("Title")]
        [Category("Title Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get
            {
                return ((string)(base.GetValue(DeleteObjectActivity.TitleProperty)));
            }
            set
            {
                base.SetValue(DeleteObjectActivity.TitleProperty, value);
            }
        }

        public static DependencyProperty ObjectIDToDeleteProperty = DependencyProperty.Register("ObjectIDToDelete", typeof(string), typeof(DeleteObjectActivity));
        [Description("ObjectIDToDelete")]
        [Category("ObjectIDToDelete Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ObjectIDToDelete
        {
            get
            {
                return ((string)(base.GetValue(DeleteObjectActivity.ObjectIDToDeleteProperty)));
            }
            set
            {
                base.SetValue(DeleteObjectActivity.ObjectIDToDeleteProperty, value);
            }
        }

        public static DependencyProperty ResolvedObjectIDToDeleteProperty = DependencyProperty.Register("ResolvedObjectIDToDelete", typeof(System.String), typeof(Granfeldt.FIM.ActivityLibrary.DeleteObjectActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public String ResolvedObjectIDToDelete
        {
            get
            {
                return ((string)(base.GetValue(Granfeldt.FIM.ActivityLibrary.DeleteObjectActivity.ResolvedObjectIDToDeleteProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.DeleteObjectActivity.ResolvedObjectIDToDeleteProperty, value);
            }
        }

        #endregion

        public DeleteObjectActivity()
        {
            InitializeComponent();
        }

        private void PrepareResolveDeleteObjectID_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Activity initialized");
            Debugging.Log("Resolving", this.ObjectIDToDelete);
            ResolveObjectIDToDeleteGrammar.GrammarExpression = this.ObjectIDToDelete;
            ResolveObjectIDToDeleteGrammar.Closed += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(ResolveObjectIDToDeleteGrammar_Closed);
        }

        private void PrepareDeleteObject_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Preparing delete of", this.ResolvedObjectIDToDelete);
            DeleteObject.ActorId = WellKnownGuids.FIMServiceAccount;
            DeleteObject.ResourceId = new Guid(this.ResolvedObjectIDToDelete);
            DeleteObject.Closed += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(DeleteObject_Closed);
        }

        private void ExitGracefully_ExecuteCode(object sender, EventArgs e)
        {
            Debugging.Log("Activity exited");
        }

        void ResolveObjectIDToDeleteGrammar_Closed(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            Debugging.Log("Resolve execution result", e.ExecutionResult);
        }

        void DeleteObject_Closed(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            Debugging.Log("Delete execution result", e.ExecutionResult);
        }

    }
}
