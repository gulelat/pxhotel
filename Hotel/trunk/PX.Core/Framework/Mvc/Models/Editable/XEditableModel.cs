using System.ComponentModel.DataAnnotations;

namespace PX.Core.Framework.Mvc.Models.Editable
{
    public class XEditableModel
    {
        [Required]
        public int Pk { get; set; }

        [Required]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
