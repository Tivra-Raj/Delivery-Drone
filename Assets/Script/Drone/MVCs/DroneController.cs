using UnityEngine;

namespace MVCs
{
    public class DroneController
    {
        public DroneView DroneView { get; private set; }
        public DroneModel DroneModel { get; private set; }

        private Rigidbody droneRigidBody;
        private float finalPitch;
        private float finalRoll;
        private float yaw;
        private float finalYawPower;

        public DroneController(DroneView dronePrefab, DroneModel droneModel)
        {
            DroneView = GameObject.Instantiate<DroneView>(dronePrefab);
            DroneModel = droneModel;
            droneRigidBody = DroneView.GetRigidbody();

            DroneModel.SetDroneController(this);
            DroneView.SetDroneController(this);
        }       

        public void HandlePhysics()
        {
            HandleEngines();
            HandleControls();
        }

        private void HandleEngines()
        {
            foreach (IEngine engine in DroneView.engines)
            {
                engine.UpdateEngine(droneRigidBody, DroneView);
            }
        }

        private void HandleControls()
        {

            float pitch = DroneView.Movement.y * DroneModel.MinMaxPitch;
            float roll = -DroneView.Movement.x * DroneModel.MinMaxRoll;
            yaw += DroneView.YawPedals * DroneModel.YawPower;

            finalPitch = Mathf.Lerp(finalPitch, pitch, Time.fixedDeltaTime * DroneModel.SmoothMove);
            finalRoll = Mathf.Lerp(finalRoll, roll, Time.fixedDeltaTime * DroneModel.SmoothMove);
            finalYawPower = Mathf.Lerp(finalYawPower, yaw, Time.fixedDeltaTime * DroneModel.SmoothMove);

            Quaternion rotation = Quaternion.Euler(finalPitch, finalYawPower, finalRoll);
            droneRigidBody.MoveRotation(rotation);
        }
    }
}