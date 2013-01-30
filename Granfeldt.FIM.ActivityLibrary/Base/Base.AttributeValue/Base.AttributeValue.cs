using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{

    public class AttributeValue
    {
        //Constructors
        /// <summary>
        /// Creates a new instance of the object type AttributeValue.
        /// </summary>
        public AttributeValue()
        {
            this.IsResolved = false;
        }
        /// <summary>
        /// Creates a new instance of the object type AttributeValue, and sets source and target attribute name.
        /// </summary>
        /// <param name="sourceAttributeName">Name of the source attribute.</param>
        /// <param name="targetAttributeName">Name of the target attribute.</param>
        public AttributeValue(string sourceAttributeName, string targetAttributeName)
        {
            this.SourceAttributeName = sourceAttributeName;
            this.TargetAttributeName = targetAttributeName;
            this.IsResolved = false;
        }
        //Fields
        private object _sourceAttributeValue;

        //Properties
        public string SourceAttributeName { get; set; }
        public object SourceAttributeValue 
        {
            get
            {
                return this._sourceAttributeValue;
            } 
            set
            {
                this._sourceAttributeValue = value;
                this.IsResolved = true;    
            } 
        }
        public string TargetAttributeName { get; set; }
        public object TargetAttributeValue { get; set; }
        public bool IsDelete { get; private set; }
        /// <summary>
        /// Indicates if the SourceAttributeName have been resolved.
        /// This is set when SourceAttributeValue is set.
        /// </summary>
        public bool IsResolved { get; private set; }
        /// <summary>
        /// Tells if the SourceAttributeValue should be resolved from SourceAttributeName.
        /// It is decided by testing if the SourceAttributeName contains a resolveable string.
        /// </summary>
        public bool ShouldResolve
        {
            get
            {
                if (this.IsResolved)
                {
                    return false;
                }
                else
                {
                    if (this.SourceAttributeName != null)
                        return this.SourceAttributeName.Trim().Contains("[//");
                    else
                        return false;
                }
            }
        }

        #region Methods
        /// <summary>
        /// Returns this AttributeValue as an UpdateRequestParameter used for updating resources in FIM.
        /// </summary>
        /// <param name="objectId">ObjectID of the resource being updated.</param>
        /// <param name="isDelete">Tells the method what type of update to return. 
        /// If isDelete is true a UpdateMode is set to: Remove.
        /// </param>
        /// <returns></returns>
        public UpdateRequestParameter ToUpdateRequestParameter(Guid objectId,bool isDelete)
        {
            if (!isDelete)
                return new UpdateRequestParameter(objectId, this.TargetAttributeName, UpdateMode.Modify, FIMAttributeUtilities.FormatValue(this.SourceAttributeValue), false);
            else
            {
                return new UpdateRequestParameter(objectId, this.TargetAttributeName, UpdateMode.Remove, this.TargetAttributeValue, false);
            }
        }
        /// <summary>
        /// Returns this AttributeValue as an UpdateRequestParameter used for updating resources in FIM, without specifying which object to update.
        /// </summary>
        /// <param name="isDelete">Tells the method what type of update to return. 
        /// If isDelete is true a UpdateMode is set to: Remove.</param>
        /// <returns></returns>
        public UpdateRequestParameter ToUpdateRequestParameter(bool isDelete)
        {

            if (!isDelete)
                return new UpdateRequestParameter(this.TargetAttributeName, UpdateMode.Modify, FIMAttributeUtilities.FormatValue(this.SourceAttributeValue), false);
            else
            {
                return new UpdateRequestParameter(this.TargetAttributeName, UpdateMode.Remove, this.TargetAttributeValue, false);
            }
            
        }
        /// <summary>
        /// Compares the source value with the supplied readValue parameter. 
        /// Also sets IsDelete to true or false
        /// </summary>
        /// <param name="readValue">A value to compare with the SourceValue.</param>
        /// <returns></returns>
        public virtual bool ShouldUpdate(object readValue)
        {
            

            // we need the original (non-converted) value if we are doing a removal as we must specify the original value (even a local date/time)
           // object originalTargetValue = this.TargetAttributeValue;
            object target = FIMAttributeUtilities.FormatValue(readValue);
            object source = FIMAttributeUtilities.FormatValue(this.SourceAttributeValue);

            return getShouldUpdate(target, source);
        }
        /// <summary>
        /// Compares the Target attribute value with the existing source attribute value. If the source attribute value is not set, 
        /// it will be compared on as a null value.
        /// </summary>
        /// <returns>Result of compare</returns>
        public virtual bool ShouldUpdate()
        {
            object target = FIMAttributeUtilities.FormatValue(this.TargetAttributeValue);
            object source = FIMAttributeUtilities.FormatValue(this.SourceAttributeValue);

            Debugging.Log(String.Format("Comparing source: {0} and target: {1}, containing the values: {2} / {3}", this.SourceAttributeName, this.TargetAttributeName, this.SourceAttributeValue, this.TargetAttributeValue));
            bool result =  getShouldUpdate(target, source);
            Debugging.Log(String.Format("Comparison resulted in: {0}",result));
            return result;
        }
        /// <summary>
        /// Private method for comparing Source and Target attributes
        /// </summary>
        /// <param name="target">Taget value</param>
        /// <param name="source">Source value</param>
        /// <returns>Whether or not the target attribute should be updated.</returns>
        private bool getShouldUpdate(object target, object source)
        {
            if (FIMAttributeUtilities.ValuesAreDifferent(source, target))
            {
                if (source == null)
                {
                    // we are in a delete state
                    Debugging.Log(string.Format("Deleting value '{1}' from {0}", this.TargetAttributeName, target == null ? "(null)" : target));
                    this.IsDelete = true;
                }
                else
                {
                    // we are in an update state
                    Debugging.Log(string.Format("Updating {0} from '{1}' to '{2}'", this.TargetAttributeName, target == null ? "(null)" : target, source));
                    this.IsDelete = false;
                }
                return true;
            }
            else
            {
                Debugging.Log(string.Format("No need to update {0}. Value is already {1}", this.TargetAttributeName, this.TargetAttributeValue == null ? "(null)" : this.TargetAttributeValue));
                this.IsDelete = false;
                return false;
            }
        }
        /// <summary>
        /// Sets the TargetAttributeValue on this AttributeValue based on the it's current TargetAttributeName.
        /// </summary>
        /// <param name="resource">the ResourceType object to pull the attribute value from.</param>
        internal virtual void SetValues(ResourceType resource)
        {
            TargetAttributeValue = resource[this.TargetAttributeName] != null ? resource[this.TargetAttributeName] : null;
            Debugging.Log(this.TargetAttributeName, this.TargetAttributeValue);
        }
        #endregion
    }

}
