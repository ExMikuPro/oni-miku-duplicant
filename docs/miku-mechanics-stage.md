# Miku Mechanics Stage

## Added in this stage

- Added a standalone Rider-friendly C# ONI mod project at `src/PrintMiku.Mechanics/`
- Added a solution file at `oni-miku-duplicant.Mechanics.sln`
- Configured the project to output `PrintMiku.Mechanics.dll` directly into `mod_folder/`
- Implemented Miku-only starting attribute and interest patches without editing Dupery or any art assets

## What is implemented now

The current patch layer only targets duplicants that match either:

- personality key `MIKU`
- displayed name `Hatsune Miku`

Implemented behavior:

- Starting attributes:
  - `Art` +9
  - `Machinery` +3
  - `Digging` -2
  - `Construction` -1
  - `Strength` -2
- Interests:
  - `Art`
  - `Technicals`

Implementation notes:

- `MinionStartingStats.GenerateAttributes` is patched to replace the generated starting level list for Miku.
- `MinionStartingStats.GenerateAptitudes` is patched to replace the generated aptitude collection for Miku.
- `MinionStartingStats.Apply` is patched as a spawn-time fallback so the final spawned duplicant also receives the expected attribute levels and aptitudes even if an upstream collection shape changes.

## Not implemented yet

Traits are intentionally not hard-coded in this stage.

The following trait concepts are still only reserved:

- Positive trait: `共鸣节拍`
- Negative trait: `声场敏感`

## ONI API points confirmed from local game assemblies

Confirmed from the local macOS ONI `Assembly-CSharp.dll` metadata:

- `MinionStartingStats`
  - fields: `Name`, `NameStringKey`, `StartingLevels`, `personality`, `skillAptitudes`
  - methods: `GenerateAttributes`, `GenerateAptitudes`, `Apply`, `ApplyAptitudes`
- `MinionResume`
  - method: `SetAptitude(skillGroupID, amount)`
- `Klei.AI.AttributeLevels`
  - method: `SetLevel(attribute_id, level)`
- `Database.Attributes`
  - fields: `Construction`, `Digging`, `Machinery`, `Strength`, `Art`
- `Database.SkillGroups`
  - fields: `Art`, `Technicals`
- `Personality`
  - fields: `nameStringKey`, `personalityType`
  - methods: `SetAttribute(attribute, value)`, `AddTrait(trait)`
- `Database.Personalities`
  - methods: `SetAttribute(personality, attribute_name, value)`, `AddTrait(personality, trait_name)`, `GetPersonalityFromNameStringKey(name_string_key)`

## ONI API points still needing confirmation before safe trait work

These are the remaining trait-related classes and patch points worth confirming in Rider before implementing custom traits:

- Trait definition / registration:
  - `Klei.AI.Trait`
  - `TraitUtil`
  - any runtime database entry point for adding a brand-new trait definition, not just attaching an existing trait ID
- Trait application flow:
  - `MinionStartingStats.GenerateTraits`
  - `MinionStartingStats.ApplyTraits`
  - `Klei.AI.Traits.Add(Trait trait)`
- Candidate DB mutation route:
  - `PersonalityLoader.Run`
  - `Database.Personalities.AddTrait(...)`

Open questions to verify:

- Where the base game stores the runtime trait database for new trait IDs
- Whether custom traits must be registered before personality loading or before minion spawn
- What localization keys and tooltip wiring a custom trait needs in this game version
- Whether the safest route is a brand-new trait definition or mapping to existing ONI traits first

## Rider build flow

Recommended:

1. Open `oni-miku-duplicant.Mechanics.sln` in Rider
2. Confirm the `OniManagedDir` property in `src/PrintMiku.Mechanics/PrintMiku.Mechanics.csproj`
3. Build the `Release` configuration

Expected output:

- DLL: `mod_folder/PrintMiku.Mechanics.dll`
- PDB: `mod_folder/PrintMiku.Mechanics.pdb`

## Local install and in-game test

1. Build the mechanics project in Rider
2. Run `./scripts/install_local_macos.sh`
3. Start ONI and enable both `Dupery` and this mod
4. Open a save with printing pod access
5. Check whether `Hatsune Miku` appears as a printable duplicant
6. Inspect the candidate or spawned duplicant for:
   - art +9
   - machinery +3
   - digging -2
   - construction -1
   - strength -2
   - art + technical interests
7. If the candidate card does not show the interests correctly but the spawned duplicant does, inspect the runtime shape of `skillAptitudes` in Rider and adjust the `GenerateAptitudes` patch first
