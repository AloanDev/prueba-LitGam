using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class AnimationManager : MonoBehaviour
    {
        #region Variables

        public Animator anim;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private Button buttonSelect;
        private static readonly int Animation = Animator.StringToHash("Animation");
        [SerializeField] private string nameScene;
        private GameManager _instance;

        #endregion

        #region Methods

        private void Awake()
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Toggle>().onValueChanged.AddListener(SelectAnimation);
            }

            buttonSelect.onClick.AddListener(StartGame); //Stand button that leads to the gameplay screen.
        }

        private void SelectAnimation(bool active) //Select animation
        {
            for (var i = 0; i < buttons.Length; i++) //List of buttons for animations
            {
                if (!buttons[i].GetComponent<Toggle>().isOn) continue;
                anim.SetInteger(Animation, i);
                PlayerPrefs.SetInt("Animation", i);
            }
        }

        private void StartGame()
        {
            SceneManager.LoadScene(nameScene); //Loads the gameplay scene because I have set it in the object component in the inspector.
        }

        #endregion
    }
}