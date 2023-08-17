using UnityEngine;

namespace Utility
{
    public class MonoGenericSingleton<T> : MonoBehaviour where T : MonoGenericSingleton<T>
    {
        
    }
}