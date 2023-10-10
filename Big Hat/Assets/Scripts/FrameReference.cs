using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameReference : MonoBehaviour
{
    public string organismReference = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetFrameName(string name)
    {
        organismReference = name;
    }
}
