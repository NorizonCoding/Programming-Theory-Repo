using UnityEngine;
using Vehicles;

public class Boat : Vehicle
{
    bool onWater = false;

    private InputSystem_Actions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (onWater && engineEnabled)
        {
            Move();
        }
    }

    private void Update()
    {
        if (inputActions.Boat.EngineToggle.WasPressedThisFrame())
        {
            ToggleEngine();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            onWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            onWater = false;
        }
    }

    protected override void Move()
    {
        
    }

    public override void DisableInput()
    {
        inputActions.Boat.Disable();
    }

    public override void EnableInput()
    {
        inputActions.Boat.Enable();
    }
}
