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
    
    public partial class PageTag
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int TagId { get; set; }
    
        public virtual Page Page { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
