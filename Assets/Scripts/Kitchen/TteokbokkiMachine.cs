using UnityEngine;

public class TteokbokkiMachine : Interactable
{
    private bool isCooking = false;
    private float cookTime = 6f;
    private float currentTime = 0f;
    private bool isReady = false;

    [Header("UI")]
    public TteokbokkiMachineUI machineUI;

    [Header("모델")]
    public GameObject modelBasic;
    public GameObject modelTteok;
    public GameObject modelGochujang;
    public GameObject modelCooking;

    private string[] ingredients = new string[2];
    private int ingredientCount = 0;

    void Start()
    {
        ShowModel(modelBasic);
    }

    void Update()
    {
        if (isCooking)
        {
            currentTime -= Time.deltaTime;
            machineUI.UpdateTimer(currentTime);

            if (currentTime <= 0f)
            {
                isCooking = false;
                isReady = true;
                currentTime = 0f;
                machineUI.ShowReady();
            }
        }
    }

    public override void Interact()
    {
        string heldItem = PlayerInteraction.Instance.heldItem;

        if (!isCooking && !isReady)
        {
            if (heldItem == "떡" || heldItem == "고추장")
                AddIngredient(heldItem);
        }
        else if (isReady)
        {
            TakeFood();
        }
    }

    void AddIngredient(string item)
    {
        foreach (string i in ingredients)
            if (i == item) return;

        ingredients[ingredientCount] = item;
        ingredientCount++;
        PlayerInteraction.Instance.DropItem();

        UpdateModel();
        Debug.Log($"재료 추가: {item} ({ingredientCount}/2)");

        if (ingredientCount >= 2)
            StartCooking();
    }

    void UpdateModel()
    {
        if (ingredientCount == 1)
        {
            if (ingredients[0] == "떡")
                ShowModel(modelTteok);
            else if (ingredients[0] == "고추장")
                ShowModel(modelGochujang);
        }
    }

    void StartCooking()
    {
        isCooking = true;
        currentTime = cookTime;
        ShowModel(modelCooking);
        machineUI.ShowTimer(cookTime);
        Debug.Log("떡볶이 조리 시작!");
    }

    void TakeFood()
    {
        isReady = false;
        ingredientCount = 0;
        ingredients = new string[2];
        ShowModel(modelBasic);
        PlayerInteraction.Instance.SetHeldItem("떡볶이");
        machineUI.HideTimer();
        Debug.Log("떡볶이 완성!");
    }

    void ShowModel(GameObject target)
    {
        modelBasic.SetActive(target == modelBasic);
        modelTteok.SetActive(target == modelTteok);
        modelGochujang.SetActive(target == modelGochujang);
        modelCooking.SetActive(target == modelCooking);
    }
}