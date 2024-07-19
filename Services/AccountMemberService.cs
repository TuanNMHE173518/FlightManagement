using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountMemberService : IAccountMemberService
    {
        private readonly IAccountMemberRepository accountMemberRepository;

        public AccountMemberService()
        {
            accountMemberRepository = new AccountMemberRepository();
        }

        public void CreateAccount(AccountMember accountMember)
        {
            accountMemberRepository.CreateAccount(accountMember);
        }

        public List<AccountMember> FindByFullNameAndRole(string fullName, string role)
        {
            var foundAccount = GetAllAccounts().Where(a => a.FullName.ToLower().Contains(fullName.ToLower()) && (string.IsNullOrEmpty(role) || a.Role.Equals(role)));

            return foundAccount.ToList();
        }

        public List<AccountMember> FindByRole(string Role)
        {
            var foundAccount = GetAllAccounts().Where(a => a.Role.Equals(Role)).ToList();

            return foundAccount;
        }

        public AccountMember? GetAccountByEmail(string email)
        {
            return accountMemberRepository.GetAccountByEmail(email);
        }

        public AccountMember? GetAccountById(int id)
        {
            return accountMemberRepository.GetAccountById(id);
        }

        public List<AccountMember> GetAllAccounts()
        {
            return accountMemberRepository.GetAllAccounts();
        }

        public void UpdateAccount(AccountMember accountMember)
        {
            accountMemberRepository.UpdateAccount(accountMember);
        }
    }
}
