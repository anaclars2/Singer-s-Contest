using UnityEngine;
using AudioSystem;
using Melanchall.DryWetMidi.Core;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Collections;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine.UI;

namespace RhythmSystem
{
    public class RhythmManager : MonoBehaviour
    {
        public static RhythmManager instance;

        [Header("Visual Settings")]
        [SerializeField] Image imageProgress;
        float songDuration;
        float songTime;
        bool songIsOver = false;

        [Header("Song Settings")]
        [SerializeField] MUSIC musicID;
        public float songDelay; // em segundos
        [SerializeField] Lane[] lanes;

        [Header("MIDI Settings")]
        [SerializeField] string[] midiLocation;
        string fileLocation; // .mid
        public static MidiFile midiFile; // .mid apos ser analisado

        [Header("Note Settings")]
        public float noteTime; // tempo na tela
        public float noteSpawnY;
        public float noteTapY;

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

        private void Update() { ProgressMusic(); }

        private void GetDataFromMidi()
        {
            var notes = midiFile.GetNotes(); // pegando as notas
            var arrayNote = new Melanchall.DryWetMidi.Interaction.Note[notes.Count]; // instanciando uma matriz
            notes.CopyTo(arrayNote, 0); // colocando as notas no array

            foreach (var lane in lanes) { lane.SetTimeStamps(arrayNote); }
            Invoke(nameof(StartMusic), songDelay);
        }

        private void StartMusic() { AudioManager.instance.PlayRhythmMusic(musicID); }

        public static double GetAudioSourceTime()
        {
            // retorna o tempo do audio
            return (double)AudioManager.instance.playerSource.timeSamples / AudioManager.instance.playerSource.clip.frequency;
        }

        private string GetFileLocation()
        {
            int i = (int)musicID;
            Debug.Log("FileLocation i: " + i);
            switch (musicID)
            {
                case MUSIC.None:
                    return midiLocation[i];
                case MUSIC.Test:
                    return midiLocation[i];
                case MUSIC.Background:
                    return midiLocation[i];
                case MUSIC.Battle:
                    return midiLocation[i];
                case MUSIC.Menu:
                    return midiLocation[i];
            }
            return null;
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
    }
}