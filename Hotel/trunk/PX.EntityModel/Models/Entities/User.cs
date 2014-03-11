using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using PX.EntityModel.Enums;
using PX.EntityModel.Resources;
using PX.Library.Configuration;

namespace PX.EntityModel
{

    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public string RoleName
        {
            get { return Role.Name; }
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public static User CurrentUser
        {
            get { return (User)HttpContext.Current.Session["CurrentUser"]; }
            set { HttpContext.Current.Session["CurrentUser"] = value; }
        }

        public string CurrentImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(ImageFileName) || !File.Exists(HttpContext.Current.Server.MapPath(Configurations.DefaultUserFolder + ImageFileName)))
                {
                    return Configurations.DefaultUserFolder + Configurations.DefaultUserImage;
                }
                return Configurations.DefaultUserFolder + ImageFileName;
            }
        }

        public string CurrentImageFileName
        {
            get
            {
                if (string.IsNullOrEmpty(ImageFileName)
                    || !File.Exists(HttpContext.Current.Server.MapPath(Configurations.DefaultUserFolder + ImageFileName)))
                {
                    return Configurations.DefaultUserImage;
                }
                return ImageFileName;
            }
        }

        public UserEnums.UserTypesEnums RoleEnums
        {
            get { return (UserEnums.UserTypesEnums)RoleId; }
        }

        public UserEnums.UserStatusEnums StatusEnums
        {
            get { return (UserEnums.UserStatusEnums)StatusId; }
        }

        [Required]
        [DisplayName(@"Confirm Password")]
        [System.Web.Mvc.Compare("Password", ErrorMessageResourceType = typeof(SystemResources), ErrorMessageResourceName = "InvalidPasswordCompare")]
        public string ConfirmPassword { get; set; }
    }

    public class UserMetaData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessageResourceType = typeof(SystemResources), ErrorMessageResourceName = "InvalidEmail")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName(@"Fist Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName(@"Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        public string ImageFileName { get; set; }

        [Required]
        public int RoleId { get; set; }

        public DateTime LastLogin { get; set; }

        [Required]
        public int StatusId { get; set; }

        public int? RecordOrder { get; set; }

        public bool RecordActive { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public string UpdatedBy { get; set; }
    }
}
