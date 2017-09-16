using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DrinkShop.Persistence
{
    /// <summary>
    /// Generic storage class with basic CRUD functionality
    /// </summary>
    /// <typeparam name="T">The storage item type</typeparam>
    public class Storage<T> where T : class
    {
        #region Private Members

        private ConcurrentDictionary<string, T> _items = new ConcurrentDictionary<string, T>();

        #endregion


        #region Public Methods

        /// <summary>
        /// Tries to update the provided item if exists in the colloection, otherwise adds a new entry for it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newItem"></param>
        /// <returns>True when the operation runs successfully and False otherwise</returns>
        public bool Add(string id, T newItem)
        {
            return (_items.AddOrUpdate(id, newItem, (k, v) => newItem) != null);
        }

        /// <summary>
        /// Checks if the item exists in the stored collection based on its id
        /// </summary>
        /// <param name="id">The ID of the requested item</param>
        /// <returns>True if found and False othewise</returns>
        public bool Contains(string id)
        {
            return _items.ContainsKey(id);
        }

        /// <summary>
        /// Fetches an item from the stored collection based on its id
        /// </summary>
        /// <param name="id">The ID of the requested item</param>
        /// <returns>T</returns>
        public T Get(string id)
        {
            return _items.ContainsKey(id) ? _items[id] : null;
        }

        /// <summary>
        /// Returns all the stored items in the collection
        /// </summary>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> GetAll()
        {
            return _items.Values;
        }

        /// <summary>
        /// Tries to update the provided item if exists in the colloection, otherwise adds a new entry for it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newItem"></param>
        /// <returns>True when the operation runs successfully and False otherwise</returns>
        public bool Upsert(string id, T newItem)
        {
            return (_items.AddOrUpdate(id, newItem, (k, v) => newItem) != null);
        }

        /// <summary>
        /// Tries to remove the item under the provided key from the collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True when the operation runs successfully and False otherwise</returns>
        public bool Delete(string id)
        {
            T tempT;
            return _items.TryRemove(id, out tempT);
        }

        #endregion
    }
}