using System.Collections.Generic;
using UnityEngine;

public class TrafficLightTrigger : MonoBehaviour
{
    public TrafficLightController trafficLight; // Reference to the connected traffic light

    private HashSet<CarAgent> carsInStopZone = new HashSet<CarAgent>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            CarAgent car = other.GetComponent<CarAgent>();
            if (car != null)
            {
                carsInStopZone.Add(car);

                // Check if the light is red
                if (trafficLight != null && trafficLight.IsRed())
                {
                    car.SetTrafficLightRed(true); // Set traffic lights red state
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
                carsInStopZone.Remove(car);
                car.SetTrafficLightRed(false); // Allow car to move again
            }
        }
    }


    public bool IsCrosswalkOccupied()
    {
        return carsInStopZone.Count > 0;
    }

    private void Update()
    {
        if (trafficLight != null)
        {
            bool isOccupied = trafficLight.IsRed();

            foreach (CarAgent car in carsInStopZone)
            {
                car.SetTrafficLightRed(isOccupied);
            }
        }
    }

}
