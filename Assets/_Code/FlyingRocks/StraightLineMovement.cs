using UnityEngine;

namespace _Code.FlyingRocks
{
    public class StraightLineMovement : MonoBehaviour
    {
    
        [SerializeField] private float _speed;
        [SerializeField] private bool _randomizeDirectionOnSpawn;
        public Vector3 Direction = Vector3.one;

        private void Awake()
        {
            if (_randomizeDirectionOnSpawn)
            {
                float x = Random.Range(-1, 1);
                float y = Random.Range(-1, 1);
                float z = Random.Range(-1, 1);
                
                Direction = new Vector3(x,y,z);
            }
        }

        void FixedUpdate()
        {
            if (Direction == Vector3.zero)
            {
                Direction = Vector3.one;
            }

            this.transform.position += Direction * (_speed * Time.deltaTime);
        }
    }
}
