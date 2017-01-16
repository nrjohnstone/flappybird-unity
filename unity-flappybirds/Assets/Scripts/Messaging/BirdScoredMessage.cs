using TinyMessenger;

namespace Assets.Scripts.Messaging
{
    public class BirdScoredMessage : ITinyMessage
    {
        public object Sender { get; private set; }
    }
}