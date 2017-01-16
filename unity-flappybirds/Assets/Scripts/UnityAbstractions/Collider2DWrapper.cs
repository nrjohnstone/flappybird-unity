using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public class Collider2DWrapper : ICollider2D
    {
        private readonly Collider2D _instance;

        public Collider2DWrapper(Collider2D instance)
        {
            _instance = instance;
        }

        public T GetComponent<T>()
        {
            return _instance.GetComponent<T>();
        }

        public string tag
        {
            get { return _instance.tag; }
            set { _instance.tag = value; }
        }
    }
}