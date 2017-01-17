using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public class AmbientRandom : IRandom
    {
        public float Range(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}