using System;
using UnityEngine;

public static partial class DataManager {
	//COLLECTABLES and OBSTACLES
	public static readonly string ObstacleTag = "Obstacle";
	public static readonly string CollectableTag = "Collectable_Coin";

	public static readonly int Lane_Left_Dictionary_Offset_Val = 0;
	public static readonly int Lane_Mid_Dictionary_Offset_Val = 50000;
	public static readonly int Lane_Right_Dictionary_Offset_Val = 100000;

	public static readonly int Distance_Between_Envirenment_Sets = 250;
}

public enum PLAYER_HORIZONTALLY_MOVING_LANE { Mid, Left, Right }
public enum COLLECTABLE_TYPES { Coin, Fruits }
public enum COLLECTABLE_TAGS { Collectable_Coin }

public enum OBSTACLE_TYPES { TutkTuk, Politician, Car }


[Serializable]
public class CollectableData {

	[SerializeField] public COLLECTABLE_TYPES CollectableType;
	[SerializeField] public int InitialPosArrayIndex;
	[SerializeField] public PLAYER_HORIZONTALLY_MOVING_LANE Lane;
	[SerializeField] public int InstanceCount;
	[SerializeField] public int InstanceGap;
	[SerializeField] public float InitialRotation;
}

[Serializable]
public class ObstacleData {

	[SerializeField] public OBSTACLE_TYPES ObstacleType;
	[SerializeField] public int InitialPosArrayIndex;
	[SerializeField] public PLAYER_HORIZONTALLY_MOVING_LANE Lane;
	[SerializeField] public int InstanceCount;
	[SerializeField] public int InstanceGap;

}


//game start
[Serializable]
public class GameStartResponseData {
	public GameStartData data;
}

[Serializable]
public class GameStartData {
	public string token;
	public int initial_value;
}


//promo score submit
[Serializable]
public class PromoScoreSubmitResponseData {
	public PromoScoreSubmitData data;
}

[Serializable]
public class PromoScoreSubmitData {

	public int my_position;
	public string my_name;
	public string my_image;
	public int highest_score;
	public int winner_list_size;
	public int next_beatable_score;
	public int winner_list_margin_score;
	public PromoScoreSubmitWinnerData[] winner_list;

}

[Serializable]
public class PromoScoreSubmitWinnerData {
	public int user_id;
	public string user_name;
	public string user_image;
	public bool subscriber;
	public int score;
	public string userUUID;
	public int rank;
	public bool this_user;
}
