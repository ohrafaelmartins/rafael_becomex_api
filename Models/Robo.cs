using System;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace rafael_becomex_api.Models
{
    public class Left
    {
        public string elbow { get; set; }
        public string wrist { get; set; }
    }

    public class Right
    {
        public string elbow { get; set; }
        public string wrist { get; set; }
    }

    public class Arm
    {
        public Left left { get; set; }
        public Right right { get; set; }
    }

    public class Head
    {
        public string rotate { get; set; }
        public string tilted_head { get; set; }
    }

    public class RootObject
    {
        public Arm arm { get; set; }
        public Head head { get; set; }
    }
}