using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    public Animator portraitAnim;
    public TypeEffect talk;
    public Image portraitImg; // 직접적으로 나타나는 초상화 이미지
    public Text questText;
    public Text nameText;
    public Sprite prevPortrait;
    public GameObject scanObject;
    public GameObject menuSet;
    public GameObject player;
    public bool isAction = false; // UI가 나타났는지 여부
    public int talkIndex;
    void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) //  서브메뉴
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
            
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
            questText.text = questManager.CheckQuest(id);
            return;
        }
        
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]); // 대화 내용을 판넬에 띄웁니다.
            nameText.text = scanObject.name;

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
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QustId", questManager.questId);
        PlayerPrefs.SetInt("QustActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        questManager.questId = PlayerPrefs.GetInt("QustId");
        questManager.questActionIndex = PlayerPrefs.GetInt("QustActionIndex");
        questManager.ControlObject();
        player.transform.position = new Vector3(x, y, 1);
        
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
