using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    public string heldItem = "";
    public bool hasPlate = false;
    public Transform handPosition;
    private GameObject heldItemObject;
    private GameObject plateObject;

    [Header("아이템 프리팹")]
    public GameObject[] itemPrefabs;
    public string[] itemNames;

    [Header("접시 프리팹")]
    public GameObject platePrefab;

    [Header("접시 머티리얼")]
    public Material cleanMaterial;
    public Material dirtyMaterial;

    private bool isPlateDirty = false;

    public void SetPlateDirty(bool dirty)
    {
        isPlateDirty = dirty;
        if (plateObject != null)
        {
            MeshRenderer renderer = plateObject.GetComponent<MeshRenderer>();
            renderer.material = dirty ? dirtyMaterial : cleanMaterial;
        }
    }

    public bool IsPlateDirty()
    {
        return isPlateDirty;
    }

    private List<Interactable> nearbyInteractables = new List<Interactable>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Interactable closest = GetClosestInteractable();
            if (closest != null)
                closest.Interact();
        }
    }

    Interactable GetClosestInteractable()
    {
        Interactable closest = null;
        float minDist = float.MaxValue;

        foreach (var interactable in nearbyInteractables)
        {
            if (interactable == null) continue;
            float dist = Vector3.Distance(transform.position, interactable.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = interactable;
            }
        }
        return closest;
    }

    public void SetInteractable(Interactable interactable)
    {
        if (!nearbyInteractables.Contains(interactable))
            nearbyInteractables.Add(interactable);
    }

    public void ClearInteractable(Interactable interactable)
    {
        nearbyInteractables.Remove(interactable);
    }

    public void PickupPlate()
    {
        if (hasPlate) return;
        if (heldItem != "") return;

        hasPlate = true;
        plateObject = Instantiate(platePrefab, handPosition);
        plateObject.transform.localPosition = Vector3.zero;
        isPlateDirty = false; 
        SetPlateDirty(false); 
        Debug.Log("접시 집음!");
    }

    public void DropPlate()
    {
        if (!hasPlate) return;

        hasPlate = false;
        heldItem = "";
        if (plateObject != null)
            Destroy(plateObject);
        if (heldItemObject != null)
            Destroy(heldItemObject);
        Debug.Log("접시 내려놓음!");
    }

    public void SetHeldItem(string item)
    {
        string[] cookedItems = { "떡볶이", "튀김완성", "오뎅완성" };
        foreach (string cooked in cookedItems)
        {
            if (item == cooked && !hasPlate)
            {
                Debug.Log("접시가 없어서 못 집음!");
                return;
            }
        }

        if (heldItem != "")
        {
            Debug.Log("이미 아이템을 들고 있어!");
            return;
        }

        if (heldItemObject != null)
            Destroy(heldItemObject);

        heldItem = item;

        int idx = System.Array.IndexOf(itemNames, item);
        if (idx >= 0 && idx < itemPrefabs.Length)
        {
            heldItemObject = Instantiate(itemPrefabs[idx], handPosition);
            heldItemObject.transform.localPosition = Vector3.zero;
        }

        Debug.Log($"들고 있는 아이템: {item}");
    }

    public void DropItem()
    {
        if (heldItemObject != null)
            Destroy(heldItemObject);

        heldItem = "";
        heldItemObject = null;
    }
}