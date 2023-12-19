using Clio.Demo.DataManagement.Processor.EqD;
using Clio.Demo.Domain.Data.EqDeriv;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.Seri;
using System.Collections.ObjectModel;

namespace Clio.Demo.DataPresentation.ViewModel
{
    public class EqDerivViewModel
    {
        public IEnumerable<Account>      Accounts  { get; set; }
        public IEnumerable<OpenPosition> Positions { get; set; }
        public IEnumerable<TradeBlotter> Trades    { get; set; }

        private readonly EqDerivProcessor _processor;

        public EqDerivViewModel(EqDerivProcessor processor)
        {
            processor.Inject<EqDerivProcessor>(out _processor);
        }

        public async Task Initialize()
        {
            try
            {
                Accounts = await _processor.GetAccounts();
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
        }

        public async Task ProcessSelection(IEnumerable<string> accounts)
        {
            try
            {
                Positions = await _processor.GetPositions(accounts);
                Trades    = await _processor.GetTrades(accounts);
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
        }
    }
}
