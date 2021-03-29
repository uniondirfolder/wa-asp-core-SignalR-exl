namespace Server
{
    public class NewMessage:Message
    {
        public string Sender { get; set; }
        public NewMessage()
        {

        }
        public static NewMessage Create(string sender, Message message) 
        {
            return new NewMessage {
                Sender=string.IsNullOrWhiteSpace(sender) ? "Anonumous" : sender,
                Text = message.Text
            };
        }
    }
}