using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
        
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기."
                                        , new int[] { 1000, 2000 }));

        questList.Add(20, new QuestData("루도의 동전 찾아주기."
                                       , new int[] { 5000, 2000 }));

        questList.Add(30, new QuestData("퀘스트 올 클리어!"
                                       , new int[] { 0 }));
    }

    public int GetQuestTalkIndex()
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex]) // NPC의 id가 'questList' Dictionary 안의 'quest' 안에서의 NPC들의 id가 있는 int[]에서의 id와 동일하다면
        {
            questActionIndex++;
        }
        ControlObject(); // 퀘스트 오브젝트 관리

        if (questActionIndex == questList[questId].npcId.Length) // questActionIndex가 'questList' Dictionary 안의 'quest' 안에서의 NPC들의 id가 있는 int[]의 길이와 같다면 (끝에 도달했다면)
        {
            NextQuest();
        }
        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
    void ControlObject()
    {
        switch (questId) {
            case 10:
                if (questActionIndex == 2)
                {
                    questObject[0].SetActive(true);
                }
                break;
            case 20:
                if (questActionIndex == 1)
                {
                    questObject[0].SetActive(false);
                }
                break;
        }
    }
}
