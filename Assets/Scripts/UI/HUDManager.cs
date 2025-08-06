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

    [SerializeField] private Vehicle car;
    [SerializeField] private Vehicle plane;
    [SerializeField] private Vehicle heli;
    [SerializeField] private Vehicle boat;

    private Vehicle vehicle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vehicle = carCamera.GetComponentInParent<Vehicle>();
    }

    // Update is called once per frame
    void Update()
    {
        speedDisplay.text = "Speed : " + (int) vehicle.Speed + "m/s";
    }

    public void CarButton()
    {
        carCamera.SetActive(true);
        boatCamera.SetActive(false);
        planeCamera.SetActive(false);
        heliCamera.SetActive(false);

        vehicle = car;
        InputObject.inputActions.Car.Enable();
        InputObject.inputActions.Boat.Disable();
        InputObject.inputActions.Plane.Disable();
        InputObject.inputActions.Heli.Disable();
    }

    public void BoatButton()
    {
        carCamera.SetActive(false);
        boatCamera.SetActive(true);
        planeCamera.SetActive(false);
        heliCamera.SetActive(false);

        vehicle = boat;
        InputObject.inputActions.Car.Disable();
        InputObject.inputActions.Boat.Enable();
        InputObject.inputActions.Plane.Disable();
        InputObject.inputActions.Heli.Disable();
    }

    public void PlaneButton()
    {
        carCamera.SetActive(false);
        boatCamera.SetActive(false);
        planeCamera.SetActive(true);
        heliCamera.SetActive(false);

        vehicle = plane;
        InputObject.inputActions.Car.Disable();
        InputObject.inputActions.Boat.Disable();
        InputObject.inputActions.Plane.Enable();
        InputObject.inputActions.Heli.Disable();
    }

    public void HeliButton()
    {
        carCamera.SetActive(false);
        boatCamera.SetActive(false);
        planeCamera.SetActive(false);
        heliCamera.SetActive(true);

        vehicle = heli;
        InputObject.inputActions.Car.Disable();
        InputObject.inputActions.Boat.Disable();
        InputObject.inputActions.Plane.Disable();
        InputObject.inputActions.Heli.Enable();
    }
}
