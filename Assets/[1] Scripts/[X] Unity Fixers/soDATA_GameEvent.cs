using System;
using UnityEngine;

//REPRESENTS AN ACTION/EVENT BEING UNDERTAKEN//
[CreateAssetMenu(fileName = "[GameEvent] New", menuName = "++GAMEEVENT")]
public class soDATA_GameEvent : ScriptableObject
{
    private event Action listeners;

    public void Raise()
    {
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