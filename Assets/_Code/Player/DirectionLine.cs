using UnityEngine;

namespace _Code.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class DirectionLine : MonoBehaviour
    {
        [SerializeField] private float _lineRange;
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
            if (Physics.Raycast(transform.position, transform.forward, out info, _lineRange))
            {
                targetDistance = info.distance;
            }
        
            _renderDistance = Mathf.Min(targetDistance, _lineRange);
            Vector3 predictionLineEndPosition = this.transform.position + transform.forward * _renderDistance;
        
            _lineRenderer.SetPosition(1, predictionLineEndPosition);
        }
    }
}