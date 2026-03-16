# Changelog

## [0.1.0] - 2026-03-16

### Added
- Initial release of CuteEngine Audio System
- **Core Audio Management**
  - Singleton-style `AudioManager` for centralized audio control
  - `AudioHandle` system for tracking and managing individual audio instances
  - `AudioUpdater` for automatic cleanup of one-shot audio clips
- **Playback Controls**
  - `Play(string name, bool loop)` - Play audio by name with optional looping
  - `Play(string id, string name, bool loop)` - Play audio with custom ID
  - `Pause(string id)` - Pause audio playback by handle ID
  - `Resume(string id)` - Resume paused audio
  - `Stop(string id)` - Stop and cleanup audio
- **Data-Driven Architecture**
  - `AudioData` ScriptableObject for audio clip configuration
  - `AudioDatabase` resource-based loading system
  - Support for multiple audio clips per AudioData entry
- **Performance Optimization**
  - AudioSource pooling system to reduce instantiation overhead
  - Automatic cleanup of completed one-shot sounds
  - DontDestroyOnLoad persistence across scene loads
- **Developer Tools**
  - `AudioLogger` for consistent audio system logging
  - Warning and error messages for debugging
  - Unique ID generation for audio instances

### Technical Details
- **Namespace**: `CuteEngine.Audio`
- **Unity Version**: 6000.3+
- **Resource Path**: `Resources/CuteEngine/Audio/AudioDatabase`
- **2D Audio Support**: All audio configured for 2D playback (spatialBlend = 0)

### Known Limitations
- Currently supports 2D audio only
- Random clip selection not yet implemented (returns first clip)
- No 3D spatial audio support
- No volume control or fade in/out
- No audio mixer integration
