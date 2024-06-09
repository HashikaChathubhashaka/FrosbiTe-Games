using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContinuousRunningGame {
    public class SingletonManager : MonoBehaviour {
        #region VARIABLES
        public static SingletonManager Instance { get; private set; }

        [Header("SCRIPTS")]
        public GameManager GameManager;
        public Player Player;
        public PathManager PathManager;
        public UIManager UIManager;
        public SoundManager SoundManager;
        #endregion

        #region UNITY METHODS
        private void Awake() {
            if (Instance == null) Instance = this;
        }

        private void OnDestroy() {
            Instance = null;
        }
        #endregion
    }
}