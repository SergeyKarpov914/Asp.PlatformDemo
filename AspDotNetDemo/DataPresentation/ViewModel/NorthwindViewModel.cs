using Clio.Demo.Domain.Data.Northwind;
using Clio.Demo.DataPresentation.Elements;
using Clio.Demo.DataPresentation.Gateway;
using Clio.Demo.Extension;
using Clio.Demo.Util.Telemetry.Seri;
using System.Collections.ObjectModel;

namespace Clio.Demo.DataPresentation.ViewModel
{
    public class NorthwindViewModel
    {
        private readonly NorthwindGateway _gateway;

        public NorthwindViewModel(NorthwindGateway gateway)
        {
            gateway.Inject(out _gateway);
        }
        public ObservableCollection<OrderElement> Orders { get; set; }
        public ObservableCollection<DealElement> Deals { get; set; }

        
        public int SelectedIndex 
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; getOrderDeals(); } 
        }
        private int _selectedIndex = 0;

        public async Task Initialize()
        {
            try
            {
                Orders = new ObservableCollection<OrderElement>((await _gateway.RetrieveData<Order>())/*.Where(x => x.OrderID < 10)*/.Select(x => new OrderElement(x)));
                getOrderDeals();
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
        }

        private void getOrderDeals()
        {
            try
            {
                if (Deals is null)
                {
                    Deals = new ObservableCollection<DealElement>();
                }
                IEnumerable<DealElement> deals = Orders[SelectedIndex].Deals.Select(z => new DealElement(z));

                Deals.Clear();

                foreach (DealElement deal in deals)
                {
                    Deals.Add(deal);
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex);
            }
        }
    }
}
