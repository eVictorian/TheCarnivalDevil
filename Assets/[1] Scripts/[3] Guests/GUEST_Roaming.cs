using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public class GUEST_Roaming : MonoBehaviour
{
    public bool debug = false;

    [SerializeField] private NavMeshAgent nmAgent;

    [SerializeField] private List<Waypoint> waypoints = new List<Waypoint>();
    [Button] void AddAllWaypointsToList(){ waypoints.Clear(); foreach (Waypoint waypoint in FindObjectsByType<Waypoint>(0)){ waypoints.Add(waypoint); } }

    [SerializeField, Expandable] private BALANCEDATA_GUEST_Roaming data;

    [SerializeField] soDATA_GameEvent blackoutStarted;
    [SerializeField] soDATA_GameEvent blackoutEnded;

    [Space(10)]

    [SerializeField, ReadOnly] private RoamBehaviour currentBehaviour;
    [SerializeField, ReadOnly] private Waypoint currentRoamingPoint;

    private Coroutine waitingAtRoamPointCoroutineInstance;

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

    public void Stop(){ StopMoving(); }
    void StopMoving()
    {
        if (waitingAtRoamPointCoroutineInstance != null){ StopCoroutine(waitingAtRoamPointCoroutineInstance); }

        nmAgent.SetDestination(transform.position);
        currentBehaviour = RoamBehaviour.Stopped;
    }

    public void Start(){ StartMoving(); }
    void StartMoving(){ nmAgent.SetDestination(currentRoamingPoint.transform.position); currentBehaviour = RoamBehaviour.Travelling; }

    void ReachedRoamPointCheck(){ if (ReachedDestination()){ waitingAtRoamPointCoroutineInstance = StartCoroutine(WaitingAtRoamPointCoroutine()); }}
        bool ReachedDestination()
        {
            if (!nmAgent.pathPending)
            {
                if (nmAgent.remainingDistance <= nmAgent.stoppingDistance)
                {
                    if (!nmAgent.hasPath || nmAgent.velocity.sqrMagnitude == 0f)
                        return true;
                }
            }
            return false;
        }

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

    private IEnumerator WaitingAtRoamPointCoroutine()
    {
        currentBehaviour = RoamBehaviour.Waiting;

        float randomWaitTime = Random.Range(data.RULE_WaitTimeRange.x,data.RULE_WaitTimeRange.y);
            if (debug){ Debug.Log(randomWaitTime); }
        yield return new WaitForSeconds(randomWaitTime);

        ChooseNextRoamPoint();
        StartMoving();

        waitingAtRoamPointCoroutineInstance = null;
    }
}

public enum RoamBehaviour
{
    None,
    Waiting,
    Travelling,
    Stopped
}