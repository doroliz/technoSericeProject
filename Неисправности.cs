//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechnoServiceProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Неисправности
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Неисправности()
        {
            this.Заявки = new HashSet<Заявки>();
        }
    
        public int Код { get; set; }
        public string Описание { get; set; }
        public Nullable<decimal> Цена { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заявки> Заявки { get; set; }
    }
}
