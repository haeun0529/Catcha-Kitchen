using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    public enum State { Walking, Seated, Waiting, Leaving }
    public State currentState = State.Walking;

    [Header("주문")]
    public List<string> orders = new List<string>();
    public List<string> remainingOrders = new List<string>();

    [Header("대기 시간")]
    public float waitTime = 60f;
    private float currentWaitTime;

    [Header("이동")]
    public float moveSpeed = 2f;
    private Vector3 targetPosition;
    private CustomerSeat assignedSeat;

    [Header("UI")]
    public CustomerUI customerUI;

    private bool playerNearby = false;

    void Start()
    {
        currentWaitTime = waitTime;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                MoveToSeat();
                break;
            case State.Waiting:
                UpdateWaitTimer();
                break;
        }

        if (playerNearby && currentState == State.Waiting &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryServe();
        }
    }

    void MoveToSeat()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition;
            currentState = State.Waiting;
            customerUI.ShowOrders(remainingOrders);
            Debug.Log("손님 착석!");
        }
    }

    void UpdateWaitTimer()
    {
        currentWaitTime -= Time.deltaTime;
        customerUI.UpdateTimer(currentWaitTime / waitTime);

        if (currentWaitTime <= 0f)
        {
            Leave(false);
        }
    }

    void TryServe()
    {
        string heldItem = PlayerInteraction.Instance.heldItem;

        if (remainingOrders.Contains(heldItem))
        {
            remainingOrders.Remove(heldItem);
            PlayerInteraction.Instance.DropItem();
            Debug.Log($"{heldItem} 서빙 완료!");

            if (remainingOrders.Count == 0)
            {
                int coin = orders.Count * 50;
                Debug.Log($"코인 획득: {coin}");
                Leave(true);
            }
            else
            {
                customerUI.ShowOrders(remainingOrders);
            }
        }
        else
        {
            Debug.Log("잘못된 음식!");
        }
    }

    public void Setup(CustomerSeat seat, List<string> orderList, float wait)
    {
        assignedSeat = seat;
        targetPosition = seat.transform.position;
        orders = new List<string>(orderList);
        remainingOrders = new List<string>(orderList);
        waitTime = wait;
        currentWaitTime = wait;
    }

    void Leave(bool served)
    {
        if (!served)
            Debug.Log("손님이 기다리다 떠남!");

        currentState = State.Leaving;
        assignedSeat.Vacate();
        customerUI.HideUI();
        Destroy(gameObject, 1f);
    }

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
}