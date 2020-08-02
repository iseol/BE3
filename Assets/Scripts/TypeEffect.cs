using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public string targetMsg;
    public int charPerSeconds;
    public GameObject EndCursor;
    public AudioSource audioSource;
    int index;
    float interval;
    public bool isAnimation;


    Text msgText;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnimation)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        isAnimation = true;

        interval = 1.0f / charPerSeconds; 
        Invoke("Effecting", interval);
        
        // charPerSec의 역수 interval: 1글자가 나오는 딜레이
    }
    void Effecting()
    {
        if (index == targetMsg.Length)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        // Sound
        if (targetMsg[index] != ' ' || targetMsg[index] != '.' || targetMsg[index] != ',')
            audioSource.Play();
        ++index;

        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        isAnimation = false;
        EndCursor.SetActive(true);
    }
}
