using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public class AmbientTime : ITime
    {
        public float deltaTime { get { return Time.deltaTime; } }
    }
}