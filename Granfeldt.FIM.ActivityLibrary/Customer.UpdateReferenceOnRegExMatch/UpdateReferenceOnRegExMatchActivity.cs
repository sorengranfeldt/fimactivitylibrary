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
using System.Text.RegularExpressions;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{

    public partial class UpdateReferenceOnRegExMatchActivity : SequenceActivity
    {


        #region Properties

        public static DependencyProperty CurrentRequestProperty = DependencyProperty.Register("CurrentRequest", typeof(RequestType), typeof(UpdateReferenceOnRegExMatchActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public RequestType CurrentRequest
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.RequestType)(base.GetValue(Granfeldt.FIM.ActivityLibrary.UpdateReferenceOnRegExMatchActivity.CurrentRequestProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.UpdateReferenceOnRegExMatchActivity.CurrentRequestProperty, value);
            }
        }

        // ^w8(ab|fe)pc|[1|2|3]*[w|t]$
        public static DependencyProperty RegExFilterProperty = DependencyProperty.Register("RegExFilter", typeof(string), typeof(UpdateReferenceOnRegExMatchActivity));
        [Description("RegExFilter")]
        [Category("RegExFilter Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RegExFilter
        {
            get
            {
                return ((string)(base.GetValue(UpdateReferenceOnRegExMatchActivity.RegExFilterProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceOnRegExMatchActivity.RegExFilterProperty, value);
            }
        }

        public static DependencyProperty PositiveRegExFilterProperty = DependencyProperty.Register("PositiveRegExFilter", typeof(string), typeof(UpdateReferenceOnRegExMatchActivity));
        [Description("PositiveRegExFilter")]
        [Category("PositiveRegExFilter Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PositiveRegExFilter
        {
            get
            {
                return ((string)(base.GetValue(UpdateReferenceOnRegExMatchActivity.PositiveRegExFilterProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceOnRegExMatchActivity.PositiveRegExFilterProperty, value);
            }
        }

        public static DependencyProperty ComputerNameAttributeNameProperty = DependencyProperty.Register("ComputerNameAttributeName", typeof(string), typeof(UpdateReferenceOnRegExMatchActivity));
        [Description("ComputerNameAttributeName")]
        [Category("ComputerNameAttributeName Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ComputerNameAttributeName
        {
            get
            {
                return ((string)(base.GetValue(UpdateReferenceOnRegExMatchActivity.ComputerNameAttributeNameProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceOnRegExMatchActivity.ComputerNameAttributeNameProperty, value);
            }
        }

        public static DependencyProperty UserNameAttributeNameProperty = DependencyProperty.Register("UserNameAttributeName", typeof(string), typeof(UpdateReferenceOnRegExMatchActivity));
        [Description("UserNameAttributeName")]
        [Category("UserNameAttribute Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string UserNameAttributeName
        {
            get
            {
                return ((string)(base.GetValue(UpdateReferenceOnRegExMatchActivity.UserNameAttributeNameProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceOnRegExMatchActivity.UserNameAttributeNameProperty, value);
            }
        }

        public static DependencyProperty TargetReferenceAttributeNameProperty = DependencyProperty.Register("TargetReferenceAttributeName", typeof(string), typeof(UpdateReferenceOnRegExMatchActivity));
        [Description("TargetReferenceAttributeName")]
        [Category("TargetReferenceAttributeName Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TargetReferenceAttributeName
        {
            get
            {
                return ((string)(base.GetValue(UpdateReferenceOnRegExMatchActivity.TargetReferenceAttributeNameProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceOnRegExMatchActivity.TargetReferenceAttributeNameProperty, value);
            }
        }

        public static DependencyProperty TargetResourceProperty = DependencyProperty.Register("TargetResource", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType), typeof(Granfeldt.FIM.ActivityLibrary.UpdateReferenceOnRegExMatchActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public ResourceType TargetResource
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType)(base.GetValue(Granfeldt.FIM.ActivityLibrary.UpdateReferenceOnRegExMatchActivity.TargetResourceProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.UpdateReferenceOnRegExMatchActivity.TargetResourceProperty, value);
            }
        }

        public static DependencyProperty ActorProperty = DependencyProperty.Register("Actor", typeof(string), typeof(UpdateReferenceOnRegExMatchActivity));
        [Description("Actor")]
        [Category("Actor Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Actor
        {
            get
            {
                return ((string)(base.GetValue(UpdateReferenceOnRegExMatchActivity.ActorProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceOnRegExMatchActivity.ActorProperty, value);
            }
        }

        #endregion

        public UpdateReferenceOnRegExMatchActivity()
        {
            InitializeComponent();
        }

        private void PrepareReadTargetCode(object sender, EventArgs e)
        {
            Debugging.Log("Enter PrepareReadTargetCode");
            Debugging.Log("Actor", this.Actor);
            Debugging.Log("Computername attribute", this.ComputerNameAttributeName);
            Debugging.Log("Usernamename attribute", this.UserNameAttributeName);
            Debugging.Log("Request targetid", CurrentRequest.Target.GetGuid());

            ReadTarget.ActorId = new Guid(this.Actor);
            ReadTarget.SelectionAttributes = new string[] { this.ComputerNameAttributeName, this.UserNameAttributeName };
            ReadTarget.ResourceId = CurrentRequest.Target.GetGuid();
            ReadTarget.StatusChanged += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(Activity_StatusChanged);

            Debugging.Log("Exit PrepareReadTargetCode");
        }

        void Activity_StatusChanged(object sender, ActivityExecutionStatusChangedEventArgs e)
        {
            Debugging.Log(string.Format("{0} {1} with result: {2}", e.ExecutionStatus, e.Activity.Name, e.ExecutionResult));
        }


        private void IfComputerNameContainsUsername(object sender, ConditionalEventArgs e)
        {
            Debugging.Log("Enter IfComputerNameContainsUsername");

            // always setup defaults for updating
            UpdateUserReference.ActorId = new Guid(this.Actor);
            UpdateUserReference.AttributeName = this.TargetReferenceAttributeName;
            UpdateUserReference.TargetId = CurrentRequest.Target.GetGuid();
            UpdateUserReference.NewValue = null;
            UpdateUserReference.StatusChanged += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(Activity_StatusChanged);

            if (ReadTarget.Resource == null)
            {
                Debugging.Log("Could not read request target");
                e.Result = false;
                return;
            }

            object computername = ReadTarget.Resource[this.ComputerNameAttributeName];
            object username = ReadTarget.Resource[this.UserNameAttributeName];

            Debugging.Log("Computername", computername);
            Debugging.Log("Username", username);

            e.Result = false;
            if ((username == null) || (computername == null))
            {
                Debugging.Log(string.Format("{0} or {1} is null. No match", this.ComputerNameAttributeName, this.UserNameAttributeName));
                return;
            }

            string computerusername = Regex.Replace(computername as string, this.RegExFilter, "", RegexOptions.IgnoreCase);
            Debugging.Log("Regex trimmed computername", computerusername);
            if ((computerusername.Equals(username as string, StringComparison.OrdinalIgnoreCase)) || (Regex.IsMatch(computername as string, this.PositiveRegExFilter, RegexOptions.IgnoreCase)))
            {
                Debugging.Log("We got a match; doing lookup for user", username);
                e.Result = true;

                // set up lookup code
                FindUser.ActorId = WellKnownGuids.FIMServiceAccount;
                FindUser.Attributes = new string[] { "ObjectID" };
                FindUser.XPathFilter = string.Format("/Person[AccountName='{0}']", username);
                FindUser.PageSize = 100;
                FindUser.StatusChanged += new System.EventHandler<ActivityExecutionStatusChangedEventArgs>(Activity_StatusChanged);
                return;
            }
            Debugging.Log("Exit IfComputerNameContainsUsername");
        }

        private void PrepareUpdateReferenceCode(object sender, EventArgs e)
        {
            Debugging.Log("Enter PrepareUpdateReferenceCode");
            if (FindUser.TotalResultsCount == 1)
            {
                ResourceType r;
                r = FindUser.EnumeratedResources[0];
                UpdateUserReference.NewValue = r["ObjectID"];
            }
            Debugging.Log("Exit PrepareUpdateReferenceCode");
        }

        private void PrepareClearingReferenceCode(object sender, EventArgs e)
        {
            Debugging.Log("Enter PrepareClearingReferenceCode");
            // all setup for clearing is done in IfComputerNameContainsUsername
            Debugging.Log("Exit PrepareClearingReferenceCode");
        }

    }

}
