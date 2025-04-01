/*using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // Object to follow
    public float smoothSpeed = 0.5f;   // Speed of following
    public Vector3 offset;    // Offset from the target

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);  // Make the camera look at the target
        }
    }
}
*/


/*using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // Object to follow (Car)
    public float smoothSpeed = 10f;  // Speed of following
    public Vector3 offset = new Vector3(0, 12, -15);    // Offset from the target
    public float tiltAngle = 25f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Compute the desired position based on the target's rotation
            Vector3 desiredPosition = target.position + target.rotation * offset;

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Compute the tilted rotation
            Quaternion desiredRotation = target.rotation * Quaternion.Euler(tiltAngle, 0, 0);

            // Smoothly rotate the camera to match the tilted rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
        }
    }
}*/
using UnityEngine;

public class CameraSmoothMove : MonoBehaviour
{
    private float duration = 30f;
    private float elapsedTime = 0f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingForward = true;

    // Helicopter movement settings
    private float yAmplitude = 5f;  // how far up/down the camera moves
    private float yFrequency = 0.075f;   // how fast the camera moves up/down

    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x + 400f, startPosition.y, startPosition.z - 400f);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);
        float smoothT = Mathf.SmoothStep(0f, 1f, t); // Ease in - ease out

        Vector3 from = movingForward ? startPosition : endPosition;
        Vector3 to = movingForward ? endPosition : startPosition;

        Vector3 basePosition = Vector3.Lerp(from, to, smoothT);

        float yOffset = Mathf.Sin(elapsedTime * yFrequency * Mathf.PI * 2f) * yAmplitude;
        basePosition.y += yOffset;

        transform.position = basePosition;

        if (t >= 1f)
        {
            // Swap direction and reset timer
            movingForward = !movingForward;
            elapsedTime = 0f;
        }
    }
}