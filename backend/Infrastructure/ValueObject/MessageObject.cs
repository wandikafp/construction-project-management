using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.ValueObject
{
    public class MessageObject
    {
        public string action { get; set; }
        public ConstructionProject? project { get; set; }
        public int? id { get; set; }
    }
}
