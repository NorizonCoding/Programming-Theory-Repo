using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vehicles;

public class HUDManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI speedDisplay;
    [SerializeField] private TextMeshProUGUI engineDisplay;

    [Header("Cameras")]
    [SerializeField] private GameObject titleCamera;
    [SerializeField] private GameObject carCamera;
    [SerializeField] private GameObject planeCamera;
    [SerializeField] private GameObject boatCamera;
    [SerializeField] private GameObject heliCamera;

    [Header("Vehicles")]
    [SerializeField] private Vehicle car;
    [SerializeField] private Vehicle plane;
    [SerializeField] private Vehicle heli;
    [SerializeField] private Vehicle boat;

    [Header("")]
    [SerializeField] GameObject HUDDisplay;

    private Vehicle vehicle;

    bool gameStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vehicle = carCamera.GetComponentInParent<Vehicle>();
    }

    // Update is called once per frame
    void Update()
    {
        speedDisplay.text = "Speed : " + (int) (vehicle.Speed*3600)/1000 + "KMH";
        if (vehicle.EngineStatus) engineDisplay.text = "Engine: ON";
        else engineDisplay.text = "Engine: OFF";
    }

    public void CarButton()
    {
        if (!gameStarted)
        {
            titleCamera.SetActive(false);
            gameStarted = true;
            HUDDisplay.SetActive(true);
        }
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
        if (!gameStarted)
        {
            titleCamera.SetActive(false);
            gameStarted = true;
            HUDDisplay.SetActive(true);
        }
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
        if (!gameStarted)
        {
            titleCamera.SetActive(false);
            gameStarted = true;
            HUDDisplay.SetActive(true);
        }
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
        if (!gameStarted)
        {
            titleCamera.SetActive(false);
            gameStarted = true;
            HUDDisplay.SetActive(true);
        }
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

    public void Play()
    {
        int worldScene = 1;
        SceneManager.LoadScene(worldScene);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
