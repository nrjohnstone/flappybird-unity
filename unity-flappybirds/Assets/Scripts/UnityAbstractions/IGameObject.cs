using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public interface IGameObject
    {
        void SetActive(bool value);
        ITransform transform { get; }
    }
}