using UnityEngine;
using Vehicles;

// INHERITANCE
public class Plane : Vehicle
{
    [SerializeField] private float rollSpeed;
    [SerializeField] private float pitchSpeed;
    [SerializeField] private float yawSpeed;

    [SerializeField] private GameObject[] propellers;
    [SerializeField] private GameObject[] wheels;

    private float thrust = 0;

    private float trueThrust = 0;

    private Vector2 moveAmt;
    private float thrustAmt;

    private bool liftOff;

    // ABSTRACTION
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

    // POLYMORPHISM // ABSTRACTION
    protected override void Move()
    {
        float liftThreshold = 70f;

        // Getting input values
        moveAmt = InputObject.inputActions.Plane.Move.ReadValue<Vector2>();
        thrustAmt = InputObject.inputActions.Plane.Thrust.ReadValue<float>();

        // Calculating forward speed
        float speed = vehicleData.maxSpeed * thrust;

        transform.Translate(speed * Time.fixedDeltaTime * Vector3.forward);

        if (curSpeed > liftThreshold)
        {
            liftOff = true;
            rigidbody.useGravity = false;
            transform.Rotate(Vector3.right, pitchSpeed * Time.fixedDeltaTime * moveAmt.y);

            transform.Rotate(Vector3.forward, rollSpeed * Time.fixedDeltaTime * -moveAmt.x);
        }
        else
        {
            liftOff = false;
            rigidbody.useGravity = true;
            transform.Rotate(Vector3.up, yawSpeed * Time.fixedDeltaTime * moveAmt.x);
        }


    }

    // POLYMORPHISM
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

    // ABSTRACTION
    private void SpinPropellers()
    {
        float ROTATION_SPEED = 720f;
        foreach (GameObject propeller in propellers)
        {
            propeller.transform.Rotate(Vector3.forward, thrust * ROTATION_SPEED * Time.fixedDeltaTime);
        }
    }

    // POLYMORPHISM
    void Update()
    {
        if (InputObject.inputActions.Plane.EngineToggle.WasPressedThisFrame())
        {
            ToggleEngine();
        }

        if (InputObject.inputActions.Plane.GearToggle.WasPressedThisFrame() && liftOff)
        {
            ToggleGear();
        }
    }

    // ABSTRACTION
    void ToggleGear()
    {
        foreach (GameObject wheel in wheels)
        {
            wheel.SetActive(!wheel.activeSelf);
        }
    }
}
