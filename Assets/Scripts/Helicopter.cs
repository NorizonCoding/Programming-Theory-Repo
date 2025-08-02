using UnityEngine;
using Vehicles;

public class Helicopter : Vehicle
{
    private InputSystem_Actions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
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

    public override void EnableInput()
    {
        inputActions.Heli.Enable();
    }

    public override void DisableInput()
    {
        inputActions.Heli.Disable();
    }
}
