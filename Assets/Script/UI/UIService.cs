using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class UIService : MonoGenericSingleton<UIService>
    {
        [Header("Game Hud")]
        [SerializeField] private GameObject gameHud;
        [SerializeField] private TextMeshProUGUI fuelText;
        [SerializeField] private Slider fuelIndicator;
        [SerializeField] private TextMeshProUGUI toatalPackageDeliveredText;

        [Header("Game Over / Game Pause Parameters")]
        [SerializeField] private GameObject gameMenu;
        [SerializeField] private TextMeshProUGUI gameMenuText;
        public static bool GameIsPaused = false;
        public static bool GameIsOver = false;

        [Header("Game Menu Buttons")]
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button mainmenuButton;
        [SerializeField] private Button quitButton;

        [Header("Pause Menu Buttons")]
        [SerializeField] private Button resume;

        [Header("Tutorial PopUp")]
        [SerializeField] private GameObject tutorialPopUp;
        [SerializeField] private TextMeshProUGUI tutorialPopUpText;

        [Header("Tutorial Toggle")]
        [SerializeField] private GameTutorialView tutorialView;
        [SerializeField] private Toggle tutorialToggle;
        [SerializeField] private bool isTutorialActive;

        [Header("Delivery Timer")]
        [SerializeField] private TextMeshProUGUI deliveryTimeText;

        private void Start()
        {
            GetFuelIndicator();
            SetGameMenuUIActive(false);

            resume.onClick.AddListener(Resume);
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            mainmenuButton.onClick.AddListener(MainMenu);
            quitButton.onClick.AddListener(OnQuitClicked);
            tutorialToggle.onValueChanged.AddListener(SetTutorialToggle);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !GameIsOver)
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public float GetFuelIndicator() => fuelIndicator.maxValue;

        public void SetFuelIndicator(float fuelIndicator) => this.fuelIndicator.maxValue = fuelIndicator;

        public void UpdateFuelUI(float fuelToDisplay)
        {
            fuelIndicator.value = fuelToDisplay;
            fuelText.SetText("Fuel Left : " + fuelToDisplay.ToString("0") + "%");

            if(fuelToDisplay <= 0)
            {
                fuelToDisplay = 0;
                fuelText.SetText("Out of Fuel!!!");
            }

        }

        public void UpdateTotalPackageDeliveredText(int packageDelivered)
        {
            toatalPackageDeliveredText.SetText("Package Delivered : " + packageDelivered.ToString());
        }

        public void UpdateDeliveryTimerText(string text)
        {
            deliveryTimeText.SetText(text);
        }

        public void GameOver()
        {
            SetGameMenuUIActive(true);
            GameIsOver = true;
            tutorialToggle.gameObject.SetActive(false);
        }

        public void SetGameMenuUIActive(bool value)
        {
            gameMenu.SetActive(value);
        }  

        public GameObject GetTutorialPopUp() => tutorialPopUp;

        public void SetTutorialText(string text)
        {
            tutorialPopUpText.SetText(text);
        }

        public void SetTutorialToggle(bool isActive)
        {
            isTutorialActive = isActive;
            tutorialToggle.isOn = isTutorialActive;
            tutorialView.SetTutorialStatus(isTutorialActive);
            tutorialView.SaveTutorialStatus();
        }

        public bool GetIsTutorialActive() => isTutorialActive;

        private void Pause()
        {
            gameMenuText.SetText("GAME PAUSED");
            SetGameMenuUIActive(true);
            playAgainButton.gameObject.SetActive(false);
            Time.timeScale = 0f;             //setting the speed of time passing to 0 to freeze the time
            GameIsPaused = true;
        }

        public void Resume()
        {
            gameMenuText.SetText("GAME OVER");
            SetGameMenuUIActive(false);
            playAgainButton.gameObject.SetActive(true);
            Time.timeScale = 1f;           //setting the speed of time passing to normal to free the time
            GameIsPaused = false;
        }

        public void OnPlayAgainClicked()
        {
            Time.timeScale = 1f;
            GameIsOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void OnQuitClicked() => Application.Quit();
    }
}