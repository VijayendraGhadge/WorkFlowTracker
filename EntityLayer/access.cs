//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class access
    {
        public string uid { get; set; }
        public int pid { get; set; }
        public int Id { get; set; }
    
        public virtual project project { get; set; }
        public virtual user user { get; set; }
    }
}
