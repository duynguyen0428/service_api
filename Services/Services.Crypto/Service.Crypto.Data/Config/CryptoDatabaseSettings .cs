namespace Service.Crypto.Data.Config
{
    public class CryptoDatabaseSettings : ICryptoDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }
    public interface ICryptoDatabaseSettings 
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}