using UnityEngine;

public class CustomerSeat : MonoBehaviour
{
    public bool isOccupied = false;
    public Customer currentCustomer = null;

    public void Occupy(Customer customer)
    {
        isOccupied = true;
        currentCustomer = customer;
    }

    public void Vacate()
    {
        isOccupied = false;
        currentCustomer = null;
    }
}