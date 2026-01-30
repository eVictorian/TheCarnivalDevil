using UnityEngine;
using System.Collections;

public class ENVIRONMENT_Blackouts : MonoBehaviour
{
    [SerializeField] private soDATA_GameEvent blackoutStartGameEvent;
    [SerializeField] private soDATA_GameEvent blackoutEndGameEvent;

    [SerializeField] private BALANCEDATA_Blackout data;

    private float blackoutDuration;
    private float whiteoutDuration;

    private float timer;

    private static bool paused = false;

    void Awake(){ Setup(); }

    void Setup()
    {
        if (data == null){ return; }

        blackoutDuration = data.RULE_BlackoutDuration;
        whiteoutDuration = data.RULE_WhiteoutDuration;
    }

    void Update(){ if (!paused) timer += Time.deltaTime; }

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