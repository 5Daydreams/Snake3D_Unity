using System;
using System.Collections;
using System.Collections.Generic;
using _Code.Apple;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private List<AppleAndWeight> _appleList;
    private List<AppleCollisionCallback> _listToChoseFrom;

    public void SpawnRandomApple()
    {
        
    }

    private void OnValidate()
    {
        _listToChoseFrom.Clear();
        foreach (AppleAndWeight apple in _appleList)
        {
            for (int i = 0; i < apple.Repeats; i++)
            {
                _listToChoseFrom.Add(apple.AppleReference);
            }
        }
    }

    [Serializable]
    public class AppleAndWeight
    {
        public int Repeats;
        public AppleCollisionCallback AppleReference;
    }
}