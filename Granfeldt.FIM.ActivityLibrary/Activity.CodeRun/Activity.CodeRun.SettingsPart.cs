// January 15, 2013 | Soren Granfeldt (soren@granfeldt.dk)
//  - initial version

using System.Web.UI.WebControls;
using System.Workflow.ComponentModel;
using Microsoft.IdentityManagement.WebUI.Controls;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary.WebUIs
{
    class CodeRunActivitySettingsPart : BaseActivitySettingsPart
    {

        const string InstanceTitle = "Title";
        const string References = "References";
        const string Parameters = "Parameters";
        const string Code = "Code";
        const string Destination = "Destination";

        protected override void CreateChildControls()
        {
            Table controlLayoutTable;
            controlLayoutTable = new Table();

            // width is set to 100% of the control size
            controlLayoutTable.Width = Unit.Percentage(100.0);
            controlLayoutTable.BorderWidth = 0;
            controlLayoutTable.CellPadding = 2;
            // add a TableRow for each textbox in the UI 
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Title:", "txt" + InstanceTitle, 400, 100, false, ""));
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("References (DLL's):<br/><i>(please note that System.dll is added by default)</i>", "txt" + References, 400, 4000, true, "System.dll"));
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Parameters:<br/><i>(parameters are passed to the FIMDynamicFunction method in the order shown. Therefore, you should make sure that the function FIMDynamicFunction accepts the correct number of parameters and possible types)</i>", "txt" + Parameters, 400, 4000, true, ""));
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Code:<br/><i>(code must always contain a class named FIMDynamicClass with a function named FIMDynamicFunction)</i>", "txt" + Code, 400, 4000, true, ""));
            controlLayoutTable.Rows.Add(this.AddTableRowTextBox("Destination:", "txt" + Destination, 400, 100, false, ""));
            this.Controls.Add(controlLayoutTable);
            base.CreateChildControls();
        }

        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            if (!this.ValidateInputs())
            {
                return null;
            }
            CodeRunActivity ThisActivity = new CodeRunActivity();
            ThisActivity.Title = this.GetText("txt" + InstanceTitle);
            ThisActivity.References = this.GetTextArray("txt" + References);
            ThisActivity.Parameters = this.GetTextArray("txt" + Parameters);
            ThisActivity.Code = this.GetText("txt" + Code);
            ThisActivity.Destination = this.GetText("txt" + Destination);

            return ThisActivity;
        }

        public override void LoadActivitySettings(Activity activity)
        {
            CodeRunActivity ThisActivity = activity as CodeRunActivity;
            if (ThisActivity != null)
            {
                this.SetText("txt" + InstanceTitle, ThisActivity.Title);
                this.SetTextArray("txt" + References, ThisActivity.References);
                this.SetTextArray("txt" + Parameters, ThisActivity.Parameters);
                this.SetText("txt" + Code, ThisActivity.Code);
                this.SetText("txt" + Destination, ThisActivity.Destination);
            }
        }

        public override ActivitySettingsPartData PersistSettings()
        {
            ActivitySettingsPartData data = new ActivitySettingsPartData();
            data[InstanceTitle] = this.GetText("txt" + InstanceTitle);
            data[References] = this.GetTextArray("txt" + References);
            data[Parameters] = this.GetTextArray("txt" + Parameters);
            data[Code] = this.GetText("txt" + Code);
            data[Destination] = this.GetText("txt" + Destination);
            return data;
        }

        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            if (null != data)
            {
                this.SetText("txt" + InstanceTitle, (string)data[InstanceTitle]);
                this.SetTextArray("txt" + References, (string[])data[References]);
                this.SetTextArray("txt" + Parameters, (string[])data[Parameters]);
                this.SetText("txt" + Code, (string)data[Code]);
                this.SetText("txt" + Destination, (string)data[Destination]);
            }
        }

        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            bool readOnly = (mode == ActivitySettingsPartMode.View);
            this.SetTextBoxReadOnlyOption("txt" + InstanceTitle, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + References, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + Parameters, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + Code, readOnly);
            this.SetTextBoxReadOnlyOption("txt" + Destination, readOnly);
        }

        public override string Title
        {
            get
            {
                return string.Format("Code Run: {0}", this.GetText("txt" + InstanceTitle));
            }
        }

        public override bool ValidateInputs()
        {
            return true;
        }

    }
}
