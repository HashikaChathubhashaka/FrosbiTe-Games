using System.Collections.Generic;
using UnityEngine;
namespace ContinuousRunningGame {
	public class PathManager : MonoBehaviour {
		#region VARIABLES
		[SerializeField] private Path[] Curves;

		  internal int envirenmentStyleCount =1;

		[SerializeField] private Transform CollectablesParent;
		[SerializeField] private Transform ObstaclesParent;

		[SerializeField] internal GameObject coinPrefab;
		[SerializeField] internal GameObject tutkTukPrefab;
		

		private int collectAndObsEnableOffset = 600; //600
		private int collectAndObsDisableOffset = 50; //50
		private int magnatableCoinActivateOffset = 40; //50

		int currentEnvirenmentStyleIndex;
		int previousEnvirenmentStyleIndex;
		int currentEnvirenmentStyleChangeStep;


		private List<Collectable> collectablesPool;
		private int amountToPoolCollectables =10;

		private List<GameObject> obstaclesPool;
		private int amountToPoolObstacles = 3;

		#endregion

		#region UNITY METHODS
		private void Awake() {
			InitCollectablePool();
			InitObstaclePool();
			RemoveCollapsingItems();
		}
		#endregion

		#region CURVE
		internal List<Vector3> GetPositionArray(int _index) {
			return Curves[_index].GetPositionArray();
		}
		internal int GetStartPositionIndex(int _index) {
			return Curves[_index].GetStartPositionIndex();
		}
		internal int GetPathCount() {
			return Curves.Length;
		}

		internal void FeedPlayerCurrentIndex(int _playerIndex, int _pathIndex) {
			ManageCollectablesAndObstacales(_playerIndex, _pathIndex);
			ManageEnabeleAndDisableEnvirenmentSets(_playerIndex, _pathIndex);
			ManageMagnatableCoin(_playerIndex, _pathIndex);
		}
		
		
		#endregion

		#region ENVIRONMENT    

		internal void RefreshAllEnvironmentSets() {
			for (int i = 0; i < Curves.Length; i++) {
				Curves[i].RefreshAllSubEnvirenmentSets();
			}

			InitEnvirenmentIndex();
		}
		private void InitEnvirenmentIndex() {
			currentEnvirenmentStyleIndex = Random.Range(0, envirenmentStyleCount);
			previousEnvirenmentStyleIndex = currentEnvirenmentStyleIndex;
			currentEnvirenmentStyleChangeStep = 0;
		}
		private void ChangeEnvirenmentIndex() {
			previousEnvirenmentStyleIndex = currentEnvirenmentStyleIndex;
			currentEnvirenmentStyleIndex = Random.Range(0, envirenmentStyleCount);
			currentEnvirenmentStyleChangeStep = 0;
		}

		internal void ManageInitialEnvirenmentSets(int _playerIndex, int _pathIndex) {
			//Current Set
			int currentSet = _playerIndex / DataManager.Distance_Between_Envirenment_Sets;
			int currentPathIndex = _pathIndex;

			//Next Set
			int nextSet;
			int nextPathIndex;

			if ((currentSet + 1) * DataManager.Distance_Between_Envirenment_Sets > Curves[currentPathIndex].GetNumberOfPointsIntheCurve()) {

				nextSet = 0;
				nextPathIndex = currentPathIndex + 1 != Curves.Length ? currentPathIndex + 1 : 0;

			} else {
				nextSet = currentSet + 1;
				nextPathIndex = currentPathIndex;
			}

			//Next Next Set
			int nextNextSet;
			int nextNextPathIndex;

			if ((nextSet + 1) * DataManager.Distance_Between_Envirenment_Sets > Curves[nextPathIndex].GetNumberOfPointsIntheCurve()) {

				nextNextSet = 0;
				nextNextPathIndex = nextPathIndex + 1 != Curves.Length ? nextPathIndex + 1 : 0;

			} else {
				nextNextSet = nextSet + 1;
				nextNextPathIndex = nextPathIndex;
			}

			//Next Next Next Set
			int nextNextNextSet;
			int nextNextNextPathIndex;

			if ((nextNextSet + 1) * DataManager.Distance_Between_Envirenment_Sets > Curves[nextNextPathIndex].GetNumberOfPointsIntheCurve()) {

				nextNextNextSet = 0;
				nextNextNextPathIndex = nextNextPathIndex + 1 != Curves.Length ? nextNextPathIndex + 1 : 0;

			} else {
				nextNextNextSet = nextNextSet + 1;
				nextNextNextPathIndex = nextNextPathIndex;
			}

			//Previus Set
			int previusSet;
			int previusPathIndex;


			if ((currentSet - 1) == -1) {

				previusPathIndex = currentPathIndex - 1 == -1 ? Curves.Length - 1 : currentPathIndex - 1;

				previusSet = (int)Curves[previusPathIndex].GetNumberOfPointsIntheCurve() / (int)DataManager.Distance_Between_Envirenment_Sets;

			} else {

				previusPathIndex = _pathIndex;

				previusSet = currentSet - 1;
			}

			//Debug.Log("/previusSet: " + previusSet + "/currentSet: " + currentSet + "/nextSet: " + nextSet + "/nextNextSet: " + nextNextSet + "/nextNextNextSet: " + nextNextNextSet);
			//Debug.Log("/previusPathIndex: " + previusPathIndex + "/currentPathIndex: " + currentPathIndex + "/nextPathIndex: " + nextPathIndex + "/nextNextPathIndex: " + nextNextPathIndex + "/nextNextNextPathIndex: " + nextNextNextPathIndex);


			Curves[nextNextNextPathIndex].FeedEnvirenmentSetEnableIndex(nextNextNextSet, currentEnvirenmentStyleIndex);
			Curves[nextNextPathIndex].FeedEnvirenmentSetEnableIndex(nextNextSet, currentEnvirenmentStyleIndex);
			Curves[nextPathIndex].FeedEnvirenmentSetEnableIndex(nextSet, currentEnvirenmentStyleIndex);
			Curves[currentPathIndex].FeedEnvirenmentSetEnableIndex(currentSet, currentEnvirenmentStyleIndex);
			Curves[previusPathIndex].FeedEnvirenmentSetEnableIndex(previusSet, currentEnvirenmentStyleIndex);

			

		}



		private void ManageEnabeleAndDisableEnvirenmentSets(int _playerIndex, int _pathIndex) {
			if (_playerIndex % DataManager.Distance_Between_Envirenment_Sets != 0) return;

			//Current Set
			int currentSet = _playerIndex / DataManager.Distance_Between_Envirenment_Sets;
			int currentPathIndex = _pathIndex;


			//Next Set
			int nextSet;
			int nextPathIndex;


			if ((currentSet + 1) * DataManager.Distance_Between_Envirenment_Sets > Curves[currentPathIndex].GetNumberOfPointsIntheCurve()) {

				nextSet = 0;
				nextPathIndex = currentPathIndex + 1 != Curves.Length ? currentPathIndex + 1 : 0;

			} else {
				nextSet = currentSet + 1;
				nextPathIndex = currentPathIndex;
			}

			//Next Next Set
			int nextNextSet;
			int nextNextPathIndex;


			if ((nextSet + 1) * DataManager.Distance_Between_Envirenment_Sets > Curves[nextPathIndex].GetNumberOfPointsIntheCurve()) {

				nextNextSet = 0;
				nextNextPathIndex = nextPathIndex + 1 != Curves.Length ? nextPathIndex + 1 : 0;

			} else {
				nextNextSet = nextSet + 1;
				nextNextPathIndex = nextPathIndex;
			}

			//Next Next Next Set
			int nextNextNextSet;
			int nextNextNextPathIndex;

			if ((nextNextSet + 1) * DataManager.Distance_Between_Envirenment_Sets > Curves[nextNextPathIndex].GetNumberOfPointsIntheCurve()) {

				nextNextNextSet = 0;
				nextNextNextPathIndex = nextNextPathIndex + 1 != Curves.Length ? nextNextPathIndex + 1 : 0;

			} else {
				nextNextNextSet = nextNextSet + 1;
				nextNextNextPathIndex = nextNextPathIndex;
			}


			//Previus Set
			int previusSet;
			int previusPathIndex;

			if ((currentSet - 1) == -1) {

				previusPathIndex = currentPathIndex - 1 == -1 ? Curves.Length - 1 : currentPathIndex - 1;

				previusSet = (int)Curves[previusPathIndex].GetNumberOfPointsIntheCurve() / (int)DataManager.Distance_Between_Envirenment_Sets;

			} else {

				previusPathIndex = currentPathIndex;

				previusSet = currentSet - 1;
			}

			//Previus Previus Set
			int previusPreviusSet;
			int previusPreviusPathIndex;

			if ((previusSet - 1) == -1) {

				previusPreviusPathIndex = previusPathIndex - 1 == -1 ? Curves.Length - 1 : previusPathIndex - 1;

				previusPreviusSet = (int)Curves[previusPreviusPathIndex].GetNumberOfPointsIntheCurve() / (int)DataManager.Distance_Between_Envirenment_Sets;

			} else {

				previusPreviusPathIndex = previusPathIndex;

				previusPreviusSet = previusSet - 1;
			}


			// Envirenment Style Change

			int previusSetEnvirenmentStyleIndex;

			if (currentEnvirenmentStyleChangeStep <= 4) {
				previusSetEnvirenmentStyleIndex = previousEnvirenmentStyleIndex;

			} else {
				previusSetEnvirenmentStyleIndex = currentEnvirenmentStyleIndex;
				previousEnvirenmentStyleIndex = currentEnvirenmentStyleIndex;
			}
			currentEnvirenmentStyleChangeStep++;


			Curves[nextNextNextPathIndex].FeedEnvirenmentSetEnableIndex(nextNextNextSet, currentEnvirenmentStyleIndex);
			Curves[previusPreviusPathIndex].FeedEnvirenmentSetDisableIndex(previusPreviusSet, previusSetEnvirenmentStyleIndex);


			//prototype
			if (currentEnvirenmentStyleChangeStep > 6) { //must me greater than 5 to change
				ChangeEnvirenmentIndex();
			}

		}

		#endregion

		#region COLLECTABLES and OBSTACLES
		internal void InitCollectablePool() {

			collectablesPool = new List<Collectable>();
			GameObject tmp;
			for (int i = 0; i < amountToPoolCollectables; i++) {

				tmp = Instantiate(coinPrefab, CollectablesParent);
				tmp.SetActive(false);
				Collectable Collectable = new Collectable(tmp);
				collectablesPool.Add(Collectable);
			}
		}
		internal void InitObstaclePool() {

			obstaclesPool = new List<GameObject>();
			GameObject tmp;
			for (int i = 0; i < amountToPoolObstacles; i++) {
				tmp = Instantiate(tutkTukPrefab, ObstaclesParent);
				tmp.SetActive(false);
				obstaclesPool.Add(tmp);
			}
		}

		internal void RemoveCollapsingItems()
		{
			
		}
		internal void ManageInitialCollctables(int _playerIndex, int _pathIndex) {

			for (int i = _playerIndex; i < _playerIndex + collectAndObsEnableOffset; i++) {

				if (i <= Curves[_pathIndex].GetNumberOfPointsIntheCurve()) {
					Curves[_pathIndex].FeedCollectAndObsEnableIndex(i);

				} else {
					int nextSet = _pathIndex + 1 != Curves.Length ? _pathIndex + 1 : 0;
					Curves[nextSet].FeedCollectAndObsEnableIndex((i) - Curves[_pathIndex].GetNumberOfPointsIntheCurve());
					
				}
			}
		}
		
		private void ManageCollectablesAndObstacales(int _playerIndex, int _pathIndex) {

			if (_playerIndex + collectAndObsEnableOffset <= Curves[_pathIndex].GetNumberOfPointsIntheCurve()) {
				Curves[_pathIndex].FeedCollectAndObsEnableIndex(_playerIndex + collectAndObsEnableOffset);

			} else {
				int nextSet = _pathIndex + 1 != Curves.Length ? _pathIndex + 1 : 0;
				Curves[nextSet].FeedCollectAndObsEnableIndex((_playerIndex + collectAndObsEnableOffset) - Curves[_pathIndex].GetNumberOfPointsIntheCurve());
			}

			if (_playerIndex - collectAndObsDisableOffset >= 0) {
				Curves[_pathIndex].FeedCollectAndObsDisableIndex(_playerIndex - collectAndObsDisableOffset);
			} else {
				int previusSet = _pathIndex - 1 == -1 ? Curves.Length - 1 : _pathIndex - 1;
				Curves[previusSet].FeedCollectAndObsDisableIndex((_playerIndex - collectAndObsDisableOffset));
			}

		}
		private void ManageMagnatableCoin(int _playerIndex, int _pathIndex) {

			if (_playerIndex + magnatableCoinActivateOffset <= Curves[_pathIndex].GetNumberOfPointsIntheCurve()) {
				Curves[_pathIndex].FeedMagnatableCollectIndex(_playerIndex + magnatableCoinActivateOffset);

			} else {
				int nextSet = _pathIndex + 1 != Curves.Length ? _pathIndex + 1 : 0;
				Curves[nextSet].FeedMagnatableCollectIndex((_playerIndex + magnatableCoinActivateOffset) - Curves[_pathIndex].GetNumberOfPointsIntheCurve());
			}

		}

		internal Collectable GetCollectable() {

			for (int i = 0; i < collectablesPool.Count; i++) {
				if (!collectablesPool[i].mGameObject.activeSelf) {					
					return collectablesPool[i];
				}
			}

			GameObject tmp = Instantiate(coinPrefab, CollectablesParent);
			tmp.SetActive(false);
			Collectable Collectable = new Collectable(tmp);
			collectablesPool.Add(Collectable);

			return Collectable;

		}
		internal GameObject GetObstacle() {

			for (int i = 0; i < obstaclesPool.Count; i++) {
				if (!obstaclesPool[i].activeInHierarchy) {
					return obstaclesPool[i];
				}
			}

			GameObject tmp = Instantiate(tutkTukPrefab, ObstaclesParent);
			tmp.SetActive(false);
			obstaclesPool.Add(tmp);

			return tmp;
		}
		
		

		#endregion




	}
	

}