using UnityEngine;
using AudioSystem;
using Melanchall.DryWetMidi.Core;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Collections;
using Melanchall.DryWetMidi.Interaction;

namespace RhythmSystem
{
    public class RhythmManager : MonoBehaviour
    {
        public static RhythmManager instance;

        [Header("Song Settings")]
        [SerializeField] MUSIC musicID;
        public float songDelay; // em segundos
        public int inputDelay; // em milisegundos
        [SerializeField] Lane[] lanes;

        [Header("MIDI Settings")]
        [SerializeField] string[] midiLocation;
        string fileLocation; // .mid
        public static MidiFile midiFile; // .mid apos ser analisado

        [Header("Note Settings")]
        public float noteTime; // tempo na tela
        public float noteSpawnY;
        [SerializeField] float noteTapY;
        public float noteDespawnY { get { return noteTapY - (noteSpawnY - noteTapY); } }

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
        }

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
    }
}