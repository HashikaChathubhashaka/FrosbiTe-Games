using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_Controller : MonoBehaviour
{
    private bool done_mcq = true;

    private bool is_clicked = false; 
    private int chosen_character; 
    public void Finish()
    {   
        if (done_mcq){
            SceneManager.LoadSceneAsync(3);
        }
        else{
            Application.OpenURL("http://localhost:3000");
            SceneManager.LoadSceneAsync(2);
        }
    }
    public void Change()
    {   
        if (is_clicked){
            chosen_character = 0;
            is_clicked = false;
        }
    }


    public void Char1(){
        if (!is_clicked){
            chosen_character = 1;
            is_clicked = true;
        }
    }
    public void Char2(){
        if (!is_clicked){
            chosen_character = 2;
            is_clicked = true;
        }
    }
    public void Char3(){
        if (!is_clicked){
            chosen_character = 3;
            is_clicked = true;
        }
    }
    public void Char4(){
        if (!is_clicked){
            chosen_character = 4;
            is_clicked = true;
        }
    }
    public void Char5(){
        if (!is_clicked){
            chosen_character =5;
            is_clicked = true;
        }
    }
    public void Char6(){
        if (!is_clicked){
            chosen_character = 6;
            is_clicked = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(chosen_character);
    }
}
