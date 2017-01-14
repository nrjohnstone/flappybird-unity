using UnityEngine;

namespace Assets.Scripts
{
    public class Bird : MonoBehaviour, IInput, IAnimator, IRigidbody2D
    {
        public float upForce = 150f;
        private bool isDead = false;
        private Animator anim;

        private BirdController birdController;
        
        private Rigidbody2D rb2d;

        public void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            birdController = new BirdController(this, this, this);
        }

        public void Update()
        {
            birdController.Update();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            rb2d.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameControl.instance.BirdDied();
        }

        public bool IsLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }

        public void SetTrigger(string triggerName)
        {
            anim.SetTrigger(triggerName);
        }

        public Vector2 velocity
        {
            get { return rb2d.velocity; }
            set { rb2d.velocity = value; }
        }

        public void AddForce(Vector2 vector2)
        {
            rb2d.AddForce(vector2);
        }
    }

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

    public interface IRigidbody2D
    {
        Vector2 velocity { get; set; }
        void AddForce(Vector2 vector2);
    }

    public interface IAnimator
    {
        void SetTrigger(string triggerName);
    }

    public interface IInput
    {
        bool IsLeftMouseButtonDown();
    }
}
