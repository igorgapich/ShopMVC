﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string IdsProduct { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}