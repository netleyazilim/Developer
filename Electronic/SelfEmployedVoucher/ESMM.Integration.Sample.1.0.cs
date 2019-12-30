using MPIP.EArchive.WSClient.CommonWS;
using System;
using System.Collections.Generic;
using System.IO;

namespace MPIP.CommonServis.TestClient
{
    /// <summary>
    /// Elektronic Serbest Meslek Makbuzu (e-smm) web servis entegrasyonu için örnek ve test kodlarını içermektedir.
    /// <remarks>Bu tasarım desenleri ya da kodlar sadece "Netle Yazılım" tarafında sağlanan çözümlerde kullanılabilir.</remarks>
    /// </summary>
    public static class SerbestMeslekMakbuzu
    {
        /// <summary>
        /// Senaryo:
        /// •   Oturum Açma 
        /// •   E-SMM Makbuz Yükleme 
        /// •   Makbuz Durumunu Sorgulama 
        /// •   Makbuz Pdf Alma 
        /// •   Makbuz Html Alma  
        /// •   Makbuz Arama 
        /// •   Makbuz İptal Etme 
        /// </summary>
        public static void PrepareESMMByNetleEFatura()
        {
            try
            {
                var client = new Integration10();
                client.Url = "https://efaturatest.inposia.com.tr/CommonInvoice.Web.Service/Integration10.asmx";

                // Modül tipi olarak eSEVoucher kullanılması önemlidir 
                var smmToken = client.CreateUserToken(@"KullanıcıAdı", "Şifre", ModuleType.eSEVoucher);

                /// smmNEF, NetleEFatura tipinde oluşturulmuş serbest meslek makbuzu belgesidir                 
                /// documentUUID değeri, o belgeye has tekil bir değer olacaktır ve gelecek sorgulamalarda kullanılacaktır, kullanıcı tarafından oluşturulup verilir                
                /// prefix değeri, belgenin numarasıyla ilgilidir, şu aşamada boş bırakılmalıdır 
                var uuid = Guid.NewGuid();
                client.UploadCommonDocument(smmToken, uuid, "", GetSMMInvoice(uuid));

                Console.WriteLine("Makbuz gönderildi");
                System.Threading.Thread.Sleep(5000); // Gönderim gerçekleştirildikten sonra, arka planda asenkron makbuz oluşturma işleminin bitmesi beklenmelidir. 
                // Ardından, makbuz durumu sorgulanarak gönderimin durumu sorgulanmalıdır. Başarılı olan makbuzlar için pdf alma işlemine geçilebilir. 

                var status = client.GetEArchiveStatus(smmToken, uuid);

                // makbuz durumu sorgulanabilir 
                Console.WriteLine("Gönderildi mi ? :" + status.OK);

                var pdf = client.GetCommonDocumentPDFByUUIDs(smmToken, new UbltrPDFInput[] { new UbltrPDFInput { UUID = uuid } });

                //uuid değerine göre pdf elde edilir 
                if (pdf.Length > 0)
                    File.WriteAllBytes(@"C:\temp\" + uuid.ToString() + ".pdf", pdf[0].PDFContent);
                else
                    Console.WriteLine("Pdf alınamadı");

                // uuid değerine göre html elde edilir 
                var html = client.GetCommonDocumentHtmlByUUID(smmToken, uuid); if (html.Length > 0)
                    File.WriteAllText(@"c:\temp\test.html", html);


                var seado = new SearchEArcDocumentOption()
                {
                    //Döküman numarası, doldurulmassa filtreye uyan tüm dökümanlar 
                    InvoiceNumber = "SMM2018000000315",

                    // Tarih aralığı başlangıç değeri 
                    StartDateTime = DateTime.Parse("2018-06-03"),

                    //Tarih aralığı bitiş değeri 
                    EndDateTime = DateTime.Parse("2018-06-22")
                };

                var search = client.SearchEArchive(smmToken, seado);

                //uuid değerine göre makbuz iptal edilir 
                client.DeleteEArchive(smmToken, uuid);

            }
            catch (Exception exc)
            {
                Console.WriteLine("Gönderim sürecinde hata oluştu. Hata detayı : " + exc.Message);
            }
        }

        static NetleEFatura GetSMMInvoice(Guid uuid)
        {
            var nef = new NetleEFatura();
            nef.GUID = Guid.NewGuid().ToString();
            ///  Belge numarası, özel bir yapıdadır, ilk 3 karakter fatura serisi(Burada seri SMM olarak belirlendi), 
            /// sonraki dört karakter yıl (2018) sonraki 9 karakter, müteselsil şekilde devam eden numara olmalıdır. 
            /// Fatura numarası ve fatura tarihi birbiriyle sıralı olmalıdır.              
            nef.DuzenlenmeTarihi = DateTime.Now; // Belge düzenlenme zamanı             
            nef.Tip = NetleEFaturaType.SATIS; // Bu şekilde sabit belirtilmelidir.             
            nef.Aciklama = "Yalnız : DörtBinYüz TL"; // Belgede not olarak eklenecek bir açıklama varsa yazılır. 
            nef.No = "SMM2018000000315";

            // Bu şekilde sabit olmalıdır. 
            nef.Senaryo = NetleEFaturaSenaryoType.EARSIVBELGE;

            // Belgeyi kesen kişi / firmanın bilgileri. Belge bir şahıs tarafından kesiliyorsa Sahis bölümü doldurulmalıdır.  
            nef.Tedarikci = new Tedarikci
            {
                FirmaAdi = "SMMİsim SMMSoyisim ",
                Il = "test",
                IlceSemt = "test",
                Sokak = "Test adres 03",
                Sahis = new Sahis { Ad = "SMMİsim", Soyad = "SMMSoyisim" },
                /// Belgeyi kesen firmanın vergi kimlik numarası ya da şahsın tc kimlik no'su. 
                /// Burada verilen değer, test hesabınıza tanımlı tckn değeridir, değiştirmeniz halinde sistemden hata alırsınız 
                VergiNoTCKimlikNo = "98745612309",
                Ulke = "TR",
                VergiDairesi = "Test"
            };

            nef.Musteri = new Musteri() // Belgenin alıcısı 
            {
                FirmaAdi = "Musteriİsim MusteriSoyisim",
                Sokak = "Test",
                Il = "İzmir",
                IlceSemt = "Test",

                // Bir şahsa kesiliyorsa doldurulmalı 
                Sahis = new Sahis { Ad = "Musteriİsim", Soyad = "MusteriSoyisim" },

                VergiNoTCKimlikNo = "20231264774",
                Ulke = "TR",
                VergiDairesi = "Test"
            };

            // Faturanın kalemleri bu listeye atılacak, sonrasında nef nesnesinin FaturaKalemleri elemanı bu listenin array'e çevrilmesiyle elde edilecek 
            var fatKalemList = new List<FaturaKalemi>();

            /// ilk fatura kalemi 
            var fatKalem = new FaturaKalemi();

            // Burada standart uluslararası birim kodları kullanılmalıdır. C62 Adet anlamına gelir.             
            fatKalem.Birim = "C62";
            fatKalem.BirimFiyat = 113.6364;
            fatKalem.ToplamTutar = 113.6364;
            fatKalem.DovizTipi = "TRY";

            // Verilen hizmetin ismi de yazılabilir. 
            fatKalem.StokAdi = "Deneme";

            /// Fatura kaleminin vergileri bu listeye atılacak, sonrasında FaturaKalemi nesnesinin 
            /// Vergiler elemanı bu listenin array'e çevrilmesiyle elde edilecek.
            var vergiler = new List<Vergi>();

            // vergi türü, 0015 Kdv anlamına gelmektedir.
            var vergi = new Vergi();
            vergi.Tur = TaxCodeContentType.Item0015;
            vergi.Oran = 8;
            vergi.Tutar = 9.0909;
            vergi.Matrah = 113.6364;
            vergiler.Add(vergi);

            // vergi türü, 0003 stopaj anlamına gelmektedir.
            vergi = new Vergi();
            vergi.Tur = TaxCodeContentType.Item0003;
            vergi.Oran = 20;
            vergi.Tutar = 22.7273;
            vergi.Matrah = 113.6364;
            vergiler.Add(vergi);

            fatKalem.Vergiler = vergiler.ToArray();
            fatKalemList.Add(fatKalem);

            /// ikinci fatura kalemi             
            /// fatKalem = new FaturaKalemi(); 
            fatKalem.Birim = "C62";
            fatKalem.BirimFiyat = 2840.9091;
            fatKalem.ToplamTutar = 2840.9091;
            fatKalem.DovizTipi = "TRY";
            fatKalem.StokAdi = "Deneme 2";

            vergiler = new List<Vergi>();
            vergi = new Vergi();

            vergi.Tur = TaxCodeContentType.Item0015;
            vergi.Oran = 8; vergi.Tutar = 227.2727;
            vergi.Matrah = 2840.9091;
            vergiler.Add(vergi);

            vergi = new Vergi();
            vergi.Tur = TaxCodeContentType.Item0003;
            vergi.Oran = 20; vergi.Tutar = 568.1818;
            vergi.Matrah = 2840.9091;
            vergiler.Add(vergi);
            fatKalem.Vergiler = vergiler.ToArray();
            fatKalemList.Add(fatKalem);

            /// üçüncü fatura kalemi 

            fatKalem = new FaturaKalemi();
            fatKalem.Birim = "C62";
            fatKalem.BirimFiyat = 1704.5455;
            fatKalem.ToplamTutar = 1704.5455;
            fatKalem.DovizTipi = "TRY";
            fatKalem.StokAdi = "Deneme 3";

            vergiler = new List<Vergi>(); vergi = new Vergi();
            vergi.Tur = TaxCodeContentType.Item0015;
            vergi.Oran = 8; vergi.Tutar = 136.3636;
            vergi.Matrah = 1704.5455;
            vergiler.Add(vergi);

            vergi = new Vergi();
            vergi.Tur = TaxCodeContentType.Item0003;
            vergi.Oran = 20;
            vergi.Tutar = 340.9091;
            vergi.Matrah = 1704.5455;
            vergiler.Add(vergi);
            fatKalem.Vergiler = vergiler.ToArray();
            fatKalemList.Add(fatKalem);

            nef.FaturaKalemleri = fatKalemList.ToArray();

            /// Toplam tutara ilişkin bilgiler oluşturuluyor:             
            /// Toplam tutar için toplam vergi bilgisi oluşturuluyor            
            vergiler = new List<Vergi>();
            vergi = new Vergi();
            vergi.Tur = TaxCodeContentType.Item0015;
            vergi.Oran = 8;
            vergi.Tutar = 372.73;
            vergi.Matrah = 4659.125;
            vergiler.Add(vergi);

            vergi = new Vergi();
            vergi.Tur = TaxCodeContentType.Item0003;
            vergi.Oran = 20;
            vergi.Tutar = 931.82;
            vergi.Matrah = 4659.1;
            vergiler.Add(vergi);
            nef.Vergiler = vergiler.ToArray();

            nef.ToplamTutar = 4659.09;
            nef.OdenecekToplamTutar = 4100; nef.DovizTipi = "TRY";
            nef.VergilerDahilTutar = 4100;
            nef.KaynakDokumanTuru = KaynakDokumanTuru.ESERBESTMESLEKMAKBUZU;

            /// SMM belgeleri alıcıya iki şekilde gönderilir, kağıt çıktısı alınarak ya da elektonik yolla e-posta gönderilerek. 
            /// Bu gönderim tipi belgede belirtilmelidir çünkü bu bilgi emtegratör tarafından ay sonunda oluşturulup 
            /// GIB'e gönderilecek raporda doğru biçimde bildirilmelidir.
            nef.GonderimTipi = GonderimTipi.KAGIT;

            return nef;
        }
    }
}
