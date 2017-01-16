using TinyMessenger;

namespace Assets.Scripts.Messaging
{
    public class BirdDiedMessage : ITinyMessage
    {
        public object Sender { get; private set; }
    }
}