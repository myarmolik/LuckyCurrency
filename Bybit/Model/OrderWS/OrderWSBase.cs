﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bybit.Model.OrderWS
{
    public class OrderWSBase
    {
        public string topic { get; set; }   
        public string action { get; set; }
        public List<OrderWSData> data { get; set; }
    }
}
