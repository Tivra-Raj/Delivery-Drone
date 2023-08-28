using UnityEngine;
using Utility;

namespace Package
{
    public class PackagePool : GenericObjectPool<PackageController>
    {
        private PackageView packagePrefab;

        public PackageController GetPackage(PackageView packagePrefab, Vector3 packagePosition, Quaternion packageRotation)
        {
            this.packagePrefab = packagePrefab;

            PackageController packageController = GetItem();
            packageController.Configure(packagePosition, packageRotation);
            packageController.PackageView.gameObject.SetActive(true);
            return packageController;
        }    

        protected override PackageController CreateItem()
        {
            PackageController packageController = new PackageController(packagePrefab);
            return packageController;
        }
    }
}