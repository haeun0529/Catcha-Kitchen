using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomerUI : MonoBehaviour
{
    public GameObject speechBubble;
    public Image timerBar;

    [Header("주문 아이콘 슬롯")]
    public Image[] orderIcons;

    private Color colorTteokbokki = new Color(0.9f, 0.2f, 0.2f); // 빨간색
    private Color colorOdeng = new Color(1f, 0.6f, 0.1f);        // 주황색
    private Color colorFry = new Color(1f, 0.9f, 0.2f);          // 노란색

    public void ShowOrders(List<string> orders)
    {
        speechBubble.SetActive(true);

        foreach (var icon in orderIcons)
            icon.gameObject.SetActive(false);

        for (int i = 0; i < orders.Count && i < orderIcons.Length; i++)
        {
            orderIcons[i].gameObject.SetActive(true);
            orderIcons[i].color = GetColor(orders[i]);
        }
    }

    Color GetColor(string itemName)
    {
        switch (itemName)
        {
            case "떡볶이": return colorTteokbokki;
            case "오뎅완성": return colorOdeng;
            case "튀김완성": return colorFry;
            default: return Color.white;
        }
    }

    public void UpdateTimer(float ratio)
    {
        timerBar.fillAmount = ratio;
        timerBar.color = ratio > 0.3f ?
            new Color(0.2f, 0.8f, 0.2f) :
            new Color(0.9f, 0.2f, 0.2f);
    }

    public void HideUI()
    {
        speechBubble.SetActive(false);
    }
}