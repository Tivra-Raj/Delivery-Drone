using UnityEngine;

namespace Package
{
    public class PackageController
    {
        public PackageView PackageView { get; private set; }

        public PackageController(PackageView packageView)
        {
            PackageView = GameObject.Instantiate<PackageView>(packageView);

            packageView.SetPackageController(this);
        }

        public void Configure(Vector3 setPosition)
        {
            PackageView.transform.position = setPosition;
        }
    }
}