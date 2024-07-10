using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private List<ShopItem> shopItems;

    private Buyer currentBuyer;

    private void Awake()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            ShopItem shopItem = shopItems[i];
            shopItems[i].GetComponent<Button>().onClick.AddListener(() => BuyItem(shopItem));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Buyer>(out var buyer))
        {
            if(other.TryGetComponent<PlayerCarry>(out var player))
            {
                int carried = player.ClearCarriers();
                buyer.Money += carried;
                PlayerMoneyChangedEvent evt = new(buyer.Money, carried);
                Event<PlayerMoneyChangedEvent>.CallEvent(evt);
                currentBuyer = buyer;
                OpenShop();
            }
        }
    }

    private void OpenShop()
    {
        shopUI.gameObject.SetActive(true);
        CheckPrices();
    }


    public void CloseShop()
    {
        shopUI.gameObject.SetActive(false);
    }

    private void CheckPrices()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].CheckCanBuy(currentBuyer.Money);
        }
    }

    private void BuyItem(ShopItem item)
    {
        if (!shopItems.Contains(item)) return;
        PlayerMoneyChangedEvent evt = new(currentBuyer.Money, -item.Cost);
        if (item.Buy(currentBuyer))
        {
            evt.newValue = currentBuyer.Money;
            Event<PlayerMoneyChangedEvent>.CallEvent(evt);
            if (!item.permanent)
            {
                shopItems.Remove(item);
                Destroy(item.gameObject);
            }
            CheckPrices();
        }
    }
}