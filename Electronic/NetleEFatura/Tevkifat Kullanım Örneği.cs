var netleEfatura = new NetleEFatura();

///diğer tamamlayıcı bilgiler

netleEfatura.Tip = NetleEFaturaType.TEVKIFAT;

///diğer tamamlayıcı bilgiler

netleEfatura.ToplamTutar = 20000;
netleEfatura.VergilerHaricTutar = 20000;
netleEfatura.VergilerDahilTutar = 23600;
netleEfatura.OdenecekToplamTutar = 20360;
netleEfatura.KDVTevkifatTutari = 3240;

///diğer tamamlayıcı bilgiler

var vergiList = new List<Vergi>()
{
    new Vergi()
    {
    Tur = TaxCodeContentType.KDVGercek,
    Oran = 18,
    Tutar = 3600,
    Matrah = 20000
    },
    new Vergi()
    {
    Tur = TaxCodeContentType.TEVKIFAT606,
    Oran = 90,
    Tutar = 3240               
    }
};

///diğer tamamlayıcı bilgiler

netleEfatura.Vergiler = vergiList.ToArray();

///diğer tamamlayıcı bilgiler
