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
    using System.ComponentModel.DataAnnotations;

    public partial class justification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public justification()
        {
            this.step_justification = new HashSet<step_justification>();
        }
    
        public int jid { get; set; }
        [Display(Name = "Timestamp")]
        public Nullable<System.DateTime> timestamp { get; set; }
        [Display(Name = "User")]
        public string uid { get; set; }
        [Display(Name = "Old Status")]
        public string status_old { get; set; }
        [Display(Name = "New Status")]
        public string status_new { get; set; }
        [Display(Name = "Justification")]
        public string justification1 { get; set; }
    
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<step_justification> step_justification { get; set; }
    }
}
