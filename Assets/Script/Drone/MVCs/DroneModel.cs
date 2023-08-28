using ScriptableObjects;
using UnityEngine;

namespace MVCs
{
    public class DroneModel
    {
        [Header("Drone Properties")]
        public float MinMaxPitch;
        public float MinMaxRoll;
        public float YawPower;
        public float SmoothMove;
        public float Speed;
        public float MaxFuel;
        public float FuelConsumptionRate;

        [Header("Drone Body Properties")]
        public float DroneWeight;
        public DroneView DronePrefab;

        public DroneController DroneController { get; private set; }

        public void SetDroneController(DroneController droneController) => DroneController = droneController;

        public DroneModel(DroneScriptableObject droneScriptableObject)
        {
            MinMaxPitch = droneScriptableObject.MinMaxPitch;
            MinMaxRoll = droneScriptableObject.MinMaxRoll;
            YawPower = droneScriptableObject.YawPower;
            SmoothMove = droneScriptableObject.SmoothMove;
            Speed = droneScriptableObject.Speed;
            MaxFuel = droneScriptableObject.MaxFuel;
            FuelConsumptionRate = droneScriptableObject.FuelConsumptionRate;

            DroneWeight = droneScriptableObject.DroneWeight;
        }
    }
}