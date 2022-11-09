namespace JobsAPI.Repositories
{
    public class SendResponse
    {


        public string message { get; set; }
        public int code { get; set; }
        public dynamic data { get; set; }
        public string error { get; set; }
        public SendResponse()
        {

        }
        public  SendResponse(string message, int code, dynamic data, string error)
        {
            this.message = message;
            this.code = code;
            this.data = data;
            this.error = error;
        }
    }
}
