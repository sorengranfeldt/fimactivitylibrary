using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Workflow.ComponentModel;
using Microsoft.IdentityManagement.WebUI.Controls;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{
    class BaseActivitySettingsPart : ActivitySettingsPart
    {

        protected TableRow AddTableRowTextBox(String labelText, String controlID, int width, int maxLength, Boolean multiLine, String defaultValue)
        {
            TableRow row = new TableRow();
            TableCell labelCell = new TableCell();
            TableCell controlCell = new TableCell();
            Label oLabel = new Label();
            TextBox oText = new TextBox();

            oLabel.Text = labelText;
            oLabel.CssClass = base.LabelCssClass;
            labelCell.Controls.Add(oLabel);
            oText.ID = controlID;
            oText.CssClass = base.TextBoxCssClass;
            oText.Text = defaultValue;
            oText.MaxLength = maxLength;
            oText.Width = width;
            if (multiLine)
            {
                oText.TextMode = TextBoxMode.MultiLine;
                oText.Rows = System.Math.Min(6, (maxLength + 60) / 60);
                oText.Wrap = true;
            }
            controlCell.Controls.Add(oText);
            row.Cells.Add(labelCell);
            row.Cells.Add(controlCell);
            return row;
        }

        protected void SetDropdownSelectedValue(string dropDownListID, string value)
        {
            DropDownList ddl = (DropDownList)this.FindControl(dropDownListID);
            if (ddl != null)
            {
                if (!string.IsNullOrEmpty(value))
                    ddl.SelectedValue = value;
            }
            else
                Debugging.Log("Cannot find control with ID '" + dropDownListID + "'");
        }

        protected TableRow AddAddRemoveDropDownList(string labelText, string controlID, int width, string defaultValue)
        {
            TableRow row = new TableRow();
            TableCell labelCell = new TableCell();
            TableCell controlCell = new TableCell();

            Label oLabel = new Label();
            oLabel.Text = labelText;
            oLabel.CssClass = base.LabelCssClass;
            labelCell.Controls.Add(oLabel);

            DropDownList ddl = new DropDownList();
            ddl.ID = controlID;
            ddl.Width = width;

            ddl.Items.Add(new ListItem("Add", "Add")); 
            ddl.Items.Add(new ListItem("Remove", "Remove"));
            ddl.SelectedValue = defaultValue;
            controlCell.Controls.Add(ddl);

            row.Cells.Add(labelCell);
            row.Cells.Add(controlCell);

            return row;
        }
        protected void SetAddRemoveDropDownList(string dropDownListID, string value)
        {
            SetDropdownSelectedValue(dropDownListID, value);
        }
        protected string GetAddRemoveDropDownList(string dropDownListID)
        {
            string g = "";
            DropDownList ddl = (DropDownList)this.FindControl(dropDownListID);
            if (ddl != null)
            {
                if (!string.IsNullOrEmpty(ddl.SelectedValue))
                    g = ddl.SelectedValue.ToString();
            }
            else
                Debugging.Log("Cannot find control with ID '" + dropDownListID + "'");
            return g;
        }
        protected TableRow AddLookupActionDropDownList(String labelText, String controlID, int width, String defaultValue)
        {
            TableRow row = new TableRow();
            TableCell labelCell = new TableCell();
            TableCell controlCell = new TableCell();

            // Add label
            Label oLabel = new Label();
            oLabel.Text = labelText;
            oLabel.CssClass = base.LabelCssClass;
            labelCell.Controls.Add(oLabel);

            DropDownList ddl = new DropDownList();
            ddl.ID = controlID;
            ddl.Width = width;

            ddl.Items.Add(new ListItem("Return first element", "")); // default is ''
            ddl.Items.Add(new ListItem("Return last element", "RETURNLAST"));
            ddl.Items.Add(new ListItem("Throw error and stop", "ERROR"));
            ddl.SelectedValue = defaultValue;
            controlCell.Controls.Add(ddl);

            row.Cells.Add(labelCell);
            row.Cells.Add(controlCell);

            return row;
        }

        protected string GetLookupActionDropDownList(string dropDownListID)
        {
            string g = "";
            DropDownList ddl = (DropDownList)this.FindControl(dropDownListID);
            if (ddl != null)
            {
                if (!string.IsNullOrEmpty(ddl.SelectedValue))
                    g = ddl.SelectedValue.ToString();
            }
            else
                Debugging.Log("Cannot find control with ID '" + dropDownListID + "'");
            return g;
        }

        protected void SetLookupActionDropDownList(string dropDownListID, string value)
        {
            SetDropdownSelectedValue(dropDownListID, value);
        }

        protected TableRow AddActorDropDownList(String labelText, String controlID, int width, String defaultValue)
        {
            TableRow row = new TableRow();
            TableCell labelCell = new TableCell();
            TableCell controlCell = new TableCell();

            // Add label
            Label oLabel = new Label();
            oLabel.Text = labelText;
            oLabel.CssClass = base.LabelCssClass;
            labelCell.Controls.Add(oLabel);

            DropDownList ddl = new DropDownList();
            ddl.ID = controlID;
            //ddl.CssClass = base.???cssclass
            ddl.Width = width;
            ListItem BuiltInSynchronizationAccount = new ListItem("Built-in Synchronization Account", WellKnownGuids.BuiltInSynchronizationAccount.ToString());
            ListItem FIMServiceAccount = new ListItem("FIMServiceAccount", WellKnownGuids.FIMServiceAccount.ToString());
            ListItem Anonymous = new ListItem("Anonymous", WellKnownGuids.Anonymous.ToString());

            ddl.Items.Add(BuiltInSynchronizationAccount);
            ddl.Items.Add(FIMServiceAccount);
            ddl.Items.Add(Anonymous);
            ddl.SelectedValue = defaultValue;
            controlCell.Controls.Add(ddl);

            row.Cells.Add(labelCell);
            row.Cells.Add(controlCell);

            return row;
        }

        protected string GetActorDropDownList(string dropDownListID)
        {
            string g = WellKnownGuids.FIMServiceAccount.ToString();
            DropDownList ddl = (DropDownList)this.FindControl(dropDownListID);
            if (ddl != null)
            {
                if (!string.IsNullOrEmpty(ddl.SelectedValue))
                    g = ddl.SelectedValue.ToString();
            }
            else
                Debugging.Log("Cannot find control with ID '" + dropDownListID + "'");
            return g;
        }

        protected void SetActorDropDownList(string dropDownListID, object value)
        {
            SetDropdownSelectedValue(dropDownListID, value.ToString());
        }

        protected TableRow AddCheckbox(String labelText, String controlID, bool defaultValue)
        {
            TableRow row = new TableRow();
            TableCell labelCell = new TableCell();
            TableCell controlCell = new TableCell();

            // Add label
            Label oLabel = new Label();
            oLabel.Text = labelText;
            oLabel.CssClass = base.LabelCssClass;
            labelCell.Controls.Add(oLabel);

            CheckBox cb = new CheckBox();
            cb.ID = controlID;
            cb.Checked = defaultValue;
            controlCell.Controls.Add(cb);

            row.Cells.Add(labelCell);
            row.Cells.Add(controlCell);

            return row;
        }

        protected bool GetCheckbox(string checkBoxID)
        {
            CheckBox checkBox = (CheckBox)this.FindControl(checkBoxID);
            if (checkBox != null)
                return checkBox.Checked;
            else
                return false;
        }

        protected void SetCheckbox(string checkBoxID, bool isChecked)
        {
            CheckBox checkBox = (CheckBox)this.FindControl(checkBoxID);
            if (checkBox != null)
            {
                checkBox.Checked = isChecked;
            }
        }

        protected string GetText(string textBoxID)
        {
            TextBox textBox = (TextBox)this.FindControl(textBoxID);
            return textBox.Text ?? String.Empty;
        }

        protected void SetText(string textBoxID, string text)
        {
            TextBox textBox = (TextBox)this.FindControl(textBoxID);
            if (textBox != null)
                textBox.Text = text;
            else
                textBox.Text = "";
        }

        protected string[] GetTextArray(string textBoxId)
        {
            TextBox textBox = (TextBox)this.FindControl(textBoxId);
            string[] words = textBox.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return words ?? null;
        }

        protected void SetTextArray(string textBoxID, string[] text)
        {
            TextBox textBox = (TextBox)this.FindControl(textBoxID);
            StringBuilder myBuilder = new StringBuilder();
            foreach (string s in text)
            {
                if (!String.IsNullOrEmpty(s))
                    myBuilder.AppendLine(s);
            }
            string toInsert = myBuilder.ToString().TrimEnd('\r', '\n');
            textBox.Text = toInsert;
        }

        //Set the text box to read mode or read/write mode
        protected void SetCheckboxReadOnlyOption(string CheckBoxID, bool readOnly)
        {
            CheckBox checkBox = (CheckBox)this.FindControl(CheckBoxID);
            checkBox.Enabled = !readOnly;
        }

        //Set the check box to read mode or read/write mode
        protected void SetTextBoxReadOnlyOption(string textBoxID, bool readOnly)
        {
            TextBox textBox = (TextBox)this.FindControl(textBoxID);
            textBox.Enabled = !readOnly;
        }

        protected void SetDropDownListDisabled(string dropDownListID, bool disabled)
        {
            DropDownList ddl = (DropDownList)this.FindControl(dropDownListID);
            if (ddl != null)
            {
                ddl.Enabled = !disabled;
            }
            else
                Debugging.Log("Cannot find control with ID '" + dropDownListID + "'");
        }


        /// <summary>
        /// Called when a user clicks the Save button in the Workflow Designer. 
        /// Returns an instance of the RequestLoggingActivity class that 
        /// has its properties set to the values entered into the text box controls
        /// used in the UI of the activity. 
        /// </summary>
        public override Activity GenerateActivityOnWorkflow(SequentialWorkflow workflow)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called by FIM when the UI for the activity must be reloaded.
        /// It passes us an instance of our workflow activity so that we can
        /// extract the values of the properties to display in the UI.
        /// </summary>
        public override void LoadActivitySettings(Activity activity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the activity settings.
        /// </summary>
        public override ActivitySettingsPartData PersistSettings()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Restores the activity settings in the UI
        /// </summary>
        public override void RestoreSettings(ActivitySettingsPartData data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Switches the activity between read only and read/write mode
        /// </summary>
        public override void SwitchMode(ActivitySettingsPartMode mode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Returns the activity name.
        /// </summary>
        public override string Title
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///  In general, this method should be used to validate information entered
        ///  by the user when the activity is added to a workflow in the Workflow
        ///  Designer.
        ///  We could add code to verify that the log file path already exists on
        ///  the server that is hosting the FIM Portal and check that the activity
        ///  has permission to write to that location. However, the code
        ///  would only check if the log file path exists when the
        ///  activity is added to a workflow in the workflow designer. This class
        ///  will not be used when the activity is actually run.
        ///  For this activity we will just return true.
        /// </summary>
        public override bool ValidateInputs()
        {
            throw new NotImplementedException();
        }
    }
}
