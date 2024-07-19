using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] Image iconImage;
    public int cardID;
    public int power;
    public bool hirou;

    public void Show(CardModel cardModel) // cardModelÇÃÉfÅ[É^éÊìæÇ∆îΩâf
    {
        power = cardModel.power;
        this.gameObject.GetComponent<CardController>().default_power = power;
        powerText.GetComponent<TextMeshProUGUI>().text = power.ToString();
        iconImage.sprite = cardModel.icon;
    }
}