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
    class CreateObjectActivitySettingsPart : BaseActivitySettingsPart
    {

        const string InstanceTitle = "Title";
        const string InitialValuePairs = "Initial value pairs";
        const string ExistenceLookupFilter = "ExistenceLookupFilter";
        const string NewObjectType = "NewObjectType";

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
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Initial values:<br/><i>(initial value, attribute name)</i>", "txt" + InitialValuePairs, 400, 400, true, ""));
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Existence lookup filter:<br/><i>(specify an XPath filter to do a pre-create lookup. If one or more objects are found, the new object is not created. If this field is left blank, a new object is always created)</i>", "txt" + ExistenceLookupFilter, 400, 100, false, ""));
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("New object type:<br/><i>(specify type of the new object, i.e. Person, Group or other)</i>", "txt" + NewObjectType, 400, 100, false, ""));
            this.Controls.Add(controlLayoutTable);

            base.CreateChildControls();
        }

        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            if (!this.ValidateInputs())
            {
                return null;
            }
            CreateObjectActivity ThisActivity = new CreateObjectActivity();
            ThisActivity.Title = this.GetText("txt" + InstanceTitle);
            ThisActivity.InitialValuePairs = this.GetTextArray("txt" + InitialValuePairs);
            ThisActivity.ExistenceLookupFilter = this.GetText("txt" + ExistenceLookupFilter);
            ThisActivity.NewObjectType = this.GetText("txt" + NewObjectType);

            return ThisActivity;
        }

        public override void LoadActivitySettings(Activity activity)
        {
            CreateObjectActivity ThisActivity = activity as CreateObjectActivity;
            if (ThisActivity != null)
            {
                this.SetText("txt" + InstanceTitle, ThisActivity.Title);
                this.SetTextArray("txt" + InitialValuePairs, ThisActivity.InitialValuePairs);
                this.SetText("txt" + ExistenceLookupFilter, ThisActivity.ExistenceLookupFilter);
                this.SetText("txt" + NewObjectType, ThisActivity.NewObjectType);
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data[InstanceTitle] = this.GetText("txt" + InstanceTitle);
            data[InitialValuePairs] = this.GetTextArray("txt" + InitialValuePairs);
            data[NewObjectType] = this.GetText("txt" + NewObjectType);
            data[ExistenceLookupFilter] = this.GetText("txt" + ExistenceLookupFilter);
            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            if (null != data)
            {
                this.SetText("txt" + InstanceTitle, (string)data[InstanceTitle]);
                this.SetTextArray("txt" + InitialValuePairs, (string[])data[InitialValuePairs]);
                this.SetText("txt" + NewObjectType, (string)data[NewObjectType]);
                this.SetText("txt" + ExistenceLookupFilter, (string)data[ExistenceLookupFilter]);
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            bool readOnly = (mode == ActivitySettingsPartMode.View);
            this.SetTextBoxReadOnlyOption("txt" + InstanceTitle, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + InitialValuePairs, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + NewObjectType, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + ExistenceLookupFilter, readOnly);
        }

        public override string Title
        {
            get
            {
                return string.Format("Create Object: {0}", this.GetText("txt" + InstanceTitle));
            }
        }

        public override bool ValidateInputs()
        {
            return true;
        }

    }
}
