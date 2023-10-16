using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PhotoSO : ScriptableObject
{
    [SerializeField]
    public SpriteList spriteList;

    public SpriteList SpriteLists
    {
        get { return spriteList; }
        set { spriteList = value; }
    }
    [System.Serializable]
    public class SpriteList
    {
        public List<SpriteReference> Level1;
        public List<SpriteReference> Level2;
        public List<SpriteReference> Level3;
        public List<SpriteReference> Miscellaneous;

    }
    [System.Serializable]
    public class SpriteReference
    {
        public Sprite image;
        public string name;
    }
}
