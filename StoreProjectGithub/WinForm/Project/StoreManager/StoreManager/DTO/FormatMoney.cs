using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace StoreManager.DTO
{
    public class FormatMoney
    {
        private static CultureInfo cul = new CultureInfo("vi-VN");
        private static FormatMoney instance;

        public static FormatMoney Instance 
        { 
            get
            {
                if(instance == null)
                    instance = new FormatMoney();
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        public FormatMoney()
        {

        }

        public String transformFormat(Object value)
        {
            return String.Format(cul, "{0:#,##0 VNĐ}", value);
        }
    }
}
