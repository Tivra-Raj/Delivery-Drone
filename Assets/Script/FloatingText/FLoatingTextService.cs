using Utility;

namespace FloatingText
{
    public class FLoatingTextService : MonoGenericSingleton<FLoatingTextService>
    {
        private FloatingTextPool floatingTextPool;

        void Start()
        {
            floatingTextPool = GetComponent<FloatingTextPool>();
        }

        public void SpawnFloatingText(float time)
        {
            CreateNewFloatingText(time);
        }
        
        public FloatingTextView CreateNewFloatingText(float time)
        {
            FloatingTextView floatingTextView = floatingTextPool.GetFloatingTextFromPool(time);
            return floatingTextView;        
        }

        public void ReturnFloatingTextToPool(FloatingTextView floatingTextToReturn)
        {
            floatingTextToReturn.gameObject.SetActive(false);
            floatingTextPool.ReturnItem(floatingTextToReturn);
        }
    }
}