using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnThanhLam.Model.Abstract
{
    public class RecusionSet<T>
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        public string Alias { get; set; }

        public int? Order { get; set; }

        public List<RecusionSet<T>> items { get; set; }

        public int level { get; set; }
    }
}