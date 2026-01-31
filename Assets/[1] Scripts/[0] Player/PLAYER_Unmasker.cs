using UnityEngine;
using UnityEngine.InputSystem;

public class PLAYER_Unmasker : MonoBehaviour
{
    [SerializeField, Range(0.1f,5)] private float maxUnmaskingDistance = 0.1f;

    [Space(10)]

    [SerializeField] Camera myCamera;

    [Space(10)]

    public int remainingGuesses = 3;

    void Awake(){ if (myCamera == null) myCamera = FindFirstObjectByType<Camera>(); }

    void Update()
    {
        Ray ray = new Ray(myCamera.transform.position, myCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxUnmaskingDistance))
        {
            if (!hit.collider.CompareTag("Mask")){ return; }



            if (remainingGuesses <= 0){}
            else if (Mouse.current.leftButton.wasPressedThisFrame){ hit.collider.gameObject.GetComponent<GUEST_Mask>().UnMask(); }
        }
    }
}