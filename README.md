# SnakeGameAlgorithms
 
Nelson Kiyoshi Kossuga
A 3D snake game with apple-powered-powerups and flying rocks in an asteroid fashion.

Patterns:
- Observer, used:
 1. in `BuffClockManager.cs` with `TrackableFloat.cs` being the value observed in the UI;
 2. in `BaseGameEventListener.cs`, `BaseGameEvent.cs` and `IGameEventListener.cs` as a system to manually send events between objects; 
- Factory Method, used
 1. in `AppleSpawner.cs` to randomize the chosen apple to spawn; 
 2. in `RockSpawner.cs` to randomize the rocks' rotation animation;
- Iterator, used in `SnakeNode.cs` to provide access to the next Node in the list - Next is called in `SnakeManagerLL.cs` in the `NewHeadColor(Color)` method;
- Strategy, used in `BoundingBox.cs` by implementing the `IPositionRandomizer.cs` interface - the initial idea was to later extend this into things like `BoundingSpheres` or any other geometric shapes, but I scrapped it due to overscoping;
- Composite, used to link `KeyboardInputSystem.cs` to `FreeMovement3D.cs`, allowing the player movement and input reads to be separated;
- Flyweight(?)/Observer, I am unsure if it really is working as a flyweight, but all apples have a manual dependency injection for an event which references the player's current score in the `AppleCollisionCallback.cs`, the same event is called by all apples;
