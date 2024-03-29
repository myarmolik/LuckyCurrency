﻿using LuckyCurrency.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Data.Repositories
{
    class UserRepository : IDataService<User>
    {
        private ApplicationContext _db;

        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }
        public User Insert(User entity)
        {
            return _db.Users.Add(entity);
        }

        public bool Delete(int id)
        {
            bool flag = false;

            var entity = _db.Users.Find(id);
            if (entity != null)
            {
                _db.Users.Remove(entity);
                flag = true;
            }

            return flag;
        }

        public User GetById(int id)
        {
            return _db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User Update(User oldEntity, User entity)
        {
            var dest = _db.Users.Find(oldEntity);

            dest.Login = entity.Login;
            dest.Password = entity.Password;
            dest.Account = entity.Account;

            return dest;
        }

        public API_Key GetAPI_Key(string login, string password)
        {
            SqlParameter loginP = new SqlParameter("@login", login);
            SqlParameter passwordP = new SqlParameter("@password", password);

            return _db.Database.SqlQuery<API_Key>("SELECT * FROM GetAPI_Key (@login, @password)", loginP, passwordP).FirstOrDefault();
        }
    }
}
