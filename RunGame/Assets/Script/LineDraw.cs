using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    [SerializeField] private LineRenderer _rend; // LineRenderer�R���|�[�l���g
    [SerializeField] private Camera _cam; // �g�p����J����
    private int posCount = 0; // ���݂̃|�C���g�̐�
    private float interval = 0.1f; // �|�C���g�Ԃ̍ŏ�����

    private void Update()
    {
        // �}�E�X�̌��݂̃��[���h���W���擾
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        // ���N���b�N�������Ă���ԁA�|�C���g��ǉ�
        if (Input.GetMouseButton(0))
        {
            SetPosition(mousePos);
        }
        // ���N���b�N�𗣂�����A�|�C���g�������Z�b�g
        else if (Input.GetMouseButtonUp(0))
        {
            posCount = 0;
        }
    }

    // �V�����|�C���g��ݒ肷�郁�\�b�h
    private void SetPosition(Vector2 pos)
    {
        // �|�C���g����苗������Ă��Ȃ��ꍇ�A�������I��
        if (!PosCheck(pos)) return;

        // �V�����|�C���g��ǉ�
        posCount++;
        _rend.positionCount = posCount;
        _rend.SetPosition(posCount - 1, pos);
    }

    // �V�����|�C���g���O�̃|�C���g�����苗������Ă��邩���`�F�b�N���郁�\�b�h
    private bool PosCheck(Vector2 pos)
    {
        // �ŏ��̃|�C���g�͏�ɋ���
        if (posCount == 0) return true;

        // �O�̃|�C���g�Ƃ̋������v�Z
        float distance = Vector2.Distance(_rend.GetPosition(posCount - 1), pos);

        // ��苗���ȏ�Ȃ狖�A����ȊO�͋���
        return distance > interval;
    }
}
