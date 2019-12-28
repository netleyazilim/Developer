var netleEfatura = new NetleEFatura();

///diğer tamamlayıcı bilgiler

netleEfatura.Tip = NetleEFaturaType.SATIS;
netleEfatura.Senaryo = NetleEFaturaSenaryoType.IHRACAT;

///diğer tamamlayıcı bilgiler

var musteri = new Musteri();
musteri.FirmaAdi = "Gümrük ve Ticaret Bakanlığı";
musteri.Sokak = "Üniversiteler Mahallesi Dumlupınar Bulvarı";
musteri.KapiNo = "151";
musteri.IlceSemt = "Çankaya";
musteri.Il = "Ankara";
musteri.Ulke = "Türkiye";
musteri.PostaKodu = "06800";
musteri.VergiNoTCKimlikNo = "XXXXXXXXXX";
musteri.VergiDairesi = "Ulus";
netleEfatura.Musteri = musteri;


var aliciMusteri = new AliciMusteri();
aliciMusteri.FirmaAdi = "İthalat yapan yabancı kurumun ünvanı";
aliciMusteri.IlceSemt = "-";
aliciMusteri.Il = "İthalat yapan yabancı kurumun ili";
aliciMusteri.Ulke = "İthalat yapan yabancı kurumun ülkesi";
aliciMusteri.TarafTuru = "EXPORT";
aliciMusteri.KurumResmiUnvan = "İthalat yapan yabancı kurumun ünvanı";
aliciMusteri.KurumKayitNumarasi = "İthalat yapan yabancı kurumun kayit numarasi";
aliciMusteri.VergiDairesi = "VAT";
aliciMusteri.VergiTipiKodu = "VAT";
netleEfatura.AliciMusteri = aliciMusteri;

///diğer tamamlayıcı bilgiler


