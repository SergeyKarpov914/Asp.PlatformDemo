using System;
using System.Collections.Generic;

namespace TableInsertGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> inserts = new List<string>();

            Dictionary<string, string> clients = new Dictionary<string, string>();
            
            for (int x = 0; x < Clients.Length; x++)
            {
                string account = AcctCode(x);
                string client = Clients[x];

                inserts.Add($"INSERT INTO [dbo].[Account] VALUES ('{account}',NULL,'{client}','A','{Date()}')");
                clients.Add(account, client);
            }
            File.WriteAllLines("../../../accounts.txt", inserts);

            
            
            inserts.Clear();

            for (int x = 0; x < 500; x++)
            {
                string account = AcctCode();
                string client  = clients[account];
                
                inserts.Add($"INSERT INTO [dbo].[OpenPosition] VALUES ('{account}',{TradeDate()},'{Und()}',{Qty()},{Exp()},{Price()},'{PC()}',{Price()},NULL,NULL,'{Date()}',{TradeDate()},{TradeDate()},'O','C')");
            }
            File.WriteAllLines("../../../position.txt", inserts);

            inserts.Clear();

            for (int x = 0; x < 500; x++)
            {
                string account = AcctCode();
                string client = clients[account];

                inserts.Add($"INSERT INTO [dbo].[TradeBlotter] VALUES ({TradeDate()},'{account}','{client}','{Side()}',{Qty()},'{Und()}',{Exp()},{Price()},'{PC()}',{Price()},NULL)");
            }
            File.WriteAllLines("../../../blotter.txt", inserts);

            inserts.Clear();

            for (int x = 0; x < Tickers.Length; x++)
            {
                inserts.Add($"INSERT INTO [dbo].[PositionRisk] VALUES ({TradeDate()},'{Tickers[x]}',{Price(100)},{Price(100)},{Price(100)},{Price(100)},{Price(100)},{Price(100)},{Price(100)})");
            }
            File.WriteAllLines("../../../risk.txt", inserts);
        }

        static string AcctCode(int next)
        {
            return $"M00{next+100}";
        }

        static string AcctCode()
        {
            Random rnd = new Random();
            int code = rnd.Next(100, 100 + Clients.Length - 1);

            return $"M00{code.ToString("D3")}";
        }

        static string Date()
        {
            Random rnd = new Random();
            int days = rnd.Next(-500, 0);

            return DateTime.Now.AddDays(days).ToString("yyyy-MM-dd HH:mm:ss");
        }

        static int TradeDate()
        {
            Random rnd = new Random();
            DateTime date = DateTime.Now.AddDays(rnd.Next(-100, 0));

            return date.Year*10000 + date.Month*100 + date.Day;
        }

        static int Exp()
        {
            Random rnd = new Random();
            DateTime date = DateTime.Now.AddDays(rnd.Next(10, 1000));

            return date.Year * 10000 + date.Month * 100 + date.Day;
        }

        static string Side()
        {
            Random rnd = new Random();
            return Sides[rnd.Next(0, 2)];
        }

        static string PC()
        {
            Random rnd = new Random();
            return PutCall[rnd.Next(0, 2)];
        }

        static string Und()
        {
            Random rnd = new Random();
            return Tickers[rnd.Next(0, Tickers.Length-1)];
        }

        static int Qty()
        {
            Random rnd = new Random();
            int qty = rnd.Next(2, 3000);

            return qty;
        }

        static double Price(int lo = 10)
        {
            Random rnd = new Random();
            double prc = Math.Round((double)rnd.Next(lo, lo * 1000) / 5.0, 4);

            return prc;
        }

        static string[] PutCall =
        {
            "P" ,
            "C",
            "S"
        };

        static string[] Sides =
        { 
            "Sold" ,
            "Bot",
            "Sell Short"
        };
        
        static string[] Tickers =
        {
            "HADI",
            "COP",
            "LTHM",
            "WM",
            "IWM",
            "IOVA",
            "JPM",
            "ADSK",
            "OMF",
            "BABA",
            "UNH",
            "HOU",
            "WDC",
            "AMAT",
            "VHT",
            "PFQ",
            "UN",
            "TTD",
            "COTY",
            "GH",
            "INSP",
            "IR",
            "BKR",
            "MGM",
            "VLY",
            "ELAN",
            "XYL",
            "CAT",
            "PXD",
            "PHH",
            "ETN",
            "SHW",
            "SU3",
            "BLDR",
            "DXJ",
            "STEM",
            "K",
            "LLY",
            "TSHA",
            "FSLY",
            "THV",
            "AZN",
            "XEL",
            "AZEK",
            "AHHY",
            "MNTS",
            "TLT",
            "NOC",
            "VALE",
            "FLH",
            "XRX",
            "DE",
            "MHTX",
            "ZTO",
            "J",
            "HOUS",
            "IGT",
            "AAL",
            "AMBA",
            "MTB",
            "DV",
            "AAP",
            "LAZ",
            "SPX",
            "SMO",
            "T",
            "GPN",
            "SOXL",
            "SPR",
            "CMS",
            "BBBY",
            "OSTK",
            "MAT",
            "SHEL",
            "NVO",
            "HLL",
            "COW",
            "PARA",
            "VMW",
            "AAPL",
            "TCOM",
            "NNAC",
            "RBLX",
            "BCYC",
            "HOUR",
            "ARCC",
            "PLUG",
            "XP",
            "IFW",
            "DDD",
            "BP",
        };

        static string[] Clients =
        {
            "BAM",
            "NORTH FOUFITH ASSET MANAGEMENT LP",
            "ODEY ASSET MGMT",
            "UCM PARTNERS",
            "COATUE",
            "PICTON MAHONEY",
            "USAA",
            "WELLS FARGO",
            "NOMURA FLOW JAPAN",
            "ROCKVILLE CAPITAL",
            "JPM",
            "NOMURA LONDON",
            "CSAM LAB",
            "MARINER",
            "LEVEL GLOBAL",
            "SILVER POINT",
            "WELLS CAPITAL MG MT",
            "BLENHEIM CAPITAL",
            "MOG",
            "SOPRANO",
            "TWO SIGMA INVESTMENTS LLC",
            "J GOLDMAN & CO LP",
            "SOUTH FERRY",
            "PKE",
            "FORE RESEARCH",
            "RONIN",
            "CARLSON CAPITAL",
            "CAMBIAR INVESTORS",
            "PARALLAX",
            "LIBRE MAX",
            //"BENGAL",
            //"LA BANQUE POSTALE AM",
            //"WUESTENROT",
            //"1832 ASSET MANAGEMENT LP FKA GCIC LTD",
            //"WINSLOW",
            //"MILENCO",
            //"FARALLON",
            //"HILLTOP",
            //"HIGHLINE CAPITAL MANAGEMENT LLC",
            //"RIVER BIRCH",
            //"TRELLUS",
            //"DLD ASSET MGMT",
            //"EDF MANN",
            //"UNIVERSA",
            //"BREVAN HOWARD",
            //"MFC GLOBAL INV MGMT US LLC",
            //"ELLIOTT MANAGEMENT CORPORATION",
            //"EMCO FI E",
            //"BANCA IMI ITALY",
            //"CO NCORDANC E",
            //"AERIS CAPITAL",
            //"NOMURA CONVEFITS LONDON",
            //"GUGGENHEIM",
            //"SYMMETRY PEAK MGMT",
            //"CAISSE DE DEPOT",
            //"LABFIANCHE",
            //"KEY GROUP",
            //"AP4",
            //"JBT CAPITAL INC",
            //"TI-IOFISTEN JABAS",
            //"CAXT ON",
            //"MAP LE/ROSE LANE",
            //"BESSEMER TRUST",
            //"SCHROEDEFIS",
            //"QUAD CAPITAL",
            //"NOMURA FIXED INCOME",
            //"BNP AM",
            //"AMUNDI",
            //"JPM ASSET MGMT LONDON",
            //"NSL AC 250643",
            //"ALGEBFIIS",
            //"NOMURA PROP LONDON",
            //"METAVAL",
            //"BTIG",
            //"BTG",
            //"MD SASS ASSOCIATES INC.",
            //"FIDELITY IM",
            //"TIMBER HILL",
            //"TAMALPAIS",
            //"XARAF",
            //"HBK",
            //"PIONEERPATH",
            //"WESTCH ESTER CAPITAL",
            //"GALLEON",
            //"CLAFIIUM CAPITAL",
        };   
    }        
}            