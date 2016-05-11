using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kurs.Admin.Authorization
{
    public class KursRole : IRole
    {
        public string Id
        {
            get; set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}