using TinyMessenger;

namespace Assets.Scripts.Messaging
{
    public class GameOverMessage : ITinyMessage
    {
        public object Sender { get; private set; }
    }
}