using Events;
using MVCs;
using Package;
using UnityEngine;

namespace DeliveryLocation
{
    public class DeliveryLocationView : MonoBehaviour
    {
        public DeliveryLocationController DeliveryLocationController { get; private set; }

        public void SetDeliveryLocationController(DeliveryLocationController deliveryLocationController)
        {
            DeliveryLocationController = deliveryLocationController;
        }

        private void OnDisable()
        {
            DeliveryLocationController.UnSubscribeEvents();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.GetComponent<PackageView>() != null && !DroneService.Instance.DroneController.DroneView.IsAttached)
            {
                EventService.Instance.OnPackageDeliveredEvent.InvokeEvent();
            }
        }
    }
}