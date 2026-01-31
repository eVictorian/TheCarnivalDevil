using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public class GUEST_Roaming : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nmAgent;

    [SerializeField] private List<Waypoint> waypoints = new List<Waypoint>();

    [SerializeField] private BALANCEDATA_GUEST_Roaming data;

    [SerializeField] soDATA_GameEvent blackoutStarted;
    [SerializeField] soDATA_GameEvent blackoutEnded;

    [Space(10)]

    [SerializeField, ReadOnly] private RoamBehaviour currentBehaviour;
    [SerializeField, ReadOnly] private Waypoint currentRoamingPoint;

    void Awake(){ Setup(); }
    void Setup()
    {
        if (nmAgent == null){ nmAgent.GetComponent<NavMeshAgent>(); }
        if (nmAgent == null){ Debug.Log("No Navmesh Agent found at: " + name); }

        blackoutStarted.RegisterListener(StopMoving);
        blackoutEnded.RegisterListener(StartMoving);

        ChooseNextRoamPoint();
        StartMoving();
    }

    void WaitAtRoamPoint(){}
    void TravelToRoamPoint(){}

    void ChooseNextRoamPoint(){ currentRoamingPoint = waypoints[Random.Range(0,waypoints.Count)]; }

    void StopMoving(){ currentBehaviour = RoamBehaviour.Stopped; }

    void StartMoving(){ nmAgent.SetDestination(currentRoamingPoint.transform.position); currentBehaviour = RoamBehaviour.Travelling; }

    void ReachedRoamPointCheck(){ if (true){ StartCoroutine(WaitingAtRoamPoint()); }}

    void Update()
    {
        switch (currentBehaviour)
        {
            case RoamBehaviour.Stopped:
                break;
            case RoamBehaviour.Waiting:
                WaitAtRoamPoint();
                break;
            case RoamBehaviour.Travelling:
                TravelToRoamPoint();
                ReachedRoamPointCheck();
                break;
        }
    }

    private IEnumerator WaitingAtRoamPoint()
    {
        currentBehaviour = RoamBehaviour.Waiting;

        yield return new WaitForSeconds(Random.Range(data.RULE_WaitTimeRange.x,data.RULE_WaitTimeRange.y));

        ChooseNextRoamPoint();
        StartMoving();
    }
}

public enum RoamBehaviour
{
    None,
    Waiting,
    Travelling,
    Stopped
}