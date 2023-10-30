using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUIService : MonoBehaviour
    {
        [Header("Main Menu Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button creditButton;
        [SerializeField] private Button quitButton;

        [Header("Credit")]
        [SerializeField] private int creditSceneIndex;

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            creditButton.onClick.AddListener(LoadCreditScene);
            quitButton.onClick.AddListener(Quit);   
        }

        private void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadCreditScene()
        {
            SceneManager.LoadScene(creditSceneIndex);
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}