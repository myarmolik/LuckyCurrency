﻿using LuckyCurrency.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Data.Repositories
{
    class AccountRepository : IDataService<Account>
    {
        private ApplicationContext _db;

        public AccountRepository(ApplicationContext db)
        {
            _db = db;
        }
        public Account Insert(Account entity)
        {
            return _db.Accounts.Add(entity);
        }

        public bool Delete(int id)
        {
            bool flag = false;

            var entity = _db.Accounts.Find(id);
            if (entity != null)
            {
                _db.Accounts.Remove(entity);
                flag = true;
            }

            return flag;
        }

        public Account GetById(int id)
        {
            return _db.Accounts.Find(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return _db.Accounts;
        }

        public Account Update(Account oldEntity, Account entity)
        {
            var dest = _db.Accounts.Find(oldEntity);

            dest.API_Key = entity.API_Key;
            dest.User = entity.User;

            return dest;
        }
    }
}
