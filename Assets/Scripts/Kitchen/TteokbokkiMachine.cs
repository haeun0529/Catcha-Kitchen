using UnityEngine;
using UnityEngine.InputSystem;

public class TteokbokkiMachine : MonoBehaviour
{
    private bool playerNearby = false;
    private bool isCooking = false;
    private float cookTime = 6f;
    private float currentTime = 0f;
    private bool isReady = false;

    [Header("UI")]
    public TteokbokkiMachineUI machineUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
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

        if (playerNearby && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            string heldItem = PlayerInteraction.Instance.heldItem;

            if (!isCooking && !isReady)
            {
                if (heldItem == "떡" || heldItem == "고추장")
                {
                    AddIngredient(heldItem);
                }
            }
            else if (isReady)
            {
                TakeFood();
            }
        }
    }

    private string[] ingredients = new string[2];
    private int ingredientCount = 0;

    void AddIngredient(string item)
    {
        foreach (string i in ingredients)
            if (i == item) return;

        ingredients[ingredientCount] = item;
        ingredientCount++;
        PlayerInteraction.Instance.DropItem();

        Debug.Log($"재료 추가: {item} ({ingredientCount}/2)");

        if (ingredientCount >= 2)
            StartCooking();
    }

    void StartCooking()
    {
        isCooking = true;
        currentTime = cookTime;
        machineUI.ShowTimer(cookTime);
        Debug.Log("떡볶이 조리 시작!");
    }

    void TakeFood()
    {
        isReady = false;
        ingredientCount = 0;
        ingredients = new string[2];
        PlayerInteraction.Instance.SetHeldItem("떡볶이");
        machineUI.HideTimer();
        Debug.Log("떡볶이 완성!");
    }
}