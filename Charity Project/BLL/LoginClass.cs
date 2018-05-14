using Charity_Project.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Charity_Project.BLL
{
    public class LoginClass
    {

        public static bool userlogin(string username, string password)
        {
            using (var obj = new Charity_DBEntities())
            {

                var count = (from p in obj.Login_tbl

                             where p.Username == username && p.Password == password

                             select p).Count();


                if (count > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }



            }


        }
    }
}