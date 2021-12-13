using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    private Dictionary<string, Dictionary<int, string[]>> talksOfScene;
    private Dictionary<int, string> talkers;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();

        talkers = GenerateTalkers(new Dictionary<int, string>());
        talksOfScene = new Dictionary<string, Dictionary<int, string[]>>();
        talksOfScene.Add("townstage", GenerateTalksInTownstage(new Dictionary<int, string[]>()));
        talksOfScene.Add("townstage1", GenerateTalksInTownstage1(new Dictionary<int, string[]>()));
    }

    private Dictionary<int, string> GenerateTalkers(Dictionary<int, string> talkers){
        string[] array = "슈라켄,어린마하,아줌마,어린마하,스승".Split(',');
        for(int i=0; i<array.Length; i++){
            talkers.Add(i, array[i]);
        }
        return talkers;
    }

    private Dictionary<int, string[]> GenerateTalksInTownstage(Dictionary<int, string[]> talks){
        talks.Add(0, new string[]{"음, 잘 잤다. 마하를 찾아가 봐야지 :0", "오른쪽에 마하 집이 있으니깐 가봐야겠다. :0"}); //대화1
        talks.Add(200, new string[]{"요즘 저쪽에 늑대와 뱀이 너무 많아 잡아와줄래? :2"}); //대화2
        talks.Add(200+10, new string[]{"부탁해 :2"});
        talks.Add(200+20, new string[]{"고마워 :2"});
        talks.Add(100, new string[]{"어 왔네, 오늘은 같이 산으로 놀러갈래? :1","나야 좋지 :0","그럼 나 먼저 간다 잘따라와 :1"}); //대화3
        return talks;
    }

    private Dictionary<int, string[]> GenerateTalksInTownstage1(Dictionary<int, string[]> talks){
        talks.Add(0, new string[]{"마하가 어딨지? :0"}); //대화4
        talks.Add(100, new string[]{"잘따라 오고 있지?:1","당연하지 애도 아니고:0","이렇게 그냥 올라가기엔 심심한데 :0","아! 우리 누가 먼저 산 정산에 올라가는지 시합하자:0","음… 그래 :1","나 먼저 출발한다 :1","야 그런게 어딨어!!!! :0"}); //대화5
        talks.Add(100+10, new string[]{});
        talks.Add(300, new string[]{"슈라켄 … 왜 이제 왔어 저기 이상한 뭔가가 있는거 같에… :3","응? 무슨 소리? :0","저기 뭔가가 여기로 다가오고 있는거 같은데?:3","어! 오크패잔병이잖아!!! 도망쳐 마하! :0"});
        return talks;
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
        //talkData.Add(100,new string[]{"쇠로 만들어진 감옥이다.","열쇠없이는 열 수 없는 것 같다."});
        //id = 200 : 다음 스테이지로 넘어갈 수 있는 문 
        //talkData.Add(200,new string[]{"평범한 문이다. \n들어갈 수 있을 것 같다."});
    }

    public string GetTalker(int index){
        if(talkers.ContainsKey(index)) {
            return talkers[index];
        } else {
            Debug.Log("Invalid talker");
            return null;
        }
    }

    public string GetTalk(string sceneName, int id, int talkIndex) //Object의 id , string배열의 index
    {
        if(talksOfScene.ContainsKey(sceneName)){
            Dictionary<int, string[]> talks = talksOfScene[sceneName];
            if(talks.ContainsKey(id)){
                if (talkIndex >= 0 && talkIndex < talks[id].Length) {
                    return talks[id][talkIndex]; //해당 아이디의 해당하는 대사를 반환한다
                }
            }
        }

        return null;
    }

    public string GetTalk(int id, int talkIndex){
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
