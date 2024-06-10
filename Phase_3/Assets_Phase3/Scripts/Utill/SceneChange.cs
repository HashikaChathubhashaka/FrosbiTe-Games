using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public void ChangeScene(int index) {
        if (index == 0) {
            Screen.orientation = ScreenOrientation.Portrait;
        } else if (index == 1) {
            Screen.orientation = ScreenOrientation.Portrait;
        } else if (index == 2) {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        } else if (index == 3) {
            Screen.orientation = ScreenOrientation.Portrait;
        }  

          
        SceneManager.LoadSceneAsync(index);
    }
}
