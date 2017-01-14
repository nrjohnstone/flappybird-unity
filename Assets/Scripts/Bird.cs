using UnityEngine;

namespace Assets.Scripts
{
    public class Bird : MonoBehaviour {
        public float upForce = 200f;
        private bool isDead = false;
        
        private Rigidbody2D rb2d;

        public void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            if (isDead == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, upForce));
                }
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            rb2d.velocity = Vector2.zero;
            isDead = true;
        }
    }
}
