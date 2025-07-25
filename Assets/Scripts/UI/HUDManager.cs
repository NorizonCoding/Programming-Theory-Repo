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
        vehicle.EnableInput();
        boat.DisableInput();
        plane.DisableInput();
        heli.DisableInput();
    }

    public void BoatButton()
    {
        carCamera.SetActive(false);
        boatCamera.SetActive(true);
        planeCamera.SetActive(false);
        heliCamera.SetActive(false);

        vehicle = boat;
        vehicle.EnableInput();
        plane.DisableInput();
        heli.EnableInput();
        car.DisableInput();
    }

    public void PlaneButton()
    {
        carCamera.SetActive(false);
        boatCamera.SetActive(false);
        planeCamera.SetActive(true);
        heliCamera.SetActive(false);

        vehicle = plane;
        vehicle.EnableInput();
        heli.DisableInput();
        boat.DisableInput();
        car.DisableInput();
    }

    public void HeliButton()
    {
        carCamera.SetActive(false);
        boatCamera.SetActive(false);
        planeCamera.SetActive(false);
        heliCamera.SetActive(true);

        vehicle = heli;
        vehicle.EnableInput();
        plane.DisableInput();
        boat.DisableInput();
        car.DisableInput();
    }
}
