# Day1.

## 1. 진행 및 완료 사항
- **노트**: 양쪽에서 생성되어 중앙 심장으로 향한다.
- **심장**: 노트들이 부근에 오면 입력을 유효하도록 한다.
  - 입력 성공 범위: `[(하트, 노트) 트리거 발생 시작, (노트, 노트) 트리거 발생 종료]`
  
## 2. 이슈 / 고민
> 노트의 이동을 RectTransfrom의 PosX 값을 중앙 심장을 향하도록 변경 시도
  - Anchor와 Pivot을 조절해두었기 때문에 알맞게 이동을 표현하기 어려웠던 것으로 판단
  - 우측 sender의 노트는 좌측으로, 좌측 sender의 노트는 오른쪽으로 진행하는 방법으로 대체
    - 각 노트의 PosX를 직접 가감