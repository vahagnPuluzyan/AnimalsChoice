using UnityEngine;

public class CameraShake : MonoBehaviour
{

    Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.2f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        camTransform = GetComponent<Transform>();
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}