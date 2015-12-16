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
    class UpdateReferenceOnRegExMatchActivitySettingsPart : BaseActivitySettingsPart
    {

        const string RegExFilter = "RegExFilter";
        const string PositiveRegExFilter = "PositiveRegExFilter";
        const string ComputerNameAttributeName = "ComputerNameAttribute";
        const string UserNameAttributeName = "UserNameAttributeName";
        const string TargetReferenceAttributeName = "TargetReferenceAttributeName";
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
            layoutTable.Rows.Add(this.AddTableRowTextBox("Regex Filter:", "txt" + RegExFilter, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddTableRowTextBox("Positive Regex Filter:<br/><sup>if computername match filter, username is always updated</sup>", "txt" + PositiveRegExFilter, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddTableRowTextBox("ComputerName attribute:", "txt" + ComputerNameAttributeName, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddTableRowTextBox("UserName attribute:", "txt" + UserNameAttributeName, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddTableRowTextBox("User reference attribute:", "txt" + TargetReferenceAttributeName, 400, 100, false, ""));
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
            UpdateReferenceOnRegExMatchActivity act = new UpdateReferenceOnRegExMatchActivity();
            act.RegExFilter = this.GetText("txt" + RegExFilter);
            act.PositiveRegExFilter = this.GetText("txt" + PositiveRegExFilter);
            act.ComputerNameAttributeName = this.GetText("txt" + ComputerNameAttributeName);
            act.UserNameAttributeName = this.GetText("txt" + UserNameAttributeName);
            act.TargetReferenceAttributeName = this.GetText("txt" + TargetReferenceAttributeName);
            act.Actor = this.GetActorDropDownList("txt" + Actor);
            return act;
        }

        public override void LoadActivitySettings(Activity activity)
        {
            Debugging.Log("LoadActivitySettings");
            UpdateReferenceOnRegExMatchActivity act = activity as UpdateReferenceOnRegExMatchActivity;
            if (act != null)
            {
                this.SetText("txt" + RegExFilter, act.RegExFilter);
                this.SetText("txt" + PositiveRegExFilter, act.PositiveRegExFilter);
                this.SetText("txt" + ComputerNameAttributeName, act.ComputerNameAttributeName);
                this.SetText("txt" + UserNameAttributeName, act.UserNameAttributeName);
                this.SetText("txt" + TargetReferenceAttributeName, act.TargetReferenceAttributeName);
                this.SetActorDropDownList("txt" + Actor, act.Actor);
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            Debugging.Log("PersistSettings");
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data[RegExFilter] = this.GetText("txt" + RegExFilter);
            data[PositiveRegExFilter] = this.GetText("txt" + PositiveRegExFilter);
            data[UserNameAttributeName] = this.GetText("txt" + UserNameAttributeName);
            data[ComputerNameAttributeName] = this.GetText("txt" + ComputerNameAttributeName);
            data[TargetReferenceAttributeName] = this.GetText("txt" + TargetReferenceAttributeName);
            data[Actor] = this.GetActorDropDownList("txt" + Actor);
            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            Debugging.Log("RestoreSettings");
            if (null != data)
            {
                this.SetText("txt" + RegExFilter, (string)data[RegExFilter]);
                this.SetText("txt" + PositiveRegExFilter, (string)data[PositiveRegExFilter]);
                this.SetText("txt" + UserNameAttributeName, (string)data[UserNameAttributeName]);
                this.SetText("txt" + ComputerNameAttributeName, (string)data[ComputerNameAttributeName]);
                this.SetText("txt" + TargetReferenceAttributeName, (string)data[TargetReferenceAttributeName]);
                this.SetActorDropDownList("txt" + Actor, (string)data[Actor]);
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            Debugging.Log("SwitchMode");
            bool isDisabled = (mode != ActivitySettingsPartMode.Edit);
            this.SetTextBoxReadOnlyOption("txt" + RegExFilter, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + PositiveRegExFilter, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + UserNameAttributeName, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + ComputerNameAttributeName, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + TargetReferenceAttributeName, isDisabled);
            this.SetDropDownListDisabled("txt" + Actor, isDisabled);
        }

        public override string Title
        {
            get { return "Update Reference on regular expression match"; }
        }

        public override bool ValidateInputs()
        {
            Debugging.Log("ValidateInputs");
            return true;
        }
    }
}
