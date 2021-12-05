using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {

        talkData.Add(1000 + 10, new string[]{"인사는 나눴으니 저기 있는 오크좀 잡아와 줄래? :1", "네 알겠습니다"});
        talkData.Add(1000 + 20, new string[]{"인사는 나눴으니 저기 있는 오크좀 잡아와 줄래? :1", "네 알겠습니다"});
        talkData.Add(1000 + 30, new string[]{"무사히 잡고 돌아왔구나, 이제 회색늑대를 잡아줄래? :1 ", "넵 알겠습니다 :0"});
        talkData.Add(1000 + 40, new string[]{"회색 늑대를 잡아 주려무나 :1 ", "넵"});

    	//id = 1000 : Talia
        talkData.Add(1000, new string[]{"안녕 꼬맹아 :1"}); // 이거 왜 안뜰까
        //id = 100 : 탈리아가 갇혀있는 Prision
        talkData.Add(100,new string[]{"쇠로 만들어진 감옥이다.","열쇠없이는 열 수 없는 것 같다."});
        //id = 200 : 다음 스테이지로 넘어갈 수 있는 문 
        talkData.Add(200,new string[]{"평범한 문이다. \n들어갈 수 있을 것 같다."});
    }


    public string GetTalk(int id, int talkIndex) //Object의 id , string배열의 index
    {
        if(talkData.ContainsKey(id))
        {
            if (talkIndex >= 0 && talkIndex < talkData[id].Length) 
            {
                return talkData[id][talkIndex]; //해당 아이디의 해당하는 대사를 반환한다
            }
            else
            {
                //Debug.Log("Out of bound(index:" + talkIndex + " of talk-data[=" + id + "])");
            }
        }
        else 
        {
            //Debug.Log("There is no talk-data for key[=" + id + "]");
        }
        return null;
    }
}
