//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_CMS.DB_Code
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            this.MenuLinks = new HashSet<MenuLink>();
        }
    
        public int MenuID { get; set; }
        public string MenuTitle { get; set; }
        public Nullable<int> PanelID { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLink> MenuLinks { get; set; }
        public virtual MenuPanel MenuPanel { get; set; }
    }
}
