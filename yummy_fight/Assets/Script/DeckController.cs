using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    // CardEntity�I�u�W�F�N�g�̃��X�g
    public List<CardEntity> deck = new List<CardEntity>();

    // �f�b�L�ɃJ�[�h��ǉ����郁�\�b�h
    public void AddCardToDeck(CardEntity card)
    {
        if (card != null)
        {
            deck.Add(card);
            // �����ŁA�K�v�ɉ�����UI�̍X�V�₻�̑��̏������s�����Ƃ��ł��܂�
        }
    }

    // ����̃J�[�h�����擾���郁�\�b�h
    public CardEntity GetCardEntity(int index)
    {
        if (index >= 0 && index < deck.Count)
        {
            return deck[index];
        }
        else
        {
            return null; // �͈͊O�̏ꍇ��null��Ԃ�
        }
    }
}