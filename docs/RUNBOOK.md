# RUNBOOK

본 문서는 팀 공통 실행 절차와 작업 규칙을 정리한다.

---

## 1. Working Agreement

- Unity 버전은 `6000.3.10f1`로 고정한다.
- 플랫폼은 `Android`를 기준으로 작업한다.
- AR 관련 기본 스택은 `AR Foundation`을 사용한다.
- 큰 구조 변경, 외부 라이브러리 도입, owner 코드 수정은 사전 합의를 거친다.

## 2. Repository Start Checklist

- Unity Hub에서 `6000.3.10f1` 설치 확인
- Android Build Support 설치 확인
- 새 Unity 프로젝트 생성
- Version Control 설정:
  - Asset Serialization Mode = `Force Text`
  - Version Control Mode = `Visible Meta Files`
- 초기 폴더 구조 생성:
  - `Assets/Scenes`
  - `Assets/Scripts/Core`
  - `Assets/Scripts/Smile`
  - `Assets/Scripts/Garden`
  - `Assets/Scripts/AR`
  - `Assets/Scripts/UI`
  - `Assets/Prefabs`
  - `Assets/Art`
  - `Assets/Audio`

## 3. Scene Rules

- `MainScene`: 메인 UI, 정원 확인, 성장 진입
- `ARScene`: 카메라 활성화, 얼굴 인식, AR 배치
- 씬 추가가 필요하면 먼저 목적과 owner를 문서화한다.

## 4. Smile Detection Work Rules

- 담당 스크립트 기준:
  - `SmileDetection.cs`: 얼굴 입력 수집, 상태 전달
  - scorer/helper 클래스: 점수 계산, threshold 판정
- public 출력 표준:
  - `SmileScore` (`0.0 ~ 1.0`)
  - `IsSmiling` (`bool`)
- 실험성 판단 로직은 바로 메인 로직에 넣지 않고 `docs/EXPERIMENTS.md`에 먼저 기록한다.

## 5. Pre-Commit Checklist

- 변경 파일 owner 범위 확인
- Scene/Prefab 변경 시 영향 범위 메모
- 테스트 또는 수동 검증 결과 기록
- 새 결정이 있으면 `docs/DECISIONS.md` 업데이트

