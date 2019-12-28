var netleEfatura = new NetleEFatura();

///diğer tamamlayıcı bilgiler

netleEfatura.Tip = NetleEFaturaType.SATIS;
netleEfatura.Senaryo = NetleEFaturaSenaryoType.YOLCUBERABERFATURA;

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
aliciMusteri.IlceSemt = "-";
aliciMusteri.Il = "Turist il";
aliciMusteri.Ulke = "Turist ülke";
aliciMusteri.TarafTuru = "TAXFREE";
aliciMusteri.Sahis = new Sahis()
{
    Ad = "Turist Ad",
    Soyad = "Turist Soyad ",
    Uyruk = "Turist uyruk",
    Pasaport = new Pasaport() { No = "Turist pasaport no", Tarih = DateTime.Now },
    BankaHesap = new BankaHesap()
    {
        No = "Turist banka hesap no",
        Aciklama = "Turist banka hesap açıklama",
        SubeAdi = "Turist banka hesap şube adı",
        BankaAdi = "Turist banka hesap banka adı"
    }
};
netleEfatura.AliciMusteri = aliciMusteri;

///diğer tamamlayıcı bilgiler

var vergiTemsilcisi = new VergiTemsilcisi();
vergiTemsilcisi.AraciKurumVergiNo = "1231231231";
vergiTemsilcisi.AraciKurumEtiket = "defaultpk";
vergiTemsilcisi.FirmaAdi = "Aracı kurum Unvanı";
vergiTemsilcisi.IlceSemt = "-";
vergiTemsilcisi.Il = "Kurum İl";
vergiTemsilcisi.Ulke = "Kurum Ülke";
netleEfatura.VergiTemsilcisi = vergiTemsilcisi;

///diğer tamamlayıcı bilgiler
