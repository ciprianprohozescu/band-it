//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModelsDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Band
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Band()
        {
            this.Applications = new HashSet<Application>();
            this.BandUsers = new HashSet<BandUser>();
            this.Invites = new HashSet<Invite>();
            this.Notifications = new HashSet<Notification>();
            this.Genres = new HashSet<Genre>();
            this.Messages = new HashSet<Message>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string ProfilePicture { get; set; }
        public string InviteMessage { get; set; }
        public byte[] RowVersion { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Applications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BandUser> BandUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invite> Invites { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Genre> Genres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
    }
}