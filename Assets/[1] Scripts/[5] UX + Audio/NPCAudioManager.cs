using UnityEngine;

public class NPCAudioManager : MonoBehaviour
{
    [SerializeField] private soDATA_GameEvent onDeathGameEvent;
    
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        onDeathGameEvent.RegisterListener(OnDeath);
    }
    
    public void OnDeath()
    {
        audioManager.Scream(transform.position);
    }

    void OnDestroy()
    {
        onDeathGameEvent.UnregisterListener(OnDeath);
    }
}
