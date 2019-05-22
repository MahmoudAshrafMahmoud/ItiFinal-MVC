using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Vendor
    {
        CraftsEntities context = new CraftsEntities();

        public  List<OrderDetails_table> get()
        {
            List<OrderDetails_table> query = context.OrderDetails_table ;

            return query;
        }
    }
}
