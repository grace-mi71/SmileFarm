# AGENTS.md - AR Smile Garden Contract (Semester Frozen)

본 문서는 AR 팀 프로젝트의 학기 내 개발 계약 문서이다.
학기 중 임의 수정 금지. 변경 사항은 `docs/DECISIONS.md` 또는 RFC로 기록한다.

---

## 1) 필수 문서 읽기 순서
1. `AGENTS.md`
2. `PROJECTS.md`
3. `docs/RUNBOOK.md`
4. `docs/TASKS.md`
5. `docs/EXPERIMENTS.md`

---

## 2) 프로젝트 개요 (고정)

### 게임 이름
Smile Garden (가칭)

### 플랫폼
모바일 AR (Android, Unity AR Foundation)

### 장르
AR 힐링 성장 게임

### 핵심 컨셉
플레이어의 **웃음(표정 인식)**을 통해 경험치를 획득하고  
이를 기반으로 **AR 정원을 성장시키는 게임**

---

## 3) 역할 및 책임 (Ownership)

### In Scope (본 팀 공통 책임)
- AR 씬 구성 (Plane Detection, Face Detection)
- 웃음 인식 시스템 구현
- 정원 성장 로직
- UI / UX 구현
- 게임 루프 설계
- 사운드 및 인터랙션

### Out of Scope (금지 또는 제한)
- 과도한 구조 변경 (팀 합의 없는 리팩토링)
- 외부 라이브러리 무단 도입
- 팀원 소유 코드 무단 변경 (Owner 승인 없이)

---

## 4) 팀원별 Ownership 규칙

각 요소는 반드시 Owner 지정

### Scene
- MainScene (메인 UI + 정원)
- ARScene (카메라 + AR 배치)

### Prefab
- Plant (식물)
- Garden (정원)
- UI Components

### Script
- SmileDetection.cs
- GardenGrowth.cs
- ARPlacement.cs
- GameManager.cs
- UIManager.cs

규칙:
- Owner = 최종 책임자
- 수정 가능하지만 Owner 승인 필요

---

## 5) 기술 스택 및 버전 고정

- Unity: **6000.3.10f1**
- AR: **AR Foundation**
- 언어: **C#**
- 플랫폼: **Android**

---

## 6) AR 인터페이스 계약

### 카메라 모드

#### 전면 카메라 (Primary)
- 얼굴 인식
- 웃음 감지
- 경험치 생성

#### 후면 카메라 (Secondary)
- 현실 공간 인식
- 정원 AR 배치

---

### 입력 (Input)

- 얼굴 표정 데이터
- 웃음 여부 (Boolean or Score)
- 터치 입력 (Tap / Drag)

---

### 출력 (Output)

- 경험치 증가
- 정원 성장
- AR 오브젝트 배치
- UI 변화

---

## 7) 게임 루프 계약

```text
앱 실행
→ 메인 화면 (정원 확인)
→ 성장 버튼 클릭
→ 카메라 활성화
→ 웃음 감지
→ 경험치 획득
→ 정원 성장
→ 결과 확인
→ 반복