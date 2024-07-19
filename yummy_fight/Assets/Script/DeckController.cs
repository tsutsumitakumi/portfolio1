using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    // CardEntityオブジェクトのリスト
    public List<CardEntity> deck = new List<CardEntity>();

    // デッキにカードを追加するメソッド
    public void AddCardToDeck(CardEntity card)
    {
        if (card != null)
        {
            deck.Add(card);
            // ここで、必要に応じてUIの更新やその他の処理を行うことができます
        }
    }

    // 特定のカード情報を取得するメソッド
    public CardEntity GetCardEntity(int index)
    {
        if (index >= 0 && index < deck.Count)
        {
            return deck[index];
        }
        else
        {
            return null; // 範囲外の場合はnullを返す
        }
    }
}