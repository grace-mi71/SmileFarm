# Smile Garden

웃음을 경험치로 바꾸고, 그 경험치로 AR 정원을 성장시키는 Android 기반 Unity 프로젝트입니다.

## 프로젝트 개요

- 프로젝트명: `Smile Garden`
- 플랫폼: `Android`
- 엔진: `Unity 6000.3.10f1`
- 렌더 파이프라인: `URP`
- AR 스택: `AR Foundation` + `Google ARCore XR Plugin`
- 핵심 컨셉: 플레이어의 웃음을 감지해 경험치를 얻고, 그 결과로 AR 정원을 성장시킨다

## 저장소 구조

```text
SmileGarden/
  AGENTS.md
  README.md
  .gitignore
  .gitattributes
  .github/
    workflows/
  docs/
  SmileFarm/
```

- `docs/`: 의사결정, 실행 절차, 작업 목록, 실험 기록
- `SmileFarm/`: 실제 Unity 프로젝트
- `.github/workflows/`: GitHub Actions 자동화

## 문서

- [AGENTS.md](./AGENTS.md)
- [DECISIONS.md](./docs/DECISIONS.md)
- [RUNBOOK.md](./docs/RUNBOOK.md)
- [TASKS.md](./docs/TASKS.md)
- [EXPERIMENTS.md](./docs/EXPERIMENTS.md)

## 현재 세팅 상태

현재까지 완료된 기본 세팅은 아래와 같습니다.

- Unity 프로젝트 생성 완료
  - 템플릿: `Universal 3D`
- Android 타깃 전환 완료
- 협업용 Unity 설정 완료
  - `Visible Meta Files`
  - `Force Text`
- AR 패키지 설치 완료
  - `com.unity.xr.management 4.5.4`
  - `com.unity.xr.arfoundation 6.3.3`
  - `com.unity.xr.arcore 6.3.3`
- 기본 씬 생성 완료
  - `MainScene`
  - `ARScene`
- `ARScene` 기본 AR 구성 완료
  - `AR Session`
  - `XR Origin (AR)`
  - `AR Camera Manager`
  - `AR Camera Background`
  - `AR Face Manager`
- Unity 협업용 Git 설정 추가 완료
  - `.gitignore`
  - `.gitattributes`

## 현재 구현된 앞 파이프라인

실기기 없이도 Unity Editor에서 테스트 가능한 최소 흐름은 만들어진 상태입니다.

```text
ARScene
-> SmileDetection
-> GameManager
-> GardenGrowth
-> UIManager Debug Overlay
```

현재 가능한 것:
- `SmileDetection`이 웃음 점수와 상태를 출력
- `GameManager`가 웃음 유지 시간을 누적
- `GardenGrowth`가 경험치와 성장 단계를 관리
- `UIManager`가 디버그 정보를 화면에 표시
- `debugScore`를 이용해 에디터에서 흐름 검증 가능

현재 아직 안 된 것:
- Android 실기기에서 실제 얼굴 추적 확인
- 실제 얼굴 데이터 기반 `SmileScore` 계산
- 실기기 기준 웃음 threshold 튜닝

## 주요 스크립트

- `SmileFarm/Assets/Scripts/Smile/SmileDetection.cs`
- `SmileFarm/Assets/Scripts/Smile/SmileScorer.cs`
- `SmileFarm/Assets/Scripts/Smile/SmileSample.cs`
- `SmileFarm/Assets/Scripts/Core/GameManager.cs`
- `SmileFarm/Assets/Scripts/Garden/GardenGrowth.cs`
- `SmileFarm/Assets/Scripts/UI/UIManager.cs`

## 현재 디버그 출력 규격

- `SmileScore`: `0.0f ~ 1.0f`
- `SmilePercent`: `0 ~ 100`
- `IsSmiling`: threshold 기반 `bool`
- `HasTrackedFace`: 실제 얼굴 추적 여부

## 커밋할 파일

커밋 대상:
- `docs/`
- `SmileFarm/Assets/`
- `SmileFarm/Packages/`
- `SmileFarm/ProjectSettings/`
- `.github/workflows/`
- `.gitignore`
- `.gitattributes`
- `README.md`

커밋 제외:
- `SmileFarm/Library/`
- `SmileFarm/Temp/`
- `SmileFarm/Logs/`
- `SmileFarm/UserSettings/`
- IDE 생성 파일

## Discord 알림 설정

현재 저장소에는 push 알림을 Discord로 보내는 GitHub Actions 워크플로우가 포함되어 있습니다.

워크플로우 파일:
- [discord-push-notify.yml](./.github/workflows/discord-push-notify.yml)

동작 방식:
- GitHub에 `push`가 발생하면 실행
- 저장소 secret에 등록된 Discord webhook으로 메시지 전송
- webhook이 없으면 워크플로우는 조용히 종료

설정 방법:
1. Discord 채널 설정에서 `Webhook` 생성
2. GitHub 저장소에서 `Settings > Secrets and variables > Actions` 이동
3. `New repository secret` 클릭
4. 이름을 `DISCORD_WEBHOOK_URL`로 설정
5. 값에 Discord webhook URL 입력
6. 이후 `git push` 시 Discord 알림 확인

## 다음 우선순위

현재 가장 중요한 다음 단계는 아래입니다.

1. Android 실기기에서 얼굴 추적이 실제로 잡히는지 확인
2. 얼굴 데이터가 들어오는지 확인
3. 그 데이터를 이용해 웃음 점수 계산 로직 연결
4. 실험 결과를 [EXPERIMENTS.md](./docs/EXPERIMENTS.md)에 기록

