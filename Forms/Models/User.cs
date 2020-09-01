using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataBase;

namespace Forms.Models
{
    public class User
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAuthentic()
        {
            db d1 = new db();
            System.Data.DataSet ds=  d1.Read(string.Format("select * from users where email='{0}' and password='{1}'",Email,Password));
            if (ds.Tables[0].Rows.Count > 0)
            {
                Name = ds.Tables[0].Rows[0][1].ToString();
                Email = ds.Tables[0].Rows[0][2].ToString();
                return true;
            }
            else
                return false;
        }
        public void Register()
        {
            db d1 = new db();
            SqlParm s1 = new SqlParm();
            s1.Add("name", Name);
            s1.Add("email", Email);
            s1.Add("pass", Password);

            d1.ExecuteQuerry(@"

INSERT INTO [dbo].[users]
           (
            [Name]
           ,[email]
           ,[password])
     VALUES
           (@name,@email,@pass);

",s1.GetParmList());
        }
    }
}