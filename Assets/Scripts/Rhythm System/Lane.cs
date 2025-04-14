using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UIElements;

namespace RhythmSystem
{
    public class Lane : MonoBehaviour
    {
        public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction; // restringe as notas a uma 'tonalidade'
        public KeyCode input;
        [SerializeField] GameObject notePrefab;
        [SerializeField] Arrow arrow;
        List<Note> notes = new List<Note>();
        [HideInInspector] public List<double> timeStamps = new List<double>();
        List<float> noteDurations = new List<float>();

        int spawnIndex = 0;
        int inputIndex = 0;

        // para os inputs
        static float normalMargin = 0.5f;
        static float goodMargin = 0.25f;
        static float perfectMargin = 0.1f;

        [Header("Effects Settings")]
        [SerializeField] GameObject normalHit;
        [SerializeField] GameObject goodHit;
        [SerializeField] GameObject perfectHit;
        [SerializeField] GameObject missHit;
        Vector3 effectPosition;
        Quaternion effectRotation;

        private void Start()
        {
            arrow = GetComponentInChildren<Arrow>();
            effectPosition = arrow.gameObject.transform.position;
            effectRotation = normalHit.gameObject.transform.rotation; // tanto faz pq todos tem a mesma rotacao
        }

        // decididndo as notas que precisamos e nao precisamos
        public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
        {
            var tempoMap = RhythmManager.midiFile.GetTempoMap();
            foreach (var note in array)
            {
                if (note.NoteName == noteRestriction)
                {
                    var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
                    var durationTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Length, tempoMap);

                    double startSeconds = metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
                    double durationSeconds = durationTimeSpan.Minutes * 60f + durationTimeSpan.Seconds + (double)durationTimeSpan.Milliseconds / 1000f;

                    timeStamps.Add(startSeconds);
                    noteDurations.Add((float)durationSeconds);
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

                    Note n = note.GetComponent<Note>();
                    n.lane = this;
                    notes.Add(n); // referencia

                    n.assignedTime = (float)timeStamps[spawnIndex]; // para a nota saber onde se posicionar

                    // verificando se a duracao é suficiente para ser uma nota longa
                    float duration = noteDurations[spawnIndex];
                    if (duration > 0.7f) 
                    {
                        n.isLong = true;
                        n.duration = duration;
                    }

                    spawnIndex++; // ir para proxima nota 
                }
            }
        }

        public void CheckMargin(float collider, float position)
        {
            float distance = Mathf.Abs(collider - position);
            string hit = " ";

            if (distance <= perfectMargin)
            {
                Instantiate(perfectHit, effectPosition, effectRotation);
                hit = "perfect";
            }
            else if (distance <= goodMargin)
            {
                Instantiate(goodHit, effectPosition, effectRotation);
                hit = "good";
            }
            else if (distance <= normalMargin)
            {
                Instantiate(normalHit, effectPosition, effectRotation);
                hit = "normal";
            }

            ScoreManager.instance.Hit(hit);

            // Debug.Log($"DISTANCE: {distance} | PERFECT: {perfectMargin}, GOOD: {goodMargin}, NORMAL: {normalMargin}");
            // Debug.Log($"POSITION NOTE: {collider} | TRANSFORM: {transform.position.y}\n| HIT TYPE: {hit}");
        }

        private void PlayerInput()
        {
            if (inputIndex < timeStamps.Count) // verificando as notas
            {
                if (Input.GetKeyDown(input))
                {
                    bool find = false;
                    for (int i = notes.Count - 1; i >= 0; i--)
                    {
                        if (notes[i].canBePressed == true)
                        {
                            float colliderPosition = notes[i].colliderPosition;
                            float position = notes[i].transform.position.y;
                            CheckMargin(colliderPosition, position);

                            Note temp = notes[i];
                            notes.RemoveAt(i);
                            Destroy(temp.gameObject);

                            inputIndex++;
                            // Debug.Log($"Hit on {inputIndex} note");
                            find = true;
                            break;
                        }
                    }
                    if (find == false) // quando o jogador errar :P
                    {
                        ScoreManager.instance.Miss();
                        Instantiate(missHit, effectPosition, effectRotation);
                        // Debug.Log($"Missed {inputIndex} note");
                        inputIndex++;
                    }
                }
            }
        }
    }
}