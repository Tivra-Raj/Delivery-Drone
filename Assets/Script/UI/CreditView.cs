using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CreditView : MonoBehaviour
    {
        //[SerializeField] private SoundType soundType;

        private void Start()
        {
            //GameService.Instance.GetSoundView().PlayBackgroundMusic(soundType, true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }
    }
}