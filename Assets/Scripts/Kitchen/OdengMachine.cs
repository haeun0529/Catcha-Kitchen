using UnityEngine;

public class OdengMachine : Interactable
{
    private bool isCooking = false;
    private float cookTime = 6f;
    private float currentTime = 0f;
    private bool isReady = false;

    [Header("UI")]
    public OdengMachineUI machineUI;

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
            if (heldItem == "오뎅")
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
        Debug.Log("오뎅 조리 시작!");
    }

    void TakeFood()
    {
        isReady = false;
        PlayerInteraction.Instance.SetHeldItem("오뎅완성");
        machineUI.HideTimer();
        Debug.Log("오뎅 완성!");
    }
}