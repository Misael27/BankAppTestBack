using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTestBack.Application.Common.Options
{
    public class LimitOptions
    {
        public const string Limit = "BusinessLimits";

        public double MaxDailyWithdrawal { get; set; }
    }
}
