using System;
using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class ColumnPool : MonoBehaviour
    {
        public int columnPoolSize = 5;
        public GameObject columnPrefab;
        
        private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
        private ColumnPoolController _columnPoolController;

        void Start ()
        {
            Func<GameObjectWrapper> columnFactory =
                () => new GameObjectWrapper(
                    (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity));

            _columnPoolController = new ColumnPoolController(MessageHub.Instance, columnFactory);
            _columnPoolController.Start();
        }
    
        void Update ()
        {
            _columnPoolController.Update();
        }
    }
}
