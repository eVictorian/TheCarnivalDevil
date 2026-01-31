using UnityEngine;
using System.Collections;
using NaughtyAttributes;

public class ENVIRONMENT_Blackouts : MonoBehaviour
{
    [SerializeField] private soDATA_GameEvent blackoutStartGameEvent;
    [SerializeField] private soDATA_GameEvent blackoutEndGameEvent;

    [Space(10)]

    [SerializeField, Expandable] private BALANCEDATA_Blackout data;

    private float blackoutDuration;
    private float whiteoutDuration;

    private float timer;

    private static bool paused = false;

    [Space(10)]

    [ReadOnly] public float timeUntilNextBlackout;

    void Awake(){ Setup(); StartBlackoutLoop(); }

    [Button]
    void Setup()
    {
        if (data == null){ return; }

        blackoutDuration = data.RULE_BlackoutDuration;
        whiteoutDuration = data.RULE_WhiteoutDuration;

        timeUntilNextBlackout = whiteoutDuration;
    }

    void Update(){ timeUntilNextBlackout = whiteoutDuration - timer; if (!paused){ timer += Time.deltaTime; }}

    public static void PauseTimer(){ paused = true; }
    public static void UnPauseTimer(){ paused = false; }

    void BlackoutStarted(){ blackoutStartGameEvent.Raise(); }
    void BlackoutEnded(){ blackoutEndGameEvent.Raise(); }

    void StartBlackoutLoop(){ StartCoroutine("BlackoutLoop"); }

    private IEnumerator BlackoutLoop()
    {
        yield return new WaitForSeconds(whiteoutDuration);

        BlackoutStarted();
        yield return new WaitForSeconds(blackoutDuration);
        BlackoutEnded();

        StartBlackoutLoop();

        ResetTimer();
    }

    void ResetTimer(){ timer = 0; }
}