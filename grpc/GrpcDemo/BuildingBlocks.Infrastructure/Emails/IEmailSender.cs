﻿namespace BuildingBlocks.Infrastructure.Emails
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessage message);
    }
}
