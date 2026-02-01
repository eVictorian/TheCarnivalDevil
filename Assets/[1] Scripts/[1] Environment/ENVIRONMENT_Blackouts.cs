using UnityEngine;
using System.Collections;
using NaughtyAttributes;
using UnityEngine.UI;

public class ENVIRONMENT_Blackouts : MonoBehaviour
{
    private static ENVIRONMENT_Blackouts instance;

    [SerializeField] private Image blackoutCover;

    [SerializeField] private soDATA_GameEvent blackoutStartGameEvent;
    [SerializeField] private soDATA_GameEvent blackoutEndGameEvent;

    [Space(10)]

    [SerializeField, Expandable] private BALANCEDATA_Blackout data;

    private float blackoutDuration;
    private float whiteoutDuration;

    private float timer;

    private static bool paused = false;

    private static Coroutine blackoutCoroutine;

    private BlackoutState blackoutState = BlackoutState.Whiteout;

    [Space(10)]

    [ReadOnly] public float timeUntilNextBlackout;

    void Awake(){ Setup(); StartBlackoutLoop(); }

    [Button]
    void Setup()
    {
        instance = this;

        if (data == null){ return; }

        blackoutDuration = data.RULE_BlackoutDuration;
        whiteoutDuration = data.RULE_WhiteoutDuration;

        timeUntilNextBlackout = whiteoutDuration;
    }

    void Update(){ timeUntilNextBlackout = whiteoutDuration - timer; if (!paused){ timer += Time.deltaTime; }}

    public static void PauseTimer()
    {
        //Debug.Log(instance.blackoutState);
        instance.blackoutCover.gameObject.SetActive(false);

        if (instance.blackoutState == BlackoutState.Blackout){ instance.BlackoutEnded(); }

        instance.StopCoroutine(blackoutCoroutine);

        instance.ResetTimer();

        paused = true;
    }
    public static void UnPauseTimer(){ paused = false; instance.StartBlackoutLoop(); }

    void BlackoutStarted(){ blackoutStartGameEvent.Raise(); }
    void BlackoutEnded(){ blackoutEndGameEvent.Raise(); }

    void StartBlackoutLoop(){ blackoutCoroutine = StartCoroutine(BlackoutLoop()); }

    private IEnumerator BlackoutLoop()
    {
        yield return new WaitForSeconds(whiteoutDuration);

        blackoutState = BlackoutState.Blackout;
        blackoutCover.gameObject.SetActive(true);
        BlackoutStarted();

        yield return new WaitForSeconds(blackoutDuration);

        blackoutState = BlackoutState.Whiteout;
        blackoutCover.gameObject.SetActive(false);
        BlackoutEnded();
        ResetTimer();

        StartBlackoutLoop();
    }

    void ResetTimer(){ timer = 0; }
}

enum BlackoutState
{
    Blackout,
    Whiteout
}