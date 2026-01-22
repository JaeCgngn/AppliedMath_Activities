# Copilot Instructions for AppliedMath_Activities

## Project Overview
AppliedMath_Activities is a **Unity 6.0.3 2D educational game project** demonstrating applied mathematics concepts through interactive gameplay mechanics. The codebase contains two major modules (W2 and W3) organized as separate scenes with distinct gameplay patterns.

## Architecture

### Project Structure
- **Assets/W2/** - Week 2 module: Circular shooter formation system with rocket-based projectiles and power-ups
- **Assets/W3/** - Week 3 module: Turret defense mechanics with detection and bullet systems
- **Assets/Scenes/** - W2.unity and W3.unity scene files (main entry points)
- **ProjectSettings/** - Unity engine configuration (URPSettings, Physics2D, Input system)
- **Packages/** - InputSystem as primary dependency for player input

### Key Components by Module

**W2 Shooter Formation System:**
- `ShooterManager` - Spawns shooters in circular formation using angle/radius math: `angle = index * (360 / count) + offset`
- `Shooting` - Fires rockets continuously via coroutine with configurable fireRate
- `Rocket` - Projectile that moves upward at constant speed, auto-destroys after lifetime
- `W2PlayerMovement` - WASD/arrow key movement with power-up collision detection
- `PowerUpSpawner` - Manages power-up objects (implementation incomplete)

**W3 Turret Defense System:**
- `Turret` - Rotates to face player using `Mathf.Atan2()` for angle calculation, includes detection range check
- `W3Shooting` - Similar to W2 Shooting but references separate firePoint transform
- `W3Bullet` - Distinct from Rocket system
- `PlayerMovement` - Simplified WASD movement (no power-up detection yet)

## Code Patterns & Conventions

### Unity MonoBehaviour Structure
- **[Header] attributes** organize inspector properties: `[Header("References")]`, `[Header("Movement Settings")]`
- **Separation of concerns**: Each class has single responsibility (movement, firing, spawning)
- **Public configuration**: Parameters exposed for scene tuning (speed, fireRate, radius, detection range)

### Movement & Physics
- **Frame-based movement**: `transform.position += direction * speed * Time.deltaTime` (not physics-based)
- **2D coordinate math**: Consistent use of `Vector3` with z=0 for 2D positioning
- **Angle conversions**: `Mathf.Deg2Rad` and `Mathf.Atan2(y, x)` for circular/rotation math

### Spawning & Instantiation
- **Prefab-based instantiation**: `Instantiate(prefab, position, rotation)` at fire points or formation positions
- **Automatic cleanup**: `Destroy(gameObject, lifetime)` for projectile lifecycle management
- **Transform parenting**: Formation shooters parented to manager for hierarchy organization

### Coroutine Patterns
- **Fire routines**: `StartCoroutine(FireRoutine())` with infinite loops and `yield return WaitForSeconds`
- **Debug logging**: Extensive `Debug.Log()` calls for gameplay events (fire start, spawning, collisions)

## Critical Workflows

### Adding New Gameplay Mechanics
1. Create MonoBehaviour script in Assets/WX/Scripts/
2. Use [Header] tags for inspector organization
3. Expose public float/int parameters for tunability
4. Implement Update() for frame-based logic or coroutines for timed events
5. Add appropriate Debug.Log statements for playtesting feedback

### Testing Changes
- Open appropriate scene (W2.unity or W3.unity) in Unity Editor
- Use Play button to test; monitor Console for Debug.Log output
- Adjust public parameters directly in Inspector during play for rapid iteration

### Prefab Management
- Prefabs stored in W2/W3 directories alongside scripts
- Scene references prefabs via public fields (bulletPrefab, shooterPrefab, etc.)
- Always assign in Inspector to avoid null reference errors

## Cross-Module Patterns

### Collision Detection
- **W2PlayerMovement** uses `OnTriggerEnter2D(Collider2D)` callbacks with layer masks
- Incomplete: PowerUp collection logic commented out, awaiting integration
- Follow this pattern for other collision-based interactions

### Configuration & Tunability
- Hard-coded values avoided; all gameplay parameters exposed as public fields
- Examples: `fireRate`, `moveSpeed`, `detectionRange`, `radius`, `shooterCount`
- This enables designer iteration without code recompilation

## Important Notes
- **Input System:** Uses legacy InputManager (KeyCode.W/A/S/D) - not modern InputSystem API yet
- **Incomplete Features:** PowerUps system partially stubbed; W3 turret detection body empty
- **No Singletons/Services:** Direct component references via public fields instead of manager patterns
- **No Physics Constraints:** Sprites move freely with no boundary checks or collider-based movement
