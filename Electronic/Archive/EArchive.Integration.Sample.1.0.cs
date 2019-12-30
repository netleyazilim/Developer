    /// <summary>
    /// e-fatura ve e-arşiv web servis entegrasyonu için örnek ve test kodlarını içermektedir.
    /// <remarks>Bu tasarım desenleri ya da kodlar sadece "Netle Yazılım" tarafında sağlanan çözümlerde kullanılabilir.</remarks>
    /// </summary>
    public static class EArsivSenaryosu
    {
        /// <summary>
        /// Senaryo 1
        /// •   Token oluşturma. 
        /// •	NetleEFatura formatında bir e-arşiv hazırlama.  
        /// •	Faturayı web servise gönderme.  
        /// •	Faturayı indirme. 
        /// •	Faturanın html’ini alma. 
        /// </summary>
        public static void PrepareElectronicArchiveByNetleEFatura()
        {
            Integration10 client = new Integration10();

            client.Url = @" https://efaturatest.inposia.com.tr/CommonInvoice.Web.Service/Integration10.asmx";
            var token = client.CreateUserToken(@"UserName", "Password", ModuleType.eArchive);
            var nef = new NetleEFatura();

            #region [  invoice header  ]
            var uniqueId = Guid.NewGuid(); nef.GUID = uniqueId.ToString();
            nef.Tedarikci = new Tedarikci() // sender information 
            {
                FirmaAdi = "Sender Company", // company name 
                VergiNoTCKimlikNo = "1122334456", // company tax number 
                WebAdresi = "www.inposia.com.tr", // web address 
                Il = "izmir", // city 
                IlceSemt = "konak", //district 
                Ulke = "Türkiye", //country 
                PostaKodu = "35000", // postal code 
                VergiDairesi = "konak" // tax office 
            };

            nef.Musteri = new Musteri() // receiver / customer information 
            {

                Alias = "urn:mail:1234567801_1_pk@inposia.com.tr", // receiver alias 
                FirmaAdi = "Receiver Company", // company name 
                VergiNoTCKimlikNo = "1623427874", // company tax number 
                WebAdresi = "www.inposia.com.tr", // web adress 
                Il = "izmir", // city 
                IlceSemt = "konak", // district 
                Ulke = "Türkiye", // country 
                PostaKodu = "35310", // post code 
                VergiDairesi = "konak" //tax office 

            };

            nef.DuzenlenmeTarihi = DateTime.Now;
            nef.No = "ABC2016000000629";
            nef.DovizTipi = "TRY";
            nef.KDVTutari = 36; nef.ToplamTutar = 200;
            nef.OdenecekToplamTutar = 236;
            nef.Tip = NetleEFaturaType.SATIS;
            nef.Senaryo = NetleEFaturaSenaryoType.TEMELFATURA;

            #endregion

            #region [  items  ]

            nef.FaturaKalemleri = new FaturaKalemi[] 
            { 
                new FaturaKalemi() { 
                    Aciklama="energry drink", //description 
                    Miktar=2, // quantity 
                    MusteriStokNo="MusStok001", // customer stock code 
                    TedarikciStokNo="TedStok001", // supplier stock code 
                    UreticiStokNo="UretStok001", // manufacturer stock code 
                    BirimFiyat=100, // unit price 
                    ToplamTutar=200, // total amount 
                    KDVOrani=18, //vat rate 
                    KDVTutari=36, // vat amount 
                    StokAdi="energy drink", // item name 
                    Birim="NIU",  // unit 
                    DovizTipi="TRY" // currency 
                     
                } 
            };

            #endregion

            var uuid = Guid.NewGuid();
            client.UploadInvoice(token, uuid, "", nef);
            var di = client.GetEArchiveStatus(token, uuid);
            Console.WriteLine("Status : " + di.OK);
            var html = client.GetCommonDocumentHtmlByUUID(token, uuid);
        }
    }
