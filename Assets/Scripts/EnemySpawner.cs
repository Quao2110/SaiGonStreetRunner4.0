using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    // Yêu cầu 1: Đặt thời gian chờ cố định là 10.0 giây
    [SerializeField] private float spawnInterval = 20f;

    [Header("Spawn X Range")]
    [SerializeField] private float minX = 3f;
    [SerializeField] private float maxX = 200f;

    // Yêu cầu 2: Khoảng cách tối thiểu giữa các lần spawn
    [SerializeField] private float minDistanceBetweenSpawns = 20f;
    // Biến private để lưu vị trí X của lần spawn gần nhất
    private float lastSpawnX;

    void Start()
    {
        // Khởi tạo vị trí spawn cuối cùng ở giữa để lần đầu spawn có thể ở bất cứ đâu
        lastSpawnX = (minX + maxX) / 2f;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // 1. Chờ đúng thời gian đã định
            yield return new WaitForSeconds(spawnInterval);

            // 2. Tính toán vị trí X ngẫu nhiên và hợp lệ
            float randomX = CalculateRandomX();

            // Nếu không thể tìm thấy vị trí hợp lệ, bỏ qua lần spawn này 
            if (randomX == float.MinValue)
            {
                Debug.LogWarning("Cannot find a valid spawn position. Range too small.");
                continue;
            }

            Vector3 spawnPosition = new Vector3(randomX, -4.9f , 0f);

            // 3. Tạo (Instantiate) kẻ địch
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // 4. Cập nhật vị trí X cuối cùng
            lastSpawnX = randomX;
        }
    }

    private float CalculateRandomX()
    {
        // Tính toán giới hạn của hai vùng hợp lệ, cách lastSpawnX ít nhất 20 đơn vị
        float leftBoundEnd = lastSpawnX - minDistanceBetweenSpawns;
        float rightBoundStart = lastSpawnX + minDistanceBetweenSpawns;

        // Vùng bên trái: [minX, leftBoundEnd]
        float effectiveMinX_L = minX;
        float effectiveMaxX_L = leftBoundEnd;

        // Vùng bên phải: [rightBoundStart, maxX]
        float effectiveMinX_R = rightBoundStart;
        float effectiveMaxX_R = maxX;

        // Tính kích thước các vùng hợp lệ (đảm bảo kích thước >= 0)
        float sizeL = Mathf.Max(0f, effectiveMaxX_L - effectiveMinX_L);
        float sizeR = Mathf.Max(0f, effectiveMaxX_R - effectiveMinX_R);

        float totalSize = sizeL + sizeR;

        if (totalSize > 0)
        {
            float randomValue = Random.Range(0f, totalSize);

            if (randomValue < sizeL)
            {
                // Chọn spawn ở vùng bên trái (random từ minX đến leftBoundEnd)
                return Random.Range(effectiveMinX_L, effectiveMaxX_L);
            }
            else
            {
                // Chọn spawn ở vùng bên phải (random từ rightBoundStart đến maxX)
                return Random.Range(effectiveMinX_R, effectiveMaxX_R);
            }
        }
        else
        {
            // Trường hợp không có không gian (phạm vi [minX, maxX] quá hẹp)
            return float.MinValue;
        }
    }
}