using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // will changer the inventory slots when an item is added or removed
    public Transform itemsParent;

    public GameObject inventoryUI;

    Inventory inventory;

    // create an array of inventory slots
    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        // using singleton pattern
        inventory = Inventory.instance;
        // subscribing to onItemChangeCallback to trigger the event whenever an item is added or removed
        inventory.onItemChangedCallback += UpdateUI;

        // check the number of inventory slots
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        // if a key is pressed, the inventory window will activate
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            // unlock cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
        // loop through all slots to check if there are more items to add
        for (int i = 0; i < slots.Length; i++)
        {
            // if i is less than the number of items in the count, then there are more items to add
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            // if we're out of items to add, then call the clearSlot method
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
