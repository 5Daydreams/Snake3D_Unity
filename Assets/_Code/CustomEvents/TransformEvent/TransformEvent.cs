using System.Collections;
using System.Collections.Generic;
using _Code.Observer.Event;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomScriptables/Events/TransformEvent", fileName = "NewTransformEvent")]
public class TransformEvent : BaseGameEvent<Transform>
{
}