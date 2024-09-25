# UnityMiniProject
 유니티 미니 프로젝트

## 구현 계획
[레퍼런스 게임: Crypt of the NecroDancer](https://store.steampowered.com/app/247080/Crypt_of_the_NecroDancer/?l=koreana)

- 타이밍
  - 표시 UI
    - 오브젝트 풀
  - 입력 판정
  - bpm에 따라 노트 생성 및 속도 조절

- 캐릭터
  - 조작
    - 이동
    - 공격

- 몬스터
  - 일반 몬스터
    - 행동 패턴
      - 플레이어 추적
      - 공격
  - 보스 몬스터
    - 행동 패턴
    - 스킬 패턴

- 맵
  - 상호작용 X
    - 고정 바닥
    - 고정 벽
  - 상호작용 O
    - 부서지는 벽
    - 이동 통로

- 아이템
  - 장비

- 애니메이션
  - UI
  - 캐릭터
  - 몬스터
  - 맵

- UI
  - 미니맵
  - 타이밍
  - 체력

- 컷신


## 구현 방법
### 타이밍
- **노트** \
노트가 양쪽에서 생성되어 중앙을 향하도록,\
양 쪽의 짝꿍 노트는 같은 타이밍에 생성되므로 오른쪽에서 생성된 노트만 판정을 갖도록 하자.
  1. 중앙 하트를 향해 이동
     - Anchor 및 Pivot을 조절하다 보니 Rect Transform의 PosX가 각 오브젝트에 맞게 설정돼 어려움
  2. 오른쪽 센더의 노트는 왼쪽으로, 왼쪽 센더의 노트는 오른쪽으로 이동
     - 불변수 `isRight` 이용해 구분

