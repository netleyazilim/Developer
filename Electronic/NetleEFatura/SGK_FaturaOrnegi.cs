var netleEfatura = new NetleEFatura();
netleEfatura.Aciklama = "Test NETLEEFATURA SGK";
netleEfatura.DuzenlenmeTarihi = DateTime.Today;

//......... diğer alanlar eklenmeli
//SGK türü ataması yapılmalıdır
netleEfatura.Tip = NetleEFaturaType.SGK;

//......... diğer alanlar eklenmeli

//SGK faturalarına özel ek alanlar doldurulmalıdır.
netleEfatura.SGKOzelFaturaAlanlari = new SGKOzelFaturaAlanlari()
{
    IlaveFaturaTipi = SGKIlaveFaturaTipi.SAGLIK_ECZANE,
    MukellefKodu = "MK_1234",
    MukellefAdi = "MA_ABC",
    DosyaNo = "DN_5678",
    FaturaDonemi = new FaturaDonemi() 
    {
        BaslangicTarihi = new DateTime(2017, 6, 1),
        BitisTarihi = new DateTime(2017, 6, 30)
    }
};


//......... diğer alanlar eklenmeli
