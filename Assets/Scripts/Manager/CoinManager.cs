using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int currentCoin = 0;
    public TextMeshProUGUI coinText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddCoin(int amount)
    {
        currentCoin += amount;
        UpdateUI();
        Debug.Log($"코인 획득: +{amount}, 현재: {currentCoin}");
    }

    void UpdateUI()
    {
        coinText.text = currentCoin.ToString();
    }
}