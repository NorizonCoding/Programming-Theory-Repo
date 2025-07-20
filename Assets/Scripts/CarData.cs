using UnityEngine;

namespace Vehicles
{
    [CreateAssetMenu]
    public class CarData : VehicleData
    {
        public Transmission transmission;

        public int wheelNumber = 4;
    }
}