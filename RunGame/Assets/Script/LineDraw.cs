using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    [SerializeField] private LineRenderer _rend; // LineRendererコンポーネント
    [SerializeField] private Camera _cam; // 使用するカメラ
    private int posCount = 0; // 現在のポイントの数
    private float interval = 0.1f; // ポイント間の最小距離

    private void Update()
    {
        // マウスの現在のワールド座標を取得
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        // 左クリックを押している間、ポイントを追加
        if (Input.GetMouseButton(0))
        {
            SetPosition(mousePos);
        }
        // 左クリックを離したら、ポイント数をリセット
        else if (Input.GetMouseButtonUp(0))
        {
            posCount = 0;
        }
    }

    // 新しいポイントを設定するメソッド
    private void SetPosition(Vector2 pos)
    {
        // ポイントが一定距離離れていない場合、処理を終了
        if (!PosCheck(pos)) return;

        // 新しいポイントを追加
        posCount++;
        _rend.positionCount = posCount;
        _rend.SetPosition(posCount - 1, pos);
    }

    // 新しいポイントが前のポイントから一定距離離れているかをチェックするメソッド
    private bool PosCheck(Vector2 pos)
    {
        // 最初のポイントは常に許可
        if (posCount == 0) return true;

        // 前のポイントとの距離を計算
        float distance = Vector2.Distance(_rend.GetPosition(posCount - 1), pos);

        // 一定距離以上なら許可、それ以外は拒否
        return distance > interval;
    }
}
