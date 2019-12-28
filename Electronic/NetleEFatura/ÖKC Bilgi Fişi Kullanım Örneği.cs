var netleEfatura = new NetleEFatura();

///diğer tamamlayıcı bilgiler

netleEfatura.Aciklama = "TestNETLEEFATURA OKC";
netleEfatura.DuzenlenmeTarihi = DateTime.Today;
netleEfatura.Tip = NetleEFaturaType.SATIS;

///diğer tamamlayıcı bilgiler

var okcBilgiFisleri = new List<OKCBilgiFisi>();
okcBilgiFisleri.Add(
    new OKCBilgiFisi() 
    { 
        No = "OKCNo201600001",
        Tarih = DateTime.Now,
        Tip = OKCBilgiFisiTipi.E_FATURA_IRSALIYE,
        OKCSeriNo = "OKCSeriNo20161",
        ZRaporuNo = "ZRaporuNo20161"                    
    }
);

okcBilgiFisleri.Add(
    new OKCBilgiFisi()
    {
        No = "OKCNo201600002",
        Tarih = new DateTime(2016, 4, 12),
        Tip = OKCBilgiFisiTipi.E_FATURA,
        OKCSeriNo = "OKCSeriNo20162",
        ZRaporuNo = "ZRaporuNo20162"                    
    }
);

netleEfatura.OKCBilgiFisleri = okcBilgiFisleri.ToArray();

///diğer tamamlayıcı bilgiler
