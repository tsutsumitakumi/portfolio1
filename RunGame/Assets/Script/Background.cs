using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private GameObject groundPrefab;

    // 最初にスポーンする地面の数
    [SerializeField]
    private int initialGroundCount = 10;

    // 地面オブジェクトのプール
    private List<GameObject> groundPool = new List<GameObject>(); 

    private const float GROUND_Y_POS = 0f;       // 地面のY座標位置
    private const float GROUND_X_DISTANCE = 24f; // 地面同士のX軸間隔

    // 次に地面を配置するX座標位置
    private float nextGroundXPos; 

    [SerializeField]
    // 新しい地面を生成するまでの待ち時間
    private float generateGroundInterval = 7f; 

    void Start()
    {
        GenerateInitialGround();                   // 初期の地面を生成
        StartCoroutine(GenerateGroundCoroutine()); // 地面生成コルーチンを開始
    }

    // 初期の地面を生成するメソッド
    void GenerateInitialGround()
    {
        for (int i = 0; i < initialGroundCount; i++)
        {
            CreateNewGround();
        }
    }

    // 新しい地面を生成するコルーチン
    IEnumerator GenerateGroundCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(generateGroundInterval);
            ActivateNewGround(); // 新しい地面を生成
        }
    }

    // 非アクティブな地面オブジェクトを再利用して新しい地面を配置するメソッド
    void ActivateNewGround()
    {
        GameObject inactiveGround = groundPool.Find(obj => !obj.activeInHierarchy);

        if (inactiveGround != null)
        {
            SetGroundPosition(inactiveGround);
            inactiveGround.SetActive(true); // オブジェクトをアクティブにする
        }
        else
        {
            CreateNewGround(); // プールに利用可能なオブジェクトがない場合は新しい地面を生成
        }
    }

    // 新しい地面を生成してプールに追加するメソッド
    void CreateNewGround()
    {
        Vector3 groundPosition = new Vector3(nextGroundXPos, GROUND_Y_POS, 0f);
        GameObject newGround = Instantiate(groundPrefab, groundPosition, Quaternion.identity);
        newGround.transform.SetParent(transform); // このオブジェクトの子として設定

        groundPool.Add(newGround); // プールに追加
        nextGroundXPos += GROUND_X_DISTANCE; // 次の地面のX座標を更新
    }

    // 地面の位置を設定するメソッド
    void SetGroundPosition(GameObject ground)
    {
        ground.transform.position = new Vector3(nextGroundXPos, GROUND_Y_POS, 0f);
        nextGroundXPos += GROUND_X_DISTANCE; // 次の地面のX座標を更新
    }
}
