using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public interface IRigidbody2D
    {
        Vector2 velocity { get; set; }
        void AddForce(Vector2 vector2);
    }
}