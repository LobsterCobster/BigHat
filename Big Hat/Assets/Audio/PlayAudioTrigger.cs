using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class PlayAudioTrigger : MonoBehaviour
{
    // [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private UnityEvent myAudioEvent; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("test");
            // myAudioSource.Play();   
            myAudioEvent.Invoke();
            
        }
    } 
}



