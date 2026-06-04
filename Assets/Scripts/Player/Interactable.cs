using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerInteraction.Instance.SetInteractable(this);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerInteraction.Instance.ClearInteractable(this);
    }
}