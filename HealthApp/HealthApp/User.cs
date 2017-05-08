using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp
{
   public class User
    {
        private string name;
        private int id;

        public User(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public string getName(){

            return name;
        }

        public int getId()
        {
            return id;
        }
        
    }
}
