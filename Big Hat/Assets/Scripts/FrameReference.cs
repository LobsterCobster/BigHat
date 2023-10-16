using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameReference : MonoBehaviour
{
    public Database database = null;
    public Organism organism = null;
    public string Photoname = null;
    public bool mis = false;
    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.Find("DatabaseInstance").GetComponent<CreateDatabase>().database;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetOrganismReference(string name)
    {
        Photoname = name;
        foreach (var item in database.organisms)
        {
            if (item.name == name)
            {
                organism = item;
                mis = false;
                break;
            }
        }
        if (organism == null)
        {
            mis = true;
        }
    }
}
