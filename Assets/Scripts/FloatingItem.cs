using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] Item item;

    // Run by EventTrigger component (PointerClick mode)
    public void PickUpItem()
    {
        if (FindAnyObjectByType<InventoryManager>().AddItem(item))
            Destroy(gameObject);
    }
}
