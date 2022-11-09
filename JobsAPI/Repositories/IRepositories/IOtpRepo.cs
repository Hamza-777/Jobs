namespace JobsAPI.Repositories.IRepositories
{
    public interface IOtpRepo
    {
        Task<SendResponse> ClearOTP();
        Task<SendResponse> CheckOTP(string value);
        Task<SendResponse> SendEmail(string toemail, string fullname);
    }
}
