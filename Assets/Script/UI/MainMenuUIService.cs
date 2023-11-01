using Sound;
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
            SoundService.Instance.PlaySoundEffects(SoundType.Button);
            SceneManager.LoadScene(1);
        }

        public void LoadCreditScene()
        {
            SoundService.Instance.PlaySoundEffects(SoundType.Button);
            SceneManager.LoadScene(creditSceneIndex);
        }

        private void Quit()
        {
            SoundService.Instance.PlaySoundEffects(SoundType.Button);
            Application.Quit();
        }
    }
}