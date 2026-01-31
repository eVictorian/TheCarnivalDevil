using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject ScreamAudio;

    public void Scream(Vector3 AudioPosition)
    {
        Instantiate(ScreamAudio, AudioPosition, new Quaternion(0,0,0,0));
    }
}
