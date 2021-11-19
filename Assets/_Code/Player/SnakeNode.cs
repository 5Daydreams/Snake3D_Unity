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
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Collider nodeCollider;
        
        [HideInInspector] public Color CurrentNodeColor;
        private MaterialPropertyBlock mpb;
        private static readonly int ColorPropertyID = Shader.PropertyToID("_Color");

        private void Awake()
        {
            CurrentNodeColor = meshRenderer.sharedMaterial.color;
        }

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
            meshRenderer.SetPropertyBlock(Mpb);
        }

        public void DisableAllColliders()
        {
            if (Next != null)
            {
                Next.DisableAllColliders();
            }

            this.nodeCollider.enabled = false;
        }
    }
}