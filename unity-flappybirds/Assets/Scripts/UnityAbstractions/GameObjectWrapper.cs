using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public class GameObjectWrapper : IGameObject
    {
        private readonly GameObject _instance;

        public GameObjectWrapper(GameObject instance)
        {
            _instance = instance;
        }

        public void SetActive(bool value)
        {
            _instance.SetActive(value);
        }

        public ITransform transform
        {
            get { return new TransformWrapper(_instance.transform); }
        }
        
    }
}