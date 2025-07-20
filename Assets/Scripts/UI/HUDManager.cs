using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Vehicles.Vehicle vehicle;

    [SerializeField] private TextMeshProUGUI speedDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedDisplay.text = (int) vehicle.Speed + "m/s";
    }
}
