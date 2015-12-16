using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Workflow.ComponentModel;
using Microsoft.IdentityManagement.WebUI.Controls;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary.WebUIs
{
    class AddRemoveFromMultiValueActivitySettingsPart : BaseActivitySettingsPart
    {

        const string LookupXPath = "TargetObjects";
        const string ValueToAddRemove = "ValueToAddRemove";
        const string MultivalueAttributeName = "TargetMultivalueAttributename";
		const string HandleDuplicates = "HandleDuplicates";
        const string Action = "Action";
        const string Actor = "Actor";

        protected override void CreateChildControls()
        {
            Table layoutTable;
            layoutTable = new Table();

            // width is set to 100% of the control size
            layoutTable.Width = Unit.Percentage(100.0);
            layoutTable.BorderWidth = 0;
            layoutTable.CellPadding = 2;

            // add a TableRow for each textbox in the UI
            layoutTable.Rows.Add(this.AddTableRowTextBox("Target object(s) (XPath Filter):", "txt" + LookupXPath, 400, 1000, false, ""));
            layoutTable.Rows.Add(this.AddTableRowTextBox("Value to add, i.e. [//Target/ObjectID]:", "txt" + ValueToAddRemove, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddTableRowTextBox("Target multi-value attributename:", "txt" + MultivalueAttributeName, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddAddRemoveDropDownList("Action:", "txt" + Action, 400, "Add"));
			layoutTable.Rows.Add(this.AddCheckbox("Add/remove duplicates", "txt" + HandleDuplicates, false));
            layoutTable.Rows.Add(this.AddActorDropDownList("Actor (run as):", "txt" + Actor, 400, WellKnownGuids.FIMServiceAccount.ToString()));
            this.Controls.Add(layoutTable);
            base.CreateChildControls();
        }

        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            Debugging.Log("GenerateActivityOnWorkflow");
            if (!this.ValidateInputs())
            {
                return null;
            }
            AddRemoveFromMultiValueActivity act = new AddRemoveFromMultiValueActivity();
            act.ValueToAddRemove = this.GetText("txt" + ValueToAddRemove);
            act.LookupXPath = this.GetText("txt" + LookupXPath);
            act.MultivalueAttributeName = this.GetText("txt" + MultivalueAttributeName);
            act.Actor = this.GetActorDropDownList("txt" + Actor);
            act.AddRemoveAction = this.GetAddRemoveDropDownList("txt" + Action);
            return act;
        }

        public override void LoadActivitySettings(Activity activity)
        {
            Debugging.Log("LoadActivitySettings");
            AddRemoveFromMultiValueActivity thisActivity = activity as AddRemoveFromMultiValueActivity;
            if (thisActivity != null)
            {
                this.SetText("txt" + LookupXPath, thisActivity.LookupXPath);
                this.SetText("txt" + ValueToAddRemove, thisActivity.ValueToAddRemove);
                this.SetText("txt" + MultivalueAttributeName, thisActivity.MultivalueAttributeName);
                this.SetActorDropDownList("txt" + Actor, thisActivity.Actor);
                this.SetAddRemoveDropDownList("txt" + Action, thisActivity.AddRemoveAction);
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            Debugging.Log("PersistSettings");
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data[LookupXPath] = this.GetText("txt" + LookupXPath);
            data[MultivalueAttributeName] = this.GetText("txt" + MultivalueAttributeName);
            data[ValueToAddRemove] = this.GetText("txt" + ValueToAddRemove);
            data[Actor] = this.GetActorDropDownList("txt" + Actor);
            data[Action] = this.GetAddRemoveDropDownList("txt" + Action);
            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            Debugging.Log("RestoreSettings");
            if (null != data)
            {
                this.SetText("txt" + LookupXPath, (string)data[LookupXPath]);
                this.SetText("txt" + MultivalueAttributeName, (string)data[MultivalueAttributeName]);
                this.SetText("txt" + ValueToAddRemove, (string)data[ValueToAddRemove]);
                this.SetActorDropDownList("txt" + Actor, (string)data[Actor]);
                this.SetAddRemoveDropDownList("txt" + Action, (string)data[Action]);
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            Debugging.Log("SwitchMode");
            bool isDisabled = (mode != ActivitySettingsPartMode.Edit);
            this.SetTextBoxReadOnlyOption("txt" + LookupXPath, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + MultivalueAttributeName, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + ValueToAddRemove, isDisabled);
            this.SetDropDownListDisabled("txt" + Actor, isDisabled);
            this.SetDropDownListDisabled("txt" + Action, isDisabled);
        }

        public override string Title
        {
            get { return "Add or Remove to/from multi-value"; }
        }

        public override bool ValidateInputs()
        {
            Debugging.Log("ValidateInputs");
            return true;
        }
    }
}
