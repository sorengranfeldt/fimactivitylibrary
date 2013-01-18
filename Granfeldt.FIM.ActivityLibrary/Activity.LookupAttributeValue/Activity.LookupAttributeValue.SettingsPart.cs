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
    class LookupAttributeValueActivitySettingsPart : BaseActivitySettingsPart
    {

        const string XPathFilter = "XPathFilter";
        const string AttributeToRetrieve = "AttributeToRetrieve";
        const string Destination = "Destination";
        const string LookupActor = "LookupActor";
        const string NonUniqueResultAction = "NonUniqueResultAction";

        protected override void CreateChildControls()
        {
            Table layoutTable;
            layoutTable = new Table();

            // width is set to 100% of the control size
            layoutTable.Width = Unit.Percentage(100.0);
            layoutTable.BorderWidth = 0;
            layoutTable.CellPadding = 2;

            // add a TableRow for each textbox in the UI
            layoutTable.Rows.Add(this.AddTableRowTextBox("XPath Filter:", "txt" + XPathFilter, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddTableRowTextBox("Attribute Name:", "txt" + AttributeToRetrieve, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddTableRowTextBox("Destination:", "txt" + Destination, 400, 100, false, ""));
            layoutTable.Rows.Add(this.AddActorDropDownList("Actor (run as):", "txt" + LookupActor, 400, WellKnownGuids.FIMServiceAccount.ToString()));
            layoutTable.Rows.Add(this.AddLookupActionDropDownList("Action on multiple lookup results:", "txt" + NonUniqueResultAction, 400, ""));
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
            LookupAttributeValueActivity LookupActivity = new LookupAttributeValueActivity();
            LookupActivity.AttributeName = this.GetText("txt" + AttributeToRetrieve);
            LookupActivity.XPathFilter = this.GetText("txt" + XPathFilter);
            LookupActivity.Destination = this.GetText("txt" + Destination);
            LookupActivity.LookupActor = this.GetActorDropDownList("txt" + LookupActor);
            LookupActivity.NonUniqueValueAction = this.GetLookupActionDropDownList("txt" + NonUniqueResultAction);
            return LookupActivity;
        }

        public override void LoadActivitySettings(Activity activity)
        {
            Debugging.Log("LoadActivitySettings");
            LookupAttributeValueActivity thisActivity = activity as LookupAttributeValueActivity;
            if (thisActivity != null)
            {
                this.SetText("txt" + XPathFilter, thisActivity.XPathFilter);
                this.SetText("txt" + AttributeToRetrieve, thisActivity.AttributeName);
                this.SetText("txt" + Destination, thisActivity.Destination);
                this.SetActorDropDownList("txt" + LookupActor, thisActivity.LookupActor);
                this.SetLookupActionDropDownList("txt" + NonUniqueResultAction, thisActivity.NonUniqueValueAction);
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            Debugging.Log("PersistSettings");
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data[XPathFilter] = this.GetText("txt" + XPathFilter);
            data[Destination] = this.GetText("txt" + Destination);
            data[AttributeToRetrieve] = this.GetText("txt" + AttributeToRetrieve);
            data[LookupActor] = this.GetActorDropDownList("txt" + LookupActor);
            data[NonUniqueResultAction] = this.GetLookupActionDropDownList("txt" + NonUniqueResultAction);
            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            Debugging.Log("RestoreSettings");
            if (null != data)
            {
                this.SetText("txt" + XPathFilter, (string)data[XPathFilter]);
                this.SetText("txt" + Destination, (string)data[Destination]);
                this.SetText("txt" + AttributeToRetrieve, (string)data[AttributeToRetrieve]);
                this.SetActorDropDownList("txt" + LookupActor, (string)data[LookupActor]);
                this.SetLookupActionDropDownList("txt" + NonUniqueResultAction, (string)data[NonUniqueResultAction]);
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            Debugging.Log("SwitchMode");
            bool isDisabled = (mode != ActivitySettingsPartMode.Edit);
            this.SetTextBoxReadOnlyOption("txt" + XPathFilter, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + Destination, isDisabled);
            this.SetTextBoxReadOnlyOption("txt" + AttributeToRetrieve, isDisabled);
            this.SetDropDownListDisabled("txt" + LookupActor, isDisabled);
            this.SetDropDownListDisabled("txt" + NonUniqueResultAction, isDisabled);
        }

        public override string Title
        {
            get { return "Lookup Value"; }
        }

        public override bool ValidateInputs()
        {
            Debugging.Log("ValidateInputs");
            return true;
        }
    }
}
