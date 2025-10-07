using Newtonsoft.Json;

namespace AuthSystem.Application.Dtos.Response
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
