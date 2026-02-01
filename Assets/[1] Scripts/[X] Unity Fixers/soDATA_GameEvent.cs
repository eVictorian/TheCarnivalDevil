using System;
using UnityEngine;

//REPRESENTS AN ACTION/EVENT BEING UNDERTAKEN//
[CreateAssetMenu(fileName = "[GameEvent] New", menuName = "++GAMEEVENT")]
public class soDATA_GameEvent : ScriptableObject
{
    private static bool debug = true;

    public GameObject raiseSource;

    private event Action listeners;

    public void Raise(GameObject source = null)
    {
        if (debug){ Debug.Log(name +" Event was Raised!"); }

        raiseSource = source;

        listeners?.Invoke();
    }

    public void RegisterListener(Action listener)
    {
        listeners += listener;
    }

    public void UnregisterListener(Action listener)
    {
        listeners -= listener;
    }
}