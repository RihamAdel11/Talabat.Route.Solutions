namespace Talabat.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statuscode, string? message = null)
        {
            StatusCode = statuscode;

            Message = message ?? GetDefaultMessageForStatusCode(statuscode);

        }

        private string? GetDefaultMessageForStatusCode(int statuscode)
        {
           
            return statuscode switch
            {
                400 => "A Bad Request You Have Made",
                401 => "Authorized you are not",
                404 => "Resource was not Found",
                500 => "Error Are the Path For The Dark Side,Errors Lead To Anger, Anger Lead to Hate ,Hate Lead to Carrer Change",
                _=>null,
            };
        }
       
       
    }
}
