using Utility;

namespace Package
{
    public class PackagePool : GenericObjectPool<PackageController>
    {
        private PackageView packagePrefab;

        public PackageController GetPackage(PackageView packagePrefab)
        {
            this.packagePrefab = packagePrefab;
            PackageController packageController = GetItem();
            packageController.PackageView.gameObject.SetActive(true);
            return packageController;
        }    

        protected override PackageController CreateItem() => new PackageController(packagePrefab);
    }
}