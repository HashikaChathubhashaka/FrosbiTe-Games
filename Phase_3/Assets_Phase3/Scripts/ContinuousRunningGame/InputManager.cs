using UnityEngine;

namespace ContinuousRunningGame {
    public class InputManager : MonoBehaviour {
        #region VARIABLES
        // Touch Input
        private Vector2 previousTouchPoint;
        float touchGap = 10;
        bool isTouchBegan;
        #endregion

        #region UNITY METHODS
        void Update() {
            #if UNITY_EDITOR
                DetectInputPhasesMouse();
            #else
                DetectInputPhases();
            #endif
            DetectInputPhasesKeyboard(); // Always call this method to handle keyboard input
        }
        #endregion

        #region INPUT
        private void DetectInputPhases() {
            foreach (Touch touch in Input.touches) {
                if (touch.position.y > Screen.height * 0.9f) return;

                switch (touch.phase) {
                    case TouchPhase.Began:
                        TouchBegan(touch.position);
                        break;

                    case TouchPhase.Moved:
                        TouchMove(touch.position);
                        break;
                }
            }
        }

        private void DetectInputPhasesMouse() {
            if (Input.GetMouseButtonDown(0)) {
                TouchBegan(Input.mousePosition);
            } else if (Input.GetMouseButton(0)) {
                TouchMove(Input.mousePosition);
            }
        }

        private void DetectInputPhasesKeyboard() {
            float moveValHori = 0f;
            float moveValVerti = 0f;

            if (Input.GetKey(KeyCode.UpArrow)) {
                moveValVerti = 1f;
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                moveValVerti = -1f;
            }

            if (Input.GetKey(KeyCode.LeftArrow)) {
                moveValHori = -1f;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                moveValHori = 1f;
            }

            if (moveValVerti != 0f) {
                SingletonManager.Instance.Player.SetPlayerMoveVerticale(moveValVerti);
            }

            if (moveValHori != 0f) {
                SingletonManager.Instance.Player.SetPlayerMoveHorizontal(moveValHori);
            }
        }

        private void TouchBegan(Vector2 currentTouchPoint) {
            previousTouchPoint = currentTouchPoint;
            isTouchBegan = true;
        }

        private void TouchMove(Vector2 currentTouchPoint) {
            if (!isTouchBegan) return;

            float moveValHori = currentTouchPoint.x - previousTouchPoint.x;
            float moveValVerti = currentTouchPoint.y - previousTouchPoint.y;

            if (moveValVerti * moveValVerti > moveValHori * moveValHori) {
                if (Mathf.Abs(moveValVerti) > touchGap) {
                    isTouchBegan = false;
                    SingletonManager.Instance.Player.SetPlayerMoveVerticale(moveValVerti);
                }
            } else {
                if (Mathf.Abs(moveValHori) > touchGap) {
                    isTouchBegan = false;
                    SingletonManager.Instance.Player.SetPlayerMoveHorizontal(moveValHori);
                }
            }
        }
        #endregion
    }
}
