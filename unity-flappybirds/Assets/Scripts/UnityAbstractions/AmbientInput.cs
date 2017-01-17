using System;
using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public class AmbientInput : IInput
    {
        public bool IsLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}
