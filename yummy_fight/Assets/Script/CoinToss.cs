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

        // �R�C������~�����猋�ʂ�\��
        if (isTossed && coinRigidbody.IsSleeping())

        {
            DetermineFirstTurn();
        }
    }

    void TossCoin()
    {
        // �R�C���Ƀ����_���ȉ�]���x��^����
        coinRigidbody.angularVelocity = Random.onUnitSphere * Random.Range(5f, 10f);

        isTossed = true;
    }

    void DetermineFirstTurn()
    {
        // �R�C���̉�]�������m���āA��U��U�����肷�郍�W�b�N��ǉ�
        Vector3 upDirection = transform.up;

        if (Vector3.Dot(upDirection, Vector3.up) > 0.5f)
        {
            Debug.Log("Player 1����U�I");
            CoinCheckO = true;
            PlayHeadsAnimation();
            // ��U�̃v���C���[�Ɋ֘A���鏈���������ɒǉ�
        }
        else
        {
            Debug.Log("Player 2����U�I");
            CoinCheckU = true;
            PlayTailsAnimation();
            // ��U�̃v���C���[�Ɋ֘A���鏈���������ɒǉ�
        }

        isTossed = false;
    }

    void PlayHeadsAnimation()
    {
        // Animator �R���|�[�l���g���A�^�b�`����Ă��邩���m�F
        Animator coinAnimator = GetComponent<Animator>();
        if (coinAnimator != null)
        {
            // �A�^�b�`����Ă���΃A�j���[�V�������Đ�����
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