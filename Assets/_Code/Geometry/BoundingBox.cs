using UnityEngine;
using Random = UnityEngine.Random;

namespace _Code.Geometry
{
    public class BoundingBox : MonoBehaviour, IPositionRandomizer
    {
        [SerializeField] private Vector3 _centerPos = Vector3.zero;
        [SerializeField] private Vector3 _size = Vector3.one;

        public Vector3 GetRandomPositionInside()
        {
            Vector3 randomizedPosition = new Vector3();

            Vector3 min = this.transform.position + _centerPos - _size * 0.5f;
            Vector3 max = this.transform.position + _centerPos + _size * 0.5f;

            randomizedPosition.x = Random.Range(min.x, max.x);
            randomizedPosition.y = Random.Range(min.y, max.y);
            randomizedPosition.z = Random.Range(min.z, max.z);

            return randomizedPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(this.transform.position + _centerPos,_size);
        }
    }
}