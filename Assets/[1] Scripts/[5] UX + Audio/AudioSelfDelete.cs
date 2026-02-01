using UnityEngine;

public class AudioSelfDelete : MonoBehaviour
{
    void Awake()
    {
        Invoke("KillYourSelfNow", GetComponent<AudioSource>().clip.length);
    }
    
    void KillYourSelfNow()
    {
        Destroy(gameObject);
    }
}
