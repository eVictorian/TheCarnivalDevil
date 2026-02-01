using System.Collections.Generic;
using UnityEngine;

public class CREATURE_Murder : MonoBehaviour
{
    [SerializeField] private soDATA_GameEvent blackoutStartedGameEvent;

    static bool RULE_MURDER_OnlyOneKillPerCheck = true;

    void Awake(){ Setup(); }
    void Setup(){ blackoutStartedGameEvent.RegisterListener(OnBlackoutCheckForMurder); }

    void OnBlackoutCheckForMurder()
    {
        ENVIRONMENT_Room myRoom = GetComponentInChildren<ENTITY_Creature>().location;

        if (myRoom == null){ Debug.Log("Creature not in a Room!"); return; }

        List<ENTITY> roomOccupants = new List<ENTITY>(myRoom.GetOccupants());

        if (!QueryMurder(roomOccupants)){ return; }
        
        foreach (ENTITY entity in roomOccupants)
        {
            if (entity is ENTITY_Creature){}
            if (entity is ENTITY_Guest){ KillGuest(entity as ENTITY_Guest); if (RULE_MURDER_OnlyOneKillPerCheck){ break; }}
            if (entity is ENTITY_Player){ KillPlayer(entity as ENTITY_Player); if (RULE_MURDER_OnlyOneKillPerCheck){ break; }
            }
        }
    }

    bool QueryMurder(List<ENTITY> occupants)
    {
        foreach (ENTITY entity in occupants)
        {
            if (entity is ENTITY_Creature){}
            if (entity is ENTITY_Guest){}
            if (entity is ENTITY_Player){ return false; }
        }

        return true;
    }

    void KillPlayer(ENTITY_Player player){ player.Die(); }

    void KillGuest(ENTITY_Guest guest){ guest.Die(); }
}