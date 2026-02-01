using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EVENT_Converter : MonoBehaviour
{
    public UnityEvent effect;
    public soDATA_GameEvent trigger;

    public float delay;

    void Awake()
    {
        if (trigger != null) trigger.RegisterListener(Activate);
    }

    void Activate(){ if (delay > 0){ StartCoroutine(TriggerDelay()); return; } effect.Invoke(); }

    private IEnumerator TriggerDelay()
    {
        yield return new WaitForSeconds(delay);

        effect.Invoke();
    }
}