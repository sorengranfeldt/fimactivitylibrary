// January 24, 2013 | Soren Granfeldt
//  - initial version

using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Workflow.ComponentModel;
using Microsoft.IdentityManagement.WebUI.Controls;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary.WebUIs
{
    class DeleteObjectActivitySettingsPart : BaseActivitySettingsPart
    {

        const string InstanceTitle = "Title";
        const string ObjectIDToDelete = "ObjectIDToDelete";

        /// <summary>
        ///  Creates a Table that contains the controls used by the activity UI
        ///  in the Workflow Designer of the FIM portal. Adds that Table to the
        ///  collection of Controls that defines each activity that can be selected
        ///  in the Workflow Designer of the FIM Portal. Calls the base class of 
        ///  ActivitySettingsPart to render the controls in the UI.
        /// </summary>
        protected override void CreateChildControls()
        {
            Table controlLayoutTable;
            controlLayoutTable = new Table();

            //Width is set to 100% of the control size
            controlLayoutTable.Width = Unit.Percentage(100.0);
            controlLayoutTable.BorderWidth = 0;
            controlLayoutTable.CellPadding = 2;
            //Add a TableRow for each textbox in the UI 
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Title:", "txt" + InstanceTitle, 400, 100, false, ""));
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("ObjectID:<br/><i>(ID of object to delete, i.e. [//Target/ObjectID] or [//WorkflowData/DeleteObjectID])</i>", "txt" + ObjectIDToDelete, 400, 400, false, "[//Target/ObjectID]"));
            this.Controls.Add(controlLayoutTable);
            base.CreateChildControls();
        }

        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            if (!this.ValidateInputs())
            {
                return null;
            }
            DeleteObjectActivity ThisActivity = new DeleteObjectActivity();
            ThisActivity.Title = this.GetText("txt" + InstanceTitle);
            ThisActivity.ObjectIDToDelete = this.GetText("txt" + ObjectIDToDelete);

            return ThisActivity;
        }

        public override void LoadActivitySettings(Activity activity)
        {
            DeleteObjectActivity ThisActivity = activity as DeleteObjectActivity;
            if (ThisActivity != null)
            {
                this.SetText("txt" + InstanceTitle, ThisActivity.Title);
                this.SetText("txt" + ObjectIDToDelete, ThisActivity.ObjectIDToDelete);
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data[InstanceTitle] = this.GetText("txt" + InstanceTitle);
            data[ObjectIDToDelete] = this.GetText("txt" + ObjectIDToDelete);
            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            if (null != data)
            {
                this.SetText("txt" + InstanceTitle, (string)data[InstanceTitle]);
                this.SetText("txt" + ObjectIDToDelete, (string)data[ObjectIDToDelete]);
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            bool readOnly = (mode == ActivitySettingsPartMode.View);
            this.SetTextBoxReadOnlyOption("txt" + InstanceTitle, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + ObjectIDToDelete, readOnly);
        }

        public override string Title
        {
            get
            {
                return string.Format("Delete Object: {0}", this.GetText("txt" + InstanceTitle));
            }
        }

        public override bool ValidateInputs()
        {
            return true;
        }

    }
}
