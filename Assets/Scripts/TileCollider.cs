using FMODUnity;
using UnityEngine;

public class TileCollider : MonoBehaviour
{
    [SerializeField] string tilePrefabName = "Tile";
    [SerializeField] EventReference cantPlaceSound;

    [SerializeField] string requireNearbyToPlaceTag = "Tile Sprite";
    [SerializeField] string disallowOverlapTag = "Tile Sprite";
    [SerializeField] float requireNearbyToPlaceRadius = 1.1f;
    [SerializeField] float disallowOverlapRadius = 0.1f;

    [Header("Items")]
    [SerializeField] Item tileItem;
    [SerializeField] Item emptyBucketItem;
    [SerializeField] Item waterBucketItem;
    [SerializeField] Item oilBucketItem;

    void Awake()
    {
        CheckOverlap();
    }

    // Run by EventTrigger component (PointerClick mode)
    public void PlaceTile()
    {
        // find inventory manager and selected item
        InventoryManager inventoryManager = FindAnyObjectByType<InventoryManager>();
        Item selectedItem = inventoryManager.GetSelectedItem();

        if (selectedItem == emptyBucketItem && inventoryManager.ConsumeSelectedItem())
        {
            if (FindAnyObjectByType<LocationManager>().atOilSpill)
            {
                inventoryManager.AddItem(oilBucketItem);
            }
            else
            {
                inventoryManager.AddItem(waterBucketItem);
            }
        }
        // if the player is holding a tile item and it can be successfully consumed...
        else if (selectedItem == tileItem && inventoryManager.ConsumeSelectedItem())
        {
            // find any colliders this object overlaps with
            Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, requireNearbyToPlaceRadius);

            // if one of those colliders is the required nearby to place object, place a new tile
            foreach (var collider in overlappingColliders)
            {
                if (collider.gameObject.CompareTag(requireNearbyToPlaceTag))
                {
                    Instantiate(Resources.Load(tilePrefabName, typeof(GameObject)), transform.position, Quaternion.identity);
                    CheckOverlap();
                    break;
                }
            }
        }
        // else, play can't place sound effect
        else
        {
            AudioManager.instance.PlaySFX(cantPlaceSound);
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
                Destroy(gameObject);
            }
        }
    }
}
