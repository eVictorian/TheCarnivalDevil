using System;
using UnityEngine;

//REPRESENTS AN ACTION/EVENT BEING UNDERTAKEN//
[CreateAssetMenu(fileName = "[GameEvent] New", menuName = "++GAMEEVENT")]
public class soDATA_GameEvent : ScriptableObject
{
    public GameObject raiseSource;

    private event Action listeners;

    public void Raise(GameObject source = null)
    {
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