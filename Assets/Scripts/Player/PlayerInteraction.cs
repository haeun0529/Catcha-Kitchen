using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    public string heldItem = "";
    public Transform handPosition;      // 손 위치 오브젝트
    private GameObject heldItemObject;  // 현재 들고 있는 오브젝트

    [Header("아이템 프리팹")]
    public GameObject[] itemPrefabs;    // 떡, 고추장, 오뎅, 튀김 순서
    public string[] itemNames;          // 떡, 고추장, 오뎅, 튀김

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetHeldItem(string item)
    {
        // 이미 들고 있으면 먼저 버리기 (임시)
        if (heldItemObject != null)
            Destroy(heldItemObject);

        heldItem = item;

        // 아이템 프리팹 찾아서 손에 붙이기
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