using TinyMessenger;
using UnityEngine;

namespace Assets.Scripts
{
    public class BirdController
    {
        public float UpForce = 150f;
        public bool IsDead { get; private set; }

        private readonly IAnimator _anim;
        private readonly IRigidbody2D _rb2D;
        private readonly ITinyMessengerHub _messengerHub;
        private readonly IInput _input;

        public BirdController(IInput input, IAnimator anim, IRigidbody2D rb2D, ITinyMessengerHub messengerHub)
        { 
            _input = input;
            _anim = anim;
            _rb2D = rb2D;
            _messengerHub = messengerHub;
        }

        public void Update()
        {
            if (IsDead)
                return;

            if (_input.IsLeftMouseButtonDown())
            {
                ChangeForceTo(new Vector2(0, UpForce));
                SetAnimationTrigger("Flap");
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            _rb2D.velocity = Vector2.zero;
            IsDead = true;
            SetAnimationTrigger("Die");
            NotifyBirdDied();
        }

        private void ChangeForceTo(Vector2 vector2)
        {
            _rb2D.velocity = Vector2.zero;
            _rb2D.AddForce(vector2);
        }

       
        private void SetAnimationTrigger(string triggerName)
        {
            _anim.SetTrigger(triggerName);
        }

        protected virtual void NotifyBirdDied()
        {
            _messengerHub.Publish(new BirdDiedMessage());
        }
    }
}