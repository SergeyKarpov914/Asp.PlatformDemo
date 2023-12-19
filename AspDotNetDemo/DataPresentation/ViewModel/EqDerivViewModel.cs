using Clio.Demo.DataManagement.Processor.EqD.DataModel;
using Clio.Demo.Domain.Data.EqDeriv;
using Clio.Demo.Util.Telemetry.Seri;
using System.Collections.ObjectModel;

namespace Clio.Demo.DataPresentation.ViewModel
{
    public class EqDerivViewModel
    {
        public ObservableCollection<Account>      Accounts  { get; set; } = new ObservableCollection<Account>();
        public ObservableCollection<OpenPosition> Positions { get; set; } = new ObservableCollection<OpenPosition>();
        public ObservableCollection<TradeBlotter> Trades    { get; set; } = new ObservableCollection<TradeBlotter>();

        public async Task Initialize()
        {
            try
            {
                Accounts.Add(new Account() {MasterCode = "xxxx", DeskCode = "SalesDev", AccountName="BANKINGNSI" });
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
        }
    }
}
