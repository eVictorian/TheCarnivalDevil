using UnityEngine;

public class AudioSelfDelete : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("KillYourSelfNow", GetComponent<AudioSource>().clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void KillYourSelfNow()
    {
        Destroy(gameObject);
    }
}
