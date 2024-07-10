using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private TMP_Text moneyText;
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
                buyer.Money += player.ClearCarriers();
                moneyText.text = buyer.Money.ToString();
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
        item.Buy(currentBuyer);
        if (!item.permanent)
        {
            shopItems.Remove(item);
            Destroy(item.gameObject);
        }
        CheckPrices();
        moneyText.text = currentBuyer.Money.ToString();
    }
}