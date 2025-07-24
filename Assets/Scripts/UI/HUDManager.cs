using UnityEngine;
using TMPro;
using Vehicles;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedDisplay;

    [SerializeField] private GameObject carCamera;
    [SerializeField] private GameObject planeCamera;
    [SerializeField] private GameObject boatCamera;
    [SerializeField] private GameObject heliCamera;

    private Vehicle vehicle;

    public InputSystem_Actions inputActions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vehicle = carCamera.GetComponentInParent<Vehicle>();

        inputActions = new InputSystem_Actions();

        inputActions.Plane.Disable();
        inputActions.Car.Enable();
        inputActions.Boat.Disable();
        inputActions.Heli.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        speedDisplay.text = "Speed : " + (int) vehicle.Speed + "m/s";
    }

    public void CarButton()
    {
        inputActions.Car.Enable();
        inputActions.Boat.Disable();
        inputActions.Plane.Disable();
        inputActions.Heli.Disable();

        carCamera.SetActive(true);
        boatCamera.SetActive(false);
        planeCamera.SetActive(false);
        heliCamera.SetActive(false);

        vehicle = carCamera.GetComponentInParent<Vehicle>();
    }

    public void BoatButton()
    {
        inputActions.Car.Disable();
        inputActions.Boat.Enable();
        inputActions.Plane.Disable();
        inputActions.Heli.Disable();

        carCamera.SetActive(false);
        boatCamera.SetActive(true);
        planeCamera.SetActive(false);
        heliCamera.SetActive(false);

        vehicle = boatCamera.GetComponentInParent<Vehicle>();
    }

    public void PlaneButton()
    {
        inputActions.Car.Disable();
        inputActions.Boat.Disable();
        inputActions.Plane.Enable();
        inputActions.Heli.Disable();

        carCamera.SetActive(false);
        boatCamera.SetActive(false);
        planeCamera.SetActive(true);
        heliCamera.SetActive(false);

        vehicle = planeCamera.GetComponentInParent<Vehicle>();
    }

    public void HeliButton()
    {
        inputActions.Car.Disable();
        inputActions.Boat.Disable();
        inputActions.Plane.Disable();
        inputActions.Heli.Enable();

        carCamera.SetActive(false);
        boatCamera.SetActive(false);
        planeCamera.SetActive(false);
        heliCamera.SetActive(true);

        vehicle = heliCamera.GetComponentInParent<Vehicle>();
    }
}
