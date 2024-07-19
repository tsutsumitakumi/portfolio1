using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhaseManager : MonoBehaviour
{
    public TextMeshProUGUI phaseText; // UIテキストをアサインするためのパブリック変数

    // フェーズを示す列挙型
    public enum GamePhase
    {
        MainPhase,
        BattlePhase,
        // 必要に応じて他のフェーズを追加
    }

    // 現在のフェーズを設定します。最初はメインフェーズとします。
    private GamePhase currentPhase = GamePhase.MainPhase;

    // フェーズを変更するメソッド
    public void ChangePhase(GamePhase newPhase)
    {
        currentPhase = newPhase;
        UpdatePhaseText();
    }

    // UIテキストを更新するメソッド
    void UpdatePhaseText()
    {
        switch (currentPhase)
        {
            case GamePhase.MainPhase:
                phaseText.text = "メインフェーズ";
                break;
            case GamePhase.BattlePhase:
                phaseText.text = "バトルフェーズ";
                break;
                // 必要に応じて他のフェーズの処理を追加
        }
    }
}