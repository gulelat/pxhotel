//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PX.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Template
    {
        public Template()
        {
            this.TemplateLogs = new HashSet<TemplateLog>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string DataType { get; set; }
        public string CurlyBracket { get; set; }
        public bool IsDefaultTemplate { get; set; }
        public int RecordOrder { get; set; }
        public bool RecordActive { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual ICollection<TemplateLog> TemplateLogs { get; set; }
    }
}
