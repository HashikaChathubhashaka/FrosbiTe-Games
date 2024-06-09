#if UNITY_IOS

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class GoogleSignInPostprocessor : MonoBehaviour
{

    [PostProcessBuild(1000)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {

        ///*  var projPath = buildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
        //  var proj = new PBXProject();
        //  proj.ReadFromFile(projPath);


        //  var targetGuid = proj.GetUnityMainTargetGuid();
        //  var googleServeiceInfoGuid = proj.FindFileGuidByProjectPath("GoogleService-Info.plist");
        //  //turn off 'UnityFramework' Target Membership
        //  proj.RemoveFileFromBuild(proj.GetUnityFrameworkTargetGuid(), googleServeiceInfoGuid);
        //  //turn on 'Unity-iPhone' Target Membership
        //  proj.AddFileToBuild(targetGuid, googleServeiceInfoGuid);

        //  proj.WriteToFile(projPath);*/

        //// Go get pbxproj file
        //string projPath = buildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";

        //// PBXProject class represents a project build settings file,
        //// here is how to read that in.
        //PBXProject proj = new PBXProject();
        //proj.ReadFromFile(projPath);

        //// This is the Xcode target in the generated project
        //string target = proj.GetUnityMainTargetGuid();

        //// Copy plist from the project folder to the build folder
        //FileUtil.CopyFileOrDirectory("Assets/GoogleService-Info.plist", buildPath + "/GoogleService-Info.plist");
        //proj.AddFileToBuild(target, proj.AddFile("GoogleService-Info.plist", "GoogleService-Info.plist"));

        //// Write PBXProject object back to the file
        //proj.WriteToFile(projPath);


        // Get plist
        string plistPath = buildPath + "/Info.plist";
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        // Get root
        var rootDict = plist.root;

        rootDict.SetString("NSLocationWhenInUseUsageDescription", "We currently do not use location in this app. If you see this, something is wrong.");
        rootDict.SetString("NSHealthUpdateUsageDescription", "Get user step count");

        plist.WriteToFile(plistPath);
    }
}
#endif

