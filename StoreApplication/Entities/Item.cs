//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreApplication.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public System.Guid CategoryId { get; set; }
    
        public virtual ItemCategory ItemCategory { get; set; }
    }
}
