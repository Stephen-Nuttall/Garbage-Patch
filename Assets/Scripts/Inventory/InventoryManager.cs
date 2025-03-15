using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InputActionReference inventoryToggle;
    [SerializeField] GameObject inventoryObject;
    [SerializeField] GameObject inventoryItemPrefab;
    [SerializeField] InventorySlot[] slots;
    [SerializeField] int numHotBarSlots = 5;

    int selectedSlot = 0;
    public static event Action<InventorySlot> slotSelected;

    void OnEnable()
    {
        inventoryToggle.action.started += OpenInventory;
    }

    void OnDisable()
    {
        inventoryToggle.action.started -= OpenInventory;
    }

    void Start()
    {
        ChangeSelectedSlot(0);
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            int newValue = selectedSlot - (int)(scroll / Mathf.Abs(scroll));

            if (newValue < 0)
            {
                newValue = numHotBarSlots - 1;
            }
            else if (newValue >= numHotBarSlots)
            {
                newValue = 0;
            }

            ChangeSelectedSlot(newValue);
        }
    }

    void ChangeSelectedSlot(int newSlotNum)
    {
        if (newSlotNum >= 0 && newSlotNum < slots.Length)
        {
            slotSelected?.Invoke(slots[newSlotNum]);
            selectedSlot = newSlotNum;
        }
    }

    void OpenInventory(InputAction.CallbackContext obj)
    {
        inventoryObject.SetActive(!inventoryObject.activeInHierarchy);
    }

    public bool AddItem(Item item)
    {
        int i = 0;
        int emptySlotIndex = -1;
        bool emptySlotFound = false;

        foreach (InventorySlot slot in slots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                if (!emptySlotFound)
                {
                    emptySlotIndex = i;
                    emptySlotFound = true;
                }
            }
            else
            {
                if (itemInSlot.GetItem() == item && itemInSlot.GetCount() < item.GetMaxStackSize())
                {
                    itemInSlot.AddCount();
                    return true;
                }
            }

            i++;
        }

        if (emptySlotFound)
        {
            SpawnNewItem(item, slots[emptySlotIndex]);
            return true;
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemObject = Instantiate(inventoryItemPrefab, slot.transform);
        newItemObject.GetComponent<InventoryItem>().InitializeItem(item);
    }

    public Item GetSelectedItem()
    {
        InventoryItem itemInSlot = slots[selectedSlot].GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            return itemInSlot.GetItem();
        }
        else
        {
            return null;
        }
    }

    public Item ConsumeSelectedItem(int amount = 1)
    {
        InventoryItem itemInSlot = slots[selectedSlot].GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.GetItem();
            itemInSlot.RemoveCount(amount);
            return item;
        }
        else
        {
            return null;
        }
    }
}
