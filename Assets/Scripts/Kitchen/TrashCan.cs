using UnityEngine;
using UnityEngine.InputSystem;

public class TrashCan : MonoBehaviour
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
            if (PlayerInteraction.Instance.heldItem != "")
            {
                Debug.Log($"{PlayerInteraction.Instance.heldItem} 버림!");
                PlayerInteraction.Instance.DropItem();
            }
            else if (PlayerInteraction.Instance.hasPlate)
            {
                Debug.Log("접시 버림!");
                PlayerInteraction.Instance.DropPlate();
            }
        }
    }
}