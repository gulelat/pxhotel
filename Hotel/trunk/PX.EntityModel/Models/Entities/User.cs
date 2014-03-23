using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using PX.Core.Configurations;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{

    [MetadataType(typeof(UserMetaData))]
    [Table(Name = "Users")]
    public partial class User
    {
        public string RoleName
        {
            get { return UserGroup.Name; }
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string AvatarPath
        {
            get
            {
                if (string.IsNullOrEmpty(AvatarFileName) || !File.Exists(HttpContext.Current.Server.MapPath(Configurations.AvatarFolder + AvatarFileName)))
                {
                    return Configurations.AvatarFolder + Configurations.DefaultAvatar;
                }
                return Configurations.AvatarFolder + AvatarFileName;
            }
        }

        public string CurrentAvatarFileName
        {
            get
            {
                if (string.IsNullOrEmpty(AvatarFileName)
                    || !File.Exists(HttpContext.Current.Server.MapPath(Configurations.AvatarFolder + AvatarFileName)))
                {
                    return Configurations.DefaultAvatar;
                }
                return AvatarFileName;
            }
        }

        public UserEnums.UserStatusEnums StatusEnums
        {
            get { return (UserEnums.UserStatusEnums)Status; }
        }

        public double LastLoginHours
        {
            get
            {
                if(LastLogin.HasValue)
                    return (DateTime.Now - LastLogin.Value).TotalHours;
                return 0;
            }
        }

        public int? Age
        {
            get
            {
                if(BirthDay.HasValue)
                {
                    return (int)(DateTime.Now - BirthDay.Value).TotalDays/365;
                }
                return null;
            }
        }
    }

    public class UserMetaData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$")]
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

        public string IdentityNumber { get; set; }

        public string AvatarFileName { get; set; }

        [Required]
        public int UserGroupId { get; set; }

        public DateTime? LastLogin { get; set; }

        [Required]
        public int Status { get; set; }

        public int? RecordOrder { get; set; }

        public bool RecordActive { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public string UpdatedBy { get; set; }
    }
}
