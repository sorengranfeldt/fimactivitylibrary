using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;
using Microsoft.ResourceManagement.Workflow.Activities;

namespace Granfeldt.FIM.ActivityLibrary
{

    [Designer(typeof(ActivityDesigner), typeof(IDesigner))]
    public partial class FindResourcesActivity : SequenceActivity
    {
        public FindResourcesActivity()
        {
            EnumeratedResources = new List<ResourceType>();
            InitializeComponent();
        }

        #region Public Fields

        public static DependencyProperty EnumeratedResourcesProperty = DependencyProperty.Register("EnumeratedResources", typeof(List<ResourceType>), typeof(FindResourcesActivity));
        public static DependencyProperty EnumeratedResourceIDsProperty = DependencyProperty.Register("EnumeratedResourceIDs", typeof(Guid[]), typeof(FindResourcesActivity));
        public static DependencyProperty ActorIdProperty = DependencyProperty.Register("ActorId", typeof(System.Guid), typeof(FindResourcesActivity));
        public static DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(System.Int32), typeof(FindResourcesActivity));
        public static DependencyProperty AttributesProperty = DependencyProperty.Register("Attributes", typeof(System.String[]), typeof(FindResourcesActivity));
        public static DependencyProperty SortingAttributesProperty = DependencyProperty.Register("SortingAttributes", typeof(Microsoft.ResourceManagement.WebServices.WSEnumeration.SortingAttribute[]), typeof(FindResourcesActivity));
        public static DependencyProperty TotalResultsCountProperty = DependencyProperty.Register("TotalResultsCount", typeof(System.Int32), typeof(FindResourcesActivity));
        public static DependencyProperty XPathFilterProperty = DependencyProperty.Register("XPathFilter", typeof(System.String), typeof(FindResourcesActivity));

        #endregion

        #region Public Properties

        [Description("EnumeratedResource")]
        [Category("Result")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<ResourceType> EnumeratedResources
        {
            get { return (List<ResourceType>)base.GetValue(FindResourcesActivity.EnumeratedResourcesProperty); }
            set { base.SetValue(FindResourcesActivity.EnumeratedResourcesProperty, value); }
        }

        [DescriptionAttribute("EnumeratedResourceIDs")]
        [CategoryAttribute("Result")]
        [BrowsableAttribute(true)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        public Guid[] EnumeratedResourceIDs
        {
            get
            {
                return ((Guid[])(base.GetValue(FindResourcesActivity.EnumeratedResourceIDsProperty)));
            }
            set
            {
                base.SetValue(FindResourcesActivity.EnumeratedResourceIDsProperty, value);
            }
        }

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Input")]
        public Guid ActorId
        {
            get
            {
                return ((System.Guid)(base.GetValue(FindResourcesActivity.ActorIdProperty)));
            }
            set
            {
                base.SetValue(FindResourcesActivity.ActorIdProperty, value);
            }
        }

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Input")]
        public Int32 PageSize
        {
            get
            {
                return ((int)(base.GetValue(FindResourcesActivity.PageSizeProperty)));
            }
            set
            {
                base.SetValue(FindResourcesActivity.PageSizeProperty, value);
            }
        }

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Input")]
        public String[] Attributes
        {
            get
            {
                return ((string[])(base.GetValue(FindResourcesActivity.AttributesProperty)));
            }
            set
            {
                base.SetValue(FindResourcesActivity.AttributesProperty, value);
            }
        }

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Input")]
        public Microsoft.ResourceManagement.WebServices.WSEnumeration.SortingAttribute[] SortingAttributes
        {
            get
            {
                return ((Microsoft.ResourceManagement.WebServices.WSEnumeration.SortingAttribute[])(base.GetValue(FindResourcesActivity.SortingAttributesProperty)));
            }
            set
            {
                base.SetValue(FindResourcesActivity.SortingAttributesProperty, value);
            }
        }

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Misc")]
        public Int32 TotalResultsCount
        {
            get
            {
                return ((int)(base.GetValue(FindResourcesActivity.TotalResultsCountProperty)));
            }
            set
            {
                base.SetValue(FindResourcesActivity.TotalResultsCountProperty, value);
            }
        }

        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
        [BrowsableAttribute(true)]
        [CategoryAttribute("Input")]
        public String XPathFilter
        {
            get
            {
                return ((string)(base.GetValue(FindResourcesActivity.XPathFilterProperty)));
            }
            set
            {
                base.SetValue(FindResourcesActivity.XPathFilterProperty, value);
            }
        }

        #endregion

        #region Code

        /// <summary> 
        /// This function is called by the enumerateactivity for each result returned.
        /// We'll put each result into the EnumeratedResources list for later processing 
        /// and passing on to other activities
        /// </summary>
        private void ReadEnumeratedResource_ExecuteCode(object sender, EventArgs e)
        {
            // If the EnumeratedGuids list is not instantiated do so now
            if (this.EnumeratedResources == null) this.EnumeratedResources = new List<ResourceType>();

            // Instantiate the result and verify that it is not null
            ResourceType result = EnumerateResourcesActivity.GetCurrentIterationItem((CodeActivity)sender) as ResourceType;
            if (result != null)
            {
                EnumeratedResources.Add(result);
            }
        }

        /// <summary> 
        /// This function populates the list of objectID's that maybe
        /// passed on to other activities
        /// </summary>
        private void GenerateResourceIds_ExecuteCode(object sender, EventArgs e)
        {
            List<Guid> resourceGuids = new List<Guid>(this.EnumeratedResources.Count);
            foreach (ResourceType resource in this.EnumeratedResources)
            {
                resourceGuids.Add(resource.ObjectID.GetGuid());
            }
            this.EnumeratedResourceIDs = resourceGuids.ToArray();
        }

        #endregion
    }
}
