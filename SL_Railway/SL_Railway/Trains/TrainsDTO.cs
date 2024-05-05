﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SL_Railway.Trains
{
    public class TrainsDTO
    {
        public int trainId { get; set; }
        public string trainName { get; set; }
        public string startLocation { get; set; }
        public string endLocation { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int capacity { get; set; }
        public double distance { get; set; }
        public decimal price { get; set; }
    }
}
