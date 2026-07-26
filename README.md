# Handy Magic

A Unity game where you cast spells with hand gestures. A separate hand-tracking source streams 21-point landmark data per hand over UDP; Unity turns specific hand shapes into spells and throws them at an AI-controlled enemy.

This was presented at the 2024 University of Santo Tomas Computer Science Research Colloquium. It won first place in the Best Thesis category in the game development track.

## Requirements

- Unity **2020.3.30f1** (see `ProjectSettings/ProjectVersion.txt`)
- An external hand-tracking sender that streams comma-separated hand landmark
  data (126 floats: 21 points × 3 axes × 2 hands) to UDP port `5052`. This
  script is not part of the repository.

## Getting Started

1. Open the project root in Unity Hub with editor version `2020.3.30f1`.
2. Open the `Menu` scene under `Assets/Scenes/Menu.unity`.
3. Press Play. From the main menu you can start:
   - **Tutorial** — a guided walkthrough of gestures and controls.
   - **Gamemode 1** / **Gamemode 2** — combat encounters against an AI enemy.
4. If you don't have a hand-tracking sender running, keyboard keybinds
   (A/B/X/Y/Z) can be used instead to trigger spells for testing.

## How It Works

- **Hand tracking** (`Assets/Handtracker`) — `UDPReceive` listens on port
  `5052` for landmark data broadcast by an external tracker; `HandTracking`
  parses the CSV payload into 3D positions for 21 landmarks per hand and
  drives placeholder hand-point objects in the scene.
- **Gesture recognition** (`Assets/HandGestures`) — scripts (`Gesture_A`,
  `Gesture_B`, `Gesture_X`, `Gesture_Y`, `Gesture_Z`) watch the distance
  between specific hand landmarks to detect a matching pose and report it as
  a string output (e.g. `"Activate[1]"`).
- **Keybinds** (`Assets/Keybinds`) — keyboard equivalents (A/B/X/Y/Z) of the
  gestures above, useful when testing without a camera/tracker.
- **Expert system** (`Assets/ExpertSystem`) — `OutputListener` (gesture-driven)
  and `OutputListener(Keybinds)` (keyboard-driven) combine gesture/keybind
  outputs into spell combinations (Fireball, Blast, Bolt, Heal, Block,
  Counter, Thorns, Weaken, ATK Up, DEF Up), applying mana costs, cooldowns,
  and ultimate-meter gain, then spawning the corresponding VFX prefab.
- **Combat stats** (`Assets/Character`, `Assets/Enemy`) — `Char_Stats` and
  `Enemy_Stats` track HP/mana/ultimate/defense, apply damage from spell
  collisions, and handle buffs like Thorns and Weaken.
- **Enemy AI** (`Assets/Enemy/EnemySpells.cs`) — casts spells against the
  player on its own logic/cooldowns.
- **Navigation & UI** (`Assets/Navigation`) — `Menu`, `Gamemode1`,
  `Gamemode2`, and `TutorialLevel` manage scene loading, pause menus, and
  in-game guide/spellbook panels.
- **Win/Lose** (`Assets/WinLose`) — `WinLoseBanner` shows the outcome banner
  and plays victory/defeat audio when a match ends.
- **Spell history & timer** (`Assets/SpellHistory`, `Assets/Timer`) — on-screen
  log of recently cast spells and a match countdown/timer.

## Project Structure

```
Assets/
  Character/        Player stats (HP, mana, ultimate, defense)
  Enemy/             Enemy stats, AI spellcasting, enemy models/animations
  ExpertSystem/      Core spellcasting logic (gesture/keybind -> spell effect)
  HandGestures/      Gesture detection from hand landmark positions
  Handtracker/       UDP receiver + landmark-to-transform mapping
  Keybinds/          Keyboard fallback input for each gesture/spell
  Navigation/        Menu, gamemode controllers, tutorial flow
  SpellHistory/      UI log of cast spells
  Timer/             Match countdown timer
  WinLose/           Victory/defeat banner and audio
  Scenes/            Menu, Tutorial, Gamemode 1, Gamemode 2
  Materials/, Images/, sound FX/, Hand Animatons/, Animations/
                     Art, audio, and animation assets
  DigitalArtistJZ/, SimpleNaturePack/, LowPolyDungeonsLite/, Hovl Studio/
                     Third-party asset packs (environment props, nature
                     assets, dungeon set pieces, magic VFX)
ProjectSettings/     Unity project configuration
Packages/            Unity Package Manager manifest and lockfile
```
