using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    // ��Q���̃v���n�u
    private GameObject iceRockPrefab, iceSpikePrefab; 

    [SerializeField]
    // ��Q����Y���W
    private float iceRockYPos = -3.5f, iceSpikeYPos = -3.5f; 

    [SerializeField]
    // ��Q�������̑ҋ@����
    private float minSpawnWaitTime = 2f, maxSpawnWaitTime = 3.5f; 

    private float spawnWaitTime; // ���ɏ�Q���𐶐����鎞��
    private float startTime;     // �Q�[���̊J�n����

    private int obstacleTypesCount = 2; // ��Q���̎��
    private int obstacleToSpawn;        // ���������Q���̎��

    private Camera mainCamera;

    private Vector3 obstacleSpawnPos = Vector3.zero; // ��Q���𐶐�����ʒu

    private GameObject newObstacle; // �V�������������Q��

    [SerializeField]
    private List<GameObject> iceRockPool, iceSpikePool; // ��Q���̃v�[��

    [SerializeField]
    private int initialObstacleToSpawn = 5; // �������������Q���̐�

    private bool increasedSpawnRate = false; // �������[�g�𑝉����������ǂ���

    private void Awake()
    {
        mainCamera = Camera.main; // ���C���J�����̎擾
        startTime = Time.time;    // �Q�[���J�n�������L�^

        // ��Q���̃v�[���𐶐�
        GenerateObstacles();
    }

    // Update���\�b�h�Ŗ��t���[�����s
    void Update()
    {
        ObstacleSpawning();

        // 40�b���߂����琶�����[�g�𑝉�������
        if (!increasedSpawnRate && Time.time - startTime > 40f)
        {
            increasedSpawnRate = true;
            minSpawnWaitTime = 1.5f;
            maxSpawnWaitTime = 2.5f;
        }
    }

    // ��Q���̃v�[���𐶐����郁�\�b�h
    void GenerateObstacles()
    {
        // �e��ނ̏�Q���̃v�[���𐶐�
        for (int i = 0; i < obstacleTypesCount; i++)
        {
            SpawnObstacles(i);
        }
    }

    // ��Q���̎�ނ��ƂɃv�[���𐶐����郁�\�b�h
    void SpawnObstacles(int obstacleType)
    {
        switch (obstacleType)
        {
            case 0:
                // �A�C�X���b�N�̃v�[���𐶐�
                for (int i = 0; i < initialObstacleToSpawn; i++)
                {
                    newObstacle = Instantiate(iceRockPrefab);
                    newObstacle.transform.SetParent(transform);
                    iceRockPool.Add(newObstacle);
                    newObstacle.SetActive(false);
                }
                break;

            case 1:
                // �A�C�X�X�p�C�N�̃v�[���𐶐�
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

    // ��Q���̐������s�����\�b�h
    void ObstacleSpawning()
    {
        if (Time.time > spawnWaitTime)
        {
            SpawnObstacle();
            spawnWaitTime = Time.time + Random.Range(minSpawnWaitTime, maxSpawnWaitTime);
        }
    }

    // �����_���Ȉʒu�ɏ�Q���𐶐����郁�\�b�h
    void SpawnObstacle()
    {
        obstacleToSpawn = Random.Range(0, obstacleTypesCount);

        // ��Q���̐����ʒu��ݒ�
        obstacleSpawnPos.x = mainCamera.transform.position.x + 20f;

        switch (obstacleToSpawn)
        {
            case 0:
                // ��A�N�e�B�u�ȃA�C�X���b�N���v�[������擾���Đݒ�
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
                // ��A�N�e�B�u�ȃA�C�X�X�p�C�N���v�[������擾���Đݒ�
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

        // ��Q���̈ʒu��ݒ�
        newObstacle.transform.position = obstacleSpawnPos;
    }
}
