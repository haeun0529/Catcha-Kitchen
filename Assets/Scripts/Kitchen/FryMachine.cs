using UnityEngine;

public class FryMachine : Interactable
{
    private bool isCooking = false;
    private float cookTime = 6f;
    private float currentTime = 0f;
    private bool isReady = false;

    [Header("UI")]
    public FryMachineUI machineUI;

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
            if (heldItem == "튀김")
                StartCooking();
        }
        else if (isReady)
        {
            TakeFood();
        }
    }

    void StartCooking()
    {
        isCooking = true;
        currentTime = cookTime;
        PlayerInteraction.Instance.DropItem();
        machineUI.ShowTimer(cookTime);
        Debug.Log("튀김 조리 시작!");
    }

    void TakeFood()
    {
        isReady = false;
        PlayerInteraction.Instance.SetHeldItem("튀김완성");
        machineUI.HideTimer();
        Debug.Log("튀김 완성!");
    }
}