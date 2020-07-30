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
        talkData.Add(1000, new string[] { "안녕?:1", "이 곳에 처음 왔구나?:2" });

        talkData.Add(2000, new string[] { "와 샌즈!:1", "아시는구나!:2" });

        talkData.Add(100, new string[] { "평범한 나무 상자다." });

        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

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
