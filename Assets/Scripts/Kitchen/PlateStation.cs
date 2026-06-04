using UnityEngine;
using UnityEngine.InputSystem;

public class PlateStation : MonoBehaviour
{
    private bool playerNearby = false;

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
        if (playerNearby && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!PlayerInteraction.Instance.hasPlate &&
                PlayerInteraction.Instance.heldItem == "")
            {
                PlayerInteraction.Instance.PickupPlate();
            }

            else if (PlayerInteraction.Instance.hasPlate)
            {
                PlayerInteraction.Instance.DropPlate();
            }
        }
    }
}