using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public class TransformWrapper : ITransform
    {
        private readonly Transform _instance;

        public TransformWrapper(Transform instance)
        {
            _instance = instance;
        }

        public Vector2 position
        {
            get { return _instance.transform.position; }
            set { _instance.transform.position = value; }
        }
    }
}