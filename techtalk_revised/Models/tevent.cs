//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace techtalk_revised.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tevent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tevent()
        {
            this.tevent_users = new HashSet<tevent_users>();
        }
    
        public int eventID { get; set; }
        public string ename { get; set; }
        public string edescription { get; set; }
        public System.DateTime scheduledOn { get; set; }
        public string presentationURL { get; set; }
        public int userID { get; set; }
    
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tevent_users> tevent_users { get; set; }
    }
}
