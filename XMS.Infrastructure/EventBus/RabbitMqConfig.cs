namespace XMS.Infrastructure.EventBus
{
    internal class RabbitMqConfig
    {
        public required string Host { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
