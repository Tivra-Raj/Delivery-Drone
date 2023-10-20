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

        [SerializeField] private int creditSceneIndex;

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            creditButton.onClick.AddListener(LoadCreditScene);
            quitButton.onClick.AddListener(Quit);
        }

        private void Play()
        {
            //GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
            SceneManager.LoadScene(1);
        }

        public void LoadCreditScene()
        {
            //GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
            SceneManager.LoadScene(creditSceneIndex);
        }

        private void Quit()
        {
            //GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
            Application.Quit();
        }
    }
}