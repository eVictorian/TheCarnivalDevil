using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EVENT_Converter : MonoBehaviour
{
    public bool debug;

    public UnityEvent effect;
    public soDATA_GameEvent trigger;

    public bool enableDelay = false;
    public float delay;

    void Awake()
    {
        if (trigger != null) trigger.RegisterListener(Activate);
    }

    void Activate()
    {
        if (debug){ Debug.Log(name + " was Activated!"); } 

        if (enableDelay){ StartCoroutine(TriggerDelay()); }
        else { if (debug){ Debug.Log(name + " was Invoked!"); }  effect.Invoke(); }
    }

    private IEnumerator TriggerDelay()
    {
        yield return new WaitForSeconds(delay);

        if (debug){ Debug.Log(name + " was Invoked!"); } effect.Invoke();
    }
}