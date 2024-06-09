using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MainMenu {
    [CreateAssetMenu(fileName = "CollectionData", menuName = "Scriptable Objects/Collection Data", order = 1)]
    public class CollectionData : ScriptableObject {

        public Collection[] mCollectionData;        
      
    }


    [Serializable]
    public class Collection {
        public string CollectionName;
        public int CollectionID;
        public Sprite CollectionThumbnailImg;
        public Sprite CollectionImg;
        public string CollectionDescription;
    }
}