using UnityEngine;

namespace Vehicles
{
    [CreateAssetMenu]
    public class VehicleData : ScriptableObject
    {
        public VehicleType vehicleType;
        public float maxSpeed; // Measured in meters/s
        public float enginePower; //Measured in Neutons
        public int yearOfManufacture;
        public float fuelConsumptionRate; // Measured in Litres/Km
        public Fuel fuelType;
        public float fuelCapacity; // In Litres
        public float mass; // Measured in Kilograms
    }
}
