using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadChunkPrefab;   // Prefab của đoạn đường
    public Transform player;             // Nhân vật / camera
    public int chunksOnScreen = 3;       // Số đoạn luôn hiển thị

    private float chunkLength;           // Độ dài thật của 1 đoạn
    private float spawnZRight = 0f;      // Điểm spawn sang phải
    private float spawnZLeft = 0f;       // Điểm spawn sang trái

    void Start()
    {
        // Tính chunkLength
        chunkLength = GetPrefabLength(roadChunkPrefab);

        // Spawn sẵn mấy đoạn ban đầu (trái + phải quanh player)
        for (int i = -chunksOnScreen / 2; i <= chunksOnScreen / 2; i++)
        {
            SpawnChunk(i * chunkLength);
        }

        // Thiết lập biên trái/phải
        spawnZRight = (chunksOnScreen / 2 + 1) * chunkLength;
        spawnZLeft = -(chunksOnScreen / 2 + 1) * chunkLength;
    }

    void Update()
    {
        // Player đi sang phải → spawn thêm bên phải
        if (player.position.x > spawnZRight - chunksOnScreen * chunkLength)
        {
            SpawnChunk(spawnZRight);
            spawnZRight += chunkLength;
        }

        // Player đi sang trái → spawn thêm bên trái
        if (player.position.x < spawnZLeft + chunksOnScreen * chunkLength)
        {
            spawnZLeft -= chunkLength;   // 👈 giảm trước
            SpawnChunk(spawnZLeft);
        }
    }

    void SpawnChunk(float xPos)
    {
        Instantiate(
            roadChunkPrefab,
            new Vector3(xPos, -8.37f, 0),   // giữ Y cố định -6.5
            Quaternion.identity
        );
    }

    // Hàm lấy độ dài prefab
    private float GetPrefabLength(GameObject prefab)
    {
        SpriteRenderer sr = prefab.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            return sr.size.x * prefab.transform.localScale.x;

        return 3f; // fallback
    }

}
