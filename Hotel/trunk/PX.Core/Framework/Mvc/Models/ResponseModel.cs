using PX.Core.Framework.Enums;

namespace PX.Core.Framework.Mvc.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public ResponseStatusEnums ResponseStatus { get; set; }
    }
}
