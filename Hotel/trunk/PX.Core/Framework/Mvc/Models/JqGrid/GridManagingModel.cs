using PX.Core.Framework.Enums;
using PX.Core.Ultilities;

namespace PX.Core.Framework.Mvc.Models.JqGrid
{
    public class GridManagingModel
    {
        public string Oper { get; set; }

        public GridOperationEnums Operation
        {
            get
            {
                return Oper.ToEnums<GridOperationEnums>();
            }
        }
    }
}
