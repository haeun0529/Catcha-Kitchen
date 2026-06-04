using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomerUI : MonoBehaviour
{
    public GameObject uiPanel;
    public TextMeshProUGUI orderText;
    public Image timerBar;

    public void ShowOrders(List<string> orders)
    {
        uiPanel.SetActive(true);
        orderText.text = string.Join("\n", orders);
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
        uiPanel.SetActive(false);
    }
}