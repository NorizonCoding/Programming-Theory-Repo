using UnityEngine;
using Vehicles;

// INHERITANCE
public class Boat : Vehicle
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    // POLYMORPHISM
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (engineEnabled)
        {
            // ABSTRACTION
            Move();
        }
    }

    // POLYMORPHISM
    private void Update()
    {
        if (InputObject.inputActions.Boat.EngineToggle.WasPressedThisFrame())
        {
            ToggleEngine();
        }
    }

    // POLYMORPHISM // ABSTRACTION
    protected override void Move()
    {
        Vector2 inputVector = InputObject.inputActions.Boat.Movement.ReadValue<Vector2>();

        transform.Translate(inputVector.y * speed * Time.fixedDeltaTime * Vector3.forward);
        transform.Rotate(Vector3.up, inputVector.x * rotationSpeed * Time.fixedDeltaTime);
    }
}
