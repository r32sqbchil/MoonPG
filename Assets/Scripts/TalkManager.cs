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
        talksOfScene.Add("TownStage", GenerateTalksInTownStage(new Dictionary<int, string[]>()));
        talksOfScene.Add("TownStage1", GenerateTalksInTownStage1(new Dictionary<int, string[]>()));
        talksOfScene.Add("TownStage3", GenerateTalksInTownStage3(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage", GenerateTalksInMountainStage(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage2", GenerateTalksInMountainStage2(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage4", GenerateTalksInMountainStage4(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage5", GenerateTalksInMountainStage5(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage5", GenerateTalksInMountainStage6(new Dictionary<int, string[]>()));
    }

    private Dictionary<int, string> GenerateTalkers(Dictionary<int, string> talkers){
        string[] array = "슈라켄,어린마하,아줌마,어린마하,스승,마하,마하,하사".Split(',');
        for(int i=0; i<array.Length; i++){
            talkers.Add(i, array[i]);
        }
        return talkers;
    }

    private Dictionary<int, string[]> GenerateTalksInTownStage(Dictionary<int, string[]> talks){
        talks.Add(0, new string[]{"음, 잘 잤다. 마하를 찾아가 봐야지 :0", "오른쪽에 마하 집이 있으니깐 가봐야겠다. :0"}); //대화1
        talks.Add(200, new string[]{"요즘 저쪽에 늑대와 뱀이 너무 많아 잡아와줄래? :2"}); //대화2
        talks.Add(200+10, new string[]{"부탁해 :2"});
        talks.Add(200+20, new string[]{"고마워 :2"});
        talks.Add(100, new string[]{"어 왔네, 오늘은 같이 산으로 놀러갈래? :1","나야 좋지 :0","그럼 나 먼저 간다 잘따라와 :1"}); //대화3
        return talks;
    }

    private Dictionary<int, string[]> GenerateTalksInTownStage1(Dictionary<int, string[]> talks){
        talks.Add(0, new string[]{"마하가 어딨지? :0"}); //대화4
        talks.Add(100, new string[]{"잘따라 오고 있지?:1","당연하지 애도 아니고:0","이렇게 그냥 올라가기엔 심심한데 :0","아! 우리 누가 먼저 산 정산에 올라가는지 시합하자:0","음… 그래 :1","나 먼저 출발한다 :1","야 그런게 어딨어!!!! :0"}); //대화5
        talks.Add(100+10, new string[]{});
        talks.Add(300, new string[]{"슈라켄 … 왜 이제 왔어 저기 이상한 뭔가가 있는거 같에… :3","응? 무슨 소리? :0","저기 뭔가가 여기로 다가오고 있는거 같은데?:3","어! 오크패잔병이잖아!!! 도망쳐 마하! :0"}); //대화6
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInTownStage3(Dictionary<int, string[]> talks){
        talks.Add(400, new string[]{"괜찮나 청년 ? :4" , "네 감사합니다." , "조심하렴 앞으로 위험할 때를 대비해 대쉬를 알려줄게 :4" , "어? 대쉬요? 감사합니다 근데 누구세요?" , " 그냥 여기 산에 살고 있는 유랑민이란다 :4"}); //대화7
        talks.Add(400+10, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInMountainStage(Dictionary<int, string[]> talks){
        talks.Add(200, new string[]{"저 더 강해지고 싶어요!" , "그럼 저기 몬스터를 사냥해와 :2"}); //대화8
        talks.Add(200+10, new string[]{});
        talks.Add(400, new string[]{"저를 제자로 받아주세요 ! 저번 전투에서는 정말 감사했습니다…!" , "스승님 전 더 강해지고 싶습니다." , "그럼 일딴 동굴의 검은색 광석을 가져와라 :4"}); //대화9
        talks.Add(400+10, new string[]{});
        return talks;
    }

    // 여기서부터 대화 수정 요망
    private Dictionary<int, string[]> GenerateTalksInMountainStage2(Dictionary<int, string[]> talks){
        talks.Add(400, new string[]{"광석을 가져왔군.. :4" , "몸을 순간적으로 증폭시키는 방법을 알려주지 :4"}); //대화10
        talks.Add(400+10, new string[]{"잘했지만 아직 제자로 받아드리기엔 약하군.. :4" , "선생님 전 꼭 제자가 되고 싶어요" , "그럼 산 뒤쪽 마을에 이상한 늑대가 나타났다. 마을을 늑대로부터 구해주어라 :4" , "네 알겠습니다 다녀오겠습니다"}); //대화11 //대화11
        talks.Add(400+20, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInMountainStage4(Dictionary<int, string[]> talks){
        talks.Add(400, new string[]{"이상한 늑대를 처치 할 수 있을 줄 몰랐는데 잘했어 소년:4" , "충분히 증명된거 같네 너의 열정:4" , "마지막 기술을 알려줄게 :4"}); //대화12
        talks.Add(400+10, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInMountainStage5(Dictionary<int, string[]> talks){
        talks.Add(400, new string[]{"소년 나는 어디 멀리 떠나야할거 같에 :4" , "스승님 어디가시는 건가요?" , "그건 말해 줄 수 없네 :4" , "나중에 다시 찾아오마 :4" , "일딴 해군을 목표로 훈련을 하고 있거라 :4"}); //대화13
        talks.Add(400+10, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInMountainStage6(Dictionary<int, string[]> talks){
        talks.Add(500, new string[]{"슈라켄 라프텔 성에 천사족과 우리 인간족이 싸운 소식기억하니? :5" , "음.. 아 그거 기억하지" , "내가 가문 사람에게 들었는데 스승님이 거기 잡혀 있다고 하더라고 :5" , "음? 스승님이 왜 거기에?" , "스승님은 해군 소속이셨고 특령이라고 하더라고 :5" , "그것도 최초의 1기 특령이라고 :5" , "특령? 매년마다 해군에서 뽑는 기밀 해군 말하는거니?" , "스승님 대단한 사람이구나" , "근데 너는 어떻게 알았어?" , "이건 비밀인데 우리 가문에 제 4기 특령 출신이 있어 :5" , "난 도련님이라 알아냈지 :5" , "근데 너이거 다른 사람한테 말하면 안된다. :5" , "그 스승님이 잡힐 정도라니 구하러 가야지!!!" , "슈라켄 진정하고 일딴 곧 있을 특령시험에 합격해서 해군원수와 만나서 상세한 내용을 들어보자 :5" , "그래 그럼 시험 보러 가자" , "아무래도 마차 준비 뒀어 마을 입구에서 기다릴께 :5"}); //대화14
        talks.Add(500+10, new string[]{});
        talks.Add(600, new string[]{"이번엔 방문 시험이더라고 :6", "장소는 옆 마을이고 :6", "그쪽으로 가는 마차를 구해놨어 :6" , "좋아 가자!!!"}); //대화15
        talks.Add(600+10, new string[]{});
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
