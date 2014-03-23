using PX.Core.Framework.Enums;

namespace PX.Core.Framework.Mvc.Models
{
    public class ResponseModel
    {
        #region Public Properties
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ResponseStatusEnums ResponseStatus { get; set; }

        #endregion

        public ResponseModel SetMessage(string message)
        {
            Message = message;
            return this;
        }
    }
}
