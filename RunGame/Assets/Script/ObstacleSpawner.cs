using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    // 障害物のプレハブ
    private GameObject iceRockPrefab, iceSpikePrefab; 

    [SerializeField]
    // 障害物のY座標
    private float iceRockYPos = -3.5f, iceSpikeYPos = -3.5f; 

    [SerializeField]
    // 障害物生成の待機時間
    private float minSpawnWaitTime = 2f, maxSpawnWaitTime = 3.5f; 

    private float spawnWaitTime; // 次に障害物を生成する時間
    private float startTime;     // ゲームの開始時間

    private int obstacleTypesCount = 2; // 障害物の種類
    private int obstacleToSpawn;        // 生成する障害物の種類

    private Camera mainCamera;

    private Vector3 obstacleSpawnPos = Vector3.zero; // 障害物を生成する位置

    private GameObject newObstacle; // 新しく生成する障害物

    [SerializeField]
    private List<GameObject> iceRockPool, iceSpikePool; // 障害物のプール

    [SerializeField]
    private int initialObstacleToSpawn = 5; // 初期生成する障害物の数

    private bool increasedSpawnRate = false; // 生成レートを増加させたかどうか

    private void Awake()
    {
        mainCamera = Camera.main; // メインカメラの取得
        startTime = Time.time;    // ゲーム開始時刻を記録

        // 障害物のプールを生成
        GenerateObstacles();
    }

    // Updateメソッドで毎フレーム実行
    void Update()
    {
        ObstacleSpawning();

        // 40秒を過ぎたら生成レートを増加させる
        if (!increasedSpawnRate && Time.time - startTime > 40f)
        {
            increasedSpawnRate = true;
            minSpawnWaitTime = 1.5f;
            maxSpawnWaitTime = 2.5f;
        }
    }

    // 障害物のプールを生成するメソッド
    void GenerateObstacles()
    {
        // 各種類の障害物のプールを生成
        for (int i = 0; i < obstacleTypesCount; i++)
        {
            SpawnObstacles(i);
        }
    }

    // 障害物の種類ごとにプールを生成するメソッド
    void SpawnObstacles(int obstacleType)
    {
        switch (obstacleType)
        {
            case 0:
                // アイスロックのプールを生成
                for (int i = 0; i < initialObstacleToSpawn; i++)
                {
                    newObstacle = Instantiate(iceRockPrefab);
                    newObstacle.transform.SetParent(transform);
                    iceRockPool.Add(newObstacle);
                    newObstacle.SetActive(false);
                }
                break;

            case 1:
                // アイススパイクのプールを生成
                for (int i = 0; i < initialObstacleToSpawn; i++)
                {
                    newObstacle = Instantiate(iceSpikePrefab);
                    newObstacle.transform.SetParent(transform);
                    iceSpikePool.Add(newObstacle);
                    newObstacle.SetActive(false);
                }
                break;
        }
    }

    // 障害物の生成を行うメソッド
    void ObstacleSpawning()
    {
        if (Time.time > spawnWaitTime)
        {
            SpawnObstacle();
            spawnWaitTime = Time.time + Random.Range(minSpawnWaitTime, maxSpawnWaitTime);
        }
    }

    // ランダムな位置に障害物を生成するメソッド
    void SpawnObstacle()
    {
        obstacleToSpawn = Random.Range(0, obstacleTypesCount);

        // 障害物の生成位置を設定
        obstacleSpawnPos.x = mainCamera.transform.position.x + 20f;

        switch (obstacleToSpawn)
        {
            case 0:
                // 非アクティブなアイスロックをプールから取得して設定
                for (int i = 0; i < iceRockPool.Count; i++)
                {
                    if (!iceRockPool[i].activeInHierarchy)
                    {
                        iceRockPool[i].SetActive(true);
                        obstacleSpawnPos.y = iceRockYPos;
                        newObstacle = iceRockPool[i];
                        break;
                    }
                }
                break;

            case 1:
                // 非アクティブなアイススパイクをプールから取得して設定
                for (int i = 0; i < iceSpikePool.Count; i++)
                {
                    if (!iceSpikePool[i].activeInHierarchy)
                    {
                        iceSpikePool[i].SetActive(true);
                        obstacleSpawnPos.y = iceSpikeYPos;
                        newObstacle = iceSpikePool[i];
                        break;
                    }
                }
                break;
        }

        // 障害物の位置を設定
        newObstacle.transform.position = obstacleSpawnPos;
    }
}
