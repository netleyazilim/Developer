var netleEfatura = new NetleEFatura();
netleEfatura.Tip = NetleEFaturaType.IHRACKAYITLI;

///diğer tamamlayıcı bilgiler

netleEfatura.ToplamTutar = 20000;
netleEfatura.VergilerHaricTutar = 20000;
netleEfatura.VergilerDahilTutar = 23600;
netleEfatura.OdenecekToplamTutar = 20000;

///diğer tamamlayıcı bilgiler

var vergiList = new List<Vergi>()
{
  new Vergi()
  {
  Tur = TaxCodeContentType.KDVGercek,
  Oran = 18,
  Tutar = 3600,
  MuafiyetKodu = TaxExemptionReasonCodeContentType.IHRACKAYITLI701,
  MuafiyetNedeni = "İhraç Kayıtlı Satışlar"
  }
};

///diğer tamamlayıcı bilgiler

netleEfatura.Vergiler = vergiList.ToArray();

///diğer tamamlayıcı bilgiler
