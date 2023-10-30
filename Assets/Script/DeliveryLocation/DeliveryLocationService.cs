using UnityEngine;
using Utility;

namespace DeliveryLocation
{
    public class DeliveryLocationService : MonoGenericSingleton<DeliveryLocationService>
    {
        [SerializeField] private DeliveryLocationView deliveryLocationPrefab;
        [SerializeField] private Transform[] SpawnPosition;
        
        public DeliveryLocationSpwanStatus spwanStatus;

        public DeliveryLocationController DeliveryLocationController { get; private set; }
        private DeliveryLocationPool deliveryLocationPool;

        private void Start()
        {
            deliveryLocationPool = GetComponent<DeliveryLocationPool>();
            spwanStatus = DeliveryLocationSpwanStatus.DeSpwaned;
        }

        public void SpawnNewDeliveryLocation()
        {
            int pickRandomDeliveryLocationSpawnPosition = Random.Range(0, SpawnPosition.Length);
            CreateNewDeliveryLocation(SpawnPosition[pickRandomDeliveryLocationSpawnPosition]);
        }

        private DeliveryLocationController CreateNewDeliveryLocation(Transform spawnPosition)
        {
            DeliveryLocationController = deliveryLocationPool.GetDeliveryLocation(deliveryLocationPrefab);
            DeliveryLocationController.Configure(spawnPosition.position);

            return DeliveryLocationController;
        }

        public void ReturnDeliveryLocationToPool(DeliveryLocationController deliveryLocationToReturn) => deliveryLocationPool.ReturnItem(deliveryLocationToReturn);
    }
}