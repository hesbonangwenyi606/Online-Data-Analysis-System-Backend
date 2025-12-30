using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FOS.Shared
{
   public class HomeData
    {
        public List<Appointment> Appointments { get; set; }
    }
}
