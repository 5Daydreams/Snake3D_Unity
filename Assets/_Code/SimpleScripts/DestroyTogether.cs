using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTogether : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private void OnDestroy()
    {
        Destroy(_target);
    }
}
