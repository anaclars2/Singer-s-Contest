using AudioSystem;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.Linq;
using UISystem;
using UnityEngine;

namespace RhythmSystem
{
    public class Lane : MonoBehaviour
    {
        public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction; // restringe as notas a uma 'tonalidade'
        public KeyCode input;
        public GameObject notePrefab;
        [SerializeField] Arrow arrow;
        List<Note> notes = new List<Note>();
        [HideInInspector] public List<double> timeStamps = new List<double>();
        List<float> noteDurations = new List<float>();

        int spawnIndex = 0;
        int inputIndex = 0;

        // para os inputs
        static float normalMargin = 0.8f;
        static float goodMargin = 0.25f;
        static float perfectMargin = 0.1f;
        Note currentHeld; // para notas longas
        float holdTimer = 0f; // para notas longas

        [Header("Effects Settings")]
        [SerializeField] GameObject normalHit;
        [SerializeField] GameObject goodHit;
        [SerializeField] GameObject perfectHit;
        [SerializeField] GameObject missHit;
        Vector3 effectPosition;
        Quaternion effectRotation;
        float volume;

        private void Start()
        {
            arrow = GetComponentInChildren<Arrow>();
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, RhythmManager.instance.noteTapY, arrow.transform.localPosition.z);

            effectPosition = arrow.gameObject.transform.position;
            effectRotation = normalHit.gameObject.transform.rotation; // tanto faz pq todos tem a mesma rotacao

            volume = AudioManager.instance.playerSource.volume;
            if (volume == 0) { volume = 0.2f; }
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
                    n.duration = duration;
                    if (duration > 0.5f)
                    {
                        n.isLong = true;
                    }

                    spawnIndex++; // ir para proxima nota 
                }
            }
        }

        private void CheckMargin(float collider, float position)
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
                KeyDown();
                KeyPressed(); // para as notas longas, segurando a tecla :D
                KeyUp(); // tbm para notas longas
            }
        }

        private void KeyDown()
        {
            if (Input.GetKeyDown(input))
            {
                List<Note> validNotes = new List<Note>();

                foreach (Note note in notes)
                {
                    if (note.canBePressed)
                    {
                        validNotes.Add(note);
                    }
                }

                if (validNotes.Count == 0) return;
                Note closestNote = validNotes.OrderBy(n => Mathf.Abs(n.transform.position.y - n.colliderPosition)).First();

                AudioManager.instance.playerSource.volume = volume;

                if (closestNote.isLong)
                {
                    currentHeld = closestNote;
                    holdTimer = 0f;
                    closestNote.isPressed = true;
                }
                else
                {
                    CheckMargin(closestNote.colliderPosition, closestNote.transform.position.y);
                    RemoveNote(closestNote);
                }

                inputIndex++;
            }
        }

        private void KeyPressed()
        {
            if (Input.GetKey(input) == true && currentHeld != null)
            {
                holdTimer += Time.deltaTime;
                AudioManager.instance.playerSource.volume = volume;

                // currentHeld.UpdateLine(holdTimer / currentHeld.duration);
                if (holdTimer >= currentHeld.duration) // nota longa concluida com sucesso
                {
                    float colliderPosition = currentHeld.colliderPosition;
                    float position = currentHeld.transform.position.y;
                    CheckMargin(colliderPosition, position);

                    RemoveNote(currentHeld);
                    inputIndex++;
                    currentHeld = null;
                }
            }
        }

        private void KeyUp()
        {
            // soltou antes do tempo
            if (Input.GetKeyUp(input) == true && currentHeld != null)
            {
                RemoveNote(currentHeld);
                inputIndex++;
                currentHeld = null;
            }
        }

        private void RemoveNote(Note _note = null)
        {
            notes.Remove(_note);
            Destroy(_note.gameObject);
        }

        public void MissNote(bool withFeedback = true)
        {
            if (withFeedback == true) { Instantiate(missHit, effectPosition, effectRotation); }
            ScoreManager.instance.Miss(withFeedback);
            AudioManager.instance.playerSource.volume = 0f;
        }

    }
}