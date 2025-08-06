using System.Collections;
using UnityEngine;
using Vehicles;

// INHERITANCE
public class Helicopter : Vehicle
{
    [SerializeField] private GameObject topPropeller;
    [SerializeField] private GameObject rearPropeller;

    [SerializeField] private float propellerRotationSpeed;

    private float thrust = 0;

    [SerializeField] private float maxSpeed;

    [SerializeField] private float maxRotationSpeed;

    [SerializeField] bool grounded = true;

    // Update is called once per frame
    void Update()
    {
        if (InputObject.inputActions.Heli.EngineToggle.WasPressedThisFrame())
        {
            ToggleEngine();
            if (engineEnabled)
            {
                StartCoroutine(ChangeThrust('i'));
            }
            else
            {
                StartCoroutine(ChangeThrust('d'));
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        SpinPropellers();
        if (engineEnabled) 
        {
            Move();
        }
    }

    protected override void Move()
    {
        Vector2 inputVector = InputObject.inputActions.Heli.Movement.ReadValue<Vector2>();
        float inputThrust = InputObject.inputActions.Heli.Thrust.ReadValue<float>();

        if (grounded) inputThrust = Mathf.Abs(inputThrust);
        transform.Translate(Time.fixedDeltaTime * maxSpeed * inputThrust * Vector3.up);

        if (!grounded)
        {
            transform.Translate(Time.fixedDeltaTime * maxSpeed * inputVector.y * Vector3.forward);
            transform.Rotate(Vector3.up, inputVector.x * maxRotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
    IEnumerator ChangeThrust(char _mode, float seconds = 3)
    {
        switch (_mode)
        {
            case 'I':
            case 'i':
                while (thrust < 1) 
                {
                    thrust += 1 / seconds * 0.1f;
                    yield return new WaitForSeconds(0.1f);
                }
                thrust = 1;
                break;
            case 'D':
            case 'd':
                while (thrust > 0)
                {
                    thrust -= 1 * 0.05f / seconds;
                    yield return new WaitForSeconds(0.1f);
                }
                thrust = 0;
                break;

        }
    }

    void SpinPropellers()
    {
        topPropeller.transform.Rotate(Vector3.up, propellerRotationSpeed * Time.fixedDeltaTime * thrust);
        rearPropeller.transform.Rotate(Vector3.right, propellerRotationSpeed * Time.fixedDeltaTime * thrust);
    }
}
