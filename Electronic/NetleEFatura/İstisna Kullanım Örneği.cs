var netleEfatura = new NetleEFatura();

///diğer tamamlayıcı bilgiler

netleEfatura.Tip = NetleEFaturaType.ISTISNA;

///diğer tamamlayıcı bilgiler

var vergiList = new List<Vergi>();
vergiList.Add(
                new Vergi()
                {
                    Tur = TaxCodeContentType.KDVGercek,                    
                    Tutar = 0,
                    MuafiyetKodu = TaxExemptionReasonCodeContentType.ISTISNA301,
                    MuafiyetNedeni = "11/1-a Mal İhracatı"
                });

netleEfatura.Vergiler = vergiList.ToArray();

///diğer tamamlayıcı bilgiler
