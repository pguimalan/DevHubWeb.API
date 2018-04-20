using Dapper;
using DevHubWeb.Data.DataHelpers;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class TransactionRepository : DataManager, ITransactionRepository
    {
        private readonly DevHubContext _context;
        private readonly IOptions<AppSettingsModel> _options;

        public TransactionRepository(DevHubContext context, IOptions<AppSettingsModel> options)
        {
            this._context = context;
            this._options = options;
        }

        public async Task<IEnumerable<TransMiscellaneousForListModel>> Billing_TransMiscellaneous_List(int timeTrackerID)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<TransMiscellaneousForListModel>("dbo.DevHub_Billing_TransMiscellaneous_List",
                   new
                   {
                       @iIntTimeTrackerID = timeTrackerID
                   },
                   commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int?> Billing_TransMiscellaneous_Remove(int tranMisc_Id)
        {
            var rec = await _context.BillingTransMiscellaneous.FindAsync(tranMisc_Id);

            if (rec == null)
                return 0;

            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int?>("dbo.DevHub_Billing_TransMiscellaneous_Remove",
                    new
                    {
                        @iIntTranMisc_Id = tranMisc_Id,
                        @iBitIsPaid = rec.IsPaid
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<int?> Billing_TransMiscellaneous_Set(TransMiscellaneousForCreateUpdate model)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int?>("dbo.DevHub_Billing_TransMiscellaneous_Set",
                    new
                    {
                        @iIntTranMisc_Id = model.TranMisc_Id,
                        @iIntTimeTrackerId = model.TimeTrackerId,
                        @iStrTranMiscDescription = model.TranMiscDescription,
                        @iDecTranMiscAmount = model.TranMiscAmount,
                        @iBitIsPaid = model.IsPaid
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TransOthersForListModel>> Billing_TransOthers_List(int timeTrackerID)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<TransOthersForListModel>("dbo.DevHub_Billing_TransOthers_List",
                   new
                   {
                       @iIntTimeTrackerID = timeTrackerID
                   },
                   commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int?> Billing_TransOthers_Remove(int tranOtherID)
        {
            var rec = await _context.BillingTransOthers.FindAsync(tranOtherID);

            if (rec == null)
                return 0;

            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int?>("dbo.DevHub_Billing_TransOthers_Remove",
                    new
                    {
                        @iIntTranOtherID = tranOtherID,
                        @iBitIsPaid = rec.IsPaid
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<int?> Billing_TransOthers_Set(TransOthersForCreateUpdate model)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int?>("dbo.DevHub_Billing_TransOthers_Set",
                    new
                    {
                        @iIntTranOtherID = model.TranOtherID,
                        @iIntTimeTrackerId = model.TimeTrackerId,
                        @iIntProductID = model.ProductID,
                        @iDecSRP = model.SRP,
                        @iDecQuantity = model.Quantity,
                        @iBitIsPaid = model.IsPaid
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TransBillingSummaryTotalModel>> DevHub_BillingSummary_Get(int timeTrackerID)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<TransBillingSummaryTotalModel>("dbo.DevHub_BillingSummary_Get",
                   new
                   {
                       @iIntTimeTrackerID = timeTrackerID
                   },
                   commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int?> DevHub_Billing_OverallTransaction_Set(TransBillingModel model)
        {  
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int?>("dbo.DevHub_Billing_OverallTransaction_Set",
                    new
                    {
                        @iIntTimeTrackerId = model.TimeTrackerId,
                        @iDecTotalBill = model.TotalBill,
                        @iDecAmountPaid = model.AmountPaid,
                        @iStrCashierUser = model.UserName
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }
    }
}
