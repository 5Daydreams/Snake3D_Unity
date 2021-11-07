﻿using _Code.LinkedList;
using _TutorialCode;
using UnityEngine;

namespace _Code.Player.LinkedListSnakeManager
{
    [RequireComponent(typeof(WaypointDropper))]
    public class SnakeNode : MonoBehaviour, ILinkedListNode<SnakeNode>
    {
        public SnakeNode Next { get; set; }
        public WaypointDropper WaypointDropper;
        public MeshRenderer Renderer;
        private MaterialPropertyBlock mpb;
        private static readonly int Albedo = Shader.PropertyToID("Albedo");

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
            Mpb.SetColor(Albedo, thing);
            Renderer.SetPropertyBlock(Mpb);
        }
    }
}