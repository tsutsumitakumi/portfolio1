using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private GameObject groundPrefab;

    // �ŏ��ɃX�|�[������n�ʂ̐�
    [SerializeField]
    private int initialGroundCount = 10;

    // �n�ʃI�u�W�F�N�g�̃v�[��
    private List<GameObject> groundPool = new List<GameObject>(); 

    private const float GROUND_Y_POS = 0f;       // �n�ʂ�Y���W�ʒu
    private const float GROUND_X_DISTANCE = 24f; // �n�ʓ��m��X���Ԋu

    // ���ɒn�ʂ�z�u����X���W�ʒu
    private float nextGroundXPos; 

    [SerializeField]
    // �V�����n�ʂ𐶐�����܂ł̑҂�����
    private float generateGroundInterval = 7f; 

    void Start()
    {
        GenerateInitialGround();                   // �����̒n�ʂ𐶐�
        StartCoroutine(GenerateGroundCoroutine()); // �n�ʐ����R���[�`�����J�n
    }

    // �����̒n�ʂ𐶐����郁�\�b�h
    void GenerateInitialGround()
    {
        for (int i = 0; i < initialGroundCount; i++)
        {
            CreateNewGround();
        }
    }

    // �V�����n�ʂ𐶐�����R���[�`��
    IEnumerator GenerateGroundCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(generateGroundInterval);
            ActivateNewGround(); // �V�����n�ʂ𐶐�
        }
    }

    // ��A�N�e�B�u�Ȓn�ʃI�u�W�F�N�g���ė��p���ĐV�����n�ʂ�z�u���郁�\�b�h
    void ActivateNewGround()
    {
        GameObject inactiveGround = groundPool.Find(obj => !obj.activeInHierarchy);

        if (inactiveGround != null)
        {
            SetGroundPosition(inactiveGround);
            inactiveGround.SetActive(true); // �I�u�W�F�N�g���A�N�e�B�u�ɂ���
        }
        else
        {
            CreateNewGround(); // �v�[���ɗ��p�\�ȃI�u�W�F�N�g���Ȃ��ꍇ�͐V�����n�ʂ𐶐�
        }
    }

    // �V�����n�ʂ𐶐����ăv�[���ɒǉ����郁�\�b�h
    void CreateNewGround()
    {
        Vector3 groundPosition = new Vector3(nextGroundXPos, GROUND_Y_POS, 0f);
        GameObject newGround = Instantiate(groundPrefab, groundPosition, Quaternion.identity);
        newGround.transform.SetParent(transform); // ���̃I�u�W�F�N�g�̎q�Ƃ��Đݒ�

        groundPool.Add(newGround); // �v�[���ɒǉ�
        nextGroundXPos += GROUND_X_DISTANCE; // ���̒n�ʂ�X���W���X�V
    }

    // �n�ʂ̈ʒu��ݒ肷�郁�\�b�h
    void SetGroundPosition(GameObject ground)
    {
        ground.transform.position = new Vector3(nextGroundXPos, GROUND_Y_POS, 0f);
        nextGroundXPos += GROUND_X_DISTANCE; // ���̒n�ʂ�X���W���X�V
    }
}
