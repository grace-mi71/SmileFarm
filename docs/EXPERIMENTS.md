# EXPERIMENTS

본 문서는 웃음 인식 및 AR 관련 실험 결과를 기록한다.
확정 전 로직은 이 문서에 먼저 남기고, 채택 시 `docs/DECISIONS.md`로 승격한다.

---

## Experiment Log Template

### Title

-

### Date

-

### Owner

-

### Goal

-

### Setup

-

### Result

-

### Decision

- Keep testing / Adopt / Reject

---

## Planned Smile Detection Experiments

### 1. Input Source Feasibility

#### Goal
- Android + AR Foundation 환경에서 웃음 판정에 사용할 수 있는 얼굴 데이터 종류를 확인한다.

#### Questions
- blend shape 계수가 직접 제공되는가?
- landmark 또는 face mesh 기반 계산이 필요한가?
- 프레임 안정성은 어느 정도인가?

### 2. Smile Score Heuristic

#### Goal
- 웃음 여부를 `bool`만이 아니라 연속 점수로 계산할 수 있는지 검증한다.

#### Candidate Signals
- 입꼬리 상승 정도
- 입 벌어짐 비율
- 볼 변화량 또는 보조 얼굴 특징

### 3. Threshold Stability

#### Goal
- 조명, 거리, 정면 여부에 따라 오검출/미검출이 얼마나 생기는지 확인한다.

#### Record
- 테스트 환경
- 성공 케이스
- 실패 케이스
- 임계값 조정안

