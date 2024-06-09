using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace ContinuousRunningGame {
    public class SoundManager : MonoBehaviour {


        #region VARIABLES

        [SerializeField] private Sound[] SoundClips;

        [SerializeField] private AudioSource BackgroundMusicAS;
        [SerializeField] private AudioSource SoundFXAS;

        private Dictionary<SoundTypes, AudioClip> allSoundClipsDic;

        #endregion


        #region UNITY METHODS
        private void Awake() {
            allSoundClipsDic = new Dictionary<SoundTypes, AudioClip>();

            foreach (var item in SoundClips) {
                allSoundClipsDic.Add(item.name, item.clip);
            }
        }

        internal void PlaySoundOneShot(SoundTypes name) {
            if (!DataManager.isSoundsOn) return;
            SoundFXAS.PlayOneShot(allSoundClipsDic[name]);
            
        }

        internal void PlayBackgroundMusic() {
            ManageMuteState();

            BackgroundMusicAS.clip = allSoundClipsDic[SoundTypes.BackgroundMusic];
            BackgroundMusicAS.Play();          
        }
        internal void ManageMuteState() {
            BackgroundMusicAS.mute = !DataManager.isMusicOn;
        }

        #endregion
    }

    [Serializable]
    public class Sound {
        public SoundTypes name;
        public AudioClip clip;
    }

    public enum SoundTypes {
       BackgroundMusic,CoinsCollect,PowerUp
    }

}