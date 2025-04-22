using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        // referenciando os audios sources, em outras palavras, os caras que sao fonte de som
        public AudioSource playerSource;
        public AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        public List<Audio> sfxList;
        public List<Audio> musicList;

        public static AudioManager instance;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }

            DontDestroyOnLoad(gameObject);
        }
        private void Start() { LoadVolumes(); }

        // tocar os audios
        #region PlayAudio
        public void PlaySfx(SOUND audioID, int index = -1, bool randomPitch = true)
        {
            Audio audio = sfxList.Find(a => a.soundID == audioID);
            if (audio != null && index < audio.clip.Count)
            {
                /*Debug.Log("audio encontrado: " + audio.soundID +
                "\nquantidade de clips: " + audio.clip.Count +
                "\nindice do audio: " + index);*/

                if (index == -1) { index = UnityEngine.Random.Range(0, audio.clip.Count); } // determinando o audio p sorte

                if (randomPitch == true) { sfxSource.pitch = UnityEngine.Random.Range(audio.minPitch, audio.maxPitch); } // nao ficar repetitivo, distorcao de pitch :D
                else { sfxSource.pitch = 1f; } // sfx ficar normal, sem distorcao de pitch

                sfxSource.PlayOneShot(audio.clip[index]);
            }

            // Debug.Log("index sfx: " + index + "\nrandom pitch: " + sfxSource.pitch);
        }

        public void PlayRhythmMusic(MUSIC musicID, int index = -1)
        {
            PlayMusic(musicID, musicSource, 0);
            PlayMusic(musicID, playerSource, 1);
        }

        public void PlayMusic(MUSIC musicID, AudioSource audioSource, int index = -1)
        {
            Audio audio = musicList.Find(a => a.musicID == musicID);

            if (audio != null && index < audio.clip.Count)
            {
                /*Debug.Log("audio encontrado: " + audio.musicID +
                "\nquantidade de clips: " + audio.clip.Count +
                "\nindice do audio: " + index);*/

                if (index == -1) { index = UnityEngine.Random.Range(0, audio.clip.Count); } // determinando o audio p sorte

                audioSource.Stop();
                audioSource.clip = audio.clip[index];
                if (audio.isLoop == true) { audioSource.loop = true; }
                audioSource.Play();
            }

            // Debug.Log("index music: " + index);
        }
        #endregion

        // parar de tocar os audios
        #region StopAudio
        public void StopMusic() { musicSource.Stop(); }
        public void StopPlayer() { playerSource.Stop(); }

        public void StopSFX() { sfxSource.Stop(); }
        #endregion

        // mudar volume
        #region SetVolume
        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat("VolumeMusic", volume);
            PlayerPrefs.Save();
            Debug.Log("volume music: " + volume);
        }
        public void SetPlayerVolume(float volume)
        {
            playerSource.volume = volume;
            PlayerPrefs.SetFloat("VolumePlayer", volume);
            PlayerPrefs.Save();
            Debug.Log("volume player: " + volume);
        }
        public void SetSFXVolume(float volume)
        {
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat("VolumeSFX", volume);
            PlayerPrefs.Save();
            Debug.Log("volume sfx: " + volume);
        }
        #endregion

        // quado comecar jogo config continuar mesma
        #region GetVolume
        public void GetMusicVolume()
        {
            float volume = PlayerPrefs.GetFloat("VolumeMusic", 0.2f);
            musicSource.volume = volume;
        }
        public void GetPlayerVolume()
        {
            float volume = PlayerPrefs.GetFloat("VolumePlayer", 0.2f);
            playerSource.volume = volume;
        }
        public void GetSFXVolume()
        {
            float volume = PlayerPrefs.GetFloat("VolumeSFX", 0.5f);
            sfxSource.volume = volume;
        }
        #endregion

        private void LoadVolumes()
        {
            GetMusicVolume();
            GetPlayerVolume();
            GetSFXVolume();
        }

        #region UnpauseAudio
        public void UnpauseMusic() { musicSource.UnPause(); }
        public void UnpausePlayer() { playerSource.UnPause(); }

        public void UnpauseSFX() { sfxSource.UnPause(); }
        #endregion

        #region PauseAudio
        public void PauseMusic() { musicSource.Pause(); }
        public void PausePlayer() { playerSource.Pause(); }

        public void PauseSFX() { sfxSource.Pause(); }
        #endregion
    }
}