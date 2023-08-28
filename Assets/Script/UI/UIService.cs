using TMPro;
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



        [Header("Game Over")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button quitButton;

        

        private void Start()
        {
            GetFuelIndicator();
            gameOverPanel.SetActive(false);
        }

        public float GetFuelIndicator()
        {
            return fuelIndicator.maxValue;
        }

        public void SetFuelIndicator(float fuelIndicator)
        {
            this.fuelIndicator.maxValue = fuelIndicator;
        }

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

        public void GameOver()
        {
            gameOverPanel.SetActive(true);
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        public void OnPlayAgainClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void OnQuitClicked() => Application.Quit();


    }
}