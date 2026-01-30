using UnityEngine;
using UnityEngine.Events;

public class EVENT_Converter : MonoBehaviour
{
    public UnityEvent effect;
    public soDATA_GameEvent trigger;

    void Awake()
    {
        if (trigger != null) trigger.RegisterListener(Activate);
    }

    void Activate(){ effect.Invoke(); }
}