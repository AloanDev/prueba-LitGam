using UnityEngine;
using UnityEngine.Serialization;

namespace Player_Logic
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "GameData")]
    public class GameData : ScriptableObject
    {
        public GameObject[] scenes;
        public int level;
       [SerializeField] public int[] targetsCount;
    }
}
