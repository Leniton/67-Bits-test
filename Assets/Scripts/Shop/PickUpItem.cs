using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : ShopItem
{
    public override bool Buy(Buyer buyer)
    {
        if (!base.Buy(buyer)) return false;

        if(buyer.TryGetComponent<PlayerCarry>(out var player))
        {
            player.maxPickUp++;
        }

        return true;
    }
}