using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;  // 生成するアイテムのプレハブの配列
    private int currentItemIndex = 0; // 現在のアイテムのインデックス
    private List<GameObject> spawnedItems = new List<GameObject>(); // 生成されたアイテムのリスト

    public Image[] itemIcons;   // 生成できるアイテムのアイコンを表示するImageの配列
    public Color selectedColor; // 選択されたアイテムのアイコンを光らせるための色

    void Update()
    {
        // マウス入力の処理
        HandleMouseInput(); 
    }

    // マウス入力の処理を行うメソッド
    private void HandleMouseInput()
    {
        // マウスの左クリックが押されたかどうかをチェック
        if (Input.GetMouseButtonDown(0))
        {
            // マウスのクリック位置をスクリーン座標からワールド座標に変換
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // カメラとの距離を設定
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // アイテムを生成する
            SpawnItem(worldPosition);
        }

        // マウスの右クリックが押されたかどうかをチェックして、アイテムを切り替える
        if (Input.GetMouseButtonDown(1))
        {
            // 現在のアイテムのインデックスを更新し、配列内でループするようにする
            currentItemIndex = (currentItemIndex + 1) % itemPrefabs.Length;
            Debug.Log("Current item switched to: " + itemPrefabs[currentItemIndex].name);

            // 選択されたアイテムのアイコンを光らせる
            HighlightSelectedItemIcon(currentItemIndex);
        }
    }

    // アイテムを指定された位置に生成するメソッド
    public void SpawnItem(Vector3 spawnPosition)
    {
        // アイテムのプレハブが指定されていない場合はエラーを出力して処理を終了する
        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogError("Item prefabs are not assigned!");
            return;
        }

        // アイテムを生成する
        GameObject newItem = Instantiate(itemPrefabs[currentItemIndex], spawnPosition, Quaternion.identity);

        // 生成されたアイテムをリストに追加する
        spawnedItems.Add(newItem);

        // 生成されたアイテムの数が5つ以上の場合は、最も古いアイテムを削除する
        if (spawnedItems.Count > 5)
        {
            GameObject oldestItem = spawnedItems[0];
            spawnedItems.RemoveAt(0);
            Destroy(oldestItem);
            Debug.Log("Oldest item destroyed.");
        }

        Debug.Log("Item spawned at: " + spawnPosition);
    }

    // 選択されたアイテムのアイコンを光らせるメソッド
    private void HighlightSelectedItemIcon(int selectedIndex)
    {
        for (int i = 0; i < itemIcons.Length; i++)
        {
            if (i == selectedIndex)
            {
                // 選択されたアイテムのアイコンを光らせる色に変更する
                itemIcons[i].color = selectedColor;
            }
            else
            {
                // 選択されていないアイテムのアイコンの色を通常の色に戻す
                itemIcons[i].color = Color.white;
            }
        }
    }
}
