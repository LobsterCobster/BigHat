using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Organism : ScriptableObject
{
    public string Name;
    public string ScientificName;

    [NonSerialized] public bool isCaptured = false;

    public void OnDisable()
    {
        isCaptured = false;
    }

}
