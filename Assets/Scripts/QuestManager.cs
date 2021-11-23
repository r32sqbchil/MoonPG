using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public int questActionIndex;
    public int questid;
    public static int GoblinDeathcount;
    public GameObject Goblin;
    Dictionary<int, QuestData> questList;

    public void CheckQuest()
    {
        questActionIndex++;
    }

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
    	//생성자 이용 (string name, int[] npcid)
        questList.Add(10, new QuestData("오크 죽여라", new int[]{1000}));
        questList.Add(20, new QuestData("오크를 죽였다", new int[]{1000}));
    }

    public int GetQuestTalkIndex(int id) // Npc Id를 받아 퀘스트 번호를 반환하는 함수 
    {
        switch(id){
            case 1000:
                return 10;
            default:
                return 0;
        }
    }

    void Update()
    {
        // GoblinDeathcount = Goblin.GetComponent<test>().Questcount;

        //
        //
        //
        // if(GameObject.Find("Goblin1").GetComponent<test>().Questcount == 1)
        // {
        //     Debug.Log("hihihihi");
        // }

        // if(Goblin.GetComponent<test>.nowHP <= 0)
        // {
        //     GoblinDeathcount += 1;
        // }
    }

    
    void NextQuest()
    {
        if(GoblinDeathcount == 1)
        {
            questid +=10;
            questActionIndex = 0;
        }


    }

    public string CheckQuest(int id) //npc id
    {
    	//해당 퀘스트가 종료 전일 때
        if(id == questList[questid].npcIds[questActionIndex]) 
        	// questList에서 questId에 해당하는 퀘스트에서 , 
            //이 퀘스트에 참여하는 npc의 순서번호(questActionIndex)와 입력받은 npc id가 같은 경우 -> 퀘스트 종료 전
            questActionIndex++;

		//퀘스트가 종료되었을 때
        if(questActionIndex == questList[questid].npcIds.Length) 
        //퀘스트 리스트에 있는 NpcId(퀘스트에 참여하는 npc) 수와 같을 때 -> 퀘스트 종료
            NextQuest();

        return questList[questid].questName;
    }
}