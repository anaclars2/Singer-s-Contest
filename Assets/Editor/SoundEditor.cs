using AudioSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Audio))]
public class SoundEditor : Editor
{
    SerializedProperty clipList;
    SerializedProperty midiLocation;

    private void OnEnable() 
    {
        clipList = serializedObject.FindProperty("clip");
        midiLocation = serializedObject.FindProperty("midiLocation");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //base.OnInspectorGUI();

        Audio _audio = (Audio)target;
        _audio.isMusic = EditorGUILayout.Toggle("Is Music", _audio.isMusic);
        EditorGUILayout.PropertyField(clipList, new GUIContent("Clips"), true);
        EditorGUILayout.PropertyField(midiLocation, new GUIContent("MIDI File Location"));

        if (_audio.isMusic)
        {
            _audio.musicID = (MUSIC)EditorGUILayout.EnumPopup("Music: ", _audio.musicID);
            _audio.isLoop = EditorGUILayout.Toggle("Loop", _audio.isLoop);
        }
        else
        {
            _audio.soundID = (SOUND)EditorGUILayout.EnumPopup("Sound Effect: ", _audio.soundID);

            _audio.hasPitchVariation = EditorGUILayout.BeginToggleGroup("Pitch Variation", _audio.hasPitchVariation);
            _audio.minPitch = EditorGUILayout.FloatField("Min", _audio.minPitch);
            _audio.maxPitch = EditorGUILayout.FloatField("Max", _audio.maxPitch);
            EditorGUILayout.EndToggleGroup();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
