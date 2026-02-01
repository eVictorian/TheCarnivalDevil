using UnityEngine;

public class ENTITY_Creature : ENTITY
{
    public void EnableCreatureScream(){ FindFirstObjectByType<CREATURE_Scream>().gameObject.SetActive(true); }
}