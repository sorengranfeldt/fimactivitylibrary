using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{
    [Designer(typeof(ActivityDesigner), typeof(IDesigner))]
    public partial class GetUpdatesToReferenceAttributeActivity : SequenceActivity
    {

        #region Properties
        public static DependencyProperty AddedReferencesProperty = DependencyProperty.Register("AddedReferences", typeof(Guid[]), typeof(GetUpdatesToReferenceAttributeActivity));

        [Description("AddedReferences")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid[] AddedReferences
        {
            get
            {
                return ((Guid[])(base.GetValue(GetUpdatesToReferenceAttributeActivity.AddedReferencesProperty)));
            }
            set
            {
                base.SetValue(GetUpdatesToReferenceAttributeActivity.AddedReferencesProperty, value);
            }
        }

        public static DependencyProperty ModifiedReferencesProperty = DependencyProperty.Register("ModifiedReferences", typeof(Guid[]), typeof(GetUpdatesToReferenceAttributeActivity));

        [DescriptionAttribute("ModifiedReferences")]
        [CategoryAttribute("Output")]
        [BrowsableAttribute(true)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        public Guid[] ModifiedReferences
        {
            get
            {
                return ((Guid[])(base.GetValue(GetUpdatesToReferenceAttributeActivity.ModifiedReferencesProperty)));
            }
            set
            {
                base.SetValue(GetUpdatesToReferenceAttributeActivity.ModifiedReferencesProperty, value);
            }
        }

        public static DependencyProperty RemovedReferencesProperty = DependencyProperty.Register("RemovedReferences", typeof(Guid[]), typeof(GetUpdatesToReferenceAttributeActivity));

        public static DependencyProperty ResourceIdProperty = DependencyProperty.Register("ResourceId", typeof(Guid), typeof(GetUpdatesToReferenceAttributeActivity));

        [Description("ResourceId")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid ResourceId
        {
            get
            {
                return ((Guid)(base.GetValue(GetUpdatesToReferenceAttributeActivity.ResourceIdProperty)));
            }
            set
            {
                base.SetValue(GetUpdatesToReferenceAttributeActivity.ResourceIdProperty, value);
            }
        }

        [Description("RemovedReferences")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Guid[] RemovedReferences
        {
            get
            {
                return ((Guid[])(base.GetValue(GetUpdatesToReferenceAttributeActivity.RemovedReferencesProperty)));
            }
            set
            {
                base.SetValue(GetUpdatesToReferenceAttributeActivity.RemovedReferencesProperty, value);
            }
        }

        public static DependencyProperty AttributeNameProperty = DependencyProperty.Register("AttributeName", typeof(string), typeof(GetUpdatesToReferenceAttributeActivity));

        [Description("AttributeName")]
        [Category("Input")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string AttributeName
        {
            get
            {
                return ((string)(base.GetValue(GetUpdatesToReferenceAttributeActivity.AttributeNameProperty)));
            }
            set
            {
                base.SetValue(GetUpdatesToReferenceAttributeActivity.AttributeNameProperty, value);
            }
        }

        public static DependencyProperty OperationTypeProperty = DependencyProperty.Register("OperationType", typeof(OperationType), typeof(GetUpdatesToReferenceAttributeActivity));

        [Description("OperationType")]
        [Category("Output")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public OperationType OperationType
        {
            get
            {
                return ((OperationType)(base.GetValue(GetUpdatesToReferenceAttributeActivity.OperationTypeProperty)));
            }
            set
            {
                base.SetValue(GetUpdatesToReferenceAttributeActivity.OperationTypeProperty, value);
            }
        }

        #endregion

        public GetUpdatesToReferenceAttributeActivity()
        {
            InitializeComponent();
        }

        public Microsoft.ResourceManagement.WebServices.WSResourceManagement.RequestType CurrentRequest = null;

        private void ExtractDataFromRequest_ExecuteCode(object sender, EventArgs e)
        {
            this.ResourceId = this.CurrentRequest.Target.GetGuid();
            this.OperationType = this.CurrentRequest.Operation;
            List<Guid> toAdd = new List<Guid>();
            List<Guid> toRemove = new List<Guid>();
            List<Guid> toModify = new List<Guid>();

            if (this.CurrentRequest.Operation == OperationType.Create)
            {
                ReadOnlyCollection<CreateRequestParameter> myParameters =
                    this.CurrentRequest.ParseParameters<CreateRequestParameter>();

                foreach (CreateRequestParameter myParameter in myParameters)
                {
                    if (String.Equals(this.AttributeName, myParameter.PropertyName,
                                      StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (myParameter.Value.GetType().FullName.StartsWith("System.Collections.Generic.List") ||
                            myParameter.Value.GetType().FullName.EndsWith("[]"))
                        {
                            foreach (object myObject in (IEnumerable)myParameter.Value)
                            {

                                if (myObject is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                                    toAdd.Add(
                                        ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier)myObject).GetGuid());
                                if (myObject is System.Guid)
                                    toAdd.Add((System.Guid)myObject);
                            }
                        }
                        else
                        {
                            if (myParameter.Value is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                                toAdd.Add(
                                    ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier)myParameter.Value).
                                        GetGuid());
                            if (myParameter.Value is System.Guid)
                                toAdd.Add((System.Guid)myParameter.Value);
                        }
                    }
                }
            }

            if (this.CurrentRequest.Operation == OperationType.Put)
            {
                ReadOnlyCollection<UpdateRequestParameter> myParameters =
                    this.CurrentRequest.ParseParameters<UpdateRequestParameter>();

                foreach (UpdateRequestParameter myParameter in myParameters)
                {
                    if (myParameter.Mode == UpdateMode.Insert)
                    {
                        if (String.Equals(this.AttributeName, myParameter.PropertyName,
                                          StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (myParameter.Value.GetType().FullName.StartsWith("System.Collections.Generic.List") ||
                                myParameter.Value.GetType().FullName.EndsWith("[]"))
                            {
                                foreach (object myObject in (IEnumerable)myParameter.Value)
                                {
                                    if (myObject is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                                        toAdd.Add(
                                            ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier)myObject).
                                                GetGuid());
                                    if (myObject is System.Guid)
                                        toAdd.Add((System.Guid)myObject);
                                }
                            }
                            else
                            {
                                if (myParameter.Value is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                                    toAdd.Add(
                                        ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier)myParameter.Value).
                                            GetGuid());
                                if (myParameter.Value is System.Guid)
                                    toAdd.Add((System.Guid)myParameter.Value);
                            }
                        }
                    }

                    if (myParameter.Mode == UpdateMode.Modify)
                    {
                        if (String.Equals(this.AttributeName, myParameter.PropertyName,
                                          StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Actually never going to happen, but who knows ?
                            if (myParameter.Value.GetType().FullName.StartsWith("System.Collections.Generic.List") ||
                                myParameter.Value.GetType().FullName.EndsWith("[]"))
                            {
                                foreach (object myObject in (IEnumerable)myParameter.Value)
                                {

                                    if (myObject is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                                        toModify.Add(
                                            ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier)myObject).
                                                GetGuid());
                                    if (myObject is System.Guid)
                                        toModify.Add((System.Guid)myObject);
                                }
                            }
                            else
                            {
                                if (myParameter.Value is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                                    toModify.Add(
                                        ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier)myParameter.Value).
                                            GetGuid());
                                if (myParameter.Value is System.Guid)
                                    toModify.Add((System.Guid)myParameter.Value);
                            }
                        }
                    }


                    if (myParameter.Mode == UpdateMode.Remove)
                    {
                        if (String.Equals(this.AttributeName, myParameter.PropertyName,
                                          StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (myParameter.Value.GetType().FullName.StartsWith("System.Collections.Generic.List") ||
                                myParameter.Value.GetType().FullName.EndsWith("[]"))
                            {
                                foreach (object myObject in (IEnumerable)myParameter.Value)
                                {

                                    if (myObject is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                                        toRemove.Add(
                                            ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier)myObject).
                                                GetGuid());
                                    if (myObject is System.Guid)
                                        toRemove.Add((System.Guid)myObject);
                                }
                            }
                            else
                            {
                                if (myParameter.Value is Microsoft.ResourceManagement.WebServices.UniqueIdentifier)
                                    toRemove.Add(
                                        ((Microsoft.ResourceManagement.WebServices.UniqueIdentifier)myParameter.Value).
                                            GetGuid());
                                if (myParameter.Value is System.Guid)
                                    toRemove.Add((System.Guid)myParameter.Value);
                            }
                        }
                    }

                }
            }
            this.AddedReferences=toAdd.Distinct().ToArray();
            this.RemovedReferences=toRemove.Distinct().ToArray();
            this.ModifiedReferences = toModify.Distinct().ToArray();
        }      
    }
}