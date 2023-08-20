using UnityEngine;

namespace Package
{
    public class PackageView : MonoBehaviour
    {
        public PackageController PackageController { get; private set; }

        public void SetPackageController(PackageController packageController)
        {
            PackageController = packageController;
        }
    }
}