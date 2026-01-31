using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

public class UX_GuessedWrong : MonoBehaviour
{
    public List<Camera> sceneCameras = new List<Camera>();
    public List<Camera> gameCameras = new List<Camera>();

    public List<Vector3> creaturePositions = new List<Vector3>();

    public int counter = 0;
    public float sceneLength;

    [SerializeField] Transform creature;

    [Button] public void StartGuessedWrongScene(){ StartCoroutine(GuessedWrongCoroutine()); }

    private IEnumerator GuessedWrongCoroutine()
    {
        creature.localPosition = creaturePositions[counter];
        counter++;

        EnableSceneCams();
        DisableGameCams();

        yield return new WaitForSeconds(sceneLength);

        EnableGameCams();
        DisableSceneCams();
    }

    void EnableSceneCams(){ ChangeCamListActiveStates(sceneCameras, true); }
    void DisableSceneCams(){ ChangeCamListActiveStates(sceneCameras, false); }

    void EnableGameCams(){ ChangeCamListActiveStates(gameCameras, true); }
    void DisableGameCams(){ ChangeCamListActiveStates(gameCameras, false); }

    void ChangeCamListActiveStates(List<Camera> cameras, bool activeState){ foreach (Camera camera in cameras){ camera.gameObject.SetActive(activeState); }}
}