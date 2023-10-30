using MVCs;
using System.Collections;
using UI;
using UnityEngine;

namespace Tutorial
{
    public class GameTutorialView : MonoBehaviour
    {
        [SerializeField] private GameObject[] gasLocationMarker;
        [SerializeField] private float tutorialDisableTimer = 5f;
        private int tutorialIndex = 7;
        private string tutorialText;
        private Coroutine timer;

        public bool isTutorialActive;

        private void Start ()
        {
            LoadTutorialStatus();
            DisableGasLocationMarker();
        }

        void Update()
        {
            if(isTutorialActive)
            {
                HandleTutorialPopUps();
                HandleTutorials();
            }
            else
            {
                UIService.Instance.GetTutorialPopUp().SetActive(false);
            }
        }

        public void LoadTutorialStatus()
        {
            isTutorialActive = PlayerPrefs.GetInt("IsTutorialActive", 1) == 1;
            UIService.Instance.SetTutorialToggle(isTutorialActive);
        }

        public void SaveTutorialStatus()
        {
            int setTutorialActiveStatus = UIService.Instance.GetIsTutorialActive() ? 1 : 0;
            PlayerPrefs.SetInt("IsTutorialActive", setTutorialActiveStatus);
            PlayerPrefs.Save();
        }

        public void SetTutorialStatus(bool isActive)
        {
            isTutorialActive = isActive;
        }

        private void HandleTutorialPopUps()
        {
            if (tutorialIndex != 0)
            {
                UIService.Instance.GetTutorialPopUp().SetActive(true);
            }
            else
            {
                tutorialText = "Great! Now you are ready to work on Deliver Drone. Objective: Race against time to deliver packages.";
                DisplayTutorial(tutorialText);
                timer = StartCoroutine(DisableTutorial(tutorialDisableTimer));
            }
        }

        private void HandleTutorials()
        {
            if(tutorialIndex == 7)
            {
                tutorialText = "Press Up Arrow to ascend and Down arrow to decend.";
                DisplayTutorial(tutorialText);

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    tutorialIndex--;
            }
            else if(tutorialIndex == 6)
            {
                tutorialText = "Press left Arrow to turn left and right arrow to turn right.";
                DisplayTutorial(tutorialText);

                if (Input.GetKeyDown(KeyCode.LeftArrow) ||  Input.GetKeyDown(KeyCode.RightArrow))
                    tutorialIndex--;
            }
            else if( tutorialIndex == 5)
            {
                tutorialText = "Press W to move forward and S to move backward.";
                DisplayTutorial(tutorialText);

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                    tutorialIndex--;
            }
            else if(tutorialIndex == 4)
            {
                tutorialText = "Press A to move left and D to move right";
                DisplayTutorial(tutorialText);

                if ( Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    tutorialIndex--;
            }
            else if(tutorialIndex == 3)
            {
                tutorialText = "Locate and Move towards big arrow pointing to packages, then Place your drone above package and press 'E' to pick up";
                DisplayTutorial(tutorialText);
               
                if (DroneService.Instance.DroneController.DroneView.IsAttached)
                    tutorialIndex--;
            }
            else if(tutorialIndex == 2)
            {
                tutorialText = "Locate Delivery location and Move towards it, then Place your drone inside marked circle and press 'E' to Deliver";
                DisplayTutorial(tutorialText);

                if (!DroneService.Instance.DroneController.DroneView.IsAttached)
                    tutorialIndex--;
            }
            else if (tutorialIndex == 1)
            {
                EnableGasLocationMarker();

                tutorialText = "If the drone's fuel reaches 0, the drone will not be able to fly. To keep it flying, you need to fill up the fuel. Reach any marked gas station and Hold 'E' to fill fuel.";
                DisplayTutorial(tutorialText);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    tutorialIndex--;
                    DisableGasLocationMarker();
                }
                    
            }
        }

        private void DisplayTutorial(string text)
        {
            UIService.Instance.SetTutorialText(text);
        }

        private IEnumerator DisableTutorial(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            UIService.Instance.GetTutorialPopUp().SetActive(false);
        }

        private void EnableGasLocationMarker()
        {
            for (int i = 0; i < gasLocationMarker.Length; i++)
                gasLocationMarker[i].SetActive(true);
        }

        private void DisableGasLocationMarker()
        {
            for (int i = 0; i < gasLocationMarker.Length; i++)
                gasLocationMarker[i].SetActive(false);
        }
    }
}