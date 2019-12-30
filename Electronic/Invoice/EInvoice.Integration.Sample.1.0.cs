using MPIP.CommonServis.TestClient.CommonWS;
using System;

namespace MPIP.CommonServis.TestClient
{
    #region [  EFaturaSenaryosu  ]
    
    /// <summary>
    /// e-fatura ve e-arşiv web servis entegrasyonu için örnek ve test kodlarını içermektedir.
    /// <remarks>Bu tasarım desenleri ya da kodlar sadece "Netle Yazılım" tarafında sağlanan çözümlerde kullanılabilir.</remarks>
    /// </summary>
    public static class EFaturaSenaryosu
    {
        /// <summary>
        /// Senaryo 1
        /// • Token oluşturma. 
        /// •	NetleEFatura formatında bir e-fatura hazırlama.  
        /// •	Faturayı web servise gönderme.  
        /// •	Faturayı indirme. 
        /// •	Faturanın html’ini alma. 
        /// </summary>
        public static void PrepareElectronicInvoiceByNetleEFatura()
        {
            Integration10 client = new Integration10();
            client.Url = @"https://efaturatest.inposia.com.tr/CommonInvoice.Web.Service/Integration10.asmx";
            var token = client.CreateUserToken(@"UserName", "Password", ModuleType.eInvoice);

            var nef = new NetleEFatura();

            #region [  invoice header  ]

            var uniqueId = Guid.NewGuid();
            nef.GUID = uniqueId.ToString();
            nef.Tedarikci = new Tedarikci() // sender information 
            {
                FirmaAdi = "Sender Company", // company name 
                VergiNoTCKimlikNo = "1234567801", // company tax number  
                WebAdresi = "www.inposia.com.tr", // web address 
                Il = "İzmir", // city  
                IlceSemt = "Konak", // district 
                Ulke = "Türkiye", // country 
                PostaKodu = "35000", // postal code 
                VergiDairesi = "konak" // tax office 
            };

            nef.Musteri = new Musteri() // receiver / customer information 
            {

                Alias = "urn:mail:1234567801_1_pk@inposia.com.tr", // receiver alias 
                FirmaAdi = "Receiver Company", // company name 
                VergiNoTCKimlikNo = "1234567801", // company tax number 
                WebAdresi = "www.inposia.com.tr", //web address 
                Il = "İzmir", // city 
                IlceSemt = "Konak", // district 
                Ulke = "Türkiye", // country 
                PostaKodu = "35310", //post code 
                VergiDairesi = "konak" // tax office  
            };

            nef.DuzenlenmeTarihi = DateTime.Now; // invoice date 

            nef.No = "ABC2016000000915"; // invoice number 
            nef.DovizTipi = "TRY"; // currency                 nef.KDVTutari = 36; // vat amount                 nef.ToplamTutar = 200; // total amount 
            nef.OdenecekToplamTutar = 236; // payable amount 
            nef.Tip = NetleEFaturaType.SATIS; // invoice type 
            nef.Senaryo = NetleEFaturaSenaryoType.TEMELFATURA; //invoice scenario 

            #endregion

            #region [  items  ]

            nef.FaturaKalemleri = new FaturaKalemi[] // invoice items 
            { 
                new FaturaKalemi() { 
                    Aciklama="energry drink", // description 
                    Miktar=2, // quantity 
                    MusteriStokNo="MusStok001", // customer stock code 
                    TedarikciStokNo="TedStok001", // supplier stock code 
                    UreticiStokNo="UretStok001", // manufacturer stock code 
                    BirimFiyat=100, // unit price 
                    ToplamTutar=200, // total amount 
                    KDVOrani=18, // vat rate 
                    KDVTutari=36, // vat amount 
                    StokAdi="energy drink", // item name 
                    Birim="NIU", // unit 
                    DovizTipi="TRY" // currency 
                     
                } 
            };

            #endregion

            var uuid = Guid.NewGuid();

            client.UploadInvoice(token, uuid, "", nef);

            var di = client.GetEInvoiceDocument(token, new DocumentKey[] { new DocumentKey { Id = uuid } });

            Console.WriteLine("Invoice status queried : " + di[0].Code + " " + di[0].Detail);

            var html = client.GetCommonDocumentHtmlByUUID(token, uuid);
        }
    }

    #endregion
}
