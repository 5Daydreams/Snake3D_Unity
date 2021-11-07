using _Code.LinkedList;
using _TutorialCode;
using UnityEngine;

namespace _Code.Player.LinkedListSnakeManager
{
    [RequireComponent(typeof(WaypointDropper))]
    public class SnakeNode : MonoBehaviour, ILinkedListNode<SnakeNode>
    {
        public SnakeNode Next { get; set; }
        public WaypointDropper WaypointDropper;
    }
}