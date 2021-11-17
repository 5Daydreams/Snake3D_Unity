using System.Collections.Generic;
using UnityEngine;

namespace _Code.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class DirectionLine : MonoBehaviour
    {
        [SerializeField] private float _lineRange;
        [SerializeField] private List<string> _ignoreTags;
        private LineRenderer _lineRenderer;
        private float _renderDistance = 0;

        private void Awake()
        {
            _lineRenderer = this.GetComponent<LineRenderer>();
        }

        void Update()
        {
            _lineRenderer.SetPosition(0, this.transform.position);

            float targetDistance = _lineRange;

            RaycastHit info = new RaycastHit();

            bool raycastHit = Physics.Raycast(transform.position, transform.forward, out info, _lineRange);

            if (_ignoreTags.Count != 0 && info.collider != null)
            {
                foreach (var tag in _ignoreTags)
                {
                    if(info.collider.CompareTag(tag))
                    {
                        raycastHit = false;
                    }
                }
            }

            if (raycastHit)
            {
                targetDistance = info.distance;
            }
            
            _renderDistance = Mathf.Min(targetDistance, _lineRange);
            Vector3 predictionLineEndPosition = this.transform.position + transform.forward * _renderDistance;

            _lineRenderer.SetPosition(1, predictionLineEndPosition);
        }
    }
}