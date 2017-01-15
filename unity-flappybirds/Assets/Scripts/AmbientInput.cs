using UnityEngine;

namespace Assets.Scripts
{
    public class AmbientInput : IInput
    {
        public bool IsLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}
