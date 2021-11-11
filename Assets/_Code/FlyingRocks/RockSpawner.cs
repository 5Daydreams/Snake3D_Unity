using System;
using _Code.Geometry;
using UnityEngine;

namespace _Code.FlyingRocks
{
    [RequireComponent(typeof(BoundingBox))]
    public class RockSpawner : MonoBehaviour
    {
        [SerializeField] private bool _enableGizmo;
        [SerializeField] private Vector3 _centerPos;
        [SerializeField] private Vector3 _movementDirection;
        [SerializeField] private StraightLineMovement _rockPrefab;
        private BoundingBox _boundingBox;

        private void Awake()
        {
            _boundingBox = this.GetComponent<BoundingBox>();
        }

        private void OnDrawGizmos()
        {
            if (!_enableGizmo)
            {
                return;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _movementDirection * 200.0f);
            Gizmos.color = Color.white;
        }

        public void SpawnRockAtRandomPosition()
        {
            Vector3 spawnPosition = _boundingBox.GetRandomPositionInside();

            StraightLineMovement spawnedRock = Instantiate(_rockPrefab, spawnPosition, Quaternion.identity);

            spawnedRock.Direction = _movementDirection.normalized;
        }
    }
}