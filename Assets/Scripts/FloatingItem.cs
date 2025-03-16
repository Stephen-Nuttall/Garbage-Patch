using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] int quantity = 1;

    // Run by EventTrigger component (PointerClick mode)
    public void PickUpItem()
    {
        quantity = FindAnyObjectByType<InventoryManager>().AddItem(item, quantity);

        if (quantity == 0)
            Destroy(gameObject);
    }
}
