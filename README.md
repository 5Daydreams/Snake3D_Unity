# SnakeGameAlgorithms
 
Nelson Kiyoshi Kossuga
A 3D snake game with apple-powered-powerups and flying rocks in an asteroid fashion.

Patterns:
- Observer, used in the UI via scriptable objects in `BuffClockManager.cs` with `TrackableFloat.cs` being the value observed;
- Factory Method, used to randomize the chosen apple to spawn in `AppleSpawner.cs`, also used in `RockSpawner.cs` for randomizing the rocks' rotation animation;
- Iterator, used in `SnakeNode.cs` to provide access to the next Node in the list - used in `SnakeManagerLL.cs` in the `NewHeadColor(Color)` method;
- 


Patterns:
- Singleton, in `InventoryManager.cs` in `class InventoryManager` as `InventoryManager.instance`
  Used singleton to make the single instance of the class that could be accessed from anywhere.
- ...
