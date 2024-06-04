using System;
using UnityEngine;


public static partial class DataManager {

	public static readonly string IMI_GAME_ID = "0cf019ed-46d2-4c1f-ae27-235056a32228";
	public static readonly string IMI_LB_ID = "70c1d687-1f4e-4fa6-b537-a8738214d5e0";
	public static string PlayStoreLink = @"";
	public static string AppStoreLink = @"";
	public static int P_SCORE = 687814, Q_SCORE = 494255, Z_SCORE = 758942, LOCK_KEY = 323232;

	//Authentication
	internal static ForceUpdate forceUpdate;
	internal static SignUp signUp;
	internal static UserProfile userProfile;
	internal static UserSubscription userSubscription;
	public static string USER_TOKEN = "userToken";

	public static string SIGN_IN_METHOD = "signInMethod";
	public static string GOOGLE_SIGN_IN = "google";
	public static string FB_SIGN_IN = "fb";
	public static string APPLE_SIGN_IN = "apple";
	public static string GUEST_SIGN_IN = "guest";
	public static string APPLE_USER_ID_KEY = "appleUserIDKey";
	public static bool isGuestMode = false;

	//Settings
	public static string HAPTICS_KEY = "isHapticsOn";
	public static bool isHapticsOn;

	public static string SOUNDS_KEY = "isSoundsOn";
	public static bool isSoundsOn;

	public static string MUSIC_KEY = "isMusicOn";
	public static bool isMusicOn;

}


[Serializable]
public class SignUp {
	[SerializeField]
	public Access_Token data;
}

[Serializable]
public class Access_Token {
	[SerializeField]
	public string access_token;
}

[Serializable]
public class UserProfile {
	[SerializeField]
	public UserProfileData data;
}

[Serializable]
public class UserProfileData {
	[SerializeField]
	public string name;
	[SerializeField]
	public string image_url;
}

[Serializable]
public struct UserSubscription {
	[SerializeField]
	public bool data;
}
[Serializable]
public struct Response {
	[SerializeField]
	public string access_token;
}

[Serializable]
public class ForceUpdate {
	public ForceUpdateData data;
}

[Serializable]
public class ForceUpdateData {
	public string force_update_version;
	public bool force_update;
}


[Serializable]
public class HighscoreResponseData {
	public string data;
}








