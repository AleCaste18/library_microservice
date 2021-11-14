namespace booksManager.Setting
{
    public class MongoDbSetting
    {
        public string Host { get; init; }
        public string Port { get; init; }

        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
