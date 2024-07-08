using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountMemberRepository : IAccountMemberRepository
    {
        public void CreateAccount(AccountMember accountMember)
        {
            AccountMemberDAO.CreateAccount(accountMember);
        }

        public AccountMember? GetAccountByEmail(string email)
        {
            return AccountMemberDAO.GetAccountByEmail(email);
        }

        public AccountMember? GetAccountById(int id)
        {
            return AccountMemberDAO.GetAccountById(id);
        }

        public List<AccountMember> GetAllAccounts()
        {
            return AccountMemberDAO.GetAccounts();
        }

        public void UpdateAccount(AccountMember accountMember)
        {
            AccountMemberDAO.UpdateAccount(accountMember);
        }
    }
}
