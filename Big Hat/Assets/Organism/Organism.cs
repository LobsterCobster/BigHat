using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Organism : ScriptableObject
{
    public string Name;
    public string ScientificName;
    public Level level;
    [SerializeField]
    public enum Level
    {
        Level1,
        Level2,
        Level3,
        None
    }

    [NonSerialized] public bool isCaptured = false;

    public void OnDisable()
    {
        isCaptured = false;
    }

}
