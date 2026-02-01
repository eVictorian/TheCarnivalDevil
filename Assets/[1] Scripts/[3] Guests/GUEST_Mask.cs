using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class GUEST_Mask : MonoBehaviour
{
    public bool debug = false;
    public bool isBusy {private set; get;} = false;
    bool interactable = true;

    [Space(10)]

    [SerializeField] private soDATA_GameEvent onMaskUpGameEvent;

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

    public void UnMask()
    {
        if (interactable)
        {
            MaskUp();
            GetComponentInParent<NPCAudioManager>().Unmask();
        }
    }
    [Button] void MaskUp(){ if (isBusy){ return; } activeCoroutine = StartCoroutine(MaskUpCoroutine()); }
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