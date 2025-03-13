using UnityEngine;

public class TileSprite : MonoBehaviour
{
    [SerializeField] GameObject collisionTilePrefab;
    [SerializeField] string disallowOverlapTag = "Tile Collision";
    [SerializeField] float disallowOverlapRadius = 0.1f;

    void Awake()
    {
        CheckOverlap();
    }

    public void BreakTile()
    {
        Destroy(transform.parent.gameObject);
        Instantiate(collisionTilePrefab, transform.position, Quaternion.identity);
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
