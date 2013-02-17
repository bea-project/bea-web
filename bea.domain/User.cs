using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Domain
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual String Firstname { get; set; }
        public virtual String Lastname { get; set; }

        public User()
        {

        }
    }
}
