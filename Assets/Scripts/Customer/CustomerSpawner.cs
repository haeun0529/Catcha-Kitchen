using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    [Header("손님 프리팹")]
    public GameObject customerPrefab;

    [Header("자리")]
    public CustomerSeat[] seats;

    [Header("입구 위치")]
    public Transform entrancePoint;

    [Header("게임 타이머")]
    public float totalGameTime = 600f;
    private float currentGameTime = 0f;

    private float spawnTimer = 0f;
    private float spawnInterval = 15f;

    private string[] allMenus = { "떡볶이", "튀김완성", "오뎅완성" };

    void Update()
    {
        currentGameTime += Time.deltaTime;
        UpdateDifficulty();

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            TrySpawnCustomer();
        }
    }

    void UpdateDifficulty()
    {
        float ratio = currentGameTime / totalGameTime;

        if (ratio < 0.3f)
        {
            // 초반 (0~3분)
            spawnInterval = 18f;
        }
        else if (ratio < 0.5f)
        {
            // 중반 (3~5분)
            spawnInterval = 12f;
        }
        else
        {
            // 후반 (5~10분)
            spawnInterval = 7f;
        }
    }

    void TrySpawnCustomer()
    {
        CustomerSeat emptySeat = GetEmptySeat();
        if (emptySeat == null) return;

        // 손님 생성
        GameObject obj = Instantiate(customerPrefab, entrancePoint.position, Quaternion.identity);
        Customer customer = obj.GetComponent<Customer>();
        CustomerUI ui = obj.GetComponent<CustomerUI>();

        // 주문 생성
        List<string> orders = GenerateOrders();

        // 대기 시간
        float wait = GetWaitTime();

        customer.Setup(emptySeat, orders, wait);
        emptySeat.Occupy(customer);
    }

    CustomerSeat GetEmptySeat()
    {
        List<CustomerSeat> emptySeats = new List<CustomerSeat>();
        foreach (var seat in seats)
            if (!seat.isOccupied)
                emptySeats.Add(seat);

        if (emptySeats.Count == 0) return null;
        return emptySeats[Random.Range(0, emptySeats.Count)];
    }

    List<string> GenerateOrders()
    {
        float ratio = currentGameTime / totalGameTime;
        int orderCount = 1;

        if (ratio >= 0.3f && ratio < 0.5f)
            orderCount = Random.Range(1, 3); // 1~2개
        else if (ratio >= 0.5f)
            orderCount = Random.Range(1, 4); // 1~3개

        List<string> orders = new List<string>();
        List<string> available = new List<string>(allMenus);

        for (int i = 0; i < orderCount; i++)
        {
            if (available.Count == 0) break;
            int idx = Random.Range(0, available.Count);
            orders.Add(available[idx]);
            available.RemoveAt(idx);
        }

        return orders;
    }

    // 대기 시간
    float GetWaitTime()
    {
        float ratio = currentGameTime / totalGameTime;

        if (ratio < 0.3f) return 60f;
        else if (ratio < 0.5f) return 45f;
        else return 30f;
    }
}