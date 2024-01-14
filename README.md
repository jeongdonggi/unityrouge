# unityrouge

에셋에 script에 코드 있음. 크기 관계로 몇개의 코드 삭제 및 에셋 삭제

## rougeknight

유니티를 이용하여 구현한 1인칭 성 지키기 게임으로 플레이어가 조작할 수 있는 캐릭터와 자동으로 공격하는 병사를 이용하여 몬스터를 막는 게임

## 개발 언어
![Unity](https://img.shields.io/badge/Unity-000000?style=flat&logo=Unity)

## 주요 기능

#### 1. 시작 화면
<img width="1024" alt="image" src="https://github.com/jeongdonggi/unityrouge/assets/100845304/7269f167-6c0a-4d32-a7e7-9f5214d57cfc">


#### 2. 게임 화면
<img width="1024" alt="image" src="https://github.com/jeongdonggi/unityrouge/assets/100845304/fa6b189c-5a51-4bb1-b0da-355fc42dc758">
<img width="1020" alt="image" src="https://github.com/jeongdonggi/unityrouge/assets/100845304/ae69508a-cf35-4923-89f8-35bc746d0eb8">

  1. 재화
      - 재화는 몬스터를 잡으면 나온다.
  2. 몬스터
      - 60초 동안 일반 몬스터가 나오게 되며 시간이 다 지나게 되면 일반 몬스터는 리젠되지 않고 보스 몬스터가 나오게 된다.
      - 필드에 있는 모든 몬스터를 잡아야 다음 스테이지로 넘어가게 된다.
      - 모든 몬스터는 기본적으로 성을 공격하지만 캐릭터가 가까이 오게되면 플레이어를 공격할 수 있도록 해주었다.
  3. 플레이어 캐릭터
       - 화살표 키보드로 움직임, shift 시 달리기, z 시 공격 
       - 죽더라도 시간이 지나면 다시 생성
  4. 병사
        - 궁수: 단일 공격, 공격속도 빠름
        - 마법사: 범위 공격, 공격속도 느림
        - 병사를 사면 다음 병사를 사는 재화에 드는 양이 증가
        - 공격 effect에 데미지를 부과하는 방식을 통해 가장 가까운 몬스터에게 이펙트를 부여하는 방식으로 타겟팅 데미지 적용
  5. stage
        - stage는 2 stage가 끝
        - stage가 올라갈수록 몬스터 체력 및 데미지를 증가시켜 난이도 조절
        - 병력의 특성을 다르게 하여 전략적인 플레이 가능
        - 각각의 병력을 추가할 때마다 필요로 하는 재화 +100
        - 플레이어와 몬스터 모두 넉백이 있고 넉백 시 데미지가 들어가지 않도록 해주어 난이도 조절

#### 일시 정지
<img width="1027" alt="image" src="https://github.com/jeongdonggi/unityrouge/assets/100845304/b3cba4d9-3abd-480c-9263-f76e6174ed31">


## 실제 플레이 영상

<https://www.youtube.com/watch?v=orldVwRH0I4>