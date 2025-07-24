using UnityEngine;

namespace Vehicles
{
    public enum Fuel
    {
        Gas, //Only for cars
        Electric, //Only for cars
        Kerosene // Only for boats and planes
    }

    public enum Transmission //Only for cars
    {
        Manual,
        Automatic
    }

    public enum VehicleType
    {
        Land, Water, Air, Heli
    }
}