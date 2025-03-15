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
    }

    public int GetCount() { return count; }

    public void AddCount(int amount = 1)
    {
        if (amount > 0)
        {
            count += amount;
            RefreshCount();
        }
    }

    public void RemoveCount(int amount = 1)
    {
        if (amount > 0)
        {
            count -= amount;
            RefreshCount();
        }

        if (count <= 0)
        {
            Destroy(gameObject);
        }
    }

    public Item GetItem() { return item; }
}
