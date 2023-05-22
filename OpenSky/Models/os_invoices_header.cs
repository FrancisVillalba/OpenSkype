//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenSky.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class os_invoices_header
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public os_invoices_header()
        {
            this.os_invoices_detail = new HashSet<os_invoices_detail>();
            this.os_payments = new HashSet<os_payments>();
        }
    
        public System.Guid Id { get; set; }
        public string ProcessNumber { get; set; }
        public string InvoicesNumber { get; set; }
        public Nullable<int> InvoicesNumberRange { get; set; }
        public Nullable<System.DateTime> DateInvoices { get; set; }
        public Nullable<System.Guid> SalesConditionId { get; set; }
        public Nullable<System.Guid> CustomerId { get; set; }
        public string CurrencyTypeId { get; set; }
        public Nullable<double> TotalInvoiceAmount { get; set; }
        public Nullable<System.Guid> BranchCompanyId { get; set; }
        public Nullable<System.Guid> TimbradoDataId { get; set; }
        public Nullable<System.Guid> UserCreatedId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.Guid> UserUpdateId { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
        public string Status { get; set; }
    
        public virtual os_currency_type os_currency_type { get; set; }
        public virtual os_customers os_customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<os_invoices_detail> os_invoices_detail { get; set; }
        public virtual os_timbrado_data os_timbrado_data { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<os_payments> os_payments { get; set; }
    }
}