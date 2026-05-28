using UnityEngine;
using TMPro;

public class FryerMachineUI : MonoBehaviour
{
    public GameObject timerPanel;
    public TextMeshProUGUI timerText;
    public Transform machine;
    public Vector3 offset = new Vector3(0, 100f, 0);

    void Update()
    {
        if (timerPanel.activeSelf && machine != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(
                machine.position + new Vector3(0, 2f, 0));
            timerPanel.transform.position = screenPos + offset;
        }
    }

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