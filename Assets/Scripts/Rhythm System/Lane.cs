using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmSystem
{
    public class Lane : MonoBehaviour
    {
        public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction; // restringe as notas a uma 'tonalidade'
        [SerializeField] KeyCode input;
        [SerializeField] GameObject notePrefab;
        List<Note> notes = new List<Note>();
        [HideInInspector] public List<double> timeStamps = new List<double>();

        int spawnIndex = 0;
        int inputIndex = 0;

        // decididndo as notas que precisamos e nao precisamos
        public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
        {
            foreach (var note in array)
            {
                if (note.NoteName == noteRestriction)
                {
                    var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, RhythmManager.midiFile.GetTempoMap());
                    timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                }
            }
        }

        private void Update()
        {
            InstantiateNote();
            PlayerInput();
        }

        private void InstantiateNote()
        {
            if (spawnIndex < timeStamps.Count)
            {
                // gerando as n notas antes do jogador tentar acerta elas
                if (RhythmManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - RhythmManager.instance.noteTime)
                {
                    var note = Instantiate(notePrefab, transform);
                    notes.Add(note.GetComponent<Note>()); // referencia
                    note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex]; // para a nota saber onde se posicionar
                    spawnIndex++; // ir para proxima nota 
                }
            }
        }

        private void PlayerInput()
        {
            if (inputIndex < timeStamps.Count) // verificando as notas
            {
                double timeStamp = timeStamps[inputIndex];
                double marginError = RhythmManager.instance.marginError;
                double audioTime = RhythmManager.GetAudioSourceTime() - (RhythmManager.instance.inputDelay / 1000.0);

                if (Input.GetKeyDown(input))
                {
                    // verificando se o jogador conseguiu dar um 'hit' na margem permitida
                    if (Math.Abs(audioTime - timeStamp) < marginError)
                    {
                        Hit();
                        Debug.Log($"Hit on {inputIndex} note");
                        Destroy(notes[inputIndex].gameObject);
                        inputIndex++;
                    }
                    else { Debug.Log($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay"); }
                }
                if (timeStamp + marginError <= audioTime) // quando o jogador errar :P
                {
                    Miss();
                    print($"Missed {inputIndex} note");
                    inputIndex++;
                }
            }
        }

        private void Hit() { ScoreManager.Hit(); }

        private void Miss() { ScoreManager.Miss(); }
    }
}