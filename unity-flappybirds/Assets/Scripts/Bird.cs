using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bird : MonoBehaviour
    {
        public float UpForce = 150f;

        private BirdController _birdController;

        public void Start()
        {
            _birdController = new BirdController(
                input: new AmbientInput(), 
                anim: new AnimatorWrapper(GetComponent<Animator>()), 
                rb2D: new Rigidbody2DWrapper(GetComponent<Rigidbody2D>()),
                messengerHub: MessageHub.Instance)
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
          
    }
}
