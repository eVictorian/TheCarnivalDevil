using UnityEngine;
using UnityEngine.Events;

public class GUEST_GreetPlayer : MonoBehaviour
{
    public bool debug;

    [Space(10)]

    [SerializeField] private Transform pivot;

    [Space(10)]

    [SerializeField] private UnityEvent onPlayerApproaches;
    [SerializeField] private UnityEvent onPlayerLeaves;

    private Vector3 playerPosition;
    
    void OnTriggerEnter(Collider colliding)
    {
        if (colliding.tag != "Player"){ return; }

        UpdatePlayerPosition(colliding.gameObject);

        onPlayerApproaches.Invoke();
        LookAtPlayer();
    }
    void OnTriggerStay(Collider colliding)
    {
        if (colliding.tag != "Player"){ return; }

        UpdatePlayerPosition(colliding.gameObject);

        LookAtPlayer();
    }
    void OnTriggerExit(Collider colliding)
    {
        if (colliding.tag != "Player"){ return; }

        onPlayerLeaves.Invoke();
    }

    void UpdatePlayerPosition(GameObject player){ playerPosition = player.transform.position; }

    void LookAtPlayer()
    {
        Vector3 dir = playerPosition - pivot.position;

        //dir.x = 0;
        //dir.y = 0;
        //dir.z = 0;

        Quaternion rot = Quaternion.LookRotation(dir);
        pivot.localRotation = rot;
    }
}