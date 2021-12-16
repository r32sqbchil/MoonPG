using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    private Dictionary<string, Dictionary<int, string[]>> talksOfScene;
    private Dictionary<int, string> talkers;

    void Start()
    {
        talkers = GenerateTalkers(new Dictionary<int, string>());
        talksOfScene = new Dictionary<string, Dictionary<int, string[]>>();

        talksOfScene.Add(Scene.SCENE_TOWN_STAGE, GenerateTalksInTownStage(new Dictionary<int, string[]>()));
        talksOfScene.Add(Scene.SCENE_TOWN_STAGE1, GenerateTalksInTownStage1(new Dictionary<int, string[]>()));
        talksOfScene.Add(Scene.SCENE_TOWN_STAGE3, GenerateTalksInTownStage3(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage", GenerateTalksInMountainStage(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage2", GenerateTalksInMountainStage2(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage4", GenerateTalksInMountainStage4(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage5", GenerateTalksInMountainStage5(new Dictionary<int, string[]>()));
        talksOfScene.Add("MountainStage6", GenerateTalksInMountainStage6(new Dictionary<int, string[]>()));
        talksOfScene.Add("TestFieldStage", GenerateTalksInTestFieldStage(new Dictionary<int, string[]>()));
        talksOfScene.Add("TestFieldStage3", GenerateTalksInTestFieldStage3(new Dictionary<int, string[]>()));
        talksOfScene.Add("FieldStage", GenerateTalksInFieldStage(new Dictionary<int, string[]>()));
        talksOfScene.Add("FieldStage1", GenerateTalksInFieldStage1(new Dictionary<int, string[]>()));
        talksOfScene.Add("FieldStage2", GenerateTalksInFieldStage2(new Dictionary<int, string[]>()));
        talksOfScene.Add("CastleStage", GenerateTalksInCastleStage(new Dictionary<int, string[]>()));
        talksOfScene.Add("CastleStage2", GenerateTalksInCastleStage2(new Dictionary<int, string[]>()));
        talksOfScene.Add("CastleBossstage", GenerateTalksInCastleBossstage(new Dictionary<int, string[]>()));
        talksOfScene.Add("CastleStage3", GenerateTalksInCastleStage3(new Dictionary<int, string[]>()));
    }

     private Dictionary<int, string> GenerateTalkers(Dictionary<int, string> talkers){
        // ________________0_______1_______2______3________4____5____6____7____8______9___10
        string[] array = "슈라켄,어린마하,아줌마,어린마하,스승,마하,마하,하사,부대장,원수,보스".Split(',');
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
        talks.Add(200+20, new string[]{"고마워 :2"});
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
    private Dictionary<int, string[]> GenerateTalksInTestFieldStage(Dictionary<int, string[]> talks){
        talks.Add(700, new string[]{"젊으니 내가 보기엔 아직 테스트 보기에는 약한거 같은데 사냥하지 그래? :7 "}); //대화16
        talks.Add(700+10, new string[]{});
        talks.Add(800, new string[]{"젊으니 테스트 볼 준비 되었니? :8" , "네! 준비 됬습니다 기대하고 있으마 이상한 늑대와 오크 패잔병을 처치해 보렴 :8"}); //대화17
        talks.Add(800+10, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInTestFieldStage3(Dictionary<int, string[]> talks){
        talks.Add(800, new string[]{"모든 테스트를 통과 한걸 축하하네 :8" , "이번에는 처음으로 두명이 통과 한거 같군 :8" , "자 이제 너희들은 해군 특령이란다. :8" , "저기 왼쪽 끝에 원수가 계셔 한번 가봐 :8" , "넵 감사합니다."}); //대화18
        talks.Add(900, new string[]{"너희가 이번 제10기 특령이구나 :9" , "최연소 특령이네 하하하하! :9" , "실력 있는 인재는 언제나 환영이지 :9" , "뭐 말하고 싶은게 있나? :9" , "원수님 혹시 라프텔 성 사건에 대해 알려 줄 수 있나요? :5" , "음.. 그건 유감이였지 누군가가 정보를 유출한거 같더라고 그래서 참패했지 :9" , "아 마침 이번에 탈환 작전을 할 생각인데 :9" , "너희 혹시 참가할 생각이 있나? :9" , "네! 저희 참가 할게요" , "정보에 의하면 성 지하 감옥에 인질들이 잡혀 있다고 하더군 :9" , "마을로 가서 부대장과 같이 라프텔 전초기지로 가게나 :9" , "알겠습니다 :5"}); //대화19
        talks.Add(900+10, new string[]{});
        // 900번과 대화를 하고와야 추가 대화 가능
        talks.Add(800+10, new string[]{"자원해서 전장에 가다니 대단하구나 너희 :8" , "첫 전쟁이니 너무 무리하지 말거라 :8" , "넵 알겠습니다" , "패기 넘치는군 자 이제 가자 :8" , "아 참고고 너희 신분은 일반 해군 병사 일것이니 :8" , "신분을 숨기도록 :8" , "넵 명심하겠습니다 :5"}); // 대화 20
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInFieldStage(Dictionary<int, string[]> talks){
        talks.Add(700, new string[]{"어? 너희 어디서 많이 본 얼굴들인데? 아닌가? :7" , "너희 저기 주변에 탐색하고 있는 병사를 처치해줘 :7"}); //대화21
        talks.Add(700+10, new string[]{});
        talks.Add(800, new string[]{"자 이제 준비 됐나? :8" , "이번 작전은 성의 탈환이다 :8" , "마하와 나는 선봉 부대이고 슈라켄은 뒷따라오거라 :8" , "성에 입성하면 슈라켄이 인질을 풀고 데리고 나오도록 :8" , "알겠습니다." , "자 이제 출발한다. 슈라켄 뒤를 맡기마 :8"}); // 대화 22
        talks.Add(800+10, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInFieldStage1(Dictionary<int, string[]> talks){
        talks.Add(500, new string[]{"마하 너 왜 쓰러져 있어?!!!!!" , "녀석들도 우리의 정보를 미리 알고 있었던거 같에 :5" , "부대장님은 먼저 성안으로 들어갔을꺼야 : 5" , "후 … 슈라켄, 천사족 애들은 참 강하네… :5" , "야!!! 더 말하지마!! 내가 꼭 구해줄께 조금만 더 버텨" , "너는 그들보다 더 강할거라 믿어, 나랑 친구해줘서 고맙다… :5"}); //대화23
        talks.Add(500+10, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInFieldStage2(Dictionary<int, string[]> talks){
        talks.Add(0, new string[]{"이 자식들 용서할 수 없어!!!!! :0"}); //대화24
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInCastleStage(Dictionary<int, string[]> talks){
        talks.Add(800, new string[]{"슈라켄 무사해서 다행이구나 :8" , "근데 너 뭐가 달라졌군 :8" , "부대장님 아무래도 이번에도 정보가 유출된거 같아요.." , "음 그런거 같군… 이번 작전 끝나고 조사해봐야겠어 :8" , "부대장님 조사건은 저한테 맡겨주세요." , "마하……마하.가 전사했습니다." , "!!!!..... 유감이구나 젊은 인재를 잃어버렸군… :8" , "꼭 범인을 찾아내도록 도와줄게 :8" , "내가 성 앞에서 버티고 있을 테니 너가 인질 구출하도록 :8" , "네 알겠습니다 부대장님 죽지 마세요" , "걱정말게 난 죽지 않아 :8"}); //대화25
        talks.Add(800+10, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInCastleStage2(Dictionary<int, string[]> talks){
        talks.Add(400, new string[]{"너는 슈라켄? :4" , "너도 엄연한 해군이구나 :4" , "오랜만입니다 스승님" , "음… 많이 강해졌구나 아서왕에게 생기는 오라가 느껴지는군 :4" , "너도 캠플리구나 :4" , "캠플리요?" , "일단 먼저 나가도록 하자 :4" , "넵"}); //대화26
        talks.Add(400+10, new string[]{});
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInCastleBossstage(Dictionary<int, string[]> talks){
        talks.Add(0, new string[]{"당신이 천사족 대장인가?" , "…..... :10" , "앞을 막는거 보니 맞군 :4" , "스승님 인질을 데리고 부대장님과 합류 해주세요" , "저도 복수는 해야 하지 않겠습니까?" , "그래 널 믿으마 죽지 말거라 :4" , "저만 믿으십쇼"}); //대화27
        return talks;
    }
    private Dictionary<int, string[]> GenerateTalksInCastleStage3(Dictionary<int, string[]> talks){
        talks.Add(400, new string[]{"엄청 강해졌군 :8" , "역시 캠플리인가? :8" , "캠플리가 뭔가요? 스승님" , "아서왕처럼 초인적인 힘을 가진 인간을 말해 :4" , "너도 그 중 한명인 거 같아 :4" , "아무튼 엄청 강하다는거야 슈라켄 :8" , "이번 작전 너 아니였으면 또 실패했을 지도 :8" , "앞으로도 잘 부탁하마 참고로 너의 스승님도 캠플러이란다. :8" , "원수도 캠플러이긴 하지 :8" , "이제 성도 탈환 했겠다 마을 돌아가서 마하 장례를 치르도록 하자 :8" , "?? 근데 슈라켄 마하 어디에서 죽었니? 시체가 안보이던데? :8", "????!!!! 성 앞 얼마 안되는 거리에 있을텐데요.......?" , "뭔지 모르겠지만 불길한 느낌이 드는군 :4"}); //대화28
        return talks;
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
}
