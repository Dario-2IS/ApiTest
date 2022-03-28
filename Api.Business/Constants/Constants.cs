using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Constants
{
    public class Constants
    {
        public class ResponseConstants
        {
            public static string Success = "OK.";
            public static string NotFound = "Not Found.";
        }

        public class EntitiesConstants
        {
            public static string Successful = "successfully completed";
        }

        public class MovementsControls
        {
            public static int DailyAmount = 100;
            public static string Debit = "RETIRO";
            public static string Credit = "DEPOSITO";
            public static string NotValid = "Invalid transaction type";
            public static string BalanceError = "Unavailable balance";
            public static string AmountError = "Daily amount exceeded";
        }
    }
}
