using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class ProductData 
    {
        public string Name { get; set; }
        public string RegularPrice { get; set; }
        public string CampaignPrice { get; set; }
        public string regularPriceColor { get; set; }
        public string CampaignPriceColor { get; set; }
        public string RegularPriceFontSize { get; set; }
        public string CampaignPriceFontSize { get; set; }
        public string TagRegularPrice { get; set; }
        public string TagCampaignPrice { get; set; }
        public string Code { get; set; }

        public string Image { get; set; } = @"\Resources\1.jpg";
        public string Description { get; set; }
        public string Keywords { get; set; }





    }
}
