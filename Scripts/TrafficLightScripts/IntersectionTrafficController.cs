using System.Collections;
using UnityEngine;

public class IntersectionTrafficController : MonoBehaviour
{
    public TrafficLightController[] trafficLights; // Array to hold all 4 traffic lights
    public float greenDuration = 5f;
    public float yellowDuration = 2f;

    private void Start()
    {
        StartCoroutine(TrafficCycle());
    }

    private IEnumerator TrafficCycle()
    {
        while (true)
        {
            for (int i = 0; i < trafficLights.Length; i++)
            {
                // Set all lights to red
                SetAllRed();

                // Activate green for the current traffic light
                trafficLights[i].SetGreen();

                // Wait for green duration
                yield return new WaitForSeconds(greenDuration);

                // Turn yellow before switching
                trafficLights[i].SetYellow();
                yield return new WaitForSeconds(yellowDuration);
            }
        }
    }

    private void SetAllRed()
    {
        foreach (TrafficLightController light in trafficLights)
        {
            light.SetRed();
        }
    }
}
