using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public interface IInput
    {
        bool IsLeftMouseButtonDown();
        bool IsKeyDown(KeyCode keyCode);
    }
}