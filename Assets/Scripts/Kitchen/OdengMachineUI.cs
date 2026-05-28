using UnityEngine;
using TMPro;

public class OdengMachineUI : MonoBehaviour
{
    public GameObject timerPanel;
    public TextMeshProUGUI timerText;

    public void ShowTimer(float time)
    {
        timerPanel.SetActive(true);
        timerText.text = Mathf.Ceil(time).ToString();
    }

    public void UpdateTimer(float time)
    {
        timerText.text = Mathf.Ceil(time).ToString();
    }

    public void ShowReady()
    {
        timerText.text = "완성!";
    }

    public void HideTimer()
    {
        timerPanel.SetActive(false);
    }
}