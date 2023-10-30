using System;
using TMPro;
using UnityEngine;
using Utility;

namespace FloatingText
{
    public class FloatingTextPool : GenericObjectPool<FloatingTextView>
    {
        [SerializeField] private FloatingTextView floatingTextPrefab;
        [SerializeField] private GameObject floatingTextHolder;

        protected override FloatingTextView CreateItem()
        {
            if (floatingTextPrefab != null)
            {
                FloatingTextView newObject = Instantiate<FloatingTextView>(floatingTextPrefab);
                newObject.gameObject.SetActive(false);
                return newObject;
            }
            else
            {
                throw new Exception("Prefab to pool is not set in the GameObjectPool script.");
            }
        }

        public FloatingTextView GetFloatingTextFromPool(float value)
        {
            FloatingTextView item = GetItem();
            item.transform.SetParent(floatingTextHolder.transform, false);
            item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("+" + value + "s");
            item.gameObject.SetActive(true);
            return item;
        }
    }
}