using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        private Animator _playerDancing;
        private static readonly int Animation = Animator.StringToHash("Animation");

        #endregion

        #region Singleton

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        #endregion

        #region Methods

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            DontDestroyOnLoad(this.gameObject); //The game manager is not destroyed
        }

        public void LoadDataAnimation() //Loads the selected animation and saves it as an integer.
        {
            _playerDancing = GameObject.FindWithTag("PlayerDancing").GetComponent<Animator>();
            int numAnimation = PlayerPrefs.GetInt("Animation");
            _playerDancing.SetInteger(Animation, numAnimation);
        }

        #endregion
    }
}