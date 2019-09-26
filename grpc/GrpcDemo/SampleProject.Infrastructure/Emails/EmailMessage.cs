namespace SampleProject.Infrastructure.Emails
{
    public class EmailMessage
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Content { get; set; }

        public EmailMessage(string @from, string to, string content)
        {
            From = @from;
            To = to;
            Content = content;
        }
    }
}