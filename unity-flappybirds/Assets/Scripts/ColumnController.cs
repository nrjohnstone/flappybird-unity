using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using TinyMessenger;
using UnityEngine;

namespace Assets.Scripts
{
    public class ColumnController
    {
        private readonly ITinyMessengerHub _messengerHub;

        public ColumnController(ITinyMessengerHub messengerHub)
        {
            _messengerHub = messengerHub;
        }

        public void OnTriggerEnter2D(ICollider2D other)
        {
            if (other.tag == "Player")
            {
                _messengerHub.Publish(new BirdScoredMessage());
            }
        }
    }
}