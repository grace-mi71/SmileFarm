# Smile Garden

AR Smile Garden team project repository.

## Overview

- Project name: `Smile Garden`
- Platform: `Android`
- Engine: `Unity 6000.3.10f1`
- Render pipeline: `URP`
- AR stack: `AR Foundation` + `Google ARCore XR Plugin`
- Core concept: detect the player's smile, convert it into experience, and grow an AR garden

## Repository Layout

```text
SmileGarden/
  AGENTS.md
  README.md
  .gitignore
  .gitattributes
  docs/
  SmileFarm/
```

- `docs/`: project decisions, runbook, tasks, and experiment notes
- `SmileFarm/`: actual Unity project

## Docs

- [AGENTS.md](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/AGENTS.md)
- [docs/DECISIONS.md](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/docs/DECISIONS.md)
- [docs/RUNBOOK.md](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/docs/RUNBOOK.md)
- [docs/TASKS.md](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/docs/TASKS.md)
- [docs/EXPERIMENTS.md](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/docs/EXPERIMENTS.md)

## Current Setup Status

The Unity project has been created and the initial AR project setup is in place.

Completed:
- Unity project created with `Universal 3D` template
- Android target selected
- Unity collaboration settings enabled
  - `Visible Meta Files`
  - `Force Text`
- AR packages installed
  - `com.unity.xr.management 4.5.4`
  - `com.unity.xr.arfoundation 6.3.3`
  - `com.unity.xr.arcore 6.3.3`
- Base scenes created
  - `MainScene`
  - `ARScene`
- `ARScene` includes the basic AR components
  - `AR Session`
  - `XR Origin (AR)`
  - `AR Camera Manager`
  - `AR Camera Background`
  - `AR Face Manager`
- Unity Git rules added at repo root
  - [`.gitignore`](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/.gitignore)
  - [`.gitattributes`](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/.gitattributes)

## Smile Detection Work

Current smile-recognition code skeleton:
- [SmileDetection.cs](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Assets/Scripts/Smile/SmileDetection.cs)
- [SmileScorer.cs](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Assets/Scripts/Smile/SmileScorer.cs)
- [SmileSample.cs](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Assets/Scripts/Smile/SmileSample.cs)
- [GameManager.cs](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Assets/Scripts/Core/GameManager.cs)
- [GardenGrowth.cs](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Assets/Scripts/Garden/GardenGrowth.cs)
- [UIManager.cs](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Assets/Scripts/UI/UIManager.cs)

Current responsibilities:
- `SmileDetection.cs`
  Receives AR face-tracking context and exposes normalized smile state
- `SmileScorer.cs`
  Contains the score calculation entry point
- `SmileSample.cs`
  Wraps score, percent, and threshold-based smile checks

Current public output standard:
- `SmileScore`: `0.0f ~ 1.0f`
- `SmilePercent`: `0 ~ 100`
- `IsSmiling`: threshold-based boolean

Note:
- Real Android smile scoring is not fully connected yet
- The current implementation includes an editor debug path so the score flow can be tested before device tuning

## Current Front Pipeline

The current editor-testable loop is:

```text
ARScene
-> SmileDetection
-> GameManager
-> GardenGrowth
-> UIManager debug overlay
```

What this currently means:
- `SmileDetection` outputs smile score, percent, and threshold state
- `GameManager` waits for the smile state to be held for a fixed time
- `GardenGrowth` receives experience and updates the current stage
- `UIManager` draws a simple debug overlay in play mode

This lets the team test the game loop in the Unity Editor using `debugScore` even before an Android device is available.

## Important Paths

- Unity project: [SmileFarm](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm)
- Scenes: [Assets/Scenes](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Assets/Scenes)
- Smile scripts: [Assets/Scripts/Smile](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Assets/Scripts/Smile)
- Project settings: [ProjectSettings](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/ProjectSettings)
- Package manifest: [Packages/manifest.json](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/SmileFarm/Packages/manifest.json)

## What To Commit

Commit:
- `docs/`
- `SmileFarm/Assets/`
- `SmileFarm/Packages/`
- `SmileFarm/ProjectSettings/`
- `.gitignore`
- `.gitattributes`
- `README.md`

Do not commit:
- `SmileFarm/Library/`
- `SmileFarm/Temp/`
- `SmileFarm/Logs/`
- `SmileFarm/UserSettings/`
- generated IDE files

## Next Steps

1. Attach `SmileDetection` to the face-tracking object in `ARScene`
2. Create one empty object such as `Managers` in `ARScene`
3. Attach `GameManager`, `GardenGrowth`, and `UIManager` to that object
4. Connect `ARFaceManager` reference in the Inspector
5. Build to an actual Android device
6. Start recording smile-detection experiments in [docs/EXPERIMENTS.md](C:/Users/MIN/Desktop/2026_1/IMP/SmileGarden/docs/EXPERIMENTS.md)
7. Replace placeholder score inputs with real AR face metrics
