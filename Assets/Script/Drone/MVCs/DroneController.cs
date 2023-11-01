using Sound;
using System.Collections;
using UI;
using UnityEngine;

namespace MVCs
{
    public class DroneController
    {
        public DroneView DroneView { get; private set; }
        public DroneModel DroneModel { get; private set; }
        public float CurrentFuel { get => currentFuel; set => currentFuel = value; }

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

            CurrentFuel = DroneModel.MaxFuel;
            UIService.Instance.SetFuelIndicator(DroneModel.MaxFuel);
            UIService.Instance.UpdateFuelUI(CurrentFuel);
        }       

        public void HandlePhysics()
        {
            HandleEngines();
            HandleControls();
        }

        private void HandleEngines()
        {
            SoundService.Instance.PlayMusic(SoundType.DroneFlying, true);
            foreach (IEngine engine in DroneView.engines)
            {
                engine.UpdateEngine(droneRigidBody, DroneView);
                
                fuelConsumptionRate = engine.GetVerticalMovement() * DroneModel.FuelConsumptionRate;  
            }
            ReduceFuel(fuelConsumptionRate);
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

            HandleDroneSpeed();

            fuelConsumptionRate = (Mathf.Abs(finalPitch) + Mathf.Abs(finalRoll)) * DroneModel.FuelConsumptionRate;
            ReduceFuel(fuelConsumptionRate);
        }

        private void HandleDroneSpeed()
        {
            Vector3 additionalSpeedForce = Vector3.zero;

            if (DroneView.Movement.y > 0)
            {
                additionalSpeedForce = DroneView.transform.forward * DroneModel.Speed;
            }
            else if (DroneView.Movement.y < 0)
            {
                additionalSpeedForce = -DroneView.transform.forward * DroneModel.Speed;
            }

            if (DroneView.Movement.x > 0)
            {
                additionalSpeedForce = DroneView.transform.right * DroneModel.Speed;
            }
            else if (DroneView.Movement.x < 0)
            {
                additionalSpeedForce = -DroneView.transform.right * DroneModel.Speed;
            }

            // Apply the additional force to the rigidbody
            droneRigidBody.AddForce(additionalSpeedForce, ForceMode.Force);
        }

        private void ReduceFuel(float fuelConsumptionRate)
        {
            CurrentFuel -= Time.deltaTime * fuelConsumptionRate/100;
            UIService.Instance.UpdateFuelUI(CurrentFuel);

            if (CurrentFuel <= 0)
            {
                currentFuel = 0;
                DroneView.stopCoroutine(droneDeath);
                droneDeath = DroneView.StartCoroutine(DroneDeath(2));
            }

            PlayLowFuelSound();
        }

        public void PlayLowFuelSound()
        {
            if (currentFuel < 10f && currentFuel >= 0f)
            {
                SoundService.Instance.PlayMusicEffect(SoundType.LowFuel, true);
            }
            else
            {
                SoundService.Instance.StopMusicEffects(SoundType.LowFuel);   
            }
        }

        public void RefillFuel()
        {
            currentFuel += Time.deltaTime * 5f;

            if (currentFuel > DroneModel.MaxFuel)
                currentFuel = DroneModel.MaxFuel;

            UIService.Instance.UpdateFuelUI(currentFuel);
        }

        public IEnumerator DroneDeath(float seconds)
        {
            DroneView.gameObject.GetComponent<DroneView>().enabled = false;
            yield return new WaitForSeconds(seconds);
            UIService.Instance.GameOver();
        }
    }
}