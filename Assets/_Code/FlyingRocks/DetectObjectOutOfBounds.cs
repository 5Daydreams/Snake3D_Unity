using System;
using System.Collections.Generic;
using _Code.Geometry;
using UnityEngine;
using UnityEngine.Events;

namespace _Code.FlyingRocks
{
    [RequireComponent(typeof(BoundingBox))]
    public class DetectObjectOutOfBounds : MonoBehaviour
    {
        [SerializeField] private bool _destroyOnOutOfBounds;
        [SerializeField] private UnityEvent _callback;
        private List<Transform> _boundObjects = new List<Transform>();
        private BoundingBox _boundaryBox;

        private void Awake()
        {
            _boundaryBox = this.GetComponent<BoundingBox>();
        }

        public void SubscribeToBoundary(Transform objectToBind)
        {
            _boundObjects.Add(objectToBind);
        }

        private void Update()
        {
            for (int i = _boundObjects.Count - 1; i >= 0; i--)
            {
                Transform transform = _boundObjects[i];

                if (_boundaryBox.ContainsVector(transform.position))
                {
                    continue;
                }

                _callback.Invoke();
                if (_destroyOnOutOfBounds)
                {
                    Destroy(transform.gameObject);
                    _boundObjects.RemoveAt(i);
                }
            }
        }
    }
}