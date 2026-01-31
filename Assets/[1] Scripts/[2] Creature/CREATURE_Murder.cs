using System.Collections.Generic;
using UnityEngine;

public class CREATURE_Murder : MonoBehaviour
{
    [SerializeField] private soDATA_GameEvent blackoutStartedGameEvent;

    void Awake(){ Setup(); }
    void Setup(){ blackoutStartedGameEvent.RegisterListener(OnBlackoutCheckForMurder); }

    void OnBlackoutCheckForMurder()
    {
        ENVIRONMENT_Room myRoom = GetComponentInChildren<ENTITY_Creature>().location;

        if (myRoom == null){ Debug.Log("Creature not in a Room!"); return; }

        List<ENTITY> roomOccupants = new List<ENTITY>(myRoom.GetOccupants());

        if (roomOccupants.Count == 2)
        {
            foreach (ENTITY entity in roomOccupants)
            {
                if (entity is ENTITY_Creature){}
                if (entity is ENTITY_Guest){ KillGuest(entity as ENTITY_Guest); }
                if (entity is ENTITY_Player){ KillPlayer(entity as ENTITY_Player); }
            }
        }
    }

    void KillPlayer(ENTITY_Player player){ player.Die(); }

    void KillGuest(ENTITY_Guest guest){ guest.Die(); }
}