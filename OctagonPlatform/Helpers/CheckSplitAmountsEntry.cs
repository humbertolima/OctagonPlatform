using OctagonPlatform.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace OctagonPlatform.Helpers
{
    public static class CheckSplitAmountsEntry
    {
        
        public static Exception CheckSurcharge(int terminalId, int surchargeId, double splitAmountFee)
        {
            try
            {


                var context = new ApplicationDbContext();
                var terminal = context.Terminals.Where(x => x.Id == terminalId)
                    .Include(x => x.Surcharges).SingleOrDefault();
                if (terminal == null) throw new Exception("Terminal not found. ");

                var terminalAmountFee = terminal.SurchargeAmountFee;
                var terminalPercentFee = terminal.SurchargePercentageFee;

                var countFee = splitAmountFee;
                var countPercentFee = 0.00;
                foreach (var item in terminal.Surcharges)
                {
                    if (item.Id != surchargeId)
                    {
                        countFee += item.SplitAmount;
                        countPercentFee += item.SplitAmountPercent;
                    }

                }

                if(countFee > terminalAmountFee)
                    throw new Exception("Surcharge amount fee biger that the remaining fee. ");
                if (countPercentFee > 100.00)
                    throw new Exception("Surcharge percent fee biger that the remaining percent fee. ");

                return new Exception("Ok");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Please check the entered values. ");
            }
        }

        public static Exception CheckInterChange(int terminalId, int interchangeId, double splitAmountFee)
        {
            try
            {


                var context = new ApplicationDbContext();
                var terminal = context.Terminals.Where(x => x.Id == terminalId)
                    .Include(x => x.InterChanges).SingleOrDefault();
                if (terminal == null) throw new Exception("Terminal not found. ");

                var countFee = splitAmountFee + terminal.InterChanges.Where(item => item.Id != interchangeId).Sum(item => item.SplitAmount);

                if (countFee > InterchangeConstants.ClientInterchangeAmount)
                    throw new Exception("Split amount fee biger that the remaining fee. ");

                return new Exception("Ok");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Please check the entered values. ");
            }
        }
    }
}