using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;  // ��������A�C�e���̃v���n�u�̔z��
    private int currentItemIndex = 0; // ���݂̃A�C�e���̃C���f�b�N�X
    private List<GameObject> spawnedItems = new List<GameObject>(); // �������ꂽ�A�C�e���̃��X�g

    public Image[] itemIcons;   // �����ł���A�C�e���̃A�C�R����\������Image�̔z��
    public Color selectedColor; // �I�����ꂽ�A�C�e���̃A�C�R�������点�邽�߂̐F

    void Update()
    {
        // �}�E�X���͂̏���
        HandleMouseInput(); 
    }

    // �}�E�X���͂̏������s�����\�b�h
    private void HandleMouseInput()
    {
        // �}�E�X�̍��N���b�N�������ꂽ���ǂ������`�F�b�N
        if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X�̃N���b�N�ʒu���X�N���[�����W���烏�[���h���W�ɕϊ�
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // �J�����Ƃ̋�����ݒ�
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // �A�C�e���𐶐�����
            SpawnItem(worldPosition);
        }

        // �}�E�X�̉E�N���b�N�������ꂽ���ǂ������`�F�b�N���āA�A�C�e����؂�ւ���
        if (Input.GetMouseButtonDown(1))
        {
            // ���݂̃A�C�e���̃C���f�b�N�X���X�V���A�z����Ń��[�v����悤�ɂ���
            currentItemIndex = (currentItemIndex + 1) % itemPrefabs.Length;
            Debug.Log("Current item switched to: " + itemPrefabs[currentItemIndex].name);

            // �I�����ꂽ�A�C�e���̃A�C�R�������点��
            HighlightSelectedItemIcon(currentItemIndex);
        }
    }

    // �A�C�e�����w�肳�ꂽ�ʒu�ɐ������郁�\�b�h
    public void SpawnItem(Vector3 spawnPosition)
    {
        // �A�C�e���̃v���n�u���w�肳��Ă��Ȃ��ꍇ�̓G���[���o�͂��ď������I������
        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogError("Item prefabs are not assigned!");
            return;
        }

        // �A�C�e���𐶐�����
        GameObject newItem = Instantiate(itemPrefabs[currentItemIndex], spawnPosition, Quaternion.identity);

        // �������ꂽ�A�C�e�������X�g�ɒǉ�����
        spawnedItems.Add(newItem);

        // �������ꂽ�A�C�e���̐���5�ȏ�̏ꍇ�́A�ł��Â��A�C�e�����폜����
        if (spawnedItems.Count > 5)
        {
            GameObject oldestItem = spawnedItems[0];
            spawnedItems.RemoveAt(0);
            Destroy(oldestItem);
            Debug.Log("Oldest item destroyed.");
        }

        Debug.Log("Item spawned at: " + spawnPosition);
    }

    // �I�����ꂽ�A�C�e���̃A�C�R�������点�郁�\�b�h
    private void HighlightSelectedItemIcon(int selectedIndex)
    {
        for (int i = 0; i < itemIcons.Length; i++)
        {
            if (i == selectedIndex)
            {
                // �I�����ꂽ�A�C�e���̃A�C�R�������点��F�ɕύX����
                itemIcons[i].color = selectedColor;
            }
            else
            {
                // �I������Ă��Ȃ��A�C�e���̃A�C�R���̐F��ʏ�̐F�ɖ߂�
                itemIcons[i].color = Color.white;
            }
        }
    }
}
