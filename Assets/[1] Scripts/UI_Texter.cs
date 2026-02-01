using TMPro;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_Texter : MonoBehaviour
{
    static UI_Texter instance;

    [SerializeField] private TextMeshProUGUI texter;

    private Coroutine currentSpeakingCoroutine;

    public soDATA_Dialogue sayOnAwake;

    void Awake(){ if (texter == null){ texter = GetComponent<TextMeshProUGUI>(); } instance = this; SayDialogue(sayOnAwake);}

    public static void Speak(soDATA_Dialogue dialogue){ instance.SayDialogue(dialogue); }

    void SayDialogue(soDATA_Dialogue dialogue){ if (currentSpeakingCoroutine != null){ StopCoroutine(currentSpeakingCoroutine); } currentSpeakingCoroutine = StartCoroutine(SpeakingCoroutine(dialogue)); }

    private IEnumerator SpeakingCoroutine( soDATA_Dialogue dialogue )
    {
        if (dialogue.voiceline != null) Instantiate(dialogue.voiceline);

        texter.text = "<b>" + dialogue.speakerName + ":</b> " + dialogue.content;

        yield return new WaitForSeconds(dialogue.length);

        texter.text = "";
        
        currentSpeakingCoroutine = null;
    }
}