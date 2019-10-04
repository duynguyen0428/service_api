namespace Service.Crypto.Application
{
    public class AddNewCredentialCmd
    {
        public string mobile_phone { get; set; }
        public int pin { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }
}