using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using Vehicles;

public class Plane : Vehicle
{
    [SerializeField] private GameObject[] propellers;

    private Vector2 moveAmt;
    private float thrustAmt;

    private InputSystem_Actions inputActions;

    protected override void Move()
    {
        moveAmt = inputActions.Plane.Move.ReadValue<Vector2>();
        thrustAmt = inputActions.Plane.Thrust.ReadValue<float>();

        Vector3 forwardMovement = Vector3.forward * thrustAmt;

        rigidbody.AddRelativeForce(forwardMovement * vehicleData.enginePower); // Add forward thrust
        rigidbody.AddRelativeTorque(moveAmt.x * vehicleData.enginePower * Vector3.up);

        float liftThreshold = 180f;

        if (curSpeed > liftThreshold)
        {
            rigidbody.useGravity = false;
            rigidbody.AddRelativeTorque(-moveAmt.x * vehicleData.enginePower * Vector3.forward);
            rigidbody.AddRelativeTorque(moveAmt.y * vehicleData.enginePower * Vector3.right);
        }
        else rigidbody.useGravity = true;
    }

    private void SpinPropellers()
    {
        float ROTATION_SPEED = 45f;
        foreach (GameObject propeller in propellers)
        {
            propeller.transform.Rotate(Vector3.forward, Speed * ROTATION_SPEED);
        }
    }

    void Update()
    {
        if (inputActions.Plane.EngineToggle.WasPressedThisFrame())
        {
            ToggleEngine();
        }
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (engineEnabled) 
        { 
            Move();
            SpinPropellers();
        }
    }

    public override void DisableInput()
    {
        inputActions.Plane.Disable();
    }

    public override void EnableInput()
    {
        inputActions.Plane.Enable();
    }

    private void Start()
    {
        inputActions = new InputSystem_Actions();
    }
}
