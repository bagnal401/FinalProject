using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // create a singleton pattern to contain the inventory data
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        // if there are more than one instance of inventory, make a debug log warning
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 16;

    // create a list of items
    public List<Item> items = new List<Item>();

    // method that adds items
    public bool Add (Item item)
    {
        // if item is not the default item, then move onto the next bool
        if (!item.isDefaultItem)
        {
            // if there is not enough space in inventory, then display error message
            if (items.Count >= space)
            {
                Debug.Log("Note enough inventory space!");
                return false;
            }

            // add item to inventory if both bool statements are true
            items.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }

        }

        return true;
    }

    // method that removes items
    public void Remove (Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
