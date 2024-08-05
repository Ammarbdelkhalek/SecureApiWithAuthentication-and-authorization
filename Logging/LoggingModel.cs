namespace SecureApiWithAuthentication.Logging
{
    public class LoggingModel
    {
        public string ?Name {  get; set; }
        public string ?Address {  get; set; }


        public void TransFormData()
        {
            Name += " - Transformed";
            Address += " - Transformed";
        }
    }
   
}
