using Events;
using Package;
using UI;
using UnityEngine;

namespace DeliveryLocation
{
    public class DeliveryLocationController
    {
        public DeliveryLocationView DeliveryLocationView { get; private set; }

        private int packageDelivered;
        public DeliveryLocationController(DeliveryLocationView deliveryLocationView)
        {
            DeliveryLocationView = GameObject.Instantiate<DeliveryLocationView>(deliveryLocationView);
            DeliveryLocationView.SetDeliveryLocationController(this);
        }

        public void Configure(Vector3 setPosition)
        {
            DeliveryLocationView.transform.position = setPosition;
        }

        public void SubscribeEvents()
        {
            EventService.Instance.OnPackageDeliveredEvent.AddListener(OnPackageEnterDeliveryLocation);
        }

        public void UnSubscribeEvents()
        {
            EventService.Instance.OnPackageDeliveredEvent.RemoveListener(OnPackageEnterDeliveryLocation);
        }

        public void OnPackageEnterDeliveryLocation()
        {   
            packageDelivered++;
            UIService.Instance.UpdateTotalPackageDeliveredText(packageDelivered);
            DeliveryLocationView.gameObject.SetActive(false);
            DeliveryLocationService.Instance.ReturnDeliveryLocationToPool(this);
            PackageService.Instance.PackageMarker.SetActive(true);
        }
    }
}