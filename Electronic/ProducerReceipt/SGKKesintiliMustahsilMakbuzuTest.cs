using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;

namespace MPIP.CommonServis.TestClient
{
    #region [  SGKKesintiliMustahsilMakbuzuTest  ]

    /// <summary>
    /// Stopajlı ve SGK prim kesintili Elektronik Mustahsil Makbuzu (e-mm) için örnek içermektedir.

    /// <remarks>Bu tasarım desenleri ya da kodlar sadece "Netle Yazılım" tarafında sağlanan çözümlerde kullanılabilir.</remarks>
    /// </summary>
    public static class SGKKesintiliMustahsilMakbuzuTest
    {
        
        public static CommonWS.NetleEFatura PrepareTestEProducerReceipt()
        {
            var nef = new CommonWS.NetleEFatura();
            nef.KaynakDokumanTuru = CommonWS.KaynakDokumanTuru.EMUSTAHSIL;

            #region [  invoice header  ]

            nef.GUID = Guid.NewGuid().ToString();
            nef.Tip = CommonWS.NetleEFaturaType.SATIS;
            nef.Senaryo = CommonWS.NetleEFaturaSenaryoType.TEMELFATURA;
            nef.No = "NMM2018000000087";
            nef.Aciklama = "Testnef";
            nef.EkAciklamalar = new string[] { "Aciklama 2", "Aciklama 3", "Aciklama 4" };
            nef.DuzenlenmeTarihi = DateTime.Today;
            nef.DovizTipi = "TRY";

            var tedarikci = new CommonWS.Tedarikci();
            tedarikci.FirmaAdi = "AAA Anonim Şirketi";
            tedarikci.Sokak = "Papatya Caddesi Yasemin Sokak";
            tedarikci.BinaAdi = "Manolya Apartmanı";
            tedarikci.KapiNo = "21";
            tedarikci.IlceSemt = "Beşiktaş";
            tedarikci.Il = "İstanbul";
            tedarikci.Ulke = "Türkiye";
            tedarikci.PostaKodu = "34100";
            tedarikci.VergiNoTCKimlikNo = "9999999999";
            tedarikci.VergiDairesi = "Büyük Mükellefler";
            tedarikci.Telefon = "(212) 925 5151";
            tedarikci.Fax = "(212) 925 5050";
            tedarikci.Eposta = "aa@aaa.com.tr";
            tedarikci.WebAdresi = "http://www.aaa.com.tr";
            nef.Tedarikci = tedarikci;

            var musteri = new CommonWS.Musteri();
            musteri.FirmaAdi = "Ahmet Tekin";
            musteri.VergiNoTCKimlikNo = "98765432101";
            musteri.Sokak = "Kültür Mahallesi";
            musteri.IlceSemt = "Konak";
            musteri.Il = "İzmir";
            musteri.Ulke = "Türkiye";
            musteri.Telefon = "(232) 444 12 34";
            musteri.Fax = "(232) 444 12 30";
            musteri.Eposta = "bb@bbb.com.tr";
            musteri.WebAdresi = "http://www.bbb.com.tr";
            nef.Musteri = musteri;

            #endregion

            #region [  items  ]

            var faturaKalemleri = new List<CommonWS.FaturaKalemi>();
            faturaKalemleri.Add(
                new CommonWS.FaturaKalemi()
                {
                    StokAdi = "SÜT",
                    Birim = "LTR",
                    DovizTipi = "TRY",
                    BirimFiyat = 2,
                    Miktar = 100,
                    ToplamTutar = 200
                });
            nef.FaturaKalemleri = faturaKalemleri.ToArray();

            #endregion

            #region [  tax area  ]

            var vergiList = new List<CommonWS.Vergi>();
            vergiList.Add(
                new CommonWS.Vergi()
                {
                    Tur = CommonWS.TaxCodeContentType.SGK_PRIM,
                    Oran = 1,
                    Matrah = 200,
                    Tutar = 2
                });

            vergiList.Add(
                new CommonWS.Vergi()
                {
                    Tur = CommonWS.TaxCodeContentType.Item0003,
                    Oran = 2,
                    Matrah = 200,
                    Tutar = 4
                });

            nef.Vergiler = vergiList.ToArray();

            #endregion

            #region [  legalmonetryfields    ]

            nef.ToplamTutar = 200;

            nef.VergilerHaricTutar = 194;

            nef.VergilerDahilTutar = 200;

            nef.OdenecekToplamTutar = 194;

            #endregion

            return nef;
        }

    }

    #endregion

}
