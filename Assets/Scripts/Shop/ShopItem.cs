using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItem : MonoBehaviour
{
    public int Cost;
    public bool permanent;
    [SerializeField] protected Image background;
    [SerializeField] protected Image image;
    [SerializeField] protected TMP_Text description;
    [SerializeField] protected TMP_Text textCost;

    protected Color canBuyColor = new Color(.1f, 1, .1f);
    protected Color cantBuyColor = new Color(1, .1f, .1f);

    protected virtual void Awake()
    {
        textCost.text = $"${Cost}";
    }

    public void CheckCanBuy(int money)
    {
        Color color = money >= Cost ? canBuyColor : cantBuyColor;
        background.color = color;
    }

    public virtual bool Buy(Buyer buyer)
    {
        if (buyer.Money < Cost) return false;

        buyer.Money -= Cost;
        
        return true;
    }
}
