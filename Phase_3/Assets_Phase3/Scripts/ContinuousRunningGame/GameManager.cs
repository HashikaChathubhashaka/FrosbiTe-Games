using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MainMenu;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Net.Http;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using PlayerInfo.initMenue;

namespace ContinuousRunningGame
{
	public class GameManager : MonoBehaviour
	{
		#region VARIABLES

		[Header("INTRO")]
		[SerializeField] SkeletonGraphic introSkeletonGraphic;

		[Header("GAME PLAY")]
		[SerializeField] TextMeshProUGUI collectedCoinsTxt;

		[SerializeField] TextMeshProUGUI coinValueTxt;

		[SerializeField] Image bottleFill;
		[SerializeField] Image bottleBack;
		[SerializeField] Sprite[] bottleImgs;

		[SerializeField] Image currentPowerupImg;
		[SerializeField] Sprite[] powerupImgs;
		[SerializeField] Button powerUpButton;
		enum PowerUpTypes { Magnet, SpeedUp, Shield }

		PowerUpTypes nextPowerUpType;
		internal bool isMagnetPowerOn;
		internal bool isSpeedUpPowerOn;
		internal bool isShieldPowerOn;

		float powerUpEnabledTimeLimit = 5;
		float spendedPowerUpTime = 0;

		bool isBottleFilled;


		int collectedCoinsCount;


		//these are custom varaibles want to change in future 
		int coinValue;

		int myscore;
		// int VATrate = 10; //want to change according to API
		int VATrate = initMenue.VATrate; //want to change according to API

		double VATvalue;
		int ElectricCharge;
		const int ValueOfOneKwh = 2;
		double TotalCharge;

		int collectedCoinsCountforBottleUI;
		int collectedCoinsLimtforBottleFill = 20;

		const int SCORE_FOR_A_COIN = 5;


		[SerializeField] GameObject speedupFXObj;

		[Header("UI")]
		[SerializeField] TextMeshProUGUI scoreTxt;




		int scoreVal;
		const int LOCK_KEY = 323232;


		[Space]
		[SerializeField] GameObject PausePanelParent;

		[Space]
		[SerializeField] GameObject ContinuePanelParent;


		[Header("Continue Quiz")]
		[SerializeField] TextMeshProUGUI QuizTxt;
		[SerializeField] Image QuizImg;
		[SerializeField] TextMeshProUGUI[] QuizAnswersArray;
		[SerializeField] QuizData QuizData;
		int correctAnswersIndex;
		int hintCollectionID;


		[Space]
		[SerializeField] CollectionData CollectionData;
		Dictionary<int, Collection> collectionDataDic = new Dictionary<int, Collection>();

		[SerializeField] GameObject CollectionInfoPanel;
		[SerializeField] TextMeshProUGUI InfoPanelTitle;
		[SerializeField] Image InfoPanelImage;
		[SerializeField] TextMeshProUGUI InfoPanelDescription;


		[Header("GameOver UI")]



		[SerializeField] TextMeshProUGUI finalpowerText;
		[SerializeField] TextMeshProUGUI ElectricChargeText;
		[SerializeField] TextMeshProUGUI VATValueText;
		[SerializeField] TextMeshProUGUI TotalChargeText;
		[SerializeField] TextMeshProUGUI FinalCollectedCoinText;
		[SerializeField] TextMeshProUGUI CoinsAfterPayText;

		[SerializeField] TextMeshProUGUI VATRateText;
		[SerializeField] TextMeshProUGUI ValueOfOneKwhText;



		[SerializeField] private StepCountManager StepCountManager;

		[Header("Player Lives")]
		public int totalLives = 1;
		public int currentLives { get; protected set; }

		private ContinuousGameState _continuousGameState;

		#endregion
		#region UNITY METHODS

		private void Awake()
		{
			Application.targetFrameRate = 60;
			SingletonManager.Instance.UIManager.ShowMainLoadingPanel();
			currentLives = totalLives;
		}

		void Start()
		{
			// Debug.Log("VATrate ggg: " + VATrate);
			introSkeletonGraphic.AnimationState.Complete += HandleEvent_AnimationComplete;
			SingletonManager.Instance.SoundManager.PlayBackgroundMusic();

			for (int i = 0; i < CollectionData.mCollectionData.Length; i++)
			{
				collectionDataDic.Add(CollectionData.mCollectionData[i].CollectionID, CollectionData.mCollectionData[i]);
			}

			SingletonManager.Instance.UIManager.InitializeLives();
			OnPressStartGamePlayButton();

		}

		private void Update()
		{
			UpdatePowerEnabledTimer();
			UpdateBottleFilledPopAnim();
		}

		#endregion

		#region INTRO

		public void StartIntroAnim()
		{
			introSkeletonGraphic.AnimationState.SetAnimation(0, "Intro_1", false);
			introSkeletonGraphic.timeScale = 1;
			introSkeletonGraphic.gameObject.SetActive(true);
		}
		void HandleEvent_AnimationComplete(TrackEntry trackEntry)
		{

			if (trackEntry.Animation.ToString() == "Intro_1")
			{
				introSkeletonGraphic.AnimationState.SetAnimation(0, "Intro_2", false);
			}
			else if (trackEntry.Animation.ToString() == "Intro_2")
			{
				introSkeletonGraphic.AnimationState.SetAnimation(0, "Intro_3", false);
			}
			else if (trackEntry.Animation.ToString() == "Intro_3")
			{
				introSkeletonGraphic.AnimationState.SetAnimation(0, "Intro_4", false);
			}
			else if (trackEntry.Animation.ToString() == "Intro_4")
			{
				introSkeletonGraphic.timeScale = 0;
				introSkeletonGraphic.gameObject.SetActive(false);
				StartGamePlay();
			}

		}

		#endregion

		#region ButtonPress

		public void OnPressStartGamePlayButton()
		{
			SingletonManager.Instance.APIManager.StartGame(false);
		}

		public void OnPressReStartGamePlayButton()
		{
			SceneManager.LoadScene(1);
			//SingletonManager.Instance.APIManager.StartGame(true);
			//StateManager.Instance.SetTransition(ContinuousGameState.Start);
			SetTimeScale(1);
		}

		public void OnPressPowerUpBtn()
		{

			if (nextPowerUpType == PowerUpTypes.Magnet)
			{
				OnMagnetPower();

			}
			else if (nextPowerUpType == PowerUpTypes.Shield)
			{
				OnShieldPower();

			}
			else if (nextPowerUpType == PowerUpTypes.SpeedUp)
			{
				OnSpeedUpPower();
			}
			currentPowerupImg.transform.GetComponent<Button>().interactable = false;
		}
		public void OnPressSwapPowerUpBtn()
		{
			if (nextPowerUpType == PowerUpTypes.Magnet)
			{
				nextPowerUpType = PowerUpTypes.SpeedUp;
				currentPowerupImg.sprite = powerupImgs[1];
			}
			else if (nextPowerUpType == PowerUpTypes.SpeedUp)
			{
				nextPowerUpType = PowerUpTypes.Shield;
				currentPowerupImg.sprite = powerupImgs[2];
			}
			else if (nextPowerUpType == PowerUpTypes.Shield)
			{
				nextPowerUpType = PowerUpTypes.Magnet;
				currentPowerupImg.sprite = powerupImgs[0];
			}
		}

		public void OnPressPauseBtn()
		{
			SetTimeScale(0);
			PausePanelParent.gameObject.SetActive(true);
		}
		public void OnPressResumeBtn()
		{

			SetTimeScale(1);
			PausePanelParent.gameObject.SetActive(false);

		}

		private void SetTimeScale(int value)
		{
			switch (value)
			{
				case 0:
					StateManager.Instance.SetTransition(ContinuousGameState.Pause);

					break;
				case 1:
					StateManager.Instance.SetTransition(ContinuousGameState.Run);
					break;
			}

			//Time.timeScale = value;
		}

		#endregion

		#region GAMEPLAY
		public void StartGamePlay()
		{

			powerUpButton.interactable = false;
			isSpeedUpPowerOn = false;
			isMagnetPowerOn = false;
			isShieldPowerOn = false;

			nextPowerUpType = PowerUpTypes.Magnet;
			currentPowerupImg.sprite = powerupImgs[0];

			speedupFXObj.gameObject.SetActive(false);
			SingletonManager.Instance.Player.SetPlayerGlow(false);

			SetScore(0);
			scoreTxt.text = GetScore().ToString();

			SettCollectedCoinsCount(0);
			collectedCoinsCountforBottleUI = GetCollectedCoinsCount();
			collectedCoinsTxt.text = GetCollectedCoinsCount().ToString();




			bottleFill.fillAmount = 0;
			bottleBack.sprite = bottleImgs[0];
			isBottleFilled = false;
			UpdateCoinCount(0);
			currentPowerupImg.transform.GetComponent<Button>().interactable = true;

			totalLives = 1;
			SetLives(1);

			SingletonManager.Instance.Player.InitGamePlay();


		}
		private void ContinueGamePlay()
		{

			Debug.Log("Game Manager/ContinueGamePlay has been called after correct answer");
			//SetPowerUpButtonActiveState(false);
			isSpeedUpPowerOn = false;
			isMagnetPowerOn = false;
			isShieldPowerOn = false;

			speedupFXObj.gameObject.SetActive(false);
			SingletonManager.Instance.Player.SetPlayerGlow(false);

			//SetScore(0);
			//scoreTxt.text = GetScore().ToString();

			//SettCollectedCoinsCount(0);
			//collectedCoinsCountforBottleUI = GetCollectedCoinsCount();
			//collectedCoinsTxt.text = GetCollectedCoinsCount().ToString();

			bottleBack.sprite = bottleImgs[0];
			isBottleFilled = false;
			UpdateCoinCount(0);
			//bottleFill.fillAmount = 0;
			//bottleFiledCount = 0;
			currentPowerupImg.transform.GetComponent<Button>().interactable = true;

			SingletonManager.Instance.Player.InitGamePlay();

		}

		async Task updateScore(string username, double score)
		{
			string jsonData = $@"{{
        		""username"": ""{username}"",
				""score"": {score}""}}";
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Put, "http://localhost:8090/score/updatescore");
			request.Headers.Add("Authorization", "Basic dXNlcjpwYXNzd29yZA==");
			var content = new StringContent(jsonData, null, "application/json");
			request.Content = content;
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();

		}

		async Task checkScore(string username, double score)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8090/score/getscore");
			request.Headers.Add("Authorization", "Basic dXNlcjpwYXNzd29yZA==");
			var content = new StringContent(username, null, "text/plain");
			request.Content = content;
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			await response.Content.ReadAsStringAsync();
			var result = response.Content.ReadAsStringAsync().Result;
			// JObject obj = (JObject)JsonConvert.DeserializeObject(result);
			// Debug.Log(obj["score"]);
			// if (score > double.Parse(obj["score"].ToString()))
			// {
			// 	await updateScore(username, score);
			// }

		}

		internal void GameOver()
		{
			string uname = initMenue.username;
			double finalscore = 0.0;
			InitQuiz();
			ContinuePanelParent.gameObject.SetActive(true);

			finalpowerText.text = GetScore().ToString() + " kWh"; //Getting total Kwh 

			ElectricCharge = GetScore() * ValueOfOneKwh;
			ElectricChargeText.text = (ElectricCharge).ToString() + "$";

			VATvalue = ElectricCharge * VATrate * 0.01;
			int VATvalueAsInt = (int)VATvalue;
			VATValueText.text = VATvalueAsInt.ToString() + "$";

			TotalCharge = ElectricCharge + VATvalueAsInt;
			TotalChargeText.text = TotalCharge.ToString() + "$";

			VATRateText.text = VATrate.ToString() + "%";

			ValueOfOneKwhText.text = ValueOfOneKwh.ToString() + "/kWh";

			FinalCollectedCoinText.text = coinValue.ToString();

			if ((coinValue - TotalCharge) < 0)
			{

				finalscore = 0;
			}

			else
			{
				finalscore = coinValue - TotalCharge;
			}

			CoinsAfterPayText.text = finalscore.ToString();
			_ = checkScore(uname, finalscore);

		}

		internal void UpdateCoinCount(int _add)
		{
			AppendtCollectedCoinsCount(_add);

			//UpdateScore(_add * SCORE_FOR_A_COIN);

			collectedCoinsTxt.text = GetCollectedCoinsCount().ToString();

			// Calculate the coin value (e.g., coin count multiplied by some factor)
			coinValue += _add * SCORE_FOR_A_COIN;

			// Update the display of coin value
			coinValueTxt.text = coinValue.ToString();


			if (!isMagnetPowerOn && !isShieldPowerOn && !isSpeedUpPowerOn && (bottleFill.fillAmount != 1))
			{
				collectedCoinsCountforBottleUI += _add;
				bottleFill.fillAmount = (float)collectedCoinsCountforBottleUI / collectedCoinsLimtforBottleFill;
			}

			if (bottleFill.fillAmount == 1)
			{

				collectedCoinsCountforBottleUI = 0;
				powerUpButton.interactable = true;
				isBottleFilled = true;

				bottleBack.sprite = bottleImgs[1];

			}


		}

		internal void UpdateScore(int _add)
		{
			AppendScore(_add);
			scoreTxt.text = GetScore().ToString();

		}


		internal int GetScore()
		{
			return scoreVal ^ LOCK_KEY;
		}

		private void SetScore(int val)
		{
			scoreVal = val ^ LOCK_KEY;
		}

		private void AppendScore(int val)
		{
			int _score = scoreVal ^ LOCK_KEY;
			_score += val;
			scoreVal = _score ^ LOCK_KEY;
		}


		private int GetCollectedCoinsCount()
		{
			return collectedCoinsCount ^ LOCK_KEY;
		}

		private void SettCollectedCoinsCount(int val)
		{
			collectedCoinsCount = val ^ LOCK_KEY;
		}

		private void AppendtCollectedCoinsCount(int val)
		{
			int _count = collectedCoinsCount ^ LOCK_KEY;
			_count += val;
			collectedCoinsCount = _count ^ LOCK_KEY;
		}

		#endregion

		#region POWER UPS
		private void OnMagnetPower()
		{
			if (isMagnetPowerOn) return;

			isMagnetPowerOn = true;
			powerUpButton.interactable = false;
			spendedPowerUpTime = 0;

			SingletonManager.Instance.SoundManager.PlaySoundOneShot(SoundTypes.PowerUp);
			// SingletonManager.Instance.HapticManager.TapVibrate();

			nextPowerUpType = PowerUpTypes.SpeedUp;
		}

		private void OnSpeedUpPower()
		{
			if (isSpeedUpPowerOn) return;

			isSpeedUpPowerOn = true;
			SingletonManager.Instance.Player.SetSpeedUpPowerActive(true);
			powerUpButton.interactable = false;
			spendedPowerUpTime = 0;

			speedupFXObj.gameObject.SetActive(true);
			SingletonManager.Instance.Player.SetPlayerGlow(true);

			SingletonManager.Instance.SoundManager.PlaySoundOneShot(SoundTypes.PowerUp);
			// SingletonManager.Instance.HapticManager.TapVibrate();
			nextPowerUpType = PowerUpTypes.Shield;
		}
		private void OnShieldPower()
		{
			if (isShieldPowerOn) return;

			isShieldPowerOn = true;
			powerUpButton.interactable = false;
			spendedPowerUpTime = 0;

			SingletonManager.Instance.Player.SetPlayerGlow(true);

			SingletonManager.Instance.SoundManager.PlaySoundOneShot(SoundTypes.PowerUp);
			// SingletonManager.Instance.HapticManager.TapVibrate(); 
			nextPowerUpType = PowerUpTypes.Magnet;
		}



		private void UpdatePowerEnabledTimer()
		{

			if (isMagnetPowerOn || isSpeedUpPowerOn || isShieldPowerOn)
			{

				spendedPowerUpTime += Time.deltaTime;

				bottleFill.fillAmount = 1 - spendedPowerUpTime / powerUpEnabledTimeLimit;

				if (spendedPowerUpTime >= powerUpEnabledTimeLimit)
				{
					bottleFill.fillAmount = 0;
					DisablePower();
				}
			}
		}

		float bottlePopAnimticks = 0;
		float bottlePopAnimTickMax = 1f;
		float bottlePopAnimSmoothStep = 0;
		private void UpdateBottleFilledPopAnim()
		{
			if (!isBottleFilled) return;

			bottlePopAnimticks += Time.deltaTime;
			bottlePopAnimticks = bottlePopAnimticks > bottlePopAnimTickMax ? bottlePopAnimTickMax : bottlePopAnimticks;
			bottlePopAnimSmoothStep = bottlePopAnimticks / bottlePopAnimTickMax;

			bottlePopAnimSmoothStep = SinCurve(bottlePopAnimSmoothStep);

			bottleFill.transform.localScale = new Vector3(1f, 1f, 1f) + new Vector3(0.1f, 0.1f, 0.1f) * bottlePopAnimSmoothStep;

			if (bottlePopAnimticks == bottlePopAnimTickMax)
			{
				bottlePopAnimticks = 0;
			}

		}
		float SinCurve(float x)
		{
			return 4 * x - 4 * x * x;
		}


		private void DisablePower()
		{

			isMagnetPowerOn = false;
			isSpeedUpPowerOn = false;
			isShieldPowerOn = false;

			SingletonManager.Instance.Player.SetSpeedUpPowerActive(false);
			speedupFXObj.gameObject.SetActive(false);
			SingletonManager.Instance.Player.SetPlayerGlow(false);

			if (nextPowerUpType == PowerUpTypes.Magnet)
			{
				currentPowerupImg.sprite = powerupImgs[0];
			}
			else if (nextPowerUpType == PowerUpTypes.SpeedUp)
			{
				currentPowerupImg.sprite = powerupImgs[1];
			}
			else if (nextPowerUpType == PowerUpTypes.Shield)
			{
				currentPowerupImg.sprite = powerupImgs[2];
			}

			currentPowerupImg.transform.GetComponent<Button>().interactable = true;
			isBottleFilled = false;
			bottleFill.transform.localScale = new Vector3(1f, 1f, 1f);
			bottleBack.sprite = bottleImgs[0];
			bottleFill.fillAmount = 0;
		}



		#endregion

		#region QUIZ

		void InitQuiz()
		{
			StateManager.Instance.SetTransition(ContinuousGameState.Revival);

			int quizIndex = Random.Range(0, QuizData.mQuizData.Length);
			QuizTxt.text = QuizData.mQuizData[quizIndex].QuizTxt;
			QuizImg.sprite = QuizData.mQuizData[quizIndex].QuizImg;
			hintCollectionID = QuizData.mQuizData[quizIndex].hintCollectionID;

			int[] indexArray = { 0, 1, 2 };

			for (int i = 0; i < indexArray.Length; i++)
			{
				int randIndex = Random.Range(0, indexArray.Length);

				int temp = indexArray[i];
				indexArray[i] = indexArray[randIndex];
				indexArray[randIndex] = temp;
			}

			for (int i = 0; i < indexArray.Length; i++)
			{
				QuizAnswersArray[i].text = QuizData.mQuizData[quizIndex].QuizAnswersArray[indexArray[i]];

				if (indexArray[i] == 0)
				{
					correctAnswersIndex = i;
				}
			}
		}

		public void QuizAnswerButtonPress(int i)
		{
			if (i == correctAnswersIndex)
			{
				StateManager.Instance.SetTransition(ContinuousGameState.Start);
				ContinueGamePlay();
				QuizTxt.transform.parent.transform.parent.gameObject.SetActive(false);
				SingletonManager.Instance.GameManager.SetLives(1);
				NoLivesAvailable();
			}
			else
			{
				SingletonManager.Instance.GameManager.LoseLives(1);
				FinalizeEndGame();
				NoLivesAvailable();
			}
		}

		public void FinalizeEndGame()
		{
			StateManager.Instance.SetTransition(ContinuousGameState.End);
			SingletonManager.Instance.UIManager.OnPressGameOverLBButton();
			ContinuePanelParent.SetActive(false);
		}


		public void OnPressShowQuizHintButton()
		{
			Collection _collecton = collectionDataDic[hintCollectionID];

			InfoPanelTitle.text = _collecton.CollectionName;
			InfoPanelImage.sprite = _collecton.CollectionThumbnailImg;
			InfoPanelDescription.text = _collecton.CollectionDescription;

			CollectionInfoPanel.gameObject.SetActive(true);
		}

		#endregion

		#region PLAYERHEALTH

		public void SetLives(int lives)
		{
			currentLives = lives;
			if (SingletonManager.Instance.UIManager != null)
			{
				SingletonManager.Instance.UIManager.InitializeLives();
			}
		}

		public void LoseLives(int lives)
		{
			currentLives -= lives;
			Debug.Log("currentLives: " + currentLives);
			if (SingletonManager.Instance.UIManager != null)
			{
				SingletonManager.Instance.UIManager.InitializeLives();
			}
		}

		private void NoLivesAvailable()
		{
			if (currentLives > 0) return;
			Debug.Log("Game Manager will be called NoLivesAvailable to end the game");
			ContinuePanelParent.gameObject.SetActive(false);
			SingletonManager.Instance.UIManager.EndGame(true);
			SetTimeScale(0);
		}

		#endregion

	}



}