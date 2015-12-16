using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{
    [Designer(typeof(ActivityDesigner), typeof(IDesigner))]
    public partial class UpdateReferenceAttributeActivity : SequenceActivity
    {
        #region Properties
        public static DependencyProperty ActorIdProperty = DependencyProperty.Register("ActorId", typeof(Guid), typeof(UpdateReferenceAttributeActivity));

        [Description("ActorId")]
        [Category("ActorId Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ActorId
        {
            get
            {
                return ((Guid)(base.GetValue(UpdateReferenceAttributeActivity.ActorIdProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributeActivity.ActorIdProperty, value);
            }
        }

        public static DependencyProperty ResourceIdProperty = DependencyProperty.Register("ResourceId", typeof(Guid), typeof(UpdateReferenceAttributeActivity));

        [Description("ResourceId")]
        [Category("ResourceId Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ResourceId
        {
            get
            {
                return ((Guid)(base.GetValue(UpdateReferenceAttributeActivity.ResourceIdProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributeActivity.ResourceIdProperty, value);
            }
        }

        public static DependencyProperty ValuesProperty = DependencyProperty.Register("Values", typeof(Guid[]), typeof(UpdateReferenceAttributeActivity));

        public static DependencyProperty AttributeNameProperty = DependencyProperty.Register("AttributeName", typeof(string), typeof(UpdateReferenceAttributeActivity));

        [Description("AttributeName")]
        [Category("AttributeName Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AttributeName
        {
            get
            {
                return ((string)(base.GetValue(UpdateReferenceAttributeActivity.AttributeNameProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributeActivity.AttributeNameProperty, value);
            }
        }

        [Description("Values")]
        [Category("Values Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid[] Values
        {
            get
            {
                return ((Guid[])(base.GetValue(UpdateReferenceAttributeActivity.ValuesProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributeActivity.ValuesProperty, value);
            }
        }

        public static DependencyProperty UpdateModeProperty = DependencyProperty.Register("UpdateMode", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode), typeof(UpdateReferenceAttributeActivity));

        [Description("UpdateMode")]
        [Category("UpdateMode Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public UpdateMode UpdateMode
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode)(base.GetValue(UpdateReferenceAttributeActivity.UpdateModeProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributeActivity.UpdateModeProperty, value);
            }
        }
        #endregion

        public UpdateReferenceAttributeActivity()
        {
            InitializeComponent();
        }

        private void PrepareUpdate_ExecuteCode(object sender, EventArgs e)
        {
            List<UpdateRequestParameter> tempParameters = new List<UpdateRequestParameter>();
            foreach (Guid guid in this.Values)
            {
                tempParameters.Add(new UpdateRequestParameter(this.AttributeName, this.UpdateMode, guid));
            }

            this.UpdateParameters = tempParameters.ToArray();
        }

        public UpdateRequestParameter[] UpdateParameters = null;

        private void AreValuesPresent(object sender, ConditionalEventArgs e)
        {
            if (this.Values != null && this.Values.Length > 0)
                e.Result = true;
            else
                e.Result = false;
        }
    }
}