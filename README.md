# Rhythm Surfer

Rhythm Surfer is a 3D audio-reactive rhythm runner game prototype built with Unity. The player navigates a character on a surfboard along the interior of a cylindrical tunnel track, rotating around the perimeter to pass through matching color obstacle gates generated dynamically from real-time music frequency analysis, while dodging mismatching colors and collecting crystals. This project is a personal prototype and demo, not a maintained product, with last substantial work completed in 2022.

## Gameplay

- **Tunnel rotation controls** — rotate player around the cylindrical track using Left/Right Arrow keys or touch swipe deltas ([`Assets/Scripts/Player/PlayerMovement.cs`](Assets/Scripts/Player/PlayerMovement.cs)).
- **Real-time audio spectrum analysis** — samples audio data into 512 spectrum bins across 8 frequency bands with buffering and highest-band tracking ([`Assets/Scripts/Audio/AudioPeer.cs`](Assets/Scripts/Audio/AudioPeer.cs)).
- **Beat-synchronized obstacle spawning** — instantiates obstacle gates ahead of the player when audio frequency bands exceed peak thresholds ([`Assets/Scripts/Audio/SpawnerWithAudio.cs`](Assets/Scripts/Audio/SpawnerWithAudio.cs)).
- **Color matching and score mechanics** — passing obstacles matching player color awards or modifies score (normal, chance, multiplier, division, plus, minus), while mismatching triggers failure ([`Assets/Scripts/Obstacle.cs`](Assets/Scripts/Obstacle.cs), [`Assets/Scripts/GameManager.cs`](Assets/Scripts/GameManager.cs)).
- **Dynamic color transition gates** — periodically spawns color-shift obstacles that update player material, particle colors, and target obstacle colors ([`Assets/Scripts/ChangeColorObs.cs`](Assets/Scripts/ChangeColorObs.cs), [`Assets/Scripts/GameManager.cs`](Assets/Scripts/GameManager.cs)).
- **Track completion crystal rewards** — detects when the active audio track finishes playing and spawns collectable score crystals ([`Assets/Scripts/CristalSpawner.cs`](Assets/Scripts/CristalSpawner.cs), [`Assets/Scripts/Cristal.cs`](Assets/Scripts/Cristal.cs)).
- **Audio-reactive visualizers** — scales background geometry and modulates emission colors in sync with frequency band buffers ([`Assets/Scripts/Audio/AudioCube.cs`](Assets/Scripts/Audio/AudioCube.cs), [`Assets/Scripts/Audio/AudioCubeScale.cs`](Assets/Scripts/Audio/AudioCubeScale.cs)).
- **UI and session management** — displays score tally, star ratings, health bar, key inventory, and pause menu overlay ([`Assets/Scripts/UI/ScorePanel.cs`](Assets/Scripts/UI/ScorePanel.cs), [`Assets/Scripts/UI/HpPanel.cs`](Assets/Scripts/UI/HpPanel.cs), [`Assets/Scripts/UI/KeyPanel.cs`](Assets/Scripts/UI/KeyPanel.cs), [`Assets/Scripts/UI/PausePanel.cs`](Assets/Scripts/UI/PausePanel.cs), [`Assets/Scripts/UI/ProgressSlider.cs`](Assets/Scripts/UI/ProgressSlider.cs)).
- **Target frame rate configuration** — enforces target frame rate of 60 FPS at application startup ([`Assets/Scripts/Base Scripts/Options.cs`](Assets/Scripts/Base Scripts/Options.cs)).

## Tech Stack

- **Engine version**: Unity 2020.3.20f1 (`ProjectSettings/ProjectVersion.txt`)
- **Primary language**: C# (.NET / Mono)
- **Dependencies** (`Packages/manifest.json`):
  - `com.unity.textmeshpro`: 3.0.6
  - `com.unity.timeline`: 1.4.8
  - `com.unity.ugui`: 1.0.0
  - `com.unity.collab-proxy`: 1.11.2
  - `com.unity.ide.visualstudio`: 2.0.11 / `com.unity.ide.rider`: 2.0.7 / `com.unity.ide.vscode`: 1.2.4
  - Built-in Unity modules (Audio, Physics, ParticleSystem, Animation, UI)

## Project Structure

| Path | Contents / Purpose | Type |
| --- | --- | --- |
| `Assets/Scripts/` | Gameplay loop, player controller, audio analysis, obstacle logic, and UI scripts | Own code |
| `Assets/Scenes/` | Unity scenes (`Gameplay.unity` functional scene, `Menu.unity` UI mockup) | Own code / Data |
| `Assets/ScriptableObjects/` | ScriptableObject definitions and asset instances for songs, prefabs, materials, and colors | Own code / Data |
| `Assets/Prefabs/` | Obstacle rings, background visualizer cubes, and crystal prefabs | Own code / Data |
| `Assets/Models/` | 3D models for character skateboarder, surfboard, circular track, keys, and crystals | Own code / Third-party |
| `Assets/Materials/` | Color materials and custom vertex color alpha shader | Own code |
| `Assets/Animations/` | Animation controller for character skateboarding animations | Own code |
| `Assets/Music Tracks/` | Audio tracks used for gameplay and spectrum analysis | Third-party audio |
| `Assets/Sounds/` | Sound effects for collect events and UI feedback | Third-party audio |
| `Assets/İcons/` | UI sprite icons for buttons, pause, stars, and diamonds | Third-party / Data |
| `Assets/Sci-Fi UI/` | Sci-Fi GUI skin asset pack with UI elements and demo scene | Third-party asset |
| `Assets/SimpleFX/` | Particle effect prefabs, materials, and demo scene | Third-party asset |
| `Assets/TextMesh Pro/` | TextMesh Pro fonts, resources, and shaders | Third-party asset |
| `Assets/VFX/` | Custom particle effect prefabs for tire smoke and fireworks | Own code / Data |
| `Packages/` | Unity package manifest declaring package dependencies | Configuration |
| `ProjectSettings/` | Unity project configuration files (editor, input, audio, tags) | Configuration |

## Running / Building

### Unity Editor
1. Install **Unity 2020.3.20f1** via Unity Hub.
2. Add and open the project directory in Unity Hub.
3. In the Project window, navigate to `Assets/Scenes/Gameplay.unity` and open it. *(Note: `ProjectSettings/EditorBuildSettings.asset` has an empty scene list, so the scene must be opened manually).*
4. Press the **Play** button in the Unity Editor toolbar.
5. Controls:
   - **Left Arrow / Right Arrow**: Rotate around the tunnel perimeter.
   - **Mouse Click & Drag**: Simulates touch input to steer.
6. *(Optional)* A secondary scene `Assets/Scenes/Menu.unity` contains UI layout elements, but is not linked to scene-transition logic in the code.

### Building
1. In the Unity Editor, open **File > Build Settings...**.
2. Click **Add Open Scenes** while `Assets/Scenes/Gameplay.unity` is open (or drag `Assets/Scenes/Gameplay.unity` into the Scenes in Build list).
3. Select the desired platform (PC, Mac & Linux Standalone or Android).
4. Click **Build** and choose an output destination.

*(Note: The build and runtime behavior has not been executed or verified in this environment, as Unity is not installed.)*

## Repository Notes

- **Automated tests**: None. Although `com.unity.test-framework` is listed in `Packages/manifest.json`, no test assemblies or test scripts exist in `Assets/`.
- **CI/CD pipelines**: None. No `.github` workflows or automated build scripts are present.
- **Commit history**: A single commit (`a1e4ced`) containing the initial project import.
- **Project status**: Learning prototype developed in September 2022 to experiment with audio spectrum analysis, procedural beat-based obstacle generation, and circular tunnel movement.

## Third-Party Assets

| Folder Path | What it provides | Licence file in repo |
| --- | --- | --- |
| `Assets/Sci-Fi UI/` | Sci-Fi GUI skin textures, fonts, and UI assets | No licence file present |
| `Assets/SimpleFX/` | Cartoon particle models, materials, and prefabs | No licence file present |
| `Assets/TextMesh Pro/` | TextMesh Pro package fonts and assets | Yes (`Assets/TextMesh Pro/Fonts/LiberationSans - OFL.txt`, `EmojiOne Attribution.txt`) |
| `Assets/Music Tracks/` | MP3 audio tracks (NCS releases and popular tracks) | No licence file present |
| `Assets/Sounds/` | Sound effect WAV files | Yes (`Assets/Sounds/license.pdf`) |
| `Assets/İcons/` | UI icon PNG files | No licence file present |
