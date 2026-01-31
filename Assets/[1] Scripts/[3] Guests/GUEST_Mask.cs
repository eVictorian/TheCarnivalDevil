using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class GUEST_Mask : MonoBehaviour
{
    //Only 1 Mask up at a time!
    public static bool isBusy {private set; get;} = false;

    [Space(10)]

    [SerializeField] private soDATA_GameEvent onMaskUpGameEvent;

    [Space(10)]

    [SerializeField, Range(0,10)] private float maskUpTime;
    [SerializeField, Range(0,10)] private float transitionDuration;
    [SerializeField] private AnimationCurve transitionSpeedCurve;

    [Space(10)]

    [SerializeField] private Quaternion pivotRotationMaskUp;
    [Button] void SetMaskUpRotationToCurrentRotation(){ pivotRotationMaskUp = pivot.localRotation; }
    [SerializeField] private Quaternion pivotRotationMaskDown;
    [Button] void SetMaskDownRotationToCurrentRotation(){ pivotRotationMaskDown = pivot.localRotation; }

    [Space(10)]

    [SerializeField] private Transform pivot;
    [SerializeField] private ENTITY entity;

    public void UnMask(){ MaskUp(); }
    [Button] public void MaskUp(){ if (isBusy){ return; } StartCoroutine(MaskUpCoroutine()); }
    private IEnumerator MaskUpCoroutine()
    {
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
        
        yield return new WaitForSeconds(maskUpTime);
    }

    [Button] public void MaskDown(){ if (isBusy){ return; } StartCoroutine(MaskDownCoroutine()); }
    private IEnumerator MaskDownCoroutine()
    {
        isBusy = true;

        float timer = 0f;
        Quaternion startRot = pivot.localRotation;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;

            // Apply animation curve
            float curvedT = transitionSpeedCurve.Evaluate(t);

            pivot.localRotation = Quaternion.Lerp(startRot, pivotRotationMaskDown, curvedT);

            yield return null;
        }

        pivot.localRotation = pivotRotationMaskDown;
    }
}