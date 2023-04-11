# MoonPG
1. 게임명
- Campliy Fantasy

2. 소개
- 친구 마하와 산골마을에서 시작되는 스토리형 RPG게임

3. 장르
- RPG

4. 제작 기간
- 2021.10 ~ 2022.12

5. 게임 엔진
- Unity

6. 참여자
- 기획자: 2명
- 아티스트: 1명
- 개발자: 2명(중도 이탈로 1명)

***
1. 콤보 공격
- 플레이어의 기본 공격을 3가지 상태로 나눔
- 각 공격마다 데미지를 올려줌
- 3번째 공격 이후에는 데미지를 초기화. Animation.GetCurrentAnimatorStateInfo를 활용하여 애니메이션 또한 초기화해줌
![image](https://user-images.githubusercontent.com/91232917/231188193-972da72f-6fca-43f0-a6a3-5af89f6808ca.png)
***
2. 보스 스킬패턴
- 스킬은 총 3가지로 나뉨
- 첫 번째 스킬은 1초 전 플레이어 위치에 창을 떨어뜨리는 것. 코루틴을 활용하여 1초 전 플레이어 x값을 받아둠
- 위치값을 받아옴과 동시에 바닥에 빨간색으로 주의 표시함
- 두 번째 스킬은 보스 주변에 원형으로 빛을 뿜는 것. 보스의 모션으로 주의 표시하고, 부딪히면 데미지를 입도록 함
- 세 번째 스킬은 2초 후 일정한 간격으로 폭발이 일어나는 것. 첫 번째처럼 코루틴을 활용하여 바닥에 주의 표시를 하였음
- 페이즈 1일 때는 두 번째 스킬까지 반복함. 코루틴을 활용
- 보스의 체력이 70% 이하가 될 시 페이즈2, 40% 이하가 될 시 페이즈3로 보스 스킬의 딜레이를 줄임
***
3. 퀘스트 시스템(Quest System)
- 디자인 패턴 중 하나인 옵저버 패턴(Observer Pattern)을 활용
- QuestManager에서 22종(스테이지 개수)의 QuestHandler(옵저버), 이를 관리하는 Dictionary 3종(questContextMap, questHandlerMap, updateObservers)을 생성
- 각 스테이지의 Quest들은 QuestHandler 옵저버를 상속받음 
- questHandlerMap에 키값과 씬 이름에 맞는 QuestHandler를 추가
- questContextMap에서는 키값이 없다면 씬 이름과 오브젝트 ID를 추가하여 넣어줌
![image](https://user-images.githubusercontent.com/91232917/231196186-16711a84-b596-485a-97be-7fbc787b4354.png)
- AddUpdateHandler()는 updateObservers내에 Handler를 추가함으로서 Handler가 Action 이벤트 발생을 전달받도록 해주는 메서드
- 반면 RemoveUpdateHandler()는 Handler가 Action 이벤트 발생을 전달받지 않도록 해줌
![image](https://user-images.githubusercontent.com/91232917/231196620-9b0aacab-97e5-45f3-9319-248896b23c02.png)
- NotifyAction()은 GameObject에 일어난 Action이 필요하다고 요구한 Quest들에 알려주는 역할을 함
![image](https://user-images.githubusercontent.com/91232917/231197811-ed79feb1-fb6a-4aea-91bb-b735f0a7f201.png)
