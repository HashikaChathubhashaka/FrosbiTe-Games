using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image char1;
    [SerializeField] private Image char2;
    [SerializeField] private Image char3;
    [SerializeField] private Image char4;
    [SerializeField] private Image char5;
    [SerializeField] private Image char6;

    private int character_id = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (character_id == 1){
            char1.gameObject.SetActive(true);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(false);
        }
        if (character_id == 2){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(true);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(false);
        }

        if (character_id == 3){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(true);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(false);
        }

        if (character_id == 4){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(true);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(false);
        }

        if (character_id == 5){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(true);
            char6.gameObject.SetActive(false);
        }

        if (character_id == 6){
            char1.gameObject.SetActive(false);
            char2.gameObject.SetActive(false);
            char3.gameObject.SetActive(false);
            char4.gameObject.SetActive(false);
            char5.gameObject.SetActive(false);
            char6.gameObject.SetActive(true);
        }
    }

    public void OnPressNewGameButton()
    {
        SceneManager.LoadSceneAsync(4);
    }

    
}
