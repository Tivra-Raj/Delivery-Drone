using System.Collections;
using UI;
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

        private float currentFuel;
        private float fuelConsumptionRate;

        private Coroutine droneDeath;

        public DroneController(DroneView dronePrefab, DroneModel droneModel)
        {
            DroneView = GameObject.Instantiate<DroneView>(dronePrefab);
            DroneModel = droneModel;
            droneRigidBody = DroneView.GetRigidbody();

            DroneModel.SetDroneController(this);
            DroneView.SetDroneController(this);

            currentFuel = DroneModel.MaxFuel;
            UIService.Instance.SetFuelIndicator(DroneModel.MaxFuel);
            UIService.Instance.UpdateFuelUI(currentFuel);
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

            fuelConsumptionRate = finalPitch * DroneModel.FuelConsumptionRate;
            ReduceFuel(fuelConsumptionRate);
        }

        public void ReduceFuel(float fuelConsumptionRate)
        {
            currentFuel -= Time.deltaTime * fuelConsumptionRate/100;
            UIService.Instance.UpdateFuelUI(currentFuel);

            if (currentFuel < 0)
            {
                DroneView.stopCoroutine(droneDeath);
                droneDeath = DroneView.StartCoroutine(DroneDeath(5));
            }
        }

        private IEnumerator DroneDeath(float seconds)
        {
            DroneView.gameObject.GetComponent<DroneView>().enabled = false;
            Physics.gravity = new Vector3 (0, -40, 0);
            yield return new WaitForSeconds(seconds);
            UIService.Instance.GameOver();
        }
    }
}