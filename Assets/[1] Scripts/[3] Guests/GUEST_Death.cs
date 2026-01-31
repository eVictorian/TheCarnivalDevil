using UnityEngine;

public class GUEST_Death : MonoBehaviour
{
    public void TriggerDeath(){ Die(); }

    void Die(){ Destroy(gameObject); }
}