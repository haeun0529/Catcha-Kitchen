using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class RefrigeratorUI : MonoBehaviour
{
    public static RefrigeratorUI Instance;

    [Header("선택 UI")]
    public GameObject selectPanel;
    public Image[] itemHighlights;
    public TextMeshProUGUI[] itemLabels;

    private string[] currentItems;
    private int selectedIndex = 0;
    public bool isOpen = false;

    public void PickItemPublic() => PickItem();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (!isOpen) return;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            ChangeSelection(-1);
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            ChangeSelection(1);
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            ChangeSelection(-2);
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            ChangeSelection(2);
    }

    public void ShowItemSelect(string[] items, Transform fridgeTransform)
    {
        currentItems = items;
        selectedIndex = 0;
        isOpen = true;
        selectPanel.SetActive(true);

        for (int i = 0; i < itemLabels.Length; i++)
            itemLabels[i].text = items[i];

        UpdateHighlight();
    }

    public void HideItemSelect()
    {
        isOpen = false;
        selectPanel.SetActive(false);
    }

    void ChangeSelection(int dir)
    {
        selectedIndex = (selectedIndex + dir + currentItems.Length) % currentItems.Length;
        UpdateHighlight();
    }

    void UpdateHighlight()
    {
        for (int i = 0; i < itemHighlights.Length; i++)
            itemHighlights[i].color = (i == selectedIndex)
                ? new Color(1f, 0.9f, 0.3f, 1f)
                : new Color(1f, 1f, 1f, 0.3f);
    }

    void PickItem()
    {
        string picked = currentItems[selectedIndex];
        PlayerInteraction.Instance.SetHeldItem(picked);
        HideItemSelect();
    }
}