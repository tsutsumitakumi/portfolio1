using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinToss : MonoBehaviour
{
    private Rigidbody coinRigidbody;
    private bool isTossed = false;
    private Animator coinAnimator;

    public AnimationClip headsAnimation;
    public AnimationClip tailsAnimation;

    private bool CoinCheckO;
    private bool CoinCheckU;

    void Start()
    {
        coinRigidbody = GetComponent<Rigidbody>();
        coinAnimator = GetComponent<Animator>();
        CoinCheckO = false;
        CoinCheckU = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) /*&& !isTossed*/)
        {
            TossCoin();
        }

        // コインが停止したら結果を表示
        if (isTossed && coinRigidbody.IsSleeping())

        {
            DetermineFirstTurn();
        }
    }

    void TossCoin()
    {
        // コインにランダムな回転速度を与える
        coinRigidbody.angularVelocity = Random.onUnitSphere * Random.Range(5f, 10f);

        isTossed = true;
    }

    void DetermineFirstTurn()
    {
        // コインの回転軸を検知して、先攻後攻を決定するロジックを追加
        Vector3 upDirection = transform.up;

        if (Vector3.Dot(upDirection, Vector3.up) > 0.5f)
        {
            Debug.Log("Player 1が先攻！");
            CoinCheckO = true;
            PlayHeadsAnimation();
            // 先攻のプレイヤーに関連する処理をここに追加
        }
        else
        {
            Debug.Log("Player 2が先攻！");
            CoinCheckU = true;
            PlayTailsAnimation();
            // 後攻のプレイヤーに関連する処理をここに追加
        }

        isTossed = false;
    }

    void PlayHeadsAnimation()
    {
        // Animator コンポーネントがアタッチされているかを確認
        Animator coinAnimator = GetComponent<Animator>();
        if (coinAnimator != null)
        {
            // アタッチされていればアニメーションを再生する
            coinAnimator.Play(headsAnimation.name);
        }
        else
        {
            Debug.LogError("Animator is not attached to the 'Coin tos' game object!");
        }
    }

    void PlayTailsAnimation()
    {
        coinAnimator.Play(tailsAnimation.name);
    }
}