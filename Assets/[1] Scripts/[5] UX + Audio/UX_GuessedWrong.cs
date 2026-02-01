using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UX_GuessedWrong : MonoBehaviour
{
    public List<Camera> sceneCameras = new List<Camera>();
    public List<Camera> gameCameras = new List<Camera>();

    public List<Vector3> creaturePositions = new List<Vector3>();

    public int counter = 0;
    public float sceneLength;

    [SerializeField] AudioSource jumpScare;

    [SerializeField] Transform creature;

    [Button] public void StartGuessedWrongScene(){ StartCoroutine(GuessedWrongCoroutine()); }

    private IEnumerator GuessedWrongCoroutine()
    {
        Debug.Log(creaturePositions[counter]);
      
        creature.localPosition = creaturePositions[counter];
        EnableSceneCams();
        DisableGameCams();

        if (counter >= creaturePositions.Count - 1)
        {
            PlayJumpscare(false);
        }
        else
        {
            if (counter >= creaturePositions.Count-2)
            {
                GameObject.FindWithTag("HeartBeatAudio").GetComponent<AudioSource>().Play();
            }
            counter++;

            yield return new WaitForSeconds(sceneLength);

            EnableGameCams();
            DisableSceneCams();
        }
    }

    public void PlayJumpscare(bool exterior = true)
    {
        ENVIRONMENT_Blackouts.PauseTimer();

        if (exterior)
        {
            creature.localPosition = creaturePositions[counter = creaturePositions.Count - 1];
            EnableSceneCams();
            DisableGameCams();
        }

        //Death stuff
        jumpScare.Play();
        Invoke("sendToStartScreen", jumpScare.clip.length);
    }

    void sendToStartScreen()
    {
        Debug.Log("EndScreenStart");
        SceneManager.LoadScene("start");
    }

    void EnableSceneCams(){ ChangeCamListActiveStates(sceneCameras, true); }
    void DisableSceneCams(){ ChangeCamListActiveStates(sceneCameras, false); }

    void EnableGameCams(){ ChangeCamListActiveStates(gameCameras, true); }
    void DisableGameCams(){ ChangeCamListActiveStates(gameCameras, false); }

    void ChangeCamListActiveStates(List<Camera> cameras, bool activeState){ foreach (Camera camera in cameras){ camera.gameObject.SetActive(activeState); }}
}