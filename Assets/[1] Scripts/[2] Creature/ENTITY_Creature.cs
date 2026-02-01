using UnityEngine;

public class ENTITY_Creature : ENTITY
{
    public CREATURE_Scream screamer;

    public void EnableCreatureScream(){ screamer.gameObject.SetActive(true); }
}