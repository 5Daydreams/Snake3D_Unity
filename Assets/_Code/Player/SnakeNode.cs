using _2DVersion._Code;
using _Code.LinkedList;
using UnityEngine;

namespace _Code.Player
{
    [RequireComponent(typeof(WaypointDropper))]
    public class SnakeNode : MonoBehaviour, ILinkedListNode<SnakeNode>
    {
        public SnakeNode Next { get; set; }
        public WaypointDropper WaypointDropper;
        [SerializeField] private MeshRenderer Renderer;
        
        [HideInInspector] public Color CurrentNodeColor;
        private MaterialPropertyBlock mpb;
        private static readonly int ColorPropertyID = Shader.PropertyToID("_Color");

        public MaterialPropertyBlock Mpb
        {
            get
            {
                if (mpb == null)
                {
                    mpb = new MaterialPropertyBlock();
                }
                return mpb;
            }
        }
        
        public void SetNodeColor(Color thing)
        {
            CurrentNodeColor = thing;
            Mpb.SetColor(ColorPropertyID, CurrentNodeColor);
            Renderer.SetPropertyBlock(Mpb);
        }
    }
}