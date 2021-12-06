# SnakeGameAlgorithms
 
Nelson Kiyoshi Kossuga
A 3D snake game with apple-powered-powerups and flying rocks in an asteroid fashion.

Patterns:
- Observer, used in `BuffClockManager.cs` with `TrackableFloat.cs` being the value observed in the UI;
- Factory Method, used in `AppleSpawner.cs` to randomize the chosen apple to spawn, also used in `RockSpawner.cs` to randomize the rocks' rotation animation;
- Iterator, used in `SnakeNode.cs` to provide access to the next Node in the list - Next is called in `SnakeManagerLL.cs` in the `NewHeadColor(Color)` method;
- Strategy, used in `BoundingBox.cs` by implementing the `IPositionRandomizer.cs` interface - the initial idea was to later extend this into things like `BoundingSpheres` or any other geometric shapes, but I scrapped it due to overscoping;
