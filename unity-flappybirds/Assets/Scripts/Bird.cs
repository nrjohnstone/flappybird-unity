using UnityEngine;

namespace Assets.Scripts
{
    public class Bird : MonoBehaviour, IInput, IAnimator, IRigidbody2D
    {
        public float upForce = 150f;
        private Animator anim;

        private BirdController birdController;
        
        private Rigidbody2D rb2d;

        public void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            birdController = new BirdController(this, this, this)
            {
                upForce = upForce
            };
        }

        public void Update()
        {
            birdController.Update();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            birdController.OnCollisionEnter2D(other);            
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
}
