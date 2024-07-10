using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private TMP_Text moneyText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Buyer>(out var buyer))
        {
            if(other.TryGetComponent<PlayerCarry>(out var player))
            {
                buyer.Money += player.ClearCarriers();
                moneyText.text = buyer.Money.ToString();
            }
        }
    }
}
