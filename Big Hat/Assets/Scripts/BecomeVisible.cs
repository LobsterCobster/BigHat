using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeVisible : MonoBehaviour
{
    public bool visible = false;
    private void OnBecameInvisible()
    {
        visible = false;
    }
    private void OnBecameVisible()
    {
        visible = true;
    }
    private void Start()
    {
        visible = false;
    }
    private void Update()
    {
        if (visible)
        {
            Debug.Log("Enabled");
        }
    }
}
