using UnityEngine;

public class InputObject : MonoBehaviour
{
    public static InputSystem_Actions inputActions;

    private void Start()
    {
        inputActions = new();
        inputActions.Enable();
    }
}