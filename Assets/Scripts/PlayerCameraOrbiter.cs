using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraOrbiter : MonoBehaviour
{
    [Header("Orbit Settings")]
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float orbitSpeed = 5f;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float rotationSmoothing = 10f;

    // Current spherical coordinates
    private bool cameraEnabled = true;
    private float azimuthAngle = 0f;
    private float polarAngle = 45f;
    private float currentDistance;
    private Vector2 orbitInput;
    private float zoomInput;

    public InputActionAsset InputActions;

    private InputAction lookAction;
    private InputAction zoomAction;

    public void EnableCamera()
    {
        cameraEnabled = true;
    }

    public void DisableCamera() { cameraEnabled = false; }

    private void OnEnable()
    {
        InputActions.FindActionMap("Plane").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Plane").Disable();
    }

    private void Start()
    {
        // Initialize distance
        currentDistance = Vector3.Distance(transform.position, centerPoint.position);
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Calculate initial angles based on starting position
        Vector3 relativePos = transform.position - centerPoint.position;
        currentDistance = relativePos.magnitude;
        azimuthAngle = Mathf.Atan2(relativePos.x, relativePos.z) * Mathf.Rad2Deg;
        polarAngle = Mathf.Asin(relativePos.y / currentDistance) * Mathf.Rad2Deg;

        lookAction = InputSystem.actions.FindAction("Plane/Look");
        zoomAction = InputSystem.actions.FindAction("Plane/Zoom");
    }

    public void OnOrbitInput(InputAction.CallbackContext context)
    {
        orbitInput = lookAction.ReadValue<Vector2>();
    }

    public void OnZoomInput(InputAction.CallbackContext context)
    {
        zoomInput = zoomAction.ReadValue<Vector2>().y; // For mouse scroll
        // If using a single axis input (like mouse scroll delta), use:
        // zoomInput = context.ReadValue<float>();
    }

    private void LateUpdate()
    {
        orbitInput = lookAction.ReadValue<Vector2>();
        zoomInput = zoomAction.ReadValue<Vector2>().y;

        if (cameraEnabled) 
        { 
            HandleOrbitInput();
            HandleZoomInput();
            UpdateCameraPosition(); 
        }

        // Face the center point smoothly
        transform.LookAt(centerPoint);
    }

    private void HandleOrbitInput()
    {
        if (orbitInput.magnitude > 0.1f)
        {
            azimuthAngle += orbitInput.x * orbitSpeed * Time.deltaTime;
            polarAngle -= orbitInput.y * orbitSpeed * Time.deltaTime; // Inverted for natural camera movement

            // Clamp vertical angle to prevent flipping
            polarAngle = Mathf.Clamp(polarAngle, -89f, 89f);
        }
    }

    private void HandleZoomInput()
    {
        if (Mathf.Abs(zoomInput) > 0.01f)
        {
            currentDistance -= zoomInput * zoomSpeed * Time.deltaTime;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        }
    }

    private void UpdateCameraPosition()
    {
        // Convert spherical coordinates to Cartesian
        float verticalAngle = polarAngle * Mathf.Deg2Rad;
        float horizontalAngle = azimuthAngle * Mathf.Deg2Rad;

        float x = currentDistance * Mathf.Cos(verticalAngle) * Mathf.Sin(horizontalAngle);
        float y = currentDistance * Mathf.Sin(verticalAngle);
        float z = currentDistance * Mathf.Cos(verticalAngle) * Mathf.Cos(horizontalAngle);

        Vector3 targetPosition = centerPoint.position + new Vector3(x, y, z);

        // Smooth movement
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            rotationSmoothing * Time.deltaTime
        );
    }
}
