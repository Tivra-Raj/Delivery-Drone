using Events;
using UnityEngine;

namespace Package
{
    public class PackageController
    {
        public PackageView PackageView { get; private set; }

        public PackageController(PackageView packageView)
        {
            PackageView = GameObject.Instantiate<PackageView>(packageView);
            PackageView.SetPackageController(this);
        }

        public void Configure(Vector3 setPosition, Quaternion setRotation)
        {
            PackageView.transform.position = setPosition;
            PackageView.transform.rotation = setRotation;
        }

        public void SubscribeEvents()
        {
            EventService.Instance.OnPackageDeliveredEvent.AddListener(OnPackageEnterDeliveryLocation);
        }

        private void UnSubscribeEvents()
        {
            EventService.Instance.OnPackageDeliveredEvent.RemoveListener(OnPackageEnterDeliveryLocation);
        }

        public void OnPackageEnterDeliveryLocation()
        {
            UnSubscribeEvents();   
            PackageView.gameObject.SetActive(false);
            PackageService.Instance.ReturnPackageToPool(this);
            PackageService.Instance.packageCount--;
            PackageService.Instance.StopCoroutine(PackageService.Instance.SpawnTimer);
            PackageService.Instance.SpawnTimer = PackageService.Instance.StartCoroutine(PackageService.Instance.SpawnPackageTimer());
        }
    }
}