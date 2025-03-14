using UnityEngine;

public class MoveInRandomDirection : MonoBehaviour
{
    [SerializeField] Vector2 minMoveSpeed;
    [SerializeField] Vector2 maxMoveSpeed;
    Vector2 movementDirection;

    void Start()
    {
        movementDirection = new(Random.Range(minMoveSpeed.x, maxMoveSpeed.x), Random.Range(minMoveSpeed.y, maxMoveSpeed.y));
    }

    void Update()
    {
        transform.position = new Vector2(
            transform.position.x + (movementDirection.x * Time.deltaTime),
            transform.position.y + (movementDirection.y * Time.deltaTime)
        );
    }
}
