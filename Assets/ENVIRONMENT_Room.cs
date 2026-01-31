using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ENVIRONMENT_Room : MonoBehaviour
{
    public bool debug = false;

    public static List<ENVIRONMENT_Room> allRooms = new List<ENVIRONMENT_Room>();
    
    [SerializeField] private Collider bounds;

    private List<ENTITY> occupants = new List<ENTITY>();
    [SerializeField, ReadOnly, Label("Occupants")] private List<ENTITY> UNITYINSPECTOR_Occupants;

    void Awake(){ Setup(); }

    void Setup()
    {
        //Update List of all Rooms
        if (!allRooms.Contains(this)) allRooms.Add(this);
    }

    #if UNITY_EDITOR
    void Update(){ UNITYINSPECTOR_Occupants = new List<ENTITY>(occupants); }
    #endif

    //Runs for objects already colliding on Awake
    void OnCollisionEnter(Collision collision)
    {
        if (debug){ Debug.Log(collision.gameObject.name); }
        
        ENTITY collidedEntity = collision.gameObject.GetComponent<ENTITY>();
            if (debug){ Debug.Log("1"); }
        if (collidedEntity == null){ return; }
            if (debug){ Debug.Log("2"); }

        collidedEntity.UpdateLocation(this);
        AddOccupant(collidedEntity);
    }

    void OnCollisionExit(Collision collision)
    {
        if (debug){ Debug.Log(collision.gameObject.name); }

        ENTITY collidedEntity = collision.gameObject.GetComponentInParent<ENTITY>();
        if (collidedEntity == null){ return; }

        if (collidedEntity.location == this ) collidedEntity.UpdateLocation(null);
        occupants.Remove(collidedEntity);
    }

    void AddOccupant(ENTITY newOccupant)
    {
        if (newOccupant == null){ return; }

        if (!occupants.Contains(newOccupant)) occupants.Add(newOccupant);

        if (debug){ Debug.Log("New Occupant in Room: " + newOccupant.name); }
    }



    //Clean Up

    void OnApplicationQuit(){ allRooms.Clear(); }
}