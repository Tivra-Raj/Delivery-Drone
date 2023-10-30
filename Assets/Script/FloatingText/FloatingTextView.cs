using System.Collections;
using UnityEngine;

namespace FloatingText
{
    public class FloatingTextView : MonoBehaviour
    {
        private Coroutine countDown;

        private void OnEnable()
        {
            stopCoroutine(countDown);
            countDown = StartCoroutine(DisableCountdown());
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
            FLoatingTextService.Instance.ReturnFloatingTextToPool(this);
        }

        public IEnumerator DisableCountdown()
        {
            yield return new WaitForSeconds(3);
            Destroy();
        }

        private void stopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}