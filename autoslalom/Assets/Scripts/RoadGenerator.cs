using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] roadTiles;
    private int nextTileToSpawn = 0;
    private Vector3 lastSpawnPosition = new Vector3(16f, 0f, 6f);
    private float distanceBetweenTilesToSpawn = 16f;
    private float distanceBetweenTilesToDespawn = 40f;
    private void Update()
    {
        if (transform.position.x - roadTiles[nextTileToSpawn].position.x >= distanceBetweenTilesToDespawn)
            Spawn();
    }
    private void Spawn()
    {
        roadTiles[nextTileToSpawn].position = new Vector3
            (lastSpawnPosition.x + distanceBetweenTilesToSpawn, lastSpawnPosition.y, lastSpawnPosition.z);
        lastSpawnPosition.x += distanceBetweenTilesToSpawn; 
        if (++nextTileToSpawn == roadTiles.Length) 
        {
            nextTileToSpawn = 0;
        }

    }
}
