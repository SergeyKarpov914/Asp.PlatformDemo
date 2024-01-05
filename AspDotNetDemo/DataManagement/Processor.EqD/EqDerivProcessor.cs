using Clio.Demo.Core.Lib.Extension;
using Clio.Demo.Core.Lib.Util;
using Clio.Demo.DataManagement.Processor.EqD.DataModel;
using Clio.Demo.Domain.Data.EqDeriv;

namespace Clio.Demo.DataManagement.Processor.EqD
{
    public sealed class EqDerivProcessor
    {
        #region c-tor

        private readonly IAccountData      _accountData;
        private readonly IOpenPositionData _positionData;
        private readonly ITradeBlotterData _tradeData;

        public EqDerivProcessor(IAccountData accountData, IOpenPositionData positionData, ITradeBlotterData tradeData)
        {
            accountData .Inject(out _accountData);
            positionData.Inject(out _positionData);
            tradeData   .Inject(out _tradeData);
        }

        #endregion c-tor

        public async Task<IEnumerable<Account>> GetAccounts(IEnumerable<string> ids = null)
        {
            IEnumerable<Account> accounts = null;
            try
            {
                accounts = await _accountData.Read(ids?.ToInClause("[MASTERCODE]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return accounts;
        }

        public async Task<IEnumerable<OpenPosition>> GetPositions(IEnumerable<string> ids = null)
        {
            IEnumerable<OpenPosition> positions = null;
            try
            {
                positions = await _positionData.Read(ids?.ToInClause("[MASTERCODE]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return positions;
        }

        public async Task<IEnumerable<TradeBlotter>> GetTrades(IEnumerable<string> ids = null)
        {
            IEnumerable<TradeBlotter> trades = null;
            try
            {
                trades = await _tradeData.Read(ids?.ToInClause("[CLIENTID]"));
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
            return trades;
        }
    }
}
