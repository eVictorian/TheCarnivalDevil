using UnityEngine;

public class NPCAudioManager : MonoBehaviour
{
    [SerializeField] private soDATA_GameEvent onDeathGameEvent;
    [SerializeField] AudioSource walkAudio;
    [SerializeField] AudioSource maskAudio;
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

    public void StartWalk()
    {
        walkAudio.Play();
    }
    public void EndWalk()
    {
        walkAudio.Stop();
    }
    public void Unmask()
    {
        maskAudio.Play();
    }

    void OnDestroy()
    {
        onDeathGameEvent.UnregisterListener(OnDeath);
    }
}
