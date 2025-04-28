using AudioSystem;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace RhythmSystem
{
    public class RhythmManager : MonoBehaviour
    {
        [Header("Visual Settings")]
        [SerializeField] Image imageProgress;
        float songDuration;
        float songTime;
        bool songIsOver = false;

        [Header("Song Settings")]
        public MUSIC musicID;
        public float songDelay; // em segundos
        public Lane[] lanes;

        [Header("MIDI Settings")]
        string fileLocation; // .mid
        public static MidiFile midiFile; // .mid apos ser analisado

        [Header("Note Settings")]
        public float noteTime; // tempo na tela
        public float noteSpawnY;
        public float noteTapY;

        [Header("Error Settings")]
        [SerializeField, Range(0, 100)] int percentageError;
        int allowedErrorRate;
        public int notesError;
        // [SerializeField] TMP_Text allowedText;
        [SerializeField] TMP_Text errorText;
        bool statisticsActived = false;

        public static RhythmManager instance;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }

        private void Start()
        {
            // carregando dados do arquivo .mid
            fileLocation = GetFileLocation();
            midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
            GetDataFromMidi();

            songIsOver = false;
        }

        private void Update()
        {
            ProgressMusic();
            CheckErrorNotes();

            errorText.text = "Erros: " + notesError + "/" + allowedErrorRate;
        }

        private void GetDataFromMidi()
        {
            var notes = midiFile.GetNotes(); // pegando as notas
            var arrayNote = new Melanchall.DryWetMidi.Interaction.Note[notes.Count]; // instanciando uma matriz
            notes.CopyTo(arrayNote, 0); // colocando as notas no array

            foreach (var lane in lanes) { lane.SetTimeStamps(arrayNote); }
            Invoke(nameof(StartMusic), songDelay);

            // descobrindo o valor de notas que pode errar
            int numberNotes = notes.Count;
            allowedErrorRate = (numberNotes * percentageError) / 100;
        }

        private void StartMusic() { AudioManager.instance.PlayRhythmMusic(musicID); }

        public static double GetAudioSourceTime()
        {
            // retorna o tempo do audio
            return (double)AudioManager.instance.playerSource.timeSamples / AudioManager.instance.playerSource.clip.frequency;
        }

        private string GetFileLocation()
        {
            Audio audio = AudioManager.instance.musicList.Find(a => a.musicID == musicID);
            if (audio == null)
            {
                Debug.Log("Could not access midi file because no Audio was found.");
                return null;
            }
            // Debug.Log($"FileLocation: {audio.midiLocation}");
            return audio.midiLocation;
        }

        private void ProgressMusic()
        {
            songTime = AudioManager.instance.playerSource.time;
            songDuration = AudioManager.instance.playerSource.clip.length;

            if (songDuration > 0 && songIsOver == false)
            {
                imageProgress.fillAmount = songTime / songDuration;
                if (songTime / songDuration > 0.99f)
                {
                    imageProgress.fillAmount = 1;
                    songIsOver = true;
                }
            }
        }

        private void CheckErrorNotes()
        {
            if (notesError > allowedErrorRate && statisticsActived == false)
            {
                statisticsActived = true;
                AudioManager.instance.StopPlayer();
                AudioManager.instance.StopMusic();
                
                UIManager.instance.Transition(TRANSITION.CloseAndOpen, SCENES.Exploration);
            }
        }

        private void CheckVictory()
        {
            if (songIsOver == true)
            {
                GameManager.instance.RhythmCombatVictory();
                UIManager.instance.Transition(TRANSITION.CloseAndOpen, SCENES.Exploration);
            }
        }
    }
}