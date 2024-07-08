using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountMemberService
    {
        List<AccountMember> GetAllAccounts();
        AccountMember? GetAccountByEmail(string email);
        AccountMember? GetAccountById(int id);
        void UpdateAccount(AccountMember accountMember);
        void CreateAccount(AccountMember accountMember);
        List<AccountMember> FindByFullName(string fullName);
        List<AccountMember> FindByRole(string Role);
    }
}
