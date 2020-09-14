using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;



namespace Engine
{
    public class LootItem
    {
        public Item Details { get; set; }
        public int DropPercentage { get; set; }
        public bool IsDefaultItem { get; set; }
        public LootItem (Item details, int droppercentage , bool isdefaultitem)
        {
            Details = details;
            DropPercentage = droppercentage;
            IsDefaultItem = isdefaultitem;
        }

    }
}
