

#region using statements

using System;
using System.Collections;

#endregion


namespace DataJuggler.WPF.Controls.Objects
{

    #region class Item
    [Serializable]
    public partial class Item
    {

        #region Private Variables
        #endregion

        #region Constructor
        public Item()
        {

        }
        #endregion

        #region Methods

        #endregion

        #region Properties
        #endregion

    }
    #endregion

    #region class ItemCollection
    [Serializable]
    public class ItemCollection : CollectionBase
    {

        #region Constructor
        public ItemCollection()
        {
        }
        #endregion

        #region Methods

            #region Add() void
            public void Add(Item item)
            {
                // Add This Item To This Collection
                this.List.Add(item);
            }
            #endregion

            #region Clone()
            public ItemCollection Clone()
            {
                // Create New Object
                ItemCollection NewItemCollection = new ItemCollection();

                // Add Each Object To Collection
                foreach(Item ItemObject in this)
                {
                    // Add This Object To Collection
                    NewItemCollection.Add(ItemObject);
                }

                // Return Cloned Object
                return NewItemCollection;

            }
            #endregion

            #region GetDefaultIndex()
            public int GetDefaultIndex()
            {
                // Initial Value
                int index = -1;

                // Check Each Item
                foreach (Item ItemObject in this)
                {
                    // Increment Index
                    index++;

                    // Check This Item
                    if (ItemObject.DefaultItem)
                    {
                        // Return Index
                        return index;
                    }
                }

                // Return Not Found (-1)
                return -1;
            }
            #endregion

            #region GetIndex(int itemID)
            public int GetIndex(int itemID)
            {
                // Initial Value
                int index = -1;

                // Check Each Item
                foreach(Item ItemObject in this)
                {
                    // Increment Index
                    index++;

                    // Check This Item
                    if(ItemObject.ItemID == itemID)
                    {
                        // Return Index
                        return index;
                    }
                }

                // Return Not Found (-1)
                return -1;
            }
            #endregion

        #endregion

        #region Properties

            #region Index
            public Item this[int Index]
            {
                get
                {
                    return (Item) this.List[Index];
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
