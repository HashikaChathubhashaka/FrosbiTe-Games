using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ContinuousRunningGame
{
	public class UIManager : MonoBehaviour
	{
		#region VARIABLES

		[Header("UI")] 
		[SerializeField] GameObject MainLoadingPanel;
		[SerializeField] GameObject GamePlayUIPanel;
		[Space] 
		[SerializeField] private GameObject gameOverPanel;
		[SerializeField] private GameObject pauseButton;
		[SerializeField] private Image countDownImg;
		[SerializeField] private Sprite[] countDownSprites;
		[Space] 
		[SerializeField] private GameObject countdownClockParent;
		[SerializeField] private Image countdownClockImage;
		[SerializeField] private TextMeshProUGUI revivalCountdownText;

		[Header("START COUNTDOWN")] 
		bool isShowGameStartCountDown = false;
		float countDownRemaintime;
		
		bool isRevivalQuestionSartCountDown = false;
		[Space]
		[SerializeField] private float timeForQuestion = 10f;

		[Space] 
		[Header("GAME OVER")] 
		[SerializeField] Image ScoreLBButtonBackImg;

		[SerializeField] Image ScoreBtnImg;
		[SerializeField] Image LBBtnImg;

		[SerializeField] Sprite[] ScoreLBButtonBackSprite;
		[SerializeField] Sprite[] ScoreBtnSprite;
		[SerializeField] Sprite[] LBBtnSprite;
		[SerializeField] LeaderboardController LeaderboardController;

		[Header("PLAYER LIVES")] 
		[SerializeField] private GameObject heartContainer;
		[SerializeField] private GameObject heartFull;
		[SerializeField] private GameObject heartEmpty;

		[Header("Characters")]
		[SerializeField] private Image char1;
    	[SerializeField] private Image char2;
		[SerializeField] private Image char3;
		[SerializeField] private Image char4;
		[SerializeField] private Image char5;
		[SerializeField] private Image char6;

    	private int character_id = 3;
		
		private ContinuousGameState _continuousGameState;

		#endregion

		#region UNITY CALLBACKS



		private void Awake()
		{
			countDownImg.gameObject.SetActive(false);
			gameOverPanel.gameObject.SetActive(false);
		}

		private void Update()
		{
			Choose_character();

			if (StateManager.Instance.currentState == ContinuousGameState.None)
			{
			}
			
			if(StateManager.Instance.currentState == ContinuousGameState.Start)
			{
				UpdateGameStartCountDown();
			}

			if(StateManager.Instance.currentState == ContinuousGameState.Run)
			{
				
			}

			if (StateManager.Instance.currentState == ContinuousGameState.Revival)
			{
				isRevivalQuestionSartCountDown = true;
				UpdateRevivalCountDown();
				SingletonManager.Instance.Player.PlayerIdle();
			}
			else
			{
				isRevivalQuestionSartCountDown = false;
			}

		}

		public void Choose_character(){
			if (character_id == 1){
            char1.gameObject.SetActive(true);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(false);
        }
        if (character_id == 2){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(true);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(false);
        }

        if (character_id == 3){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(true);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(false);
        }

        if (character_id == 4){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(true);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(false);
        }

        if (character_id == 5){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(true);
            char6.gameObject.SetActive(false);
        }

        if (character_id == 6){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(true);
        }
		}

		#endregion

		#region BUTTON PRESS

		public void OnPressContinuePanleCloseButton()
		{
			ActiveGamePlayUIPanel(false);
		}

		public void OnPressGameOverScoreButton()
		{
			ScoreLBButtonBackImg.sprite = ScoreLBButtonBackSprite[0];
			ScoreBtnImg.sprite = ScoreBtnSprite[0];
			LBBtnImg.sprite = LBBtnSprite[0];
		}

		public void OnPressGameOverLBButton()
		{
			ScoreLBButtonBackImg.sprite = ScoreLBButtonBackSprite[1];
			ScoreBtnImg.sprite = ScoreBtnSprite[1];
			LBBtnImg.sprite = LBBtnSprite[1];
		}

		public void OnPressRestartButton()
		{
			SceneManager.LoadSceneAsync(4);
		}

		public void OnPressHomeButton()
		{
			SceneManager.LoadSceneAsync(3);
		}




		#endregion

		#region UI ANIMATIONS

		#region START COUNT DOWN

		internal void ShowGameStartCountDown()
		{
			isShowGameStartCountDown = true;
			countDownRemaintime = 4;
			countDownImg.gameObject.SetActive(true);
		}

		void UpdateGameStartCountDown()
		{
			if (!isShowGameStartCountDown) return;
			countDownRemaintime -= Time.deltaTime;
			SingletonManager.Instance.Player.PlayerIdle();
			switch (countDownRemaintime)
			{
				case > 3:
					countDownImg.sprite = countDownSprites[0];
					break;
				case > 2:
					countDownImg.sprite = countDownSprites[1];
					break;
				case > 1:
					countDownImg.sprite = countDownSprites[2];
					break;
				case > 0:
					countDownImg.sprite = countDownSprites[3];
					break;
				default:
					isShowGameStartCountDown = false;
					countDownImg.gameObject.SetActive(false);

					SingletonManager.Instance.Player.StartGame();
					break;
			}
		}

		private void UpdateRevivalCountDown()
		{
			if (!isRevivalQuestionSartCountDown) return;
			
			if (timeForQuestion < 0)
			{
				SingletonManager.Instance.GameManager.FinalizeEndGame();
				countdownClockImage.gameObject.SetActive(false);
				countdownClockParent.SetActive(false);
				//Enable a panel to inform player that time ran out, and then to lead to the leaderboard
				gameOverPanel.SetActive(true);
			}

			timeForQuestion -= Time.deltaTime;
			var intCountDownValue = (int)Math.Round(timeForQuestion);
			revivalCountdownText.text = intCountDownValue.ToString();

			countdownClockImage.fillAmount = timeForQuestion / 15f;
			
			switch (timeForQuestion)
			{
				case > 6:
					countdownClockImage.color = Color.green;
					break;
				case > 3:
					countdownClockImage.color = Color.yellow;
					break;
				case > 0:
					countdownClockImage.color = Color.red;
					break;
				default:
					break;
			}

		}


		#endregion

		#endregion

		#region UI AVAILABILITY
		public void PauseButtonAvailability(bool isEnable)
		{
			//pauseButton.SetActive(isEnable);
			pauseButton.GetComponent<Button>().interactable = isEnable;
		}
		

		#endregion
		
		#region PLAYER LIVES	

		public void InitializeLives()
		{
			if (heartContainer == null)
			{
				return;
			}

			foreach (Transform child in heartContainer.transform)
			{
				Destroy(child.gameObject);
			}
			
			var deadLives = SingletonManager.Instance.GameManager.totalLives - SingletonManager.Instance.GameManager.currentLives;
			for (var i = 0; i < SingletonManager.Instance.GameManager.totalLives; i++)
			{
				GameObject heart;

				if (deadLives > 0)
				{
					heart = Instantiate(heartEmpty);
				}
				else
				{ 
					heart = Instantiate(heartFull);
				}

				heart.transform.SetParent(heartContainer.transform, false);
				heart.GetComponent<RectTransform>().localPosition = new Vector3(heartContainer.GetComponent<RectTransform>().sizeDelta.x / 2 - i * (heart.GetComponent<RectTransform>().sizeDelta.x * 1.25f), 0, 0);
				deadLives--;
			}
		}

		public void EndGame(bool isGameOver)
		{
			if(isGameOver)
			{
				OnPressContinuePanleCloseButton();
			}
			
			if (gameOverPanel)
			{
				gameOverPanel.SetActive(isGameOver);
			}
		}

		#endregion

		internal void ShowMainLoadingPanel()
		{
			MainLoadingPanel.SetActive(true);
		}

		internal void HideMainLoadingPanel()
		{
			MainLoadingPanel.SetActive(false);
		}

		internal void ShowLoadingScreen()
		{
			Debug.Log("Impliment Loading Screen");
		}

		internal void HideLoadingScreen()
		{
			Debug.Log("Impliment Hide LoadingS creen");
		}

		internal void ShowErrorPopUpanel(string msg)
		{
			Debug.Log(msg);
		}


		internal void ActiveGamePlayUIPanel(bool state)
		{
			GamePlayUIPanel.SetActive(state);
		}


		internal void ShowGameOverLeaderboard(PromoScoreSubmitData data)
		{
			Debug.Log("Impliment Show GameOver Leaderboard");
			LeaderboardController.ShowLeaderboard(data);
		}
	}
}
