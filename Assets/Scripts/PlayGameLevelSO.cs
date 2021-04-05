using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayGameLevelSO", menuName = "Create PlayGameLevelSO")]
public class PlayGameLevelSO : ScriptableObject
{
    public List<GameLevelImage> gameLevelImageList = new List<GameLevelImage>();

    [Serializable]
    public class GameLevelImage
    {
        public int no;
        public Sprite levelSprite;
    }

        
}
