using UnityEditor;
using UnityEditor.Callbacks;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using UnityEngine;


public static class SignInWithApplePostprocessor
{
    [PostProcessBuild(1)]
    public static void OnPostProcessBuild(BuildTarget target, string path)
    {
#if UNITY_IOS

        if (target != BuildTarget.iOS)
            return;

        var projectPath = PBXProject.GetPBXProjectPath(path);

        // Adds entitlement depending on the Unity version used
 

            var project = new PBXProject();
            project.ReadFromString(System.IO.File.ReadAllText(projectPath));
            var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", null, project.GetUnityMainTargetGuid());

       
            //SignIn With Apple
            manager.AddSignInWithAppleWithCompatibility(project.GetUnityFrameworkTargetGuid());


            //dynamiclink
            manager.AddAssociatedDomains(new string[] { "applinks:fijijuice.page.link" });

            manager.WriteToFile();

#endif
    }
}