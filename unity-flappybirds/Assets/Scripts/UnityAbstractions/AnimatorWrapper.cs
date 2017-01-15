using UnityEngine;

namespace Assets.Scripts.UnityAbstractions
{
    public class AnimatorWrapper : IAnimator
    {
        private readonly Animator _instance;

        public AnimatorWrapper(Animator instance)
        {
            _instance = instance;
        }

        public void SetTrigger(string triggerName)
        {
            _instance.SetTrigger(triggerName);
        }
    }
}