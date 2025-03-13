using UnityEngine;

public class TileCollider : MonoBehaviour
{
    [SerializeField] string tilePrefabName = "Tile";

    [SerializeField] string requireNearbyToPlaceTag = "Tile Sprite";
    [SerializeField] string disallowOverlapTag = "Tile Sprite";
    [SerializeField] float requireNearbyToPlaceRadius = 1.1f;
    [SerializeField] float disallowOverlapRadius = 0.1f;

    void Awake()
    {
        CheckOverlap();
    }

    public void PlaceTile()
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
