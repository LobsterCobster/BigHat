using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Organism : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;

    [NonSerialized] public bool isCaptured = false;
    [NonSerialized] public Sprite sprite;

    public void OnDisable()
    {
        isCaptured = false;
        sprite = null;
    }
}
