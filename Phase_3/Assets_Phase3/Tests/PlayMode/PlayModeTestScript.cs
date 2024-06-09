using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using ContinuousRunningGame;
public class PlayModeTestScript
{
  /*  private Player player;

    [OneTimeSetUp]
    public void Setup() {
        SceneManager.LoadScene("Scenes/Game");     
        
    }


    [UnityTest]
    public IEnumerator TestPlayerMoveRight() {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();

        Vector3 curruntPosition = player.transform.GetChild(0).transform.position;

        player.SetPlayerMoveHorizontal(15);

        yield return new WaitForSeconds(0.2f); //transition time
        
        Assert.AreEqual(curruntPosition.x + 1, player.transform.GetChild(0).transform.position.x ) ;       
            
    }




    [UnityTest]
    public IEnumerator TestPlayerMoveLeft() {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();

        Vector3 curruntPosition = player.transform.GetChild(0).transform.position;

        player.SetPlayerMoveHorizontal(-15);

        yield return new WaitForSeconds(0.2f); //transition time

        Assert.AreEqual(curruntPosition.x - 1, player.transform.GetChild(0).transform.position.x );

    }

    [UnityTest]
    public IEnumerator TestPlayerJump() {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();

        Vector3 curruntPosition = player.transform.GetChild(0).transform.position;

        player.SetPlayerMoveVerticale(15);
               

        yield return new WaitForSeconds(0.5f); //transition time
      
        Assert.AreEqual(curruntPosition.y + 4, (int) player.transform.GetChild(0).transform.position.y );

        yield return new WaitForSeconds(1f); //transition time

        Assert.AreEqual(curruntPosition.y  , (int)player.transform.GetChild(0).transform.position.y);
    }

    [UnityTest]
    public IEnumerator TestPlayerDown() {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();

        Vector3 curruntPosition = player.transform.GetChild(0).transform.position;

        player.SetPlayerMoveVerticale(-15);

        yield return new WaitForSeconds(0.5f); //transition time       

        Assert.AreEqual(curruntPosition.y -1, (int)player.transform.GetChild(0).transform.position.y);

        yield return new WaitForSeconds(1f); //transition time
        
        Assert.AreEqual(curruntPosition.y , (int)player.transform.GetChild(0).transform.position.y);
    }*/
}
