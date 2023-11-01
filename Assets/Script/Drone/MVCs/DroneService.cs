using FloatingText;
using ScriptableObjects;
using Sound;
using System;
using System.Collections;
using UI;
using UnityEngine;
using Utility;

namespace MVCs
{
    public class DroneService : MonoGenericSingleton<DroneService>
    {
        [SerializeField] private DroneScriptableObject ConfigDrone;
        [SerializeField] private float totalDeliveryTime = 300f; // Total time in min
        [SerializeField] private float additionalTime = 20f;

        public float currentDeliveryTime;
        private Coroutine countDown;

        public DroneController DroneController { get; private set; }
        public float AdditionalTime { get => additionalTime; set => additionalTime = value; }

        private void Start()
        {
            CreateNewDrone();
            DroneController.DroneView.stopCoroutine(countDown);
            countDown = StartCoroutine(DeliveryCountDown());
        }

        private void Update()
        {
            PlayCountDownTimeSound();
        }

        private void PlayCountDownTimeSound()
        {
            if (currentDeliveryTime <= 10 && currentDeliveryTime >= 0)
            {
                SoundService.Instance.PlaySoundEffect(SoundType.Timer);
            }
            else
            {
                SoundService.Instance.StopSoundEffects(SoundType.Timer);
            }
        }

        private DroneController CreateNewDrone()
        {
            DroneScriptableObject droneScriptableObject = ConfigDrone;
            DroneModel droneModel = new DroneModel(droneScriptableObject);
            DroneController = new DroneController(droneScriptableObject.DronePrefab, droneModel);

            return DroneController;
        }

        public IEnumerator DeliveryCountDown()
        {
            currentDeliveryTime = totalDeliveryTime;
            while (currentDeliveryTime >= 0)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(currentDeliveryTime);
                string timeString = timeSpan.ToString(@"mm\:ss");
                UIService.Instance.UpdateDeliveryTimerText("Remaining Time : " + timeString);

                currentDeliveryTime--;
                yield return new WaitForSeconds(1);
                
            }
            

            countDown = StartCoroutine(DroneController.DroneDeath(2f));
        }

        public void GiveAdditionalTimeOnDelivery()
        {
            currentDeliveryTime += AdditionalTime;
            FLoatingTextService.Instance.SpawnFloatingText(AdditionalTime);
        }
    }
}