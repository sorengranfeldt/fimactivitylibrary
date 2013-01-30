using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ResourceManagement.WebServices.WSResourceManagement;

namespace Granfeldt.FIM.ActivityLibrary
{
    public class AttributeValueCollection : ICollection<AttributeValue>
    {
        //Constructors
        /// <summary>
        /// Creates a new instance of AttributeValueCollection.
        /// </summary>
        public AttributeValueCollection()
        {
            this._attributeValues = new List<AttributeValue>();
        }
        /// <summary>
        /// Creates a new instance of AttributeValueCollection that takes the ObjectID of the target resource.
        /// </summary>
        /// <param name="targetObjectID">ObjectID of the target resource</param>
        public AttributeValueCollection(Guid targetObjectID)
        {
            this.TargetObjectID = targetObjectID;
            this._attributeValues = new List<AttributeValue>();
        }
        
        //Fields
        /// <summary>
        /// List of all the AttributeValue objects in the collection.
        /// </summary>
        private List<AttributeValue> _attributeValues;
        /// <summary>
        /// Target resource that needs to be updates.
        /// </summary>
        private ResourceType _targetResource;
        //Properties
        public Guid TargetObjectID { get; set; }
        public ResourceType TargetResource
        {
            get { return _targetResource; }
            set 
            {
                _targetResource = value;
                PopulateAttributeValues();
            }
        }
        /// <summary>
        /// Gets all the AttributeValues in the collection, which contains differences between the source and the target value
        /// </summary>
        /// <returns>All changes in the collection to send to target.</returns>
        public List<UpdateRequestParameter> Changes
        {
            get
            {
                List<UpdateRequestParameter> changes = new List<UpdateRequestParameter>();
                foreach (AttributeValue a in _attributeValues)
                {
                    Debugging.Log(a.TargetAttributeName, a.TargetAttributeValue);
                    if (a.ShouldUpdate())
                    {
                        changes.Add(a.ToUpdateRequestParameter(this.TargetObjectID, a.IsDelete));
                    }
                }
                return changes;
            }
        }

        #region Methods
        /// <summary>
        /// Sets attribute values on all AttributeValue objects in the collection.
        /// </summary>
        private void PopulateAttributeValues()
        {
            foreach (AttributeValue val in _attributeValues)
            {
                val.SetValues(_targetResource);
            }
        }
        
        #region ICollection implementation
        
        public void Add(AttributeValue item)
        {
            this._attributeValues.Add(item);
        }

        public void Clear()
        {
            this._attributeValues.Clear();
        }

        public bool Contains(AttributeValue item)
        {
            return this._attributeValues.Contains(item);
        }

        public void CopyTo(AttributeValue[] array, int arrayIndex)
        {
            this._attributeValues.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._attributeValues.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(AttributeValue item)
        {
            return this._attributeValues.Remove(item);
        }

        public IEnumerator<AttributeValue> GetEnumerator()
        {
            return this._attributeValues.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
        #endregion
    }
}
