using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private TMP_Text changedText;
    [Space]
    [SerializeField] private Color GainColor;
    [SerializeField] private Color LoseColor;

    private int totalDelta;

    private void Awake()
    {
        Event<PlayerMoneyChangedEvent>.OnEvent += MoneyChanged;
    }

    private void MoneyChanged(PlayerMoneyChangedEvent evt)
    {
        if (evt.delta == 0) return;
        mainText.text = evt.newValue.ToString();
        totalDelta += evt.delta;
        changedText.text = $"{(totalDelta > 0 ? "+" : "-")}{Mathf.Abs(totalDelta)}";
        changedText.color = totalDelta > 0f ? GainColor : LoseColor;
        StopAllCoroutines();
        StartCoroutine(ChangePopUp());
    }

    private IEnumerator ChangePopUp()
    {
        Color color = changedText.color;
        color.a = 1f;

        while (color.a > 0)
        {
            changedText.color = color;
            yield return null;
            color.a -= Time.deltaTime;
        }

        totalDelta = 0;
    }
}
