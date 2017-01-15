using TinyMessenger;
using UnityEngine;

namespace Assets.Scripts
{
    public class BirdController
    {
        public float upForce = 150f;
        public bool isDead { get; private set; }

        private readonly IAnimator _anim;
        private readonly IRigidbody2D _rb2d;
        private readonly ITinyMessengerHub _messengerHub;
        private readonly IInput _input;

        public BirdController(IInput input, IAnimator anim, IRigidbody2D rb2D, ITinyMessengerHub messengerHub)
        { 
            _input = input;
            _anim = anim;
            _rb2d = rb2D;
            _messengerHub = messengerHub;
        }

        public void Update()
        {
            if (isDead == false)
            {
                if (_input.IsLeftMouseButtonDown())
                {
                    _rb2d.velocity = Vector2.zero;
                    _rb2d.AddForce(new Vector2(0, upForce));
                    _anim.SetTrigger("Flap");
                }
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            _rb2d.velocity = Vector2.zero;
            isDead = true;
            _anim.SetTrigger("Die");
            NotifyBirdDied();
        }

        protected virtual void NotifyBirdDied()
        {
            _messengerHub.Publish(new BirdDiedMessage());
        }
    }
}