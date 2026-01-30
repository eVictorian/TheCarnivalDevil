using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ENVIRONMENT_Room : MonoBehaviour
{
    public bool debug;

    public static List<ENVIRONMENT_Room> allRooms = new List<ENVIRONMENT_Room>();
    
    [SerializeField] private BoxCollider bounds;

    private List<ENTITY> occupants = new List<ENTITY>();
    [SerializeField, ReadOnly, Label("Occupants")] private List<ENTITY> UNITYINSPECTOR_Occupants;

    void Awake(){ Setup(); }

    void Setup()
    {
        //Update List of all Rooms
        if (!allRooms.Contains(this)) allRooms.Add(this);

        //Check for Starting Occupants
        ForceUpdateOccupants();
    }

    void ForceUpdateOccupants()
    {
        Vector3 worldCenter = bounds.transform.TransformPoint(bounds.center);
        Vector3 halfExtents = bounds.size * 0.5f;

        Collider[] hits = Physics.OverlapBox(
            worldCenter,
            halfExtents,
            bounds.transform.rotation,
            ~0, // all layers
            QueryTriggerInteraction.Collide
        );

        foreach (var hit in hits)
        {
            if (hit != bounds) // ignore self
                Debug.Log("Starts inside: " + hit.name);

                if (hit.GetComponentInParent<ENTITY>() != null){ AddOccupant(hit.GetComponentInParent<ENTITY>()); }
                if (hit.GetComponent<ENTITY>() != null){ AddOccupant(hit.GetComponentInParent<ENTITY>()); }
        }
    }

    void Update(){ UNITYINSPECTOR_Occupants = new List<ENTITY>(occupants); }

    void OnCollisionEnter(Collision collision)
    {
        if (debug){ Debug.Log(collision.gameObject.name); }
        
        ENTITY collidedEntity = collision.gameObject.GetComponentInParent<ENTITY>();

        if (collidedEntity == null){ return; }

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
    }



    //Clean Up

    void OnApplicationQuit(){ allRooms.Clear(); }
}