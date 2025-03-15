using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] Image image;
    [SerializeField] Sprite selectedSprite, nonSelectedSprite;

    void OnEnable()
    {
        InventoryManager.slotSelected += SelectionCheck;
    }

    void OnDisable()
    {
        InventoryManager.slotSelected -= SelectionCheck;
    }

    void Awake()
    {
        image.sprite = nonSelectedSprite;
    }

    void SelectionCheck(InventorySlot newSlot)
    {
        if (newSlot == this)
        {
            image.sprite = selectedSprite;
        }
        else
        {
            image.sprite = nonSelectedSprite;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryItem>(out var inventoryItem))
            {
                inventoryItem.parentAfterDrag = transform;
            }
        }
    }
}
