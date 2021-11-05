using UnityEngine;

namespace _Code.Camera.Imports
{
    public interface IFollowTarget
    {
        void FollowTarget(Transform targetToBeFollowed);
    }
}