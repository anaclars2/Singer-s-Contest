using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AudioSystem;

namespace RhythmSystem
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;

        [Header("Score Settings")]
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text multiplierText;
        int comboScore;

        [SerializeField] int scoreAddNormal = 100; // por nota 'NORMAL'
        [SerializeField] int scoreAddGood = 125; // por nota 'GOOD'
        [SerializeField] int scoreAddPerfect = 175; // por nota 'PERFECT'
        [SerializeField] int scoreRemove = 0; // por nota tbm

        int scoreMultiplier;
        int multiplierTracker; // quando vamos para o proximo 'nivel' de multiplicador
        [SerializeField] int[] multiplierThresholds;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }

        private void Start()
        {
            comboScore = 0; // provisorio ate ter o sistema de save
            scoreMultiplier = 1;
        }

        public void Hit(string hitType)
        {
            if (hitType == "normal")
            {
                NoteHit(scoreAddNormal);
            }
            else if (hitType == "good")
            {
                NoteHit(scoreAddGood);
            }
            else if (hitType == "perfect")
            {
                NoteHit(scoreAddPerfect);
            }
        }

        private void NoteHit(int scoreAdd)
        {
            AddMultipler();

            comboScore += scoreAdd * scoreMultiplier;
            scoreText.text = "Score: " + comboScore.ToString();
            AudioManager.instance.PlaySfx(SOUND.Hit);
        }

        public void Miss(bool withSound = true)
        {
            RemoveMultipler();

            // comboScore -= scoreRemove;
            // scoreText.text = "Score: " + comboScore.ToString();

            if (withSound == true) { AudioManager.instance.PlaySfx(SOUND.Miss); }
            RhythmManager.instance.notesError++;
        }

        private void AddMultipler()
        {
            // mudando os 'niveis' do multiplicaodr
            if (scoreMultiplier - 1 < multiplierThresholds.Length)
            {
                multiplierTracker++;
                if (multiplierThresholds[scoreMultiplier - 1] <= multiplierTracker)
                {
                    multiplierTracker = 0;
                    scoreMultiplier++;
                    multiplierText.text = "Multiplier: x" + scoreMultiplier.ToString();
                }
            }
        }

        private void RemoveMultipler()
        {
            scoreMultiplier = 1;
            multiplierTracker = 0;

            multiplierText.text = "Multiplier: x" + scoreMultiplier.ToString();
        }
    }
}