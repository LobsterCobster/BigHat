using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueTree : ScriptableObject
{
    [SerializeField]
    public DialogueSection[] dialoguesection;
    [SerializeField]
    [TextArea]
    public string[] questEnd;
    [System.Serializable]
    public struct DialogueSection
    {
        [TextArea]
        public string[] questdialogue;
        public string answer;
        [TextArea]
        public string[] success;
        [TextArea]
        public string[] fact;
        [TextArea]
        public string[] failure;
    }
}
