using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu (fileName = "Sentence", menuName ="Instruction")]
public class Instructions : ScriptableObject
{
    [TextArea]
    public string sentence;
    public float timeToStart;
    public AudioClip audio;
}
