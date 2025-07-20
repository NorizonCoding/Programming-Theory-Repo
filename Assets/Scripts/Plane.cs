using UnityEngine;
using Vehicles;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Plane : Vehicle, IInput
{
    [SerializeField] Transform cameraTarget;

    public InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction engineAction;
    private InputAction thrustAction;

    private Vector2 moveAmt;
    private float thrustAmt;

    private void OnEnable()
    {
        InputActions.FindActionMap("Plane").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Plane").Disable();
    }

    public void LookAround()
    {
        //lookAmt = lookAction.ReadValue<Vector2>();
        //Transform cameraTransform = gameObject.GetComponentInChildren<Camera>().transform;

        

        //cameraTransform.LookAt(cameraTarget);
    }

    public void Move()
    {
        moveAmt = moveAction.ReadValue<Vector2>();
        thrustAmt = thrustAction.ReadValue<float>();

        Vector3 forwardMovement = Vector3.forward * thrustAmt;

        const float ROTATION_SPEED = 10f;

        rigidbody.AddRelativeForce(forwardMovement * vehicleData.enginePower); // Add forward thrust
        rigidbody.AddRelativeTorque(moveAmt.x * ROTATION_SPEED * vehicleData.enginePower * Vector3.up);

        float liftThreshold = 180f;

        if (curSpeed > liftThreshold)
        {
            rigidbody.useGravity = false;
            rigidbody.AddRelativeTorque(-moveAmt.x * ROTATION_SPEED * vehicleData.enginePower * Vector3.forward);
            rigidbody.AddRelativeTorque(moveAmt.y * ROTATION_SPEED * vehicleData.enginePower * Vector3.right);
        }
        else rigidbody.useGravity = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();

        moveAction = InputSystem.actions.FindAction("Plane/Move");
        engineAction = InputSystem.actions.FindAction("Engine Toggle");
        thrustAction = InputSystem.actions.FindAction("Plane/Thrust");
    }

    void Update()
    {
        if (engineAction.WasPressedThisFrame())
        {
            ToggleEngine();
        }
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        LookAround();
        if (engineEnabled) Move();
    }
}
