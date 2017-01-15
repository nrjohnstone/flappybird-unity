using UnityEngine;

namespace Assets.Scripts
{
    public class BirdController
    {
        public float upForce = 150f;
        private bool isDead = false;

        private readonly IAnimator _anim;
        private readonly IRigidbody2D _rb2d;
        private readonly IInput _input;

        public BirdController(IInput input, IAnimator anim, IRigidbody2D rb2D)
        { 
            _input = input;
            _anim = anim;
            _rb2d = rb2D;
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
    }
}