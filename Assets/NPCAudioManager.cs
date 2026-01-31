using UnityEngine;

public class NPCAudioManager : MonoBehaviour
{
    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        OnDeath();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void OnDeath()
    {
        audioManager.Scream(gameObject.transform.position);
    }
}
