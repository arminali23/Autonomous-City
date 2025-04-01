using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundCrosswalkTrigger : MonoBehaviour
{
    public CrosswalkTrigger crosswalk; // Assign this in the Inspector or find it dynamically
    private HashSet<CarAgent> carsInside = new HashSet<CarAgent>(); // Track cars inside the trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            CarAgent car = other.GetComponent<CarAgent>();
            if (car != null)
            {
                carsInside.Add(car); // Add car to tracking list

                if (crosswalk != null && crosswalk.IsCrosswalkOccupied())
                {
                    Debug.Log("A pedestrian is crossing! Car should wait.");
                    car.SetPedestrianNearby(true);
                }
                else
                {
                    car.SetPedestrianNearby(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            CarAgent car = other.GetComponent<CarAgent>();
            if (car != null)
            {
                carsInside.Remove(car); // Remove car from tracking list
            }
        }
    }

    private void Update()
    {
        if (crosswalk != null)
        {
            bool isOccupied = crosswalk.IsCrosswalkOccupied();

            foreach (CarAgent car in carsInside)
            {
                car.SetPedestrianNearby(isOccupied);
            }
        }
    }
}
