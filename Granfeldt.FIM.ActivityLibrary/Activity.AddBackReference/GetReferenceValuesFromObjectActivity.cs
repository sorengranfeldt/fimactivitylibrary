using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace Granfeldt.FIM.ActivityLibrary
{
    [Designer(typeof(ActivityDesigner), typeof(IDesigner))]
    public partial class GetReferenceValuesFromObjectActivity: SequenceActivity
    {
        #region Properties


        public static DependencyProperty ObjectIDProperty = DependencyProperty.Register("ObjectID", typeof(Guid), typeof(GetReferenceValuesFromObjectActivity));

        [Description("ObjectID")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ObjectID
        {
            get
            {
                return ((Guid)(base.GetValue(GetReferenceValuesFromObjectActivity.ObjectIDProperty)));
            }
            set
            {
                base.SetValue(GetReferenceValuesFromObjectActivity.ObjectIDProperty, value);
            }
        }

        public static DependencyProperty AttributeNameProperty = DependencyProperty.Register("AttributeName", typeof(String), typeof(GetReferenceValuesFromObjectActivity));

        [Description("AttributeName")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public String AttributeName
        {
            get
            {
                return ((String)(base.GetValue(GetReferenceValuesFromObjectActivity.AttributeNameProperty)));
            }
            set
            {
                base.SetValue(GetReferenceValuesFromObjectActivity.AttributeNameProperty, value);
            }
        }

        public static DependencyProperty ActorIdProperty = DependencyProperty.Register("ActorId", typeof(Guid), typeof(GetReferenceValuesFromObjectActivity));

        [Description("ActorId")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ActorId
        {
            get
            {
                return ((Guid)(base.GetValue(GetReferenceValuesFromObjectActivity.ActorIdProperty)));
            }
            set
            {
                base.SetValue(GetReferenceValuesFromObjectActivity.ActorIdProperty, value);
            }
        }

        public static DependencyProperty CurrentValuesProperty = DependencyProperty.Register("CurrentValues", typeof(Guid[]), typeof(GetReferenceValuesFromObjectActivity));

        [Description("CurrentValues")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid[] CurrentValues
        {
            get
            {
                return ((Guid[])(base.GetValue(GetReferenceValuesFromObjectActivity.CurrentValuesProperty)));
            }
            set
            {
                base.SetValue(GetReferenceValuesFromObjectActivity.CurrentValuesProperty, value);
            }
        }

        #endregion

        public GetReferenceValuesFromObjectActivity()
        {
            InitializeComponent();
        }

        public String[] SelectionAttributes = default(System.String[]);
        public Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType CurrentResource = new Microsoft.ResourceManagement.WebServices.WSResourceManagement.ResourceType();

        private void PrepareReading_ExecuteCode(object sender, EventArgs e)
        {
            this.SelectionAttributes = new string[] {this.AttributeName};
        }

        private void ProcessResults_ExecuteCode(object sender, EventArgs e)
        {
            object PropertyValue = this.CurrentResource[this.AttributeName];
            List<Guid> currentValues = new List<Guid>();

            if (null != PropertyValue)
            {
                if (PropertyValue.GetType().FullName.StartsWith("System.Collections.Generic.List") ||
                    PropertyValue.GetType().FullName.EndsWith("[]"))
                {
                    foreach (object myObject in (IEnumerable) PropertyValue)
                    {
                        if (myObject is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                        {
                            currentValues.Add(
                                ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier) myObject).GetGuid());
                        }
                        if (myObject is System.Guid)
                        {
                            currentValues.Add((System.Guid) myObject);
                        }

                    }
                }
                else
                {
                    if (PropertyValue is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                    {
                        currentValues.Add(
                            ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier) PropertyValue).GetGuid());
                    }
                    if (PropertyValue is System.Guid)
                    {
                        currentValues.Add((System.Guid) PropertyValue);
                    }
                }
            }

         this.CurrentValues = currentValues.Distinct().ToArray();           
        }
    }
}