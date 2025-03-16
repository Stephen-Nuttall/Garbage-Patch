using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text countText;

    [HideInInspector] public Transform parentAfterDrag;
    Item item;
    int count = 1;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.GetImage();
        RefreshCount();
    }

    void RefreshCount()
    {
        countText.text = count.ToString();
        countText.enabled = count > 1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        countText.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        transform.position = parentAfterDrag.position;
        countText.gameObject.SetActive(true);
    }

    public int GetCount() { return count; }

    // returns leftover amount
    public int AddCount(int amount = 1)
    {
        if (amount > 0)
        {
            count += amount;
            if (count > item.GetMaxStackSize())
            {
                int leftoverAmount = count - item.GetMaxStackSize();
                count = item.GetMaxStackSize();
                return leftoverAmount;
            }
            RefreshCount();
            return 0;
        }

        return amount;
    }

    public bool RemoveCount(int amount = 1)
    {
        if (amount > 0)
        {
            count -= amount;

            if (count < 0)
            {
                count += amount;
                return false;
            }
            else if (count == 0)
            {
                Destroy(gameObject);
                return true;
            }
            else
            {
                RefreshCount();
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public Item GetItem() { return item; }
}
