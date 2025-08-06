using System.Collections;
using UnityEngine;
using Vehicles;

public class Helicopter : Vehicle
{
    [SerializeField] private GameObject topPropeller;
    [SerializeField] private GameObject rearPropeller;

    [SerializeField] private float propellerRotationSpeed;

    private float thrust = 0;

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

        if (engineEnabled)
        {
            SpinPropellers();
        }
    }

    protected override void Move()
    {
        
    }

    IEnumerator ChangeThrust(char _mode, float seconds = 1)
    {
        switch (_mode)
        {
            case 'I':
            case 'i':
                while (thrust < 1) 
                {
                    thrust += 1 * Time.fixedDeltaTime / seconds;
                    yield return null;
                }
                thrust = 1;
                break;
            case 'D':
            case 'd':
                while (thrust > 0)
                {
                    thrust -= 1 * Time.fixedDeltaTime / seconds;
                    yield return null;
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
