using UnityEngine;

namespace Assets.Scripts
{
    public class Rigidbody2DWrapper : IRigidbody2D
    {
        private readonly Rigidbody2D _instance;

        public Rigidbody2DWrapper(Rigidbody2D instance)
        {
            _instance = instance;
        }

        public Vector2 velocity
        {
            get { return _instance.velocity; }
            set { _instance.velocity = value; }
        }

        public void AddForce(Vector2 vector2)
        {
            _instance.AddForce(vector2);
        }
    }
}