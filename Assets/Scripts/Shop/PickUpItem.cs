using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : ShopItem
{
    public override bool Buy(Buyer buyer)
    {
        if (!base.Buy(buyer)) return false;

        return true;
    }
}
