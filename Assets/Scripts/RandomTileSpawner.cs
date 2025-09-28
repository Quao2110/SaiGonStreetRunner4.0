using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTileSpawner : MonoBehaviour
{
    public Tilemap tilemap;            // Kéo Tilemap vào đây
    public TileBase[] brickTiles;      // Kéo 1 hoặc nhiều Tile Asset vào đây
    public int xStart = -10;           // Vùng random (X trái)
    public int xEnd = 10;              // Vùng random (X phải)
    public int yStart = 0;             // Vùng random (Y dưới)
    public int yEnd = 0;               // Vùng random (Y trên)
    [Range(0f, 1f)]
    public float spawnChance = 0.3f;   // Xác suất xuất hiện

    void Start()
    {
        GenerateRandomBricks();
    }

    public void GenerateRandomBricks()
    {
        if (tilemap == null || brickTiles.Length == 0)
        {
            Debug.LogWarning("Chưa gán Tilemap hoặc BrickTiles!");
            return;
        }

        tilemap.ClearAllTiles();

        for (int x = xStart; x <= xEnd; x++)
        {
            for (int y = yStart; y <= yEnd; y++)
            {
                if (Random.value < spawnChance)
                {
                    TileBase tile = brickTiles[Random.Range(0, brickTiles.Length)];
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }
}
