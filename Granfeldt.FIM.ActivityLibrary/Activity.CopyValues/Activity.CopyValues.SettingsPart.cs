using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Workflow.ComponentModel;
using Microsoft.IdentityManagement.WebUI.Controls;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary.WebUIs
{
    class CopyValuesActivitySettingsPart : BaseActivitySettingsPart
    {

        const string InstanceTitle = "Title";
        const string AttributePairs = "Attribute Pairs";
        const string ConditionTrue = "ConditionTrue";
        const string AlternativeTargetObject = "AlternativeTargetObject";

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
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Update pairs:<br/><i>(source value, condition attribute name, target attribute name)</i>", "txt" + AttributePairs, 400, 400, true, ""));
            controlLayoutTable.Rows.Add(this.AddCheckbox("Update only on True condition?<br/><i>(if unchecked, updates are performed if condition attribute value is missing or false. If no condition attribute is specified, an update is always done)</i>", "txt"+ConditionTrue, true));
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Alternative target:<br/><i>(you can specify an XPath filter to lookup an object that should be updated instead of the default Target object)</i>", "txt" + AlternativeTargetObject, 400, 100, false, ""));
            this.Controls.Add(controlLayoutTable);

            base.CreateChildControls();
        }

        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            if (!this.ValidateInputs())
            {
                return null;
            }
            CopyValuesActivity ThisActivity = new CopyValuesActivity();
            ThisActivity.Title = this.GetText("txt" + InstanceTitle);
            ThisActivity.AttributePairs = this.GetTextArray("txt" + AttributePairs);
            ThisActivity.UpdateOnTrue = this.GetCheckbox("txt" + ConditionTrue);
            ThisActivity.AlternativeTargetObject = this.GetText("txt" + AlternativeTargetObject);

            return ThisActivity;
        }

        public override void LoadActivitySettings(Activity activity)
        {
            CopyValuesActivity ThisActivity = activity as CopyValuesActivity;
            if (ThisActivity != null)
            {
                this.SetText("txt" + InstanceTitle, ThisActivity.Title);
                this.SetTextArray("txt" + AttributePairs, ThisActivity.AttributePairs);
                this.SetCheckbox("txt" + ConditionTrue, ThisActivity.UpdateOnTrue);
                this.SetText("txt" + AlternativeTargetObject, ThisActivity.AlternativeTargetObject);
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data[InstanceTitle] = this.GetText("txt" + InstanceTitle);
            data[AttributePairs] = this.GetTextArray("txt" + AttributePairs);
            data[ConditionTrue] = this.GetCheckbox("txt" + ConditionTrue);
            data[AlternativeTargetObject] = this.GetText("txt" + AlternativeTargetObject);
            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            if (null != data)
            {
                this.SetText("txt" + InstanceTitle, (string)data[InstanceTitle]);
                this.SetTextArray("txt" + AttributePairs, (string[])data[AttributePairs]);
                this.SetCheckbox("txt" + ConditionTrue, (bool)data[ConditionTrue]);
                this.SetText("txt" + AlternativeTargetObject, (string)data[AlternativeTargetObject]);
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            bool readOnly = (mode == ActivitySettingsPartMode.View);
            this.SetTextBoxReadOnlyOption("txt" + InstanceTitle, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + AttributePairs, readOnly);
            this.SetCheckboxReadOnlyOption("txt" + ConditionTrue, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + AlternativeTargetObject, readOnly);
        }

        public override string Title
        {
            get
            {
                return string.Format("Copy Values: {0}", this.GetText("txt" + InstanceTitle));
            }
        }

        public override bool ValidateInputs()
        {
            return true;
        }

    }
}
