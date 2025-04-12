using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AudioSystem;

namespace RhythmSystem
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;

        [SerializeField] TMP_Text scoreText;
        static int comboScore;
        [SerializeField] int scoreAdd = 0;
        [SerializeField] int scoreRemove = 0;
        static int _scoreAdd, _scoreRemove;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }

        private void Start()
        {
            comboScore = 0;
            _scoreAdd = scoreAdd;
            _scoreRemove = scoreRemove;
        }

        public static void Hit()
        {
            comboScore = comboScore + _scoreAdd;
            AudioManager.instance.PlaySfx(SOUND.Hit);
        }

        public static void Miss()
        {
            // comboScore = comboScore - _scoreRemove;
            AudioManager.instance.PlaySfx(SOUND.Miss);
        }

        private void Update() { scoreText.text = "Score: " + comboScore.ToString(); }
    }
}