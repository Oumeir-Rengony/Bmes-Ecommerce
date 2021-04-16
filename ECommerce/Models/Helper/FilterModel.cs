using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Helper
{
    public class FilterModel
    {
        public string CategorySlug { get; set; } = "all-categories";

        public string BrandSlug { get; set; } = "all-brands";
    }
}
