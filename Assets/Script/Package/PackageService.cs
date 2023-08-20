using ScriptableObjects;
using UnityEngine;
using Utility;

namespace Package
{
    public class PackageService : MonoGenericSingleton<PackageService>
    {
        public PackageScriptableObject[] ConfigPackage;
        public Transform[] SpawnPosition;

        public PackageController PackageController { get; private set; }
        private PackagePool packagePool;
        

        private void Start()
        {
            packagePool = GetComponent<PackagePool>();
            for(int i = 0; i< 5; i++)
            {
                SpawnPackage();
            }
            
        }

        private void SpawnPackage()
        {
            int pickRandomPackageSpawnPosition = Random.Range(0, SpawnPosition.Length);
            CreateNewPackage(SpawnPosition[pickRandomPackageSpawnPosition]);
        }

        private PackageController CreateNewPackage(Transform spawnPosition)
        {
            int pickRandomPackage = Random.Range(0, ConfigPackage.Length);
            PackageScriptableObject packageScriptableObject = ConfigPackage[pickRandomPackage];

            PackageController = packagePool.GetPackage(packageScriptableObject.PackagePrefab);
            PackageController.Configure(spawnPosition.position);

            return PackageController;
        }

        public void ReturnPackageToPool(PackageController packageToReturn) => packagePool.ReturnItem(packageToReturn);
    }
}