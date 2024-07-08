using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAccountMemberRepository
    {
        List<AccountMember> GetAllAccounts();

        AccountMember? GetAccountByEmail(string email);
        AccountMember? GetAccountById(int id);
        void UpdateAccount(AccountMember accountMember);
        void CreateAccount(AccountMember accountMember);


    }
}
