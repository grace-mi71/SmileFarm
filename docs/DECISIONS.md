# DECISIONS

본 문서는 학기 중 확정된 기술/운영 결정을 기록한다.
임의 변경 대신, 새 결정은 날짜와 함께 아래 형식으로 추가한다.

---

## 2026-03-17 Initial Repository And Unity Setup

### Status
Accepted

### Context
- 현재 저장소에는 Unity 프로젝트가 아직 생성되지 않았다.
- `AGENTS.md` 기준으로 학기 중 과도한 구조 변경은 피해야 한다.
- 팀 협업 시 Scene, Prefab, Script ownership 충돌을 줄일 필요가 있다.
- 웃음 인식 담당 작업은 빠른 실험과 안정적인 판정 로직 분리가 중요하다.

### Decision
- 저장소는 단일 Git repository + 단일 Unity project 구조로 운영한다.
- Unity 프로젝트 생성 전까지는 문서와 운영 규칙만 먼저 정리한다.
- Unity 프로젝트 생성 후 기본 추적 대상은 `Assets/`, `Packages/`, `ProjectSettings/`로 한다.
- Unity 협업 설정은 `Visible Meta Files`, `Force Text`를 기본으로 한다.
- Scene은 `MainScene`, `ARScene` 2개를 기준으로 유지한다.
- Script 폴더는 책임 기준으로 분리한다: `Core`, `Smile`, `Garden`, `AR`, `UI`.
- 웃음 인식 로직은 Unity/AR 입력 계층과 판정 계층을 분리한다.
- `SmileDetection.cs`는 입력 수집 담당, 실제 판정은 별도 scorer/helper 클래스로 분리한다.

### Consequences
- 초기 구조가 단순해져 팀원 온보딩이 쉬워진다.
- 대규모 리팩토링 없이 ownership 중심으로 작업을 나눌 수 있다.
- 웃음 인식은 Android AR 환경 제약에 따라 실험이 필요하므로 `docs/EXPERIMENTS.md`에서 추적한다.

