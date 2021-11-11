using _Code.Geometry;
using UnityEngine;

namespace _Code.FlyingRocks
{
    public class RockSpawner : MonoBehaviour
    {
        [SerializeField] private bool _enableGizmo;
        [SerializeField] private Vector3 _centerPos;
        [SerializeField] private Vector3 _size;
        [SerializeField] private Vector3 _movementDirection;
        [SerializeField] private BoundingBox _boundingBox;
        [SerializeField] private StraightLineMovement _rockPrefab;

        private void OnDrawGizmos()
        {
            if (!_enableGizmo)
            {
                return;
            }

            Gizmos.DrawWireCube(this.transform.position + _centerPos, _size);
        }

        public void SpawnRockAtRandomPosition()
        {
            Vector3 spawnPosition = _boundingBox.GetRandomPositionInside();
            
            StraightLineMovement spawnedRock = Instantiate(_rockPrefab,spawnPosition,Quaternion.identity);

            spawnedRock.Direction = _movementDirection.normalized;
        }

    }
}