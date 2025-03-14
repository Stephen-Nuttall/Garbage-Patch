using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector2 spawnRange;
    [SerializeField] float xOffsetFromPlayer = -12f;
    [SerializeField] Transform[] trashPrefabs;
    [SerializeField] float spawnFrequency = 5f;  // seconds
    [SerializeField] bool spawningTrash = true;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (spawningTrash)
        {
            Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)],
            new Vector2(playerTransform.position.x + xOffsetFromPlayer, transform.position.y + Random.Range(spawnRange.x, spawnRange.y)),
            Quaternion.identity);

            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
