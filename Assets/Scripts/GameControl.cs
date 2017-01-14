using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameControl : MonoBehaviour
    {
        public static GameControl instance;
        public GameObject gameOverText;
        public bool gameOver = false;
        public float scrollSpeed = -1.5f;


        void Awake () {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
           
        }

        void Update()
        {
            if (gameOver && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void BirdDied () {
            gameOverText.SetActive(true);
            gameOver = true;
        }
    }
}
