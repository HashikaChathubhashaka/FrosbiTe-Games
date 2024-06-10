using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


namespace ContinuousRunningGame {

    [CreateAssetMenu(fileName = "QuizData", menuName = "Scriptable Objects/Quiz Data", order = 1)]
    public class QuizData : ScriptableObject {
        public Quiz[] mQuizData;
    }

    [Serializable]
    public class Quiz {
        public string  QuizTxt;
        public int hintCollectionID;
        public Sprite QuizImg;
        public string[] QuizAnswersArray;
    }

}