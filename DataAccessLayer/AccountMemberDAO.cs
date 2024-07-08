using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AccountMemberDAO
    {


        public static List<AccountMember> GetAccounts()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.AccountMembers.ToList();

        }


        public static AccountMember? GetAccountByEmail(string email)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.AccountMembers.FirstOrDefault(x => x.Email == email);

        }

        public static AccountMember GetAccountById(int id)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.AccountMembers.Find(id);
        }

        public static void UpdateAccount(AccountMember account)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.AccountMembers.Update(account);
            context.SaveChanges();
        }

        public static void CreateAccount(AccountMember account)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.AccountMembers.Add(account);
            context.SaveChanges();
        }
    }
}
