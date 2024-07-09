using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItem : MonoBehaviour
{
    public int Cost;
    [SerializeField] private Image background;
    [SerializeField] protected Image image;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text textCost;

    private Color canBuyColor = new Color(.3f, 1, .3f);
    private Color cantBuyColor = new Color(1, .3f, .3f);

    private void Awake()
    {
        textCost.text = $"${Cost}";
    }

    public void CheckCanBuy(int money)
    {
        background.color = money >= Cost ? canBuyColor : cantBuyColor;
    }

    public virtual void Buy(Buyer buyer)
    {
        if (buyer.Money < Cost) return;//check if it works also on the inherited

        buyer.Money -= Cost;
    }
}
