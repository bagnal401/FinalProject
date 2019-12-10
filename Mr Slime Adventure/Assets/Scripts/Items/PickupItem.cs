using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    // creates an item variable
    public Item item;

    // when the player collider collides with another collider, it prints that the item was picked up and destroys the game object
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            print("Picked up " + item.name);
            // assigns wasPickedUp bool statement if the item was picked up
            bool wasPickedUp = Inventory.instance.Add(item);

            // if item was picked up, then destroy the game object
            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
