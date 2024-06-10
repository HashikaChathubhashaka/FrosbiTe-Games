using System;
using System.Collections.Generic;
using UnityEngine;

namespace ContinuousRunningGame
{
	public class Player : MonoBehaviour
	{
		#region VARIABLES

		[Header("PLAYER MOVEMENTS")] [SerializeField]
		private Transform player;

		[SerializeField] private Animator playerAnimator;
		[SerializeField] private Transform magnetCollectablePoint;
		[SerializeField] private Material playerMaterial;
		internal List<Vector3> positionArray;
		int currentPositionArrayIndex;

		//Player Forward Move
		private bool playerIsMovingForward;
		private int playerMoveIterationCount;
		private int playerPositionIndex;

		[SerializeField] private LayerMask CollectableMask;
		[SerializeField] private LayerMask ObstacleMask;

		private Vector3 playerNextPosition;
		private Vector3 playerPreviosPosition;
		private Vector3 playerDirectionVector;

		//Player Horizontal/Vertical Move
		private Vector3 playerCurrentPosition_V;
		private Vector3 playerCurrentPosition_H;
		private Vector3 playerDesiredHorizontalMoveDistance;
		private Vector3 playerDesiredVerticalMoveDistance;
		private Vector3 playerDefaultLocalPosition;
		private bool playerJumpAndGameOverBool;

		private float moveTicks_V;
		private float mSmoothStep_V;

		private float moveTicks_H;
		private float mSmoothStep_H;

		private float moveTicks_H_cam;
		private float mSmoothStep_H_cam;

		private float swapTransitionTime = 0.35f;
		private float swapTransitionTime_cam = 0.45f;
		private float jumpTransitionTime = 0.8f;

		private float horizontalLaneOffset = 1.75f;
		private float verticalJumpOffset = 1.5f;
		private float verticalDownOffset = -1f;

		bool playerIsMovingHorizontally = false;
		bool cameraIsMovingHorizontally = false;
		bool playerIsMovingVertically = false;


		PLAYER_HORIZONTALLY_MOVING_LANE playerHorizontallyMovingLane;

		//score
		const float SCORE_FOR_A_POINT = 0.05f;
		float pointScoreVal;


		[Header("CAMERA")] [SerializeField] GameObject CamObject;
		[SerializeField] private Transform camLookAtTransform;
		private Vector3 camLookAtTransformCurrentPosition;
		private Vector3 camLookAtTransformDefaultLocalPosition;

		[SerializeField] private Transform camPossitionTransform;
		private Vector3 camTransformCurrentPosition;
		private Vector3 camTransformDefaultLocalPosition;


		//ShieldGlow
		bool isPlayerGlowing;
		float glowAnimticks = 0;
		float glowAnimtickMax = 1f;
		float glowAnimSmoothStep = 0;

		#endregion
		
		#region UNITY METHODS

		private void Start()
		{
			positionArray = new List<Vector3>();

			playerDefaultLocalPosition = player.transform.localPosition;
			camLookAtTransformDefaultLocalPosition = camLookAtTransform.localPosition;
			camTransformDefaultLocalPosition = camPossitionTransform.localPosition;
			playerAnimator.speed = 0;
		}
		
		void FixedUpdate()
		{
			if(StateManager.Instance.currentState == ContinuousGameState.Run)
			{
				MovePlayerForward();
				MovePlayerHorizontal();
				MoveCameraHorizontal();
				MovePlayerVertical();
				MoveCameraForward();
			
				CheckCollectablesOverlap();
				CheckObstaclesOverlap();
			
				UpdatePlayerGlow();
			}

			playerIsMovingForward = StateManager.Instance.currentState switch
			{
				ContinuousGameState.Pause => false,
				ContinuousGameState.Run => true,
				_ => playerIsMovingForward
			};
		}

		#endregion

		#region PLAYER MOVEMENTS

		private void MovePlayerForward()
		{
			if (!playerIsMovingForward) return;

			for (int i = 0; i < playerMoveIterationCount; i++)
			{

				playerPreviosPosition = transform.position;

				//transform.position = Vector3.Lerp(playerStartPosition, playerNextPosition, 1);
				transform.position = playerNextPosition;

				playerDirectionVector = transform.position - playerPreviosPosition;
				float angle = Mathf.Atan2(playerDirectionVector.z, playerDirectionVector.x) * Mathf.Rad2Deg - 90;

				transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);


				SingletonManager.Instance.PathManager.FeedPlayerCurrentIndex(playerPositionIndex, currentPositionArrayIndex);

				pointScoreVal += SCORE_FOR_A_POINT;
				if (pointScoreVal >= 1)
				{
					pointScoreVal = 0;
					SingletonManager.Instance.GameManager.UpdateScore(1);
				}


				playerPositionIndex++;

				if (playerPositionIndex == positionArray.Count)
				{

					playerPositionIndex = 0;

					currentPositionArrayIndex =
						currentPositionArrayIndex + 1 == SingletonManager.Instance.PathManager.GetPathCount()
							? 0
							: currentPositionArrayIndex + 1;

					positionArray = SingletonManager.Instance.PathManager.GetPositionArray(currentPositionArrayIndex);

				}

				playerNextPosition = positionArray[playerPositionIndex];
			}
		}

		private void MoveCameraForward()
		{
			if (!playerIsMovingForward) return;

			CamObject.transform.position = camPossitionTransform.position;
			CamObject.transform.LookAt(camLookAtTransform);
		}

		public void SetPlayerMoveHorizontal(float moveVal)
		{
			if (playerIsMovingHorizontally || cameraIsMovingHorizontally) return;
			if (!playerIsMovingForward) return;


			Vector3 newMove;
			if (moveVal > 0f)
			{
				if (playerHorizontallyMovingLane == PLAYER_HORIZONTALLY_MOVING_LANE.Right) return;

				newMove = new Vector3(horizontalLaneOffset, 0, 0);

			}
			else if (moveVal < 0f)
			{
				if (playerHorizontallyMovingLane == PLAYER_HORIZONTALLY_MOVING_LANE.Left) return;

				newMove = new Vector3(-horizontalLaneOffset, 0, 0);

			}
			else
			{

				return;
			}

			playerDesiredHorizontalMoveDistance = newMove;

			playerCurrentPosition_H = player.localPosition;
			camTransformCurrentPosition = camPossitionTransform.localPosition;
			camLookAtTransformCurrentPosition = camLookAtTransform.localPosition;

			moveTicks_H = 0;
			playerIsMovingHorizontally = true;

			cameraIsMovingHorizontally = true;
			moveTicks_H_cam = 0;
		}

		private void MovePlayerHorizontal()
		{

			if (!playerIsMovingHorizontally) return;
			if (!playerIsMovingForward) return;


			moveTicks_H += Time.deltaTime;
			mSmoothStep_H = moveTicks_H / swapTransitionTime;
			mSmoothStep_H = 3 * mSmoothStep_H * mSmoothStep_H - 2 * mSmoothStep_H * mSmoothStep_H * mSmoothStep_H;

			player.localPosition =
				new Vector3(playerCurrentPosition_H.x + playerDesiredHorizontalMoveDistance.x * mSmoothStep_H,
					player.localPosition.y, player.localPosition.z);



			moveTicks_H = moveTicks_H > swapTransitionTime ? swapTransitionTime : moveTicks_H;

			if (moveTicks_H == swapTransitionTime)
			{

				player.localPosition = new Vector3(playerCurrentPosition_H.x + playerDesiredHorizontalMoveDistance.x,
					player.localPosition.y, player.localPosition.z);

				if (player.localPosition.x == horizontalLaneOffset)
				{
					playerHorizontallyMovingLane = PLAYER_HORIZONTALLY_MOVING_LANE.Right;
				}
				else if (player.localPosition.x == -horizontalLaneOffset)
				{
					playerHorizontallyMovingLane = PLAYER_HORIZONTALLY_MOVING_LANE.Left;
				}
				else
				{
					playerHorizontallyMovingLane = PLAYER_HORIZONTALLY_MOVING_LANE.Mid;
				}


				playerIsMovingHorizontally = false;
				moveTicks_H = 0;
			}
		}

		private void MoveCameraHorizontal()
		{

			if (!cameraIsMovingHorizontally) return;
			if (!playerIsMovingForward) return;

			moveTicks_H_cam += Time.deltaTime;
			mSmoothStep_H_cam = moveTicks_H_cam / swapTransitionTime_cam;
			mSmoothStep_H_cam = 3 * mSmoothStep_H_cam * mSmoothStep_H_cam -
			                    2 * mSmoothStep_H_cam * mSmoothStep_H_cam * mSmoothStep_H_cam;

			camPossitionTransform.localPosition = camTransformCurrentPosition +
			                                      playerDesiredHorizontalMoveDistance * 0.6f * mSmoothStep_H_cam;
			camLookAtTransform.localPosition = camLookAtTransformCurrentPosition +
			                                   playerDesiredHorizontalMoveDistance * 0.6f * mSmoothStep_H_cam;

			moveTicks_H_cam = moveTicks_H_cam > swapTransitionTime_cam ? swapTransitionTime_cam : moveTicks_H_cam;

			if (moveTicks_H_cam == swapTransitionTime_cam)
			{

				camPossitionTransform.localPosition =
					camTransformCurrentPosition + playerDesiredHorizontalMoveDistance * 0.6f;
				camLookAtTransform.localPosition =
					camLookAtTransformCurrentPosition + playerDesiredHorizontalMoveDistance * 0.6f; //0.65

				cameraIsMovingHorizontally = false;
				moveTicks_H_cam = 0;
			}
		}


		public void SetPlayerMoveVerticale(float moveVal)
		{
			if (playerIsMovingVertically) return;
			if (!playerIsMovingForward) return;

			if (moveVal > 0f)
			{
				playerDesiredVerticalMoveDistance = new Vector3(0, verticalJumpOffset, 0);
				playerAnimator.SetTrigger("isJumping");

			}
			else if (moveVal < 0f)
			{
				playerAnimator.SetTrigger("isRolling");
				playerDesiredVerticalMoveDistance = new Vector3(0, 0, 0); //verticalDownOffset


			}
			else
			{
				return;
			}

			playerCurrentPosition_V = player.localPosition;

			moveTicks_V = 0;
			playerIsMovingVertically = true;

			playerJumpAndGameOverBool = false;
		}

		private void MovePlayerVertical()
		{
			if (!playerIsMovingVertically) return;

			moveTicks_V += Time.deltaTime;
			mSmoothStep_V = moveTicks_V / jumpTransitionTime;
			mSmoothStep_V = 4 * mSmoothStep_V - 4 * mSmoothStep_V * mSmoothStep_V;

			player.localPosition = new Vector3(player.localPosition.x,
				playerCurrentPosition_V.y + playerDesiredVerticalMoveDistance.y * mSmoothStep_V,
				player.localPosition.z);

			if (!playerJumpAndGameOverBool && !playerIsMovingForward && moveTicks_V < 0.5)
			{
				moveTicks_V = (jumpTransitionTime - moveTicks_V);
				playerJumpAndGameOverBool = true;
			}

			moveTicks_V = moveTicks_V > jumpTransitionTime ? jumpTransitionTime : moveTicks_V;

			if (moveTicks_V == jumpTransitionTime)
			{

				player.localPosition = new Vector3(player.localPosition.x, playerCurrentPosition_V.y,
					player.localPosition.z);

				playerIsMovingVertically = false;
				moveTicks_V = 0;
			}

		}

		internal Vector3 GetPlayerMagnetCollectablePoint()
		{
			return magnetCollectablePoint.position;
		}

		#endregion

		#region PHYSICS OVERLAPS

		private void CheckCollectablesOverlap()
		{
			if (!playerIsMovingForward) return;

			Collider[] hitColliders = Physics.OverlapSphere(transform.GetChild(0).position, 0.5f, CollectableMask);
			if (hitColliders.Length > 0)
			{
				if (hitColliders[0].tag == Enum.GetName(typeof(COLLECTABLE_TAGS), COLLECTABLE_TAGS.Collectable_Coin) || hitColliders[0].CompareTag(DataManager.CollectableTag))
				{
					hitColliders[0].gameObject.transform.gameObject.SetActive(false);
					SingletonManager.Instance.GameManager.UpdateCoinCount(1);
					SingletonManager.Instance.SoundManager.PlaySoundOneShot(SoundTypes.CoinsCollect);
					// SingletonManager.Instance.HapticManager.TapPopVibrate();
				}
			}
		}

		private void CheckObstaclesOverlap()
		{
			if (!playerIsMovingForward) return;
			if (SingletonManager.Instance.GameManager.isSpeedUpPowerOn) return;

			Collider[] hitColliders = Physics.OverlapSphere(transform.GetChild(0).position, 0.5f, ObstacleMask);
			if (hitColliders.Length > 0)
			{

				if (hitColliders[0].tag == DataManager.ObstacleTag)
				{

					if (!SingletonManager.Instance.GameManager.isShieldPowerOn)
					{
						GameOver();
					}
					else
					{
						hitColliders[0].gameObject.SetActive(false);
					}
				}
			}
		}

		private void CheckObstaclesAndCollectableOverlap()
		{
			Collider[] collectableHitColliders = Physics.OverlapSphere(transform.GetChild(0).position, 0.5f, CollectableMask);
			Collider[] obstacleHitColliders = Physics.OverlapSphere(transform.GetChild(0).position, 0.5f, ObstacleMask);
			Debug.Log("collectableHitColliders = " + collectableHitColliders.Length);
			Debug.Log("obstacleHitColliders = " + obstacleHitColliders.Length);
		}


		#endregion

		#region PLAYER GLOW

		internal void SetPlayerGlow(bool state)
		{

			if (state)
			{
				isPlayerGlowing = true;
				playerMaterial.SetFloat("_isGlowing", 1);
			}
			else
			{
				if (!SingletonManager.Instance.GameManager.isMagnetPowerOn &&
				    !SingletonManager.Instance.GameManager.isShieldPowerOn)
				{
					isPlayerGlowing = false;
					playerMaterial.SetFloat("_isGlowing", 0);
				}

			}

		}


		void UpdatePlayerGlow()
		{
			if (!isPlayerGlowing) return;


			glowAnimticks += Time.deltaTime;
			glowAnimticks = glowAnimticks > glowAnimtickMax ? glowAnimtickMax : glowAnimticks;
			glowAnimSmoothStep = glowAnimticks / glowAnimtickMax;

			glowAnimSmoothStep = smoothSin(glowAnimSmoothStep);


			playerMaterial.SetFloat("_glowVal", glowAnimSmoothStep);

			if (glowAnimticks == glowAnimtickMax)
			{
				glowAnimticks = 0;
			}

		}

		float smoothSin(float x)
		{
			return 4 * x - 4 * x * x;
		}

		#endregion

		#region UTILLS

		internal void InitGamePlay()
		{

			currentPositionArrayIndex =
				UnityEngine.Random.Range(0, SingletonManager.Instance.PathManager.GetPathCount());

			positionArray = SingletonManager.Instance.PathManager.GetPositionArray(currentPositionArrayIndex);
			playerPositionIndex =
				SingletonManager.Instance.PathManager.GetStartPositionIndex(currentPositionArrayIndex);

			playerMoveIterationCount = 1;

			playerPreviosPosition = positionArray[playerPositionIndex - 1];
			
			playerNextPosition = positionArray[playerPositionIndex];

			playerHorizontallyMovingLane = PLAYER_HORIZONTALLY_MOVING_LANE.Mid;

			SingletonManager.Instance.PathManager.RefreshAllEnvironmentSets();

			SingletonManager.Instance.PathManager.ManageInitialCollctables(playerPositionIndex,
				currentPositionArrayIndex);
			SingletonManager.Instance.PathManager.ManageInitialEnvirenmentSets(playerPositionIndex,
				currentPositionArrayIndex);

			player.localPosition = playerDefaultLocalPosition;
			camLookAtTransform.localPosition = camLookAtTransformDefaultLocalPosition;
			camPossitionTransform.localPosition = camTransformDefaultLocalPosition;

			//playerAnimator.SetTrigger("setStartGame");	
			
			//playerAnimator.Play("Armature_runing");
			//playerAnimator.SetTrigger("isRunning");

			playerAnimator.speed = 0;
			playerIsMovingForward = false;

			pointScoreVal = 0;

			//Player Init Position
			transform.position = playerNextPosition;
			playerDirectionVector = transform.position - playerPreviosPosition;

			float angle = Mathf.Atan2(playerDirectionVector.z, playerDirectionVector.x) * Mathf.Rad2Deg - 90;

			transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);

			SingletonManager.Instance.PathManager.FeedPlayerCurrentIndex(playerPositionIndex,
				currentPositionArrayIndex);

			playerPositionIndex++;

			playerNextPosition = positionArray[playerPositionIndex];

			//Cam Init Position
			CamObject.transform.position = camPossitionTransform.position;
			CamObject.transform.LookAt(camLookAtTransform);

			SingletonManager.Instance.UIManager.ShowGameStartCountDown();
		}

		public void StartGame()
		{
			if (playerIsMovingForward) return;

			playerIsMovingForward = true;
			playerAnimator.speed = 1;
			
			StateManager.Instance.SetTransition(ContinuousGameState.Run);
			playerAnimator.Play("Armature_runing");
		}

		public void PauseGame()
		{
			if (StateManager.Instance.currentState == ContinuousGameState.Pause)
			{
				playerAnimator.speed = 0;
			}
			else
			{
				playerAnimator.speed = 1;
			}
		}

		public void PlayerIdle()
		{
			if(StateManager.Instance.currentState == ContinuousGameState.Start || StateManager.Instance.currentState == ContinuousGameState.Pause)
			{
				playerAnimator.speed = 1;
				playerAnimator.Play("Idle Animation xxx");
			}
		}

		private void GameOver()
		{
			playerIsMovingForward = false;

			playerIsMovingHorizontally = false;
			cameraIsMovingHorizontally = false;
			
			StateManager.Instance.SetTransition(ContinuousGameState.Dodge);
			playerAnimator.SetTrigger("isDodgingBack");
			
			Invoke("InvokeableGameOver", 2);

			// SingletonManager.Instance.HapticManager.TapNopeVibrate();
		}

		public void PlayerIdle(bool _isIdle)
		{
			playerAnimator.SetBool("isIdle", _isIdle);
		}

		private void InvokeableGameOver()
		{
			SingletonManager.Instance.GameManager.GameOver();
		}

		internal void SetSpeedUpPowerActive(bool state)
		{
			if (state)
			{
				playerMoveIterationCount = 2;

			}
			else
			{
				playerMoveIterationCount = 1;

			}
		}
		#endregion
	}
}