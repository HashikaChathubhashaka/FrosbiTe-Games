using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContinuousRunningGame
{
    public enum ContinuousGameState { None, Start, Run, Dodge, Revival, Pause, Resume, End }

    public class StateManager : MonoBehaviour
    {
        private static StateManager _instance;
        public static StateManager Instance { get { return _instance; } }
        
        public ContinuousGameState currentState;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            
            currentState = ContinuousGameState.Start;
        }

        public void SetTransition(ContinuousGameState requestedState)
        {
            currentState = requestedState;
        }

        private void Update()
        {
            SingletonManager.Instance.Player.PauseGame();

            // Update the current state
            switch (currentState)
            {
                case ContinuousGameState.Start:
                    SingletonManager.Instance.UIManager.PauseButtonAvailability(false);
                    break;
                case ContinuousGameState.Run:
                    SingletonManager.Instance.UIManager.PauseButtonAvailability(true);
                    break;
                case ContinuousGameState.Dodge:
                    SingletonManager.Instance.UIManager.PauseButtonAvailability(true);
                    break;
                case ContinuousGameState.Revival:
                    SingletonManager.Instance.UIManager.PauseButtonAvailability(false);
                    break;
                case ContinuousGameState.Pause:
                    SingletonManager.Instance.UIManager.PauseButtonAvailability(false);
                    break;
                case ContinuousGameState.Resume:
                    SetTransition(ContinuousGameState.Run);
                    SingletonManager.Instance.UIManager.PauseButtonAvailability(true);
                    break;
                case ContinuousGameState.End:
                    SingletonManager.Instance.UIManager.PauseButtonAvailability(false);
                    break;
                case ContinuousGameState.None:
                    break;
                default:
                    break;
            }

            
            
            
        }

        public void TransitionToState(ContinuousGameState nextState)
        {
            // Perform any exit actions for the current state
            switch (currentState)
            {
                case ContinuousGameState.Run:
                    // Stop player movement, animations, etc.
                    break;
                case ContinuousGameState.Pause:
                    // Unpause the game
                    break;
                // Add more cases as needed
                default:
                    break;
            }
            
            currentState = nextState;
            
            // Perform any enter actions for the next state
            switch (currentState)
            {
                case ContinuousGameState.Start:
                    // Start countdown, display idle animation
                    break;
                case ContinuousGameState.Run:
                    // Start player movement, running animations, etc.
                    break;
                case ContinuousGameState.Pause:
                    // Pause the game
                    break;
                // Add more cases as needed
                default:
                    break;
            }
        }
    }
    
    
}