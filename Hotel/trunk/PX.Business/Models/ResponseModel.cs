using PX.EntityModel.Enums;

namespace PX.Business.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public ResponseStatusEnums ResponseStatus { get; set; }
    }
}
