using UnityEngine;

public class GUEST_Death : MonoBehaviour
{
    [SerializeField] private soDATA_GameEvent onGuestDeathGameEvent;

    public void TriggerDeath(){ onGuestDeathGameEvent.Raise(); Die(); }

    void Die(){ Destroy(gameObject); }
}