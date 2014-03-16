using System.Data.Objects.DataClasses;

namespace PX.EntityModel.Framework.Repositories.RepositoryBase.Models
{
    public class HotelEntityObject
    {
        #region Constructor
        public HotelEntityObject()
        {
            Success = false;
            Message = string.Empty;
        }
        #endregion

        #region Public Properties

        public EntityObject EntityObject { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        #endregion
    }
}
