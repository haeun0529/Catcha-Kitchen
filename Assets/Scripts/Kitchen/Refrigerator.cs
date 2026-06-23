using UnityEngine;
using UnityEngine.InputSystem;

public class Refrigerator : Interactable
{
    public string[] items = { "떡", "고추장", "오뎅", "튀김" };
    private bool playerNearby = false;

    public override void Interact()
    {
        if (!RefrigeratorUI.Instance.isOpen)
            RefrigeratorUI.Instance.ShowItemSelect(items, transform);
        else
            RefrigeratorUI.Instance.PickItemPublic();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (RefrigeratorUI.Instance != null)
                RefrigeratorUI.Instance.HideItemSelect();
        }
    }

    void Update()
    {
        if (RefrigeratorUI.Instance == null) return;

        if (playerNearby && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!RefrigeratorUI.Instance.isOpen)
                RefrigeratorUI.Instance.ShowItemSelect(items, transform);
            else
                RefrigeratorUI.Instance.PickItemPublic();
        }
    }
}