using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Store.Models
{
    public enum Cities
    {
        None, London, Paris, Chicago
    }

    public class User : IdentityUser
    {
        public Cities City { get; set; }
        public string Address { get; set; }
    }
}
