using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace ContinuousRunningGame {
    public class Path : MonoBehaviour {
        #region VARIABLES
        [Header("SETTINGS")]
        [SerializeField] bool VisualizeInEditor = false;

        [Header("CURVE")]
        [SerializeField] public Transform[] controlPoints;
        [SerializeField] bool CalculatePonitCountInEditor = false;
        [SerializeField] private float pointsFactor = 0.2f;
        [SerializeField] private int numberOfPointsIntheCurve = 10;
        [SerializeField] private float distance;

        private List<Vector3> pathPointsPosArray;

        private Vector3 newPointPosition;
        private Vector3 previosPosition;

        [Header("ENVIRENMENT")]
        [SerializeField] private GameObject EnvirenmentStyleParent;
        [SerializeField] private GameObject PathMeshParent;

        [Tooltip("Do not use 0 as index")]
        [SerializeField] private List<int> startPossitionArray;


        [Header("COLLECTABLES and OBSTACLES")]

        [SerializeField] private List<CollectableData> Collectables;
        [SerializeField] private List<ObstacleData> Obstacles;
       

        private Dictionary<int, CollectableTransformData> collectablesTransformDataDic;
        private Dictionary<int, Collectable> enabledCollectablesDic;

        private Dictionary<int, ObstacleTransformData> obstaclesTransformDataDic;
        private Dictionary<int, GameObject> enabledObstaclesDic;

        int enableingCollectableDicKey, enableingObstacleDicKey;
        float enableingCollectableLaneOffset, enableingObstacleLaneOffset;

        private float horizontalLaneOffset = 1.75f;
        private float collectableVerticalLaneOffset = 0.5f;
        private float collectablesRotationSpeed = 2;

        private float magnatableCollectableReachingTime = 0.5f;


        #endregion


        #region UNITY METHODS
        private void Awake() {
            pathPointsPosArray = new List<Vector3>();      

            VisualizeInEditor = false;
            CalculatePonitCountInEditor = false;
            
            GenrateBasePoints();

        }

        private void Start() {
        }

        private void FixedUpdate() {
            RotateCollectables();
            MagnaticPower();
        }

        #endregion

        #region CURVE


        internal void GenrateBasePoints() {
            collectablesTransformDataDic = new Dictionary<int, CollectableTransformData>();
            enabledCollectablesDic = new Dictionary<int, Collectable>();

            obstaclesTransformDataDic = new Dictionary<int, ObstacleTransformData>();
            enabledObstaclesDic = new Dictionary<int, GameObject>();

            for (int i = 0; i < numberOfPointsIntheCurve; i++) {
                float t = (float)i / (float)numberOfPointsIntheCurve;
                //P = (1?t)^3 P1 + 3(1?t)^2 tP2 +3(1?t)t^2 P3 + t^3 P4
                newPointPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

                //GenrateDebugPath();

                InitCollectables(i, newPointPosition, previosPosition);
                InitObstacles(i, newPointPosition, previosPosition);

                pathPointsPosArray.Add(newPointPosition);

                previosPosition = newPointPosition;
            }

            RefreshAllSubEnvirenmentSets();
        }


        private void GenrateDebugPath() {
            GameObject debugPath = Instantiate(transform.GetChild(0).gameObject, newPointPosition, Quaternion.identity);
            debugPath.transform.parent = transform;
        }

        internal List<Vector3> GetPositionArray() {
            return pathPointsPosArray;
        }

        internal int GetStartPositionIndex() {
            return startPossitionArray[UnityEngine.Random.Range(0, startPossitionArray.Count)];
        }

        #region GIZMOS
        #if UNITY_EDITOR
        private void OnDrawGizmos() {
            if (VisualizeInEditor) DrowGizmosBasePoints();
            if (CalculatePonitCountInEditor) CalculatePointCount();
        }
        private void CalculatePointCount() {
            Vector3 oldPoition = controlPoints[0].position;
            distance = 0;

            for (float t = 0; t <= 1; t += (1 / (float)numberOfPointsIntheCurve)) {
                //P = (1?t)^3 P1 + 3(1?t)^2 tP2 +3(1?t)t^2 P3 + t^3 P4
                newPointPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

                distance += Vector3.Distance(oldPoition, newPointPosition);
                oldPoition = newPointPosition;
            }
            numberOfPointsIntheCurve = (int)(distance / pointsFactor);

        }

        private void DrowGizmosBasePoints() {
            for (int i = 0; i < numberOfPointsIntheCurve; i++) {
                float t = (float)i / (float)numberOfPointsIntheCurve;
                //P = (1?t)^3 P1 + 3(1?t)^2 tP2 +3(1?t)t^2 P3 + t^3 P4
                newPointPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;
                Gizmos.DrawSphere(newPointPosition, 0.1f);

                if (i % DataManager.Distance_Between_Envirenment_Sets == 0) {
                    Handles.Label(newPointPosition, i.ToString());
                }
            }

            Gizmos.DrawLine(new Vector3(controlPoints[0].position.x, controlPoints[0].position.y, controlPoints[0].position.z), new Vector3(controlPoints[1].position.x, controlPoints[1].position.y, controlPoints[1].position.z));
            Gizmos.DrawLine(new Vector3(controlPoints[2].position.x, controlPoints[2].position.y, controlPoints[2].position.z), new Vector3(controlPoints[3].position.x, controlPoints[3].position.y, controlPoints[3].position.z));
        }
        #endif

        #endregion
        #endregion

        #region PLAYER INTERACTIONS
        internal int GetNumberOfPointsIntheCurve() {
            return numberOfPointsIntheCurve;
        }

        internal void FeedCollectAndObsEnableIndex(int _index) {
            EnableCollectable(_index);
            EnableObstacles(_index);
        }

        internal void FeedMagnatableCollectIndex(int _index) {
            GetMagnatableCollectable(_index);
        }


        internal void FeedCollectAndObsDisableIndex(int _index) {
            DisableCollectable(_index);
            DisableObstacles(_index);
        }
        internal void FeedEnvirenmentSetEnableIndex(int _index, int _envirenmentStyleIndex) {
            EnableEnvirenmentSet(_index, _envirenmentStyleIndex);
        }
        internal void FeedEnvirenmentSetDisableIndex(int _index, int _envirenmentStyleIndex) {
            DisableEnvirenmentSet(_index, _envirenmentStyleIndex);
        }


        #endregion

        #region COLLECTABLES

        void InitCollectables(int _index, Vector3 _newPosition, Vector3 _previosPosition) {
            foreach (CollectableData collectable in Collectables) {
                if (collectable.InitialPosArrayIndex == _index && collectable.InstanceCount > 0) {
					AddCollectablestoDic(collectable.CollectableType, _newPosition, _previosPosition, collectable.Lane, collectable.InitialRotation, _index);
                    collectable.InitialPosArrayIndex += collectable.InstanceGap;
                    collectable.InitialRotation += 10;
                    collectable.InstanceCount--;
                }
            }

        }
        void AddCollectablestoDic(COLLECTABLE_TYPES _type, Vector3 _newPosition, Vector3 _previosPosition, PLAYER_HORIZONTALLY_MOVING_LANE _lane, float _rotation, int _index) {

            int dictionaryStartOffset;

            if (_lane == PLAYER_HORIZONTALLY_MOVING_LANE.Left) {
                dictionaryStartOffset = DataManager.Lane_Left_Dictionary_Offset_Val;
            } else if (_lane == PLAYER_HORIZONTALLY_MOVING_LANE.Right) {
                dictionaryStartOffset = DataManager.Lane_Right_Dictionary_Offset_Val;
            } else { //_lane == PLAYER_HORIZONTALLY_MOVING_LANE.Mid         
                dictionaryStartOffset = DataManager.Lane_Mid_Dictionary_Offset_Val;
            }

            Vector3 directionVector = _newPosition - _previosPosition;
            float angle = Mathf.Atan2(directionVector.z, directionVector.x) * Mathf.Rad2Deg - 90;

            if (_type == COLLECTABLE_TYPES.Coin) {
                collectablesTransformDataDic.Add(dictionaryStartOffset + _index,new CollectableTransformData (_newPosition, Quaternion.AngleAxis(angle, Vector3.down),_rotation));
            }

        }                
        void RotateCollectables() {
            if (enabledCollectablesDic.Count==0) return;
            foreach (var collectable in enabledCollectablesDic) {
                if (!collectable.Value.mGameObject.gameObject.activeSelf) continue;
                collectable.Value.mGameObject.transform.GetChild(0).GetChild(0).transform.rotation = Quaternion.AngleAxis(collectable.Value.mGameObject.transform.GetChild(0).GetChild(0).transform.rotation.eulerAngles.y + collectablesRotationSpeed, Vector3.up);
            }
        }

        void EnableCollectable(int _index) {
           
            if (collectablesTransformDataDic.ContainsKey(_index + DataManager.Lane_Left_Dictionary_Offset_Val)) {
                enableingCollectableDicKey = _index + DataManager.Lane_Left_Dictionary_Offset_Val;
                enableingCollectableLaneOffset = -horizontalLaneOffset;
             
            }else if (collectablesTransformDataDic.ContainsKey(_index + DataManager.Lane_Mid_Dictionary_Offset_Val)) {
                enableingCollectableDicKey = _index + DataManager.Lane_Mid_Dictionary_Offset_Val;
                enableingCollectableLaneOffset = 0;
    
            } else if (collectablesTransformDataDic.ContainsKey(_index + DataManager.Lane_Right_Dictionary_Offset_Val)) {
                enableingCollectableDicKey = _index + DataManager.Lane_Right_Dictionary_Offset_Val;
                enableingCollectableLaneOffset = horizontalLaneOffset;
              
            } else {
             
                return;
            }
      
            Collectable collectable = SingletonManager.Instance.PathManager.GetCollectable();

            //Enable Random Car
            for (int i = 0; i < 5; i++) {
                collectable.mGameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
            int randomCollectable = Random.Range(0, 5);
            collectable.mGameObject.transform.GetChild(0).GetChild(0).GetChild(randomCollectable).gameObject.SetActive(true);


            collectable.mGameObject.transform.position = collectablesTransformDataDic[ enableingCollectableDicKey].poition;
            collectable.mGameObject.transform.rotation = collectablesTransformDataDic[ enableingCollectableDicKey].rotation;
            collectable.mGameObject.transform.GetChild(0).GetChild(0).transform.rotation = Quaternion.AngleAxis(collectablesTransformDataDic[ enableingCollectableDicKey].rotationOffset, Vector3.up);
            collectable.mGameObject.transform.GetChild(0).localPosition = new Vector3(enableingCollectableLaneOffset, collectableVerticalLaneOffset, 0);
            collectable.mGameObject.transform.GetChild(0).gameObject.SetActive(true);
            collectable.mIsActractable = false;
            collectable.mGameObject.gameObject.SetActive(true);

            enabledCollectablesDic.Add(enableingCollectableDicKey, collectable);
        }
        void DisableCollectable(int _index) {
            if (enabledCollectablesDic.ContainsKey(_index + DataManager.Lane_Left_Dictionary_Offset_Val)) {
                enabledCollectablesDic[_index + DataManager.Lane_Left_Dictionary_Offset_Val].mGameObject.SetActive(false);
                enabledCollectablesDic[_index + DataManager.Lane_Left_Dictionary_Offset_Val].mIsActractable = false;
                enabledCollectablesDic.Remove(_index + DataManager.Lane_Left_Dictionary_Offset_Val);
            }
            if (enabledCollectablesDic.ContainsKey(_index + DataManager.Lane_Mid_Dictionary_Offset_Val)) {
                enabledCollectablesDic[_index + DataManager.Lane_Mid_Dictionary_Offset_Val].mGameObject.SetActive(false);
                enabledCollectablesDic[_index + DataManager.Lane_Mid_Dictionary_Offset_Val].mIsActractable = false;
                enabledCollectablesDic.Remove(_index + DataManager.Lane_Mid_Dictionary_Offset_Val);
            }
            if (enabledCollectablesDic.ContainsKey(_index + DataManager.Lane_Right_Dictionary_Offset_Val)) {
                enabledCollectablesDic[_index + DataManager.Lane_Right_Dictionary_Offset_Val].mGameObject.SetActive(false);
                enabledCollectablesDic[_index + DataManager.Lane_Right_Dictionary_Offset_Val].mIsActractable = false;
                enabledCollectablesDic.Remove(_index + DataManager.Lane_Right_Dictionary_Offset_Val);
            }
        }
        private void DisableAllEnabledCollectables() {
            foreach (var collectable in enabledCollectablesDic) {
                collectable.Value.mGameObject.SetActive(false);
                collectable.Value.mIsActractable = false;
            }
            enabledCollectablesDic.Clear();
        }


        #endregion

        #region MAGNATABLE COLLECTABLES

        void GetMagnatableCollectable(int _index) {
            if (!SingletonManager.Instance.GameManager.isMagnetPowerOn) return;

            if (enabledCollectablesDic.ContainsKey(_index + DataManager.Lane_Left_Dictionary_Offset_Val)) {
                EnableActractable(enabledCollectablesDic[_index + DataManager.Lane_Left_Dictionary_Offset_Val]);
            }
            if (enabledCollectablesDic.ContainsKey(_index + DataManager.Lane_Mid_Dictionary_Offset_Val)) {
                EnableActractable(enabledCollectablesDic[_index + DataManager.Lane_Mid_Dictionary_Offset_Val]);
            }
            if (enabledCollectablesDic.ContainsKey(_index + DataManager.Lane_Right_Dictionary_Offset_Val)) {
                EnableActractable(enabledCollectablesDic[_index + DataManager.Lane_Right_Dictionary_Offset_Val]);
            }
        }


    
        private void EnableActractable(Collectable collectable) {
        
            collectable.mReachingTime = magnatableCollectableReachingTime;
            collectable.mInitialPoition  = collectable.mGameObject.gameObject.transform.GetChild(0).position;
            collectable.mIsActractable  = true;             
        }

        void MagnaticPower() {
           // if (!SingletonManager.Instance.GameManager.isMagnetPowerOn) return;

            foreach (var collectable in enabledCollectablesDic) {
                if (!collectable.Value.mGameObject.activeSelf) continue;
                if (!collectable.Value.mIsActractable) continue;

                collectable.Value.mReachingTime -= Time.deltaTime;

                if (collectable.Value.mReachingTime > 0) {

                    collectable.Value.mGameObject.transform.GetChild(0).position = Vector3.Lerp(collectable.Value.mInitialPoition, SingletonManager.Instance.Player.GetPlayerMagnetCollectablePoint(), 1 - (collectable.Value.mReachingTime / magnatableCollectableReachingTime));

                } else {
                    collectable.Value.mGameObject.transform.GetChild(0).gameObject.SetActive(false);
                    collectable.Value.mIsActractable = false;
                }

            }
        }


        #endregion

        #region OBSTACLES

        void InitObstacles(int _index, Vector3 _newPosition, Vector3 _previosPosition) {
            foreach (ObstacleData Obstacle in Obstacles) {
                if (Obstacle.InitialPosArrayIndex == _index && Obstacle.InstanceCount > 0) {
                    AddObstaclestoDic(Obstacle.ObstacleType, _newPosition, _previosPosition, Obstacle.Lane, _index);
                    Obstacle.InitialPosArrayIndex += Obstacle.InstanceGap;
                    Obstacle.InstanceCount--;
                }
            }

        }
        void AddObstaclestoDic(OBSTACLE_TYPES _type, Vector3 _newPosition, Vector3 _previosPosition, PLAYER_HORIZONTALLY_MOVING_LANE _lane, int _index) {

            Vector3 horizontalOffset = new Vector3();

            int dictionaryStartOffset;
            if (_lane == PLAYER_HORIZONTALLY_MOVING_LANE.Left) {
                horizontalOffset = new Vector3(-horizontalLaneOffset, 0, 0);
                dictionaryStartOffset = DataManager.Lane_Left_Dictionary_Offset_Val;
            } else if (_lane == PLAYER_HORIZONTALLY_MOVING_LANE.Right) {
                horizontalOffset = new Vector3(horizontalLaneOffset, 0, 0);
                dictionaryStartOffset = DataManager.Lane_Right_Dictionary_Offset_Val;
            } else { //_lane == PLAYER_HORIZONTALLY_MOVING_LANE.Mid
                horizontalOffset = new Vector3(0, 0, 0);
                dictionaryStartOffset = DataManager.Lane_Mid_Dictionary_Offset_Val;
            }


            Vector3 directionVector = _newPosition - _previosPosition;
            float angle = Mathf.Atan2(directionVector.z, directionVector.x) * Mathf.Rad2Deg - 90;

            if (_type == OBSTACLE_TYPES.TutkTuk) {

                obstaclesTransformDataDic.Add(dictionaryStartOffset + _index, new ObstacleTransformData(_newPosition, Quaternion.AngleAxis(angle, Vector3.down)));

            }

        }
        void EnableObstacles(int _index) {


            if (obstaclesTransformDataDic.ContainsKey(_index + DataManager.Lane_Left_Dictionary_Offset_Val)) {
                enableingObstacleDicKey = _index + DataManager.Lane_Left_Dictionary_Offset_Val;
                enableingObstacleLaneOffset = -horizontalLaneOffset;

            } else if (obstaclesTransformDataDic.ContainsKey(_index + DataManager.Lane_Mid_Dictionary_Offset_Val)) {
                enableingObstacleDicKey = _index + DataManager.Lane_Mid_Dictionary_Offset_Val;
                enableingObstacleLaneOffset = 0;

            } else if (obstaclesTransformDataDic.ContainsKey(_index + DataManager.Lane_Right_Dictionary_Offset_Val)) {
                enableingObstacleDicKey = _index + DataManager.Lane_Right_Dictionary_Offset_Val;
                enableingObstacleLaneOffset = horizontalLaneOffset;

            } else {
                return;
            }

            //Get Car
            GameObject obstacle = SingletonManager.Instance.PathManager.GetObstacle(); 
            obstacle.transform.position = obstaclesTransformDataDic[enableingObstacleDicKey].poition;
            obstacle.transform.rotation = obstaclesTransformDataDic[enableingObstacleDicKey].rotation;
                     
            //Enable Random Car
            for (int i = 0; i < 3; i++) {
                obstacle.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
            int randomCar = Random.Range(0, 3);
            obstacle.transform.GetChild(0).GetChild(randomCar).gameObject.SetActive(true);

            //Enable Colider 
            obstacle.transform.GetChild(1).gameObject.SetActive(true);

            //Change Lane
            obstacle.transform.GetChild(0).transform.localPosition = new Vector3(enableingCollectableLaneOffset, 0, 0);
            obstacle.transform.GetChild(1).transform.localPosition = new Vector3(enableingCollectableLaneOffset, 0, 0);

            //Rotate Car
            int rot = Random.Range(0, 2);
            if (rot != 0) {
                obstacle.transform.GetChild(0).transform.rotation = Quaternion.AngleAxis(obstacle.transform.GetChild(0).transform.rotation.eulerAngles.y + 180, Vector3.up);
            }

            obstacle.gameObject.SetActive(true);

            enabledObstaclesDic.Add(enableingObstacleDicKey, obstacle);
        }          
        void DisableObstacles(int _index) {
            if (enabledObstaclesDic.ContainsKey(_index + DataManager.Lane_Left_Dictionary_Offset_Val)) {
                enabledObstaclesDic[_index + DataManager.Lane_Left_Dictionary_Offset_Val].gameObject.SetActive(false);
                enabledObstaclesDic.Remove(_index + DataManager.Lane_Left_Dictionary_Offset_Val);
            }
            if (enabledObstaclesDic.ContainsKey(_index + DataManager.Lane_Mid_Dictionary_Offset_Val)) {
                enabledObstaclesDic[_index + DataManager.Lane_Mid_Dictionary_Offset_Val].gameObject.SetActive(false);
                enabledObstaclesDic.Remove(_index + DataManager.Lane_Mid_Dictionary_Offset_Val);
            }
            if (enabledObstaclesDic.ContainsKey(_index + DataManager.Lane_Right_Dictionary_Offset_Val)) {
                enabledObstaclesDic[_index + DataManager.Lane_Right_Dictionary_Offset_Val].gameObject.SetActive(false);
                enabledObstaclesDic.Remove(_index + DataManager.Lane_Right_Dictionary_Offset_Val);
            }
        }
        private void DisableAllObstacles() {
            foreach (var Obstacle in enabledObstaclesDic) {
                Obstacle.Value.gameObject.SetActive(false);
            }
            enabledObstaclesDic.Clear(); 
        }

        #endregion

        #region ENVIRENMENT
        void EnableEnvirenmentSet(int _index, int _envirenmentStyleIndex) {
           
            EnvirenmentStyleParent.transform.GetChild(_envirenmentStyleIndex).transform.GetChild(_index).gameObject.SetActive(true);
            PathMeshParent.transform.transform.GetChild(_index).gameObject.SetActive(true);
        }
        void DisableEnvirenmentSet(int _index, int _envirenmentStyleIndex) {
            
            EnvirenmentStyleParent.transform.GetChild(_envirenmentStyleIndex).transform.GetChild(_index).gameObject.SetActive(false);
            PathMeshParent.transform.transform.GetChild(_index).gameObject.SetActive(false);
        }
        internal void RefreshAllSubEnvirenmentSets() {

            for (int i = 0; i < EnvirenmentStyleParent.transform.childCount; i++) {

                for (int k = 0; k < EnvirenmentStyleParent.transform.GetChild(i).transform.childCount; k++) {

                    EnvirenmentStyleParent.transform.GetChild(i).transform.GetChild(k).gameObject.SetActive(false);
                    PathMeshParent.transform.GetChild(k).gameObject.SetActive(false);
                }
            }

            DisableAllObstacles();
            DisableAllEnabledCollectables();
            
        }

        #endregion

        #region UTILL
        public class CollectableTransformData {
            public Vector3 poition { get; set; }
            public Quaternion rotation { get; set; }

            public float rotationOffset;

            public CollectableTransformData(Vector3 poition, Quaternion rotation, float rotationOffset) {
                this.poition = poition;
                this.rotation = rotation;
                this.rotationOffset = rotationOffset;
            }
        }
      
        public class ObstacleTransformData {
            public Vector3 poition { get; set; }
            public Quaternion rotation { get; set; }

            public ObstacleTransformData(Vector3 poition, Quaternion rotation) {
                this.poition = poition;
                this.rotation = rotation;
            }
        }
     
        #endregion
               
    }
    
}

public class Collectable {
    public GameObject mGameObject { get; set; }
    public float mReachingTime { get; set; }
    public Vector3 mInitialPoition { get; set; }

    public bool mIsActractable;
    public Collectable(GameObject gameObject) {
        this.mGameObject = gameObject;
        this.mIsActractable = false;
    }
}


