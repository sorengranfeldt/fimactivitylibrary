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
    class ReconcileSoftwareGroupMembersActivitySettingsPart : BaseActivitySettingsPart
    {

        const string UserMembersAttributeName = "UserMembersAttributeName";
        const string ComputerMembersAttributeName = "ComputerMembersAttributeName";

        protected override void CreateChildControls()
        {
            Table layoutTable;
            layoutTable = new Table();

            // width is set to 100% of the control size
            layoutTable.Width = Unit.Percentage(100.0);
            layoutTable.BorderWidth = 0;
            layoutTable.CellPadding = 2;

            // add a TableRow for each textbox in the UI
            layoutTable.Rows.Add(this.AddTableRowTextBox("User members attributename:", "txt" + UserMembersAttributeName, 400, 100, false, "ExplicitMemberComputerOwners"));
            layoutTable.Rows.Add(this.AddTableRowTextBox("Computer members attributename:", "txt" + ComputerMembersAttributeName, 400, 100, false, "ExplicitMember"));
            this.Controls.Add(layoutTable);
            base.CreateChildControls();
        }

        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            if (!this.ValidateInputs())
            {
                return null;
            }
            ReconcileSoftwareGroupMembersActivity act = new ReconcileSoftwareGroupMembersActivity();
            act.ComputerMembersAttributeName = this.GetText("txt" + ComputerMembersAttributeName);
            act.UserMembersAttributeName = this.GetText("txt" + UserMembersAttributeName);
            return act;
        }

        public override void LoadActivitySettings(Activity activity)
        {
            ReconcileSoftwareGroupMembersActivity thisActivity = activity as ReconcileSoftwareGroupMembersActivity;
            if (thisActivity != null)
            {
                this.SetText("txt" + UserMembersAttributeName, thisActivity.UserMembersAttributeName);
                this.SetText("txt" + ComputerMembersAttributeName, thisActivity.ComputerMembersAttributeName);
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data[UserMembersAttributeName] = this.GetText("txt" + UserMembersAttributeName);
            data[ComputerMembersAttributeName] = this.GetText("txt" + ComputerMembersAttributeName);
            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            if (null != data)
            {
                this.SetText("txt" + UserMembersAttributeName, (string)data[UserMembersAttributeName]);
                this.SetText("txt" + ComputerMembersAttributeName, (string)data[ComputerMembersAttributeName]);
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            bool isDisabled = (mode != ActivitySettingsPartMode.Edit);
            this.SetTextBoxReadOnlyOption("txt" + UserMembersAttributeName, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + ComputerMembersAttributeName, isDisabled);
        }

        public override string Title
        {
            get { return "Reconcile software group members"; }
        }

        public override bool ValidateInputs()
        {
            return true;
        }
    }
}
