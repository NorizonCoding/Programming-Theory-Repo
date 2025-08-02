using UnityEngine;
using Vehicles;

public class Boat : Vehicle
{
    private InputSystem_Actions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions();
    }

    // Update is called once per frame
    void Update()
    {
        
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
