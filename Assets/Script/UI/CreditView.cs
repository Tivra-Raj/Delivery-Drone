﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CreditView : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }
    }
}