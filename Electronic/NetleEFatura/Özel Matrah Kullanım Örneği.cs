var netleEfatura = new NetleEFatura();

///diğer tamamlayıcı bilgiler

netleEfatura.Tip = NetleEFaturaType.OZELMATRAH;

///diğer tamamlayıcı bilgiler

var vergiList = new List<Vergi>();
vergiList.Add(
  new Vergi()
  {
  Tur = TaxCodeContentType.KDVGercek,                    
  Tutar = 0,
  MuafiyetKodu = TaxExemptionReasonCodeContentType.OZELMATRAH807,
  MuafiyetNedeni = "Gazete, dergi ve benzeri periyodik yayınlar"
  }
);

///diğer tamamlayıcı bilgiler

netleEfatura.Vergiler = vergiList.ToArray();

///diğer tamamlayıcı bilgiler
