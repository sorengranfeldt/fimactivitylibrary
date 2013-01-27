// January 17, 2013 | Soren Granfeldt
//  - initial version
// January 22, 2013 | Soren Granfeldt
//  - added additional code to handle comparison of date, booleans and reference types

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
using Microsoft.ResourceManagement.Workflow.Activities;
using System.Text.RegularExpressions;

namespace Granfeldt.FIM.ActivityLibrary
{
    public partial class UpdateSingleValueAttributeAsNeededActivity : SequenceActivity
    {

        #region Properties

        public static DependencyProperty NewValueProperty = DependencyProperty.Register("NewValue", typeof(object), typeof(UpdateSingleValueAttributeAsNeededActivity));
        [Description("New value to write (or null for delete)")]
        [Category("NewValue Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object NewValue
        {
            get
            {
                return ((object)(base.GetValue(UpdateSingleValueAttributeAsNeededActivity.NewValueProperty)));
            }
            set
            {
                base.SetValue(UpdateSingleValueAttributeAsNeededActivity.NewValueProperty, value);
            }
        }

        public static DependencyProperty AttributeNameProperty = DependencyProperty.Register("AttributeName", typeof(string), typeof(UpdateSingleValueAttributeAsNeededActivity));
        [Description("Name of attribute to update")]
        [Category("AttributeName Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AttributeName
        {
            get
            {
                return ((string)(base.GetValue(UpdateSingleValueAttributeAsNeededActivity.AttributeNameProperty)));
            }
            set
            {
                base.SetValue(UpdateSingleValueAttributeAsNeededActivity.AttributeNameProperty, value);
            }
        }

        public static DependencyProperty TargetIdProperty = DependencyProperty.Register("TargetId", typeof(System.Guid), typeof(Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity));
        [Description("The ID of the object to update")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public Guid TargetId
        {
            get
            {
                return ((System.Guid)(base.GetValue(Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity.TargetIdProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity.TargetIdProperty, value);
            }
        }

        public static DependencyProperty TargetResourceProperty = DependencyProperty.Register("TargetResource", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType), typeof(Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity));
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Parameters")]
        public ResourceType TargetResource
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType)(base.GetValue(Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity.TargetResourceProperty)));
            }
            set
            {
                base.SetValue(Granfeldt.FIM.ActivityLibrary.UpdateSingleValueAttributeAsNeededActivity.TargetResourceProperty, value);
            }
        }

        public static DependencyProperty ActorIdProperty = DependencyProperty.Register("ActorId", typeof(System.Guid), typeof(UpdateSingleValueAttributeAsNeededActivity));
        [Description("ID of the actor updating the target object")]
        [Category("ActorId Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public System.Guid ActorId
        {
            get
            {
                return ((System.Guid)(base.GetValue(UpdateSingleValueAttributeAsNeededActivity.ActorIdProperty)));
            }
            set
            {
                base.SetValue(UpdateSingleValueAttributeAsNeededActivity.ActorIdProperty, value);
            }
        }

        #endregion

        public UpdateSingleValueAttributeAsNeededActivity()
        {
            InitializeComponent();
        }

        private void TargetUpdateNeeded_Condition(object sender, ConditionalEventArgs e)
        {
            List<UpdateRequestParameter> updateParameters = new List<UpdateRequestParameter>();

            e.Result = false;
            object CurrentValue = TargetResource[this.AttributeName];

            object convertedSourceValue = FIMAttributeUtilities.FormatValue(CurrentValue);
            object convertedNewValue = FIMAttributeUtilities.FormatValue(this.NewValue);

            if (FIMAttributeUtilities.ValuesAreDifferent(convertedSourceValue, convertedNewValue))
            {
                e.Result = true;

                // if the new value is null then remove current value.
                // otherwise, update attribute to new value
                updateParameters.Add(new UpdateRequestParameter(this.AttributeName, this.NewValue == null ? UpdateMode.Remove : UpdateMode.Modify, this.NewValue == null ? CurrentValue : convertedNewValue));

                UpdateResource.ActorId = this.ActorId;
                UpdateResource.ResourceId = this.TargetId;
                UpdateResource.UpdateParameters = updateParameters.ToArray();
                if (this.NewValue == null)
                {
                    Debugging.Log(string.Format("Removing existing value '{0}' from {1}", CurrentValue, this.AttributeName));
                }
                else
                {
                    Debugging.Log(string.Format("Updating {0} from '{1}' to '{2}'", this.AttributeName, CurrentValue == null ? "(null)" : CurrentValue, this.NewValue == null ? "(null)" : this.NewValue));
                }
            }
            else
            {
                Debugging.Log(string.Format("No need to update {0}. Value is already '{1}'", this.AttributeName, convertedNewValue));
            }

        }

        private void SetupReadTarget_ExecuteCode(object sender, EventArgs e)
        {
            ReadResource.ActorId = this.ActorId;
            ReadResource.ResourceId = this.TargetId;
            ReadResource.SelectionAttributes = new string[] { this.AttributeName };
        }

    }
}
