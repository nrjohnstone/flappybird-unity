using UnityEngine;

namespace Assets.Scripts
{
    public class Column : MonoBehaviour {

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Bird>() != null)
            {
                Game.instance.BirdScored();
            }
        }
    }
}
