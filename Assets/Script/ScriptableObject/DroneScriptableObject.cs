using MVCs;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "DroneScriptableObject", menuName = "ScriptableObject/CreateNewDroneScriptableObject")]
    public class DroneScriptableObject : ScriptableObject
    {
        [Header("Drone Properties")]
        public float MinMaxPitch = 30f;
        public float MinMaxRoll = 30f;
        public float YawPower = 4f;
        public float SmoothMove = 2f;
        public float Speed = 4f;
        public float MaxFuel = 100f;
        public float FuelConsumptionRate;

        [Header("Drone Body Properties")]
        public float DroneWeight = 1f;
        public DroneView DronePrefab;
    }
}