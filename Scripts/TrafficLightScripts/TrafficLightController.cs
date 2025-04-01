using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public GameObject[] redLights;
    public GameObject[] yellowLights;
    public GameObject[] greenLights;

    private enum LightState { Red, Yellow, Green }
    private LightState currentState = LightState.Red; // Default to red

    public bool IsRed() => currentState == LightState.Red;

    public void SetRed()
    {
        currentState = LightState.Red;
        SetLightState(redLights, true);
        SetLightState(yellowLights, false);
        SetLightState(greenLights, false);
    }

    public void SetYellow()
    {
        currentState = LightState.Yellow;
        SetLightState(redLights, false);
        SetLightState(yellowLights, true);
        SetLightState(greenLights, false);
    }

    public void SetGreen()
    {
        currentState = LightState.Green;
        SetLightState(redLights, false);
        SetLightState(yellowLights, false);
        SetLightState(greenLights, true);
    }

    private void SetLightState(GameObject[] lights, bool state)
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(state);
        }
    }
}
