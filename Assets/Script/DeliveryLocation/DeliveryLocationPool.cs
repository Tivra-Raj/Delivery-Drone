using Utility;

namespace DeliveryLocation
{
    public class DeliveryLocationPool : GenericObjectPool<DeliveryLocationController>
    {
        private DeliveryLocationView deliveryLocationPrefab;

        public DeliveryLocationController GetDeliveryLocation(DeliveryLocationView deliveryLocationPrefab)
        {
            this.deliveryLocationPrefab = deliveryLocationPrefab;

            DeliveryLocationController deliveryLocationController = GetItem();
            DeliveryLocationService.Instance.spwanStatus = DeliveryLocationSpwanStatus.Spwaned;
            deliveryLocationController.DeliveryLocationView.gameObject.SetActive(true);
            deliveryLocationController.SubscribeEvents();
            return deliveryLocationController;
        }

        protected override DeliveryLocationController CreateItem() => new DeliveryLocationController(deliveryLocationPrefab);
    }
}