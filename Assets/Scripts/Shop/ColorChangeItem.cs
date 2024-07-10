using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeItem : ShopItem
{
    public Color color;

    protected override void Awake()
    {
        base.Awake();
        image.color = color;
    }

    public override bool Buy(Buyer buyer)
    {
        if (!base.Buy(buyer)) return false;

        Renderer renderer = buyer.GetComponentInChildren<Renderer>();
        if(renderer)
        {
            renderer.material.color = color;
            return true;
        }
        buyer.Money += Cost;
        return false;
    }
}
