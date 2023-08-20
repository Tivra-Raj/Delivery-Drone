using Package;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PackageScriptableObject", menuName = "ScriptableObject/CreateNewPackageScriptableObject")]
    public class PackageScriptableObject : ScriptableObject
    {
        public PackageView PackagePrefab;
    }
}