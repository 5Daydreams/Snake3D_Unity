using System;
using System.Collections.Generic;
using _Code.ExtensionsMethods;
using _Code.Geometry;
using UnityEngine;

namespace _Code.Apple
{
    [RequireComponent(typeof(BoundingBox))]
    public class AppleSpawner : MonoBehaviour
    {
        [SerializeField] private int _spawnOnStart = 5;
        [SerializeField] private List<AppleCollisionCallback> _appleList;
        [SerializeField] private List<int> _appleProbabilityFactor;
        private BoundingBox _boundingBox;
        private List<AppleCollisionCallback> _listToChoseFrom = new List<AppleCollisionCallback>();

        private void Awake()
        {
            _boundingBox = this.GetComponent<BoundingBox>();

            for (int i = 0; i <= _spawnOnStart; i++)
            {
                SpawnRandomAppleInRandomPosition();
            }
        }

        public void SpawnRandomAppleInRandomPosition()
        {
            AppleCollisionCallback chosenApple = _listToChoseFrom.CopyRandomElement();

            Vector3 spawnPosition = _boundingBox.GetRandomPositionInside();

            Instantiate(chosenApple, spawnPosition,Quaternion.identity);
        }

        private void OnValidate()
        {
            _listToChoseFrom.Clear();

            while (_appleProbabilityFactor.Count < _appleList.Count)
            {
                _appleProbabilityFactor.Add(1);
            }
            
            for (var index = 0; index < _appleList.Count; index++)
            {
                AppleCollisionCallback apple = _appleList[index];
                
                for (int i = 0; i < _appleProbabilityFactor[index]; i++)
                {
                    _listToChoseFrom.Add(apple);
                }
            }
        }
    }
}