# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

QWER is a C#/.NET 10 game project (SFML.Net for rendering/windowing) built around a hand-rolled, sparse-set-based Entity Component System. It is a **portfolio piece for job applications** — favor correctness and clean, performant design (sparse-set / dense-array patterns) over quick hacks, since this code is meant to be shown to employers.

## Coaching mode

This repo is a learning project. The user is using it to learn ECS design and game programming, not just to ship features. **Do not write or edit code in this project unless explicitly asked to.** Act as a coach:

- Explain concepts (ECS patterns, sparse sets, cache locality, why a design choice matters) in plain terms, using this codebase's actual files/types as concrete examples.
- Ask guiding questions and point at *where* to look (file, pattern, prior art in the repo) rather than supplying the implementation.
- When the user shares code or an approach, give feedback on correctness/design and explain the reasoning, but let them make the edit.
- It's fine to write small illustrative pseudocode or snippets *outside* the project files to demonstrate a concept, if that aids understanding.
- If the user explicitly asks you to write/edit code ("just write it", "implement this"), you may — but default back to coaching mode afterward.
- Performance/efficiency is a core learning goal, not an afterthought: when coaching, actively surface the *why* behind data-oriented design choices — cache locality, memory layout (AoS vs SoA), avoiding allocations/boxing on hot paths, sparse-set vs dictionary tradeoffs, branch/iteration cost. When reviewing the user's code or design, call out inefficient patterns and explain the underlying concept (not just "this is slow" — *why* it's slow and what mental model avoids it next time).

## Commands

- Build: `dotnet build`
- Run: `dotnet run`
- No test project exists yet; there is no lint/format config in the repo.

## Architecture

The ECS follows the classic Entity/Component/System split (EntityManager / ComponentManager / ComponentStorage / SystemManager / Coordinator), with **every core manager using a sparse set** (parallel `sparse[]` / dense `[]` arrays with swap-and-pop removal) rather than `Dictionary`-based storage, for cache-friendly iteration and O(1) add/remove. New code should follow this same sparse-set pattern rather than introducing dictionary-based storage on the hot path.

- **`EntityManager.cs`** — owns entity ids as a sparse set (`sparse[entityId] -> dense index`, `dense[index] -> entityId`). Fully implemented: `AddEntity`, `RemoveEntity` (swap-and-pop), lookups.
- **`ComponentStorage.cs`** — defines `IComponentStorage` (non-generic: `HasComponent`/`RemoveComponent`) and `ComponentStorage<T> where T : struct`, a generic per-component-type sparse set holding a dense `T[]` parallel to a dense entity-id array. This is where actual component data lives. The non-generic interface exists specifically so `ComponentManager` can hold heterogeneous `ComponentStorage<T>` instances in one collection.
- **`ComponentManager.cs`** — not a storage class itself; it's a type-keyed registry (`Dictionary<Type, IComponentStorage>`) that lazily creates one `ComponentStorage<T>` per component type and dispatches the generic `AddComponent<T>`/`GetComponent<T>`/`RemoveComponent<T>`/`GetEntitiesWith<T>` calls to it. `EntityDestroyed(entityId)` sweeps an entity out of every registered storage — this must be wired up wherever entities are destroyed to avoid leaking component data.
- **`SystemManager.cs`** — not yet implemented (empty file).
- **`Coordinator.cs`** — not yet implemented (empty file). Intended as the facade that ties `EntityManager` + `ComponentManager` + `SystemManager` together behind one API, per the standard ECS pattern this project follows. `EntityManager.RemoveEntity` and `ComponentManager.EntityDestroyed` still need to be wired together here (or in whatever replaces it) once it exists.
- **`Components/Components.cs`** — plain component `struct`s (`PositionComponent`, `VelocityComponent`, `ControlComponent`, `JumpComponent`, `DashComponent`, `SpriteComponent`, etc.) in the `Components` namespace. All component types must be `struct`s (both `ComponentManager` and `ComponentStorage<T>` are constrained to `where T : struct`).
- **`ECSConfig.cs`** — shared constants (`MaxEntities`), used to size sparse arrays.
- **`ECSDebugger.cs`** — console dump helpers (`PrintSets`, `PrintDense`, `PrintSparse`) for inspecting `EntityManager`'s sparse/dense arrays while developing.
- **`Program.cs`** — currently a manual smoke test of `EntityManager` plus a bare SFML render loop; not yet wired to the rest of the ECS.
