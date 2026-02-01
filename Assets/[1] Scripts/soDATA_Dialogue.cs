using UnityEngine;

[CreateAssetMenu(fileName = "[Dialogue] New", menuName = "++DIALOGUE")]
public class soDATA_Dialogue : ScriptableObject
{
    public string speakerName;

    [TextArea] public string content;

    [Range(0,60)] public float length;

    
    public GameObject voiceline;
}