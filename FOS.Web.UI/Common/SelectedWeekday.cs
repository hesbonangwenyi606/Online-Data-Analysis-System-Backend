using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FOS.Shared.Common
{
    public class SelectedWeekday
    { 
        [DisplayName("MON")]
        public bool Monday { get; set; }
        [DisplayName("TUE")]
        public bool Tuesday { get; set; }
        [DisplayName("WED")]
        public bool Wednesday { get; set; }
        [DisplayName("THU")]
        public bool Thursday { get; set; }
        [DisplayName("FRI")]
        public bool Friday { get; set; }
        [DisplayName("SAT")]
        public bool Saturday { get; set; }
        [DisplayName("SUN")]
        public bool Sunday { get; set; }
        /// </Visit Plan>


        public SelectedWeekday(bool m, bool t, bool w, bool th, bool f, bool s, bool sn)
        {
            Monday = m;
            Tuesday = t;
            Wednesday = w;
            Thursday = th;
            Friday = f;
            Saturday = s;
            Sunday = sn;
        }

        public string GetSelectionByBits()
        {
            string bits = "";
            bits += Monday ? "1" : "0";
            bits += Tuesday ? "1" : "0";
            bits += Wednesday ? "1" : "0";
            bits += Thursday ? "1" : "0";
            bits += Friday ? "1" : "0";
            bits += Saturday ? "1" : "0";
            bits += Sunday ? "1" : "0";

            return bits;
        }

        public void MakeSelection(string bits){
            char[] arr = bits.ToCharArray();
            Monday = arr[0] == '1' ? true : false;
            Tuesday = arr[1] == '1' ? true : false;
            Wednesday = arr[2] == '1' ? true : false;
            Thursday = arr[3] == '1' ? true : false;
            Friday = arr[4] == '1' ? true : false;
            Saturday = arr[5] == '1' ? true : false;
            Sunday = arr[6] == '1' ? true : false;

        }

        public string GetSelectionByDays() {
            string str = "";
            str += Monday ? "Monday," : "";
            str += Tuesday ? "Tuesday," : "";
            str += Wednesday ? "Wednesday," : "";
            str += Thursday ? "Thursday," : "";
            str += Friday ? "Friday," : "";
            str += Saturday ? "Saturday," : "";
            str += Sunday ? "Sunday" : "";

            return str.TrimEnd(',');
        }

    }
}