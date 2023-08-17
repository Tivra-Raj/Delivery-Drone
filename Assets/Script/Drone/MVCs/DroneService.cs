using ScriptableObjects;
using Utility;

namespace MVCs
{
    public class DroneService : MonoGenericSingleton<DroneService>
    {
        public DroneScriptableObject ConfigDrone;

        public DroneController DroneController { get; private set; }

        private void Start()
        {
            CreateNewDrone();
        }

        private DroneController CreateNewDrone()
        {
            DroneScriptableObject droneScriptableObject = ConfigDrone;
            DroneModel droneModel = new DroneModel(droneScriptableObject);
            DroneController = new DroneController(droneScriptableObject.DronePrefab, droneModel);

            return DroneController;
        }
    }
}