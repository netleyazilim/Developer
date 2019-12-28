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
                    MuafiyetKodu = TaxExemptionReasonCodeContentType.KISMIISTISNA201,
                    MuafiyetNedeni = "17/1 Kültür ve Eğitim Amacı Taşıyan İşlemler"
                });

netleEfatura.Vergiler = vergiList.ToArray();

///diğer tamamlayıcı bilgiler
