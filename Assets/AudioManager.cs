using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject ScreamAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Scream(Vector3 AudioPosition)
    {
        Instantiate(ScreamAudio, AudioPosition, new Quaternion(0,0,0,0));
    }
    
}
