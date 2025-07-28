using UnityEngine;
using Vehicles;

public class Plane : Vehicle
{
    [SerializeField] private GameObject[] propellers;
    [SerializeField] private GameObject[] wheels;

    [SerializeField] private float thrust = 0;

    [SerializeField] private float trueThrust = 0;

    private Vector2 moveAmt;
    private float thrustAmt;

    private bool liftOff;

    private InputSystem_Actions inputActions;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputSystem_Actions();
    }

    private void ChangeThrust()
    {
        if (engineEnabled && thrust < vehicleData.enginePower)
        {
            trueThrust += (vehicleData.thrustIncreaseRate * Time.fixedDeltaTime * thrustAmt);
            trueThrust = Mathf.Clamp(trueThrust, 0, vehicleData.enginePower);
            thrust = Mathf.InverseLerp(0, vehicleData.enginePower, trueThrust);
        }

        if (!engineEnabled && thrust > 0)
        {
            trueThrust -= (vehicleData.thrustIncreaseRate * Time.fixedDeltaTime * 4);
            trueThrust = Mathf.Clamp(thrust, 0, vehicleData.enginePower);
            thrust = Mathf.InverseLerp(0, vehicleData.enginePower, trueThrust);
        }
    }

    protected override void Move()
    {
        float liftThreshold = 70f;

        // Getting input values
        moveAmt = inputActions.Plane.Move.ReadValue<Vector2>();
        thrustAmt = inputActions.Plane.Thrust.ReadValue<float>();

        // Calculating forward speed
        float speed = vehicleData.maxSpeed * thrust;

        transform.Translate(speed * Time.fixedDeltaTime * Vector3.forward);

        if (curSpeed > liftThreshold)
        {
            liftOff = true;
            rigidbody.useGravity = false;
            transform.Rotate(Vector3.right, 30f * Time.fixedDeltaTime * moveAmt.y);

            transform.Rotate(Vector3.forward, 10f * Time.fixedDeltaTime * -moveAmt.x);
        }
        else
        {
            liftOff = false;
            rigidbody.useGravity = true;
            transform.Rotate(Vector3.up, 2.5f * Time.fixedDeltaTime * moveAmt.x);
        }


    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ChangeThrust();
        if (engineEnabled)
        {
            Move();
            SpinPropellers();
        }
    }

    private void SpinPropellers()
    {
        float ROTATION_SPEED = 720f;
        foreach (GameObject propeller in propellers)
        {
            propeller.transform.Rotate(Vector3.forward, thrust * ROTATION_SPEED * Time.fixedDeltaTime);
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

    void Update()
    {
        if (inputActions.Plane.EngineToggle.WasPressedThisFrame())
        {
            ToggleEngine();
        }

        if (inputActions.Plane.GearToggle.WasPressedThisFrame() && liftOff)
        {
            ToggleGear();
        }
    }

    void ToggleGear()
    {
        foreach (GameObject wheel in wheels)
        {
            wheel.SetActive(!wheel.activeSelf);
        }
    }
}
