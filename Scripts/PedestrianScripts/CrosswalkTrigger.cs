using UnityEngine;
using System.Collections.Generic;

public class CrosswalkTrigger : MonoBehaviour
{
    private HashSet<GameObject> pedestriansInCrosswalk = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            pedestriansInCrosswalk.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            pedestriansInCrosswalk.Remove(other.gameObject);
        }
    }

    public bool IsCrosswalkOccupied()
    {
        return pedestriansInCrosswalk.Count > 0;
    }
}
