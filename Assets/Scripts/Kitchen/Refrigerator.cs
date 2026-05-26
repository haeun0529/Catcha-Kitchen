using UnityEngine;
using UnityEngine.InputSystem;

public class Refrigerator : MonoBehaviour
{
    public string[] items = { "떡", "고추장", "오뎅", "튀김" };
    private bool playerNearby = false;

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