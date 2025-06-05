using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static AccountMember getAccountById(string accountId)
        {
            AccountMember accountMember = new AccountMember();
            if(accountId.Equals("PS0001")) // just for demonstration
            {
                accountMember.MemberId = accountId;
                accountMember.MemberPassword = "@1";
                accountMember.MemberRole = 1;
            }
            return accountMember;
        }
    }
}
