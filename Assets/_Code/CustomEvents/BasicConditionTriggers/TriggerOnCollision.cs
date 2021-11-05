using _Code.CustomEvents.VoidEvent;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerOnCollision : MonoBehaviour
{
    [SerializeField] private VoidEvent _onTriggerEnterCallback;
    private void OnTriggerEnter(Collider other)
    {
        _onTriggerEnterCallback.Raise();
    }
}
