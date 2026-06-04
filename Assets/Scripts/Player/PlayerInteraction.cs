using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    public string heldItem = "";
    public bool hasPlate = false;
    public Transform handPosition;
    private GameObject heldItemObject;

    [Header("아이템 프리팹")]
    public GameObject[] itemPrefabs;
    public string[] itemNames;

    [Header("접시 프리팹")]
    public GameObject platePrefab;
    private GameObject plateObject;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PickupPlate()
    {
        if (hasPlate) return;
        if (heldItem != "") return;

        hasPlate = true;
        plateObject = Instantiate(platePrefab, handPosition);
        plateObject.transform.localPosition = Vector3.zero;
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