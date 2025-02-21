using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Module.Master
{
    public class DealerServiceClass :commonClass
    {
        public Nullable<Int32> ServiceId { get; set; }
        [Required(ErrorMessage = "Service Name is required")]
        public string ServiceName { get; set; }
        [Required(ErrorMessage = "Unit is required")]
        public int Unit { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
    public class DealerServiceSearch : commonSearchClass
    {
        public string ServiceName { get; set; }


    }
}
