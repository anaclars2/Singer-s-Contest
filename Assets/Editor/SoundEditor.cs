using UnityEditor;
using AudioSystem;

[CustomEditor(typeof(Audio))]
public class SoundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Audio _audio = (Audio)target;
        _audio.isMusic = EditorGUILayout.Toggle("Is Music", _audio.isMusic);

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
    }
}
