using ScriptableObjects;
using System.Collections;
using UnityEngine;
using Utility;

namespace Package
{
    public class PackageService : MonoGenericSingleton<PackageService>
    {
        [SerializeField] private PackageScriptableObject[] ConfigPackage;
        private PackagePool packagePool;

        [SerializeField] private Transform[] SpawnPosition;
        [SerializeField] private int TotalPackageCount = 10;
        [SerializeField] private int TimeNeededToSpawnNextPackage = 10;
        private float TimeRemainingSeconds = 0;

        public int packageCount = 0;
        public GameObject PackageMarker;
        public Coroutine SpawnTimer { get; set; }

        private void Start()
        {
            packagePool = GetComponent<PackagePool>();
            
            for (int i = 0; i < 2; i++)
            {
                SpawnPackage();
            }
            TimeRemainingSeconds = TimeNeededToSpawnNextPackage;
            SpawnTimer = StartCoroutine(SpawnPackageTimer());
        }

        public IEnumerator SpawnPackageTimer()
        {
            while (packageCount < TotalPackageCount)
            {  
                yield return new WaitForSeconds(TimeRemainingSeconds);  
                SpawnPackage();
            }
        }

        private void SpawnPackage()
        {
            TimeRemainingSeconds = TimeNeededToSpawnNextPackage;
            int pickRandomPackageSpawnPosition = Random.Range(0, SpawnPosition.Length);

            CreateNewPackage(SpawnPosition[pickRandomPackageSpawnPosition]);
            packageCount++;
        }

        private PackageController CreateNewPackage(Transform spawnPosition)
        {
            int pickRandomPackage = Random.Range(0, ConfigPackage.Length);
            PackageScriptableObject packageScriptableObject = ConfigPackage[pickRandomPackage];

            PackageController packageController = packagePool.GetPackage(packageScriptableObject.PackagePrefab, spawnPosition.position, spawnPosition.rotation);
            return packageController;
        }

        public void ReturnPackageToPool(PackageController packageToReturn) => packagePool.ReturnItem(packageToReturn);
    }
}