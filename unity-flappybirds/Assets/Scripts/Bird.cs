using UnityEngine;

namespace Assets.Scripts
{
    public class Bird : MonoBehaviour, IInput, IAnimator, IRigidbody2D
    {
        public float UpForce = 150f;
        private Animator _anim;

        private BirdController _birdController;
        
        private Rigidbody2D _rb2D;

        public void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();

            _birdController = new BirdController(this, this, this, MessageHub.Instance)
            {
                UpForce = UpForce
            };
        }

        public void Update()
        {
            _birdController.Update();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            _birdController.OnCollisionEnter2D(other);            
        }

        public bool IsLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }

        public void SetTrigger(string triggerName)
        {
            _anim.SetTrigger(triggerName);
        }

        public Vector2 velocity
        {
            get { return _rb2D.velocity; }
            set { _rb2D.velocity = value; }
        }

        public void AddForce(Vector2 vector2)
        {
            _rb2D.AddForce(vector2);
        }
    }
}
