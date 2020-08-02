using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData; // 초상화 스프라이트의 id, 초상화 스프라이트

    public Sprite[] portraitArr; // 초상화 스프라이트가 있는 배열

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();// 딕셔너리 값을 초기화
        GenerateData(); // 딕셔너리 값을 다시 불러옴
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:1", "이 곳에 처음 왔구나?:2", "한 번 둘러보도록 해.:1" });

        talkData.Add(2000, new string[] { "여어.:1", "이 호수는 정말 아름답지?:0", "사실 이 호수에는 무언가의 비밀이 숨겨져 있다고 해.:1" });

        talkData.Add(3000, new string[] { "평범한 나무 상자다." });

        talkData.Add(4000, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

        // 퀘스트 대화
        talkData.Add(10 + 1000, new string[] { "어서 와.:0", "이 마을에 놀라운 전설이 있다는데,:1", "오른쪽 호수 쪽에 있는 루도가 알려줄꺼야.:0" });

        talkData.Add(11 + 2000, new string[] { "여어.:1", "이 호수의 전설을 들으러 온 거야?:0", "그럼 일 좀 하나 해 주면 좋을텐데...:1", "내 집 근처에 떨어진 동전 좀 주워줬으면 해.:0" });


        talkData.Add(20 + 1000, new string[] { "루도의 동전?:1", "돈을 흘리고 다니면 못 쓰지!:3", "나중에 루도에게 한마디 해야겠어.:3" });
        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다 줘.:1" });
        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });

        talkData.Add(21 + 2000, new string[] { "엇, 찾아줘서 고마워.:2" });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        // id - id%10 -> 퀘스트 맨 처음 대사
        // id - id%100 -> 기본 대사

        if (!talkData.ContainsKey(id))
        // talkData 딕셔너리에서 id값에 따른 대사가 없다면,
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);
            else
                return GetTalk(id - id % 10, talkIndex);
        }
        if (talkIndex == talkData[id].Length) // 대화 배열의 끝자락에 도달했을 경우 
            return null; // 대화를 종료합니다.
        else
            return talkData[id][talkIndex]; // 대화 딕셔너리의 값을 불러옵니다.
    }
    public Sprite GetPortrait(int id, int portraitIndex) // 초상화를 불러옵니다.
    {
        return portraitData[id + portraitIndex];
    }
}