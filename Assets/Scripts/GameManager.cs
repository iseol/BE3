﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    public Animator portraitAnim;
    public TypeEffect talk;
    public Image portraitImg; // 직접적으로 나타나는 초상화 이미지
    public Sprite prevPortrait;
    public GameObject scanObject;
    public bool isAction = false; // UI가 나타났는지 여부
    public int talkIndex;
    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj; // scanObject 변수에 할당
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetBool("isShow", isAction);
    }
    void Talk(int id, bool isNpc)
    {
        int questTalkIndex;
        string talkData;

        if (talk.isAnimation)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex();
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }


        if (talkData == null) // 대화 데이터가 더이상 없을 경우,
        {
            isAction = false; // 대화를 종료하고 움직임을 활성화
            talkIndex = 0; // 인덱스 초기화
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
        
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]); // 대화 내용을 판넬에 띄웁니다.

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // 대화 내용에 따른 표정 변화를 불러옵니다.
            portraitImg.color = new Color(1, 1, 1, 1); // 초상화 활성화
            if (prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
            
            
        }
        else
        {
            talk.SetMsg(talkData);
            portraitImg.color = new Color(1, 1, 1, 0); // 초상화 비활성화
        }

        isAction = true; // 움직임 비활성화
        talkIndex++; // 대화 내용 배열의 인덱스 증가 (스페이스 키를 누르면 다음 대화로 넘어가짐)
    }
}
