using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace Granfeldt.FIM.ActivityLibrary
{
    [Designer(typeof(ActivityDesigner), typeof(IDesigner))]
    public partial class UpdateReferenceAttributesAsNeededActivity: SequenceActivity
    {
        public UpdateReferenceAttributesAsNeededActivity()
        {
            InitializeComponent();
        }

        public static DependencyProperty ActorIdProperty = DependencyProperty.Register("ActorId", typeof(System.Guid), typeof(UpdateReferenceAttributesAsNeededActivity));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("ActorId Category")]
        public Guid ActorId
        {
            get
            {
                return ((System.Guid)(base.GetValue(UpdateReferenceAttributesAsNeededActivity.ActorIdProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributesAsNeededActivity.ActorIdProperty, value);
            }
        }

        public static DependencyProperty ResourceIdProperty = DependencyProperty.Register("ResourceId", typeof(System.Guid), typeof(UpdateReferenceAttributesAsNeededActivity));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("ResourceId Category")]
        public Guid ResourceId
        {
            get
            {
                return ((System.Guid)(base.GetValue(UpdateReferenceAttributesAsNeededActivity.ResourceIdProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributesAsNeededActivity.ResourceIdProperty, value);
            }
        }

        public static DependencyProperty UpdateModeProperty = DependencyProperty.Register("UpdateMode", typeof(Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode), typeof(UpdateReferenceAttributesAsNeededActivity));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("UpdateMode Category")]
        public Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode UpdateMode
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode)(base.GetValue(UpdateReferenceAttributesAsNeededActivity.UpdateModeProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributesAsNeededActivity.UpdateModeProperty, value);
            }
        }

        public static DependencyProperty ValuesProperty = DependencyProperty.Register("Values", typeof(System.Guid[]), typeof(UpdateReferenceAttributesAsNeededActivity));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Values Category")]
        public Guid[] Values
        {
            get
            {
                return ((System.Guid[])(base.GetValue(UpdateReferenceAttributesAsNeededActivity.ValuesProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributesAsNeededActivity.ValuesProperty, value);
            }
        }

        public static DependencyProperty AttributeNameProperty = DependencyProperty.Register("AttributeName", typeof(System.String), typeof(UpdateReferenceAttributesAsNeededActivity));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("AttributeName Category")]
        public String AttributeName
        {
            get
            {
                return ((string)(base.GetValue(UpdateReferenceAttributesAsNeededActivity.AttributeNameProperty)));
            }
            set
            {
                base.SetValue(UpdateReferenceAttributesAsNeededActivity.AttributeNameProperty, value);
            }
        }

        public Guid[] ValuesToSet = default(System.Guid[]);

        private void FindOutIfThereIsAPointInDoingAnything(object sender, ConditionalEventArgs e)
        {
            if(null==this.Values || this.Values.Length==0)
            {
                e.Result = false;
                return;
            }
            switch (this.UpdateMode)
            {
                case  Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode.Insert:
                    this.ValuesToSet = this.Values.Except(this.CurrentValues).ToArray();
                    e.Result = true;
                    break;
                case Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode.Modify:
                    if (this.CurrentValues.Except(this.Values).Count()>0 ||
                        this.Values.Except(this.CurrentValues).Count()>0)
                    {
                        this.ValuesToSet = this.Values;
                        e.Result = true;
          
                    }
                    else
                    {
                        e.Result = false;
                    }
                    break;
                case Microsoft.ResourceManagement.WebServices.WSResourceManagement.UpdateMode.Remove:
                    this.ValuesToSet = this.Values.Intersect(this.CurrentValues).ToArray();
                    e.Result = true;
                    break;               
            } 
        }

        public Guid[] CurrentValues = default(System.Guid[]);
    }
}