using UnityEngine;

public class TileSprite : MonoBehaviour
{
    [Header("General Information")]
    [SerializeField] string disallowOverlapTag = "Tile Collision";
    [SerializeField] float disallowOverlapRadius = 0.1f;
    [SerializeField] bool breakable = true;

    [Header("Prefabs")]
    [SerializeField] GameObject collisionTilePrefab;
    [SerializeField] GameObject flimsySailPrefab;

    [Header("Items")]
    [SerializeField] Item tileItem;
    [SerializeField] Item flimsySail;
    [SerializeField] Vector3 sailSpawnOffset = new(0.5f, 1f);

    void Awake()
    {
        CheckOverlap();
    }

    // Run by EventTrigger component (PointerClick mode)
    public void BreakTile()
    {
        // find inventory manager and selected item
        InventoryManager inventoryManager = FindAnyObjectByType<InventoryManager>();
        Item selectedItem = inventoryManager.GetSelectedItem();

        if (selectedItem == flimsySail && inventoryManager.ConsumeSelectedItem())
        {
            Instantiate(flimsySailPrefab, transform.position + sailSpawnOffset, Quaternion.identity);
        }
        else if (breakable && FindAnyObjectByType<InventoryManager>().AddItem(tileItem) <= 0)
        {
            Destroy(transform.parent.gameObject);
            Instantiate(collisionTilePrefab, transform.position, Quaternion.identity);
        }
    }

    void CheckOverlap()
    {
        // find any colliders this object overlaps with
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, disallowOverlapRadius);

        // if one of those colliders is a disallowed object, destroy this object
        foreach (var collider in overlappingColliders)
        {
            if (collider.gameObject.CompareTag(disallowOverlapTag))
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
