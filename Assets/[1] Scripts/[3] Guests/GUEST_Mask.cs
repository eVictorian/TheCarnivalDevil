using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class GUEST_Mask : MonoBehaviour
{
    public bool debug = false;
    public bool isBusy {private set; get;} = false;
    bool interactable = true;

    [Space(10)]

    [SerializeField] private soDATA_GameEvent onMaskUpGameEvent;
    [SerializeField] private soDATA_GUEST_Masks maskSelectionInput;
    [SerializeField] private GameObject defaultMaskObjectInput;
    private static List<GameObject> maskSelection;
    private static GameObject defaultMaskObject;

    [Space(10)]

    [SerializeField, Range(0,10)] private float maskUpTime;
    [SerializeField, Range(0,10)] private float transitionDuration;
    [SerializeField] private AnimationCurve transitionSpeedCurve = new AnimationCurve();
    [Button] void DEBUGResetTransitionCurve(){ transitionSpeedCurve = new AnimationCurve(); }

    [Space(10)]

    [SerializeField] private Quaternion pivotRotationMaskUp;
    [Button] void SetMaskUpRotationToCurrentRotation(){ pivotRotationMaskUp = Quaternion.Normalize(pivot.localRotation); }
    [SerializeField] private Quaternion pivotRotationMaskDown;
    [Button] void SetMaskDownRotationToCurrentRotation(){ pivotRotationMaskDown = Quaternion.Normalize(pivot.localRotation); }

    [Space(10)]

    [SerializeField] private Transform pivot;
    [SerializeField] private ENTITY entity;

    private static Coroutine activeCoroutine;

    void Awake()
    {
        if (maskSelection.Count == 0) maskSelection = new List<GameObject>(maskSelectionInput.masks);

        if (defaultMaskObject == null) defaultMaskObject = defaultMaskObjectInput;
    }

    public static void SetupAllMasks()
    {
        if (maskSelection.Count < 1){ return; }

        int[] randomizedMaskIndices = new int[maskSelection.Count];

        int i = 0;
        foreach (GameObject mask in maskSelection){ randomizedMaskIndices[Random.Range(0,maskSelection.Count)] = i; i++; }

        int i2 = 0;
        foreach (GUEST_Mask masked in FindObjectsByType<GUEST_Mask>(0))
        {
            if (i2 == randomizedMaskIndices.Count()){ break; }

            masked.SetMask(maskSelection[randomizedMaskIndices[i2]]);
            i2++;
        }
    }

    public void SetMask(GameObject newMask){}

    public void UnMask()
    {
        if (interactable)
        {
            MaskUp();
        }
    }
    [Button] void MaskUp(){ if (isBusy){ return; } GetComponentInParent<NPCAudioManager>().Unmask(); activeCoroutine = StartCoroutine(MaskUpCoroutine()); }
    private IEnumerator MaskUpCoroutine()
    {
        ENVIRONMENT_Blackouts.PauseTimer();
        isBusy = true;

        float timer = 0f;
        Quaternion startRot = pivot.localRotation;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;

            // Apply animation curve
            float curvedT = transitionSpeedCurve.Evaluate(t);

            pivot.localRotation = Quaternion.Lerp(startRot, pivotRotationMaskUp, curvedT);

            yield return null;
        }

        pivot.localRotation = pivotRotationMaskUp;

        onMaskUpGameEvent.Raise(entity.gameObject);
        interactable = false;
        
        yield return new WaitForSeconds(maskUpTime);

        isBusy = false;
        ENVIRONMENT_Blackouts.UnPauseTimer();
        MaskDown();
    }

    [Button] void MaskDown(){ if (isBusy){ return; } activeCoroutine = StartCoroutine(MaskDownCoroutine()); }
    private IEnumerator MaskDownCoroutine()
    {
            if (debug){ Debug.Log("prmu" + pivotRotationMaskUp); }
            if (debug){ Debug.Log("prmd" + pivotRotationMaskDown); }
            if (debug){ Debug.Log("current" + pivot.transform.localRotation); }

        isBusy = true;

        float timer = 0f;
        Quaternion startRot = pivotRotationMaskUp;

            if (debug){ Debug.Log("A" + startRot); }
            if (debug){ Debug.Log("to B" + pivotRotationMaskDown); }
            if (debug){ Debug.Log("over: " + transitionDuration); }

        GetComponentInParent<NPCAudioManager>().Unmask();
        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / transitionDuration);

            // Apply animation curve
            float curvedT = transitionSpeedCurve.Evaluate(t);

                if (debug){ Debug.Log(curvedT); }

            Quaternion newRotation = Quaternion.Lerp(startRot, pivotRotationMaskDown, curvedT);
            
                if (debug){ Debug.Log(newRotation); }

            pivot.localRotation = newRotation;

            yield return null;
        }

        pivot.localRotation = pivotRotationMaskDown;

        isBusy = false;
        interactable = true;
    }
}