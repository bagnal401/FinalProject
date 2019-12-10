using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // variable to store item icon image
    public Image icon;
    // variable to reference remove button
    public Button removeButton;
    
    // variable to keep track of current item in the slot
    Item item;

    // use AddItem method when a new item is picked up
    public void AddItem (Item newItem)
    {
        item = newItem;

        // assigns the item icon to the item sprite
        icon.sprite = item.icon;
        // displays the item icon in the inventory slot
        icon.enabled = true;
        removeButton.interactable = true;
    }

    // use ClearSlot method when inventory slot is empty
    public void ClearSlot()
    {
        item = null;

        // removes item sprite
        icon.sprite = null;
        icon.enabled = false;
        // makes the remove button not interactable
        removeButton.interactable = false;
    }

    // when the remove button is clicked, it removes the item from the inventory slot
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
