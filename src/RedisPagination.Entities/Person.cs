using RedisPagination.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPagination.Entities
{
    public class Person:BaseEntity,IEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
