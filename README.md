# Smile Garden

> 웃음을 모아 꽃을 피우고, AR 정원을 성장시키는 모바일 힐링 게임

`Smile Garden`은 플레이어의 미소를 실시간으로 감지해 경험치로 바꾸고, 그 보상으로 꽃과 정원을 성장시키는 Android 기반 Unity 프로젝트입니다.  
얼굴 위에 피어나는 꽃 연출과 메인 정원의 성장, 레벨 시스템, 상점 커스터마이징을 하나의 플레이 흐름으로 연결한 것이 이 프로젝트의 핵심입니다.

## 프로젝트 소개

Smile Garden은 "웃으면 성장한다"는 직관적인 컨셉을 바탕으로 만들어진 AR 게임입니다.  
플레이어가 카메라 앞에서 웃음을 유지하면 점수가 쌓이고, 일정 조건을 만족할 때마다 경험치가 지급됩니다. 그렇게 모인 경험치는 AR 씬의 꽃 성장과 메인 메뉴의 플레이어 성장에 반영됩니다.

한마디로 정리하면:

- 웃음을 입력으로 사용하는 AR 성장형 게임
- 얼굴 추적과 꽃 연출을 결합한 감성형 플레이 경험
- 메인 메뉴, 상점, 사운드, 로딩까지 포함된 앱 구조 완성

## 주요 기능

### 실시간 웃음 감지

- `AR Foundation` 기반 얼굴 추적
- 입 너비, 입 벌어짐, 입꼬리 상승량을 이용한 웃음 점수 계산
- 점수 스무딩 처리로 흔들림 완화
- Unity Editor에서 테스트할 수 있도록 디버그 점수 입력 지원

### AR 꽃 성장 시스템

- 웃음을 일정 시간 유지하면 경험치 획득
- 누적 경험치에 따라 성장 단계 상승
- 얼굴 위 꽃 오브젝트가 단계별로 활성화되어 성장 상태를 시각적으로 표현

### 메인 메뉴 진행 구조

- 플레이어 레벨, 경험치, 재화 관리
- AR 세션 결과를 메인 메뉴 상태로 전달
- 프로필, 상점, 설정 패널 구성

### 연출과 피드백

- 꽃 색상 변경 기능
- 버튼 클릭음, 배경음, 성장 효과음 재생
- 로딩 씬과 UI 애니메이션 포함

## 플레이 흐름

```text
Title
 -> MainMenu
 -> ARScene
 -> Smile Detection
 -> Experience Reward
 -> Flower Growth
 -> MainMenu Progress Update
```

플레이어는 타이틀 화면에서 시작한 뒤 메인 메뉴를 거쳐 AR 씬으로 진입합니다.  
AR 씬에서는 얼굴 추적과 웃음 점수 계산이 이루어지고, 웃음을 유지하면 경험치가 쌓입니다. 이 결과는 다시 메인 메뉴의 성장 정보와 연결됩니다.

## 씬 구성

현재 프로젝트에 포함된 주요 씬은 아래와 같습니다.

- `Title`
- `LoadingScene`
- `MainMenu`
- `ARScene`

테스트용 씬:

- `3dmodelTest`

## 기술 스택

- Engine: `Unity 6000.3.10f1`
- Platform: `Android`
- Rendering: `Universal Render Pipeline`
- AR: `AR Foundation 6.3.3`
- Device Support: `Google ARCore XR Plugin 6.3.3`
- XR Management: `4.5.4`
- Input: `Unity Input System 1.18.0`

## 프로젝트 구조

```text
SmileGarden/
  README.md
  SmileFarm/
    Assets/
      Scenes/
      Scripts/
    Packages/
    ProjectSettings/
  .github/
    workflows/
  SmileFarm.apk
```

## 핵심 스크립트

### Smile

- `SmileFarm/Assets/Scripts/Smile/SmileDetection.cs`
- `SmileFarm/Assets/Scripts/Smile/SmileFaceMeshEstimator.cs`
- `SmileFarm/Assets/Scripts/Smile/SmileScorer.cs`
- `SmileFarm/Assets/Scripts/Smile/SmileMetrics.cs`

### Core / Garden

- `SmileFarm/Assets/Scripts/Core/GameManager.cs`
- `SmileFarm/Assets/Scripts/Garden/GardenGrowth.cs`
- `SmileFarm/Assets/Scripts/Garden/FaceFlowerAnchor.cs`
- `SmileFarm/Assets/Scripts/Garden/FaceFlowerStageView.cs`

### Main Menu / UI

- `SmileFarm/Assets/Scripts/MainMenu/PlayerManager.cs`
- `SmileFarm/Assets/Scripts/MainMenu/MainMenuUIManager.cs`
- `SmileFarm/Assets/Scripts/MainMenu/SmileSessionTransfer.cs`
- `SmileFarm/Assets/Scripts/UI/UIManager.cs`

### Audio / Shop / Loading

- `SmileFarm/Assets/Scripts/Audio/SoundManager.cs`
- `SmileFarm/Assets/Scripts/Audio/BGMManager.cs`
- `SmileFarm/Assets/Scripts/Audio/GrowthSoundController.cs`
- `SmileFarm/Assets/Scripts/Shop/ShopColorManager.cs`
- `SmileFarm/Assets/Scripts/Load/LoadingManager.cs`

## 실행 방법

### 개발 환경

- `Unity 6000.3.10f1`
- ARCore 지원 Android 기기 권장

### 프로젝트 열기

1. Unity Hub에서 `SmileFarm` 폴더를 엽니다.
2. Unity 버전을 `6000.3.10f1`로 맞춥니다.
3. Build Settings에서 `Title`, `LoadingScene`, `MainMenu`, `ARScene`이 포함되어 있는지 확인합니다.
4. Android로 빌드하거나 에디터에서 기본 흐름을 확인합니다.

### 에디터 테스트

실기기 없이도 일부 흐름은 확인할 수 있습니다.

- `SmileDetection`의 디버그 점수로 웃음 상태 테스트
- `GameManager`의 보상 루프 테스트
- `GardenGrowth`의 단계 변화 및 UI 반영 확인

실제 얼굴 추적 정확도와 AR 위치 보정은 Android 실기기 테스트가 필요합니다.

## 현재 상태

현재 프로젝트는 단순 프로토타입을 넘어서 아래 흐름이 연결된 상태입니다.

- 웃음 감지
- 경험치 보상
- 꽃 성장 단계 변화
- 메인 메뉴 성장 반영
- 상점 색상 변경
- 사운드 및 UI 연출
- APK 빌드 산출

루트 경로에는 빌드 결과물인 `SmileFarm.apk`가 포함되어 있습니다.

## 한 줄 요약

Smile Garden은 플레이어의 웃음을 입력으로 삼아, 얼굴 위 꽃과 정원의 성장을 만들어내는 Unity 기반 AR 힐링 게임 프로젝트입니다.
