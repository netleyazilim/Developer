using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;

namespace MPIP.CommonServis.TestClient
{
    #region [  MustahsilMakbuzuTest  ]

    /// <summary>
    /// Elektronic Mustahsil Makbuzu (e-mm) web servis entegrasyonu için örnek ve test kodlarını içermektedir.

    /// <remarks>Bu tasarım desenleri ya da kodlar sadece "Netle Yazılım" tarafında sağlanan çözümlerde kullanılabilir.</remarks>
    /// </summary>
    public static class MustahsilMakbuzuTest
    {
        #region [  test kodu calistirilmadan once değiştirilmesi gereken değişkenler  ]

        /// <summary>
        /// Entegratör tarafından sağlanan 'web-servis-kullanıcı adı'
        /// </summary>
        const string WS_USER_NAME = @"???";

        /// <summary>
        /// Entegratör tarafından sağlanan 'web-servis-şifresi'
        /// </summary>
        const string WS_PASSWORD = @"???";

        /// <summary>
        /// Inposia Özel Entegratör - Web Servisi
        /// <remarks>Diğer entegratörler için de geçerlidir. Sadece adres bilgisi değiştirilmelidir.</remarks>
        /// </summary>
        const string WS_URL = "https://efaturatest.inposia.com.tr/CommonInvoice.Web.Service/Integration10.asmx";

        /// <summary>
        /// NOT : gönderici vergi kimlik numarası test şirketine ve oturum bilgilerine göre değiştirilmelidir
        /// </summary>
        const string SENDER_TAX_ID = "6310545918";

        /// <summary>
        /// Not : test koşullarına göre alıcı vkn ya da tckn değeri değiştirilebilir.
        /// </summary>
        const string RECEIVER_TAX_ID = "16076512345";

        #endregion

        /// <summary>
        /// web servis ortamına erişimde kullanılacak istemci-tabanlı sınıf
        /// </summary>
        static CommonWS.Integration10 client;

        /// <summary>
        /// web servis tarafından güvenlik doğrulaması sonrasında geriye dönen ve zaman-aşımlı özel değer
        /// </summary>
        static string securityToken = null;

        /// <summary>
        /// Static sınıfa ilk yöntem ile erişim yapıldığında, thread-safe, bir kerelik çalışması gereken kodlar
        /// </summary>
        static MustahsilMakbuzuTest()
        {
            Console.WriteLine("Mustahsil makbuzu - common web servisi");

            Console.WriteLine("web servis client nesnesi olusturuluyor");
            client = new CommonWS.Integration10();
            client.Timeout = Convert.ToInt32(TimeSpan.FromMinutes(2).TotalMilliseconds);

            ///proxy tanımı yoksa bu satır kapatılabilir.
            client.Proxy = DFS.Library.Net.WSClientHelper.NewWebProxy();
        }

        /// <summary>
        /// Netle web servis yapısında kullanılan ortak NetleEFatura sınıfını hazırlamak için kullanılır
        /// </summary>
        /// <param name="testSenderVKN">Test için gönderici VKN (Vergi Kimlik Numarası)</param>
        /// <param name="testBuyerVKN">Test için alıcı VKN</param>
        /// <param name="numberSuffix">E-MM belgesinde kullanılacak yasal (GİB) numarasına ilişkin 3-hane alfa-sayısal değer</param>
        /// <returns>NetleEFatura örnek sınıfı (instance)</returns>
        internal static CommonWS.NetleEFatura PrepareTestEProducerReceipt(string testSenderVKN, string testBuyerVKN, string numberSuffix = null)
        {
            var nef = new CommonWS.NetleEFatura();
            nef.KaynakDokumanTuru = CommonWS.KaynakDokumanTuru.EMUSTAHSIL;

            #region [  invoice header  ]

            var uniqueId = Guid.NewGuid();
            nef.GUID = uniqueId.ToString();
            nef.Tedarikci = new CommonWS.Tedarikci()
            {
                FirmaAdi = "NETLE GLOBAL COZUM AILESI",
                VergiNoTCKimlikNo = testSenderVKN,
                WebAdresi = "www.netle.com.tr",
                Il = "izmir",
                IlceSemt = "konak",
                Ulke = "tr",
                PostaKodu = "35222",
                VergiDairesi = "konak"
            };

            nef.Musteri = new CommonWS.Musteri()
            {
                FirmaAdi = "CUSTOMER1",

                ///not:begin
                ///eğer testBuyerVkn değeri TCKN ise bu alan doldurulmalı, 
                ///tüzel firma ise vkn (10 digit) bu durumda sahis nesnesi bos gecilmelidir.
                Sahis = new CommonWS.Sahis() { Ad = "Mehmet Ali", Soyad = "Yunusgillerdenmi", },
                ///not:end
                ///
                Eposta = "skyflood@hotmail.com",
                VergiNoTCKimlikNo = testBuyerVKN,
                WebAdresi = "www.NETLEGLOBAL.com",
                Il = "izmir",
                IlceSemt = "konak",
                Ulke = "tr",
                PostaKodu = "35222",
                VergiDairesi = "konak"
            };

            nef.DuzenlenmeTarihi = DateTime.Now;

            if (string.IsNullOrEmpty(numberSuffix))
                numberSuffix = "NTL";

            nef.No = numberSuffix + "2019" + "101".PadLeft(9, '0');

            //universal currency code
            nef.DovizTipi = "TRY";

            //constant value (if taxexamptionreason required: then this value must be changed)
            nef.Tip = CommonWS.NetleEFaturaType.SATIS;

            //constant value for earchive
            //nef.Senaryo = CommonWS.NetleEFaturaSenaryoType.IHRACAT;
            nef.Senaryo = CommonWS.NetleEFaturaSenaryoType.TEMELFATURA;


            #endregion

            #region [  items  ]

            var klist = new List<CommonWS.FaturaKalemi>();
            for (int i = 0; i < 2; i++)
                klist.Add(new CommonWS.FaturaKalemi()
                {
                    Aciklama = "Sağlıklı Süt",
                    Miktar = 1,
                    BirimFiyat = 100,
                    ToplamTutar = 200,
                    StokAdi = "KonakKopSüt_" + i.ToString(),
                    Birim = "C62",
                    DovizTipi = "TRY"
                });

            nef.FaturaKalemleri = klist.ToArray();

            #endregion

            #region [  tax area  ]

            nef.Vergiler = new CommonWS.Vergi[]    {
                new CommonWS.Vergi() { 
                    Matrah=200, 
                    Oran=18,
                    Tur= CommonWS.TaxCodeContentType.Item0015, //kdv - vat
                    Tutar=36}    };

            #endregion

            #region [  legalmonetryfields    ]

            ///line extension amount
            nef.ToplamTutar = 200;

            ///tax inclusive 
            nef.KDVMatrahi = 200;

            ///tax amount
            nef.KDVTutari = 36;

            ///payable amount
            nef.OdenecekToplamTutar = 236;

            #endregion

            return nef;
        }

        /// <summary>
        /// web servis üzerinden ilk erişim yapıldığında entegratör sunucusundan güvenlik bağlantı kodu alınmalıdır
        /// </summary>
        static void CheckSecurityToken()
        {
            if (securityToken != null)
                return;

            ///EProducerReceipt = Mustahsil Makbuzu
            securityToken = client.CreateUserToken(WS_USER_NAME, WS_PASSWORD, CommonWS.ModuleType.EProducerReceipt);
            Console.WriteLine("Güvenlik bağlantı değeri : " + securityToken);
        }

        /// <summary>
        /// web servis yöntemlerinden dönen SoapException sınıfına özel, detail alanındaki hata kodunun ayrıştırılması
        /// </summary>
        static int GetErrorCodeFromSoapException(SoapException sexc)
        {
            if (sexc == null) return -1;
            if (sexc.Detail == null) return -1;
            if (String.IsNullOrEmpty(sexc.Detail.InnerXml)) return -1;

            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(sexc.Detail.InnerXml);
            if (xml.DocumentElement == null) return -1;

            var codeNode = xml.DocumentElement.ChildNodes[0];
            if (codeNode == null) return -1;
            if (codeNode.Name != "code") return -1;
            if (codeNode.FirstChild == null) return -1;

            return Convert.ToInt32(codeNode.FirstChild.Value);
        }

        /// <summary>
        /// e-mustahsil web servis işlemlerinin ortak bir senaryoda kullanım örnekleri 
        /// e-mm Gönder, al, sorgula, html içerik indir vb.
        /// </summary>
        public static void ProducerReceiptMethodTest()
        {
            Console.WriteLine("Test işlemleri basliyor");

            CheckSecurityToken();

            #region [  yeni mustahsil belgesi olustur  ]

            ///mustahsil makbuzu icin gönderilmek üzere yeni bir belge oluştur
            var eDocNumberPrefix = "NTL";
            var netleEMustahsil = PrepareTestEProducerReceipt(SENDER_TAX_ID, RECEIVER_TAX_ID, eDocNumberPrefix);
            Console.WriteLine("Mustahsil makbuzu için oluşturulan belge tekil numarası (ubl.UUID) : " + netleEMustahsil.GUID);
            Console.WriteLine("Mustahsil makbuzu için yasal belge numarası (ubl.ID) : " + netleEMustahsil.No);

            #endregion

            #region [  entegratore yeni mustahsil belgesi yukle  ]

            ///mustahsil makbuzu için yasal yeni belge oluşturulması için entegratöre web servis ile iletiliyor
            Guid nefGuid = Guid.Parse(netleEMustahsil.GUID);
            try
            {
                client.UploadCommonDocument(securityToken, nefGuid, eDocNumberPrefix, netleEMustahsil);
            }
            catch (SoapException sexc)
            {
                var serviceProviderErrorCode = GetErrorCodeFromSoapException(sexc);
                Console.WriteLine("Elektronik belge gönderiminde entegratör tabanlı bir hata oluştu.");
                Console.WriteLine("Entegrator islem hata kodu : " + serviceProviderErrorCode.ToString());
                Console.WriteLine("Entegrator islem hata aciklamasi : " + sexc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Elektronik belge gönderiminde genel bir hata oluştu. Hata : " + exc.Message);
                return;
            }

            #endregion

            #region [  durum sorgulaması yap  (10 dakika bekle)]

            //System.Threading.Thread.Sleep(TimeSpan.FromMinutes(10));

            #region [  hata kodlarının yorumlanması -  bölüm 1  ]

            ///entegrator islemlerinin sonucunun kesin olabilmesi icin yeni gonderilen
            ///belge icin en az 10 dakika beklenmelidir.
            ///beklenmemesi ve gönderim sonrasında yapılacak sorgulamalarda aşağıdaki hata kodları gelecektir. 
            ///1)10014:Belge henüz işlenmemiş ya da kuyrukta olabilir
            ///2)10015:Belge işleniyor... - ETTN B7E504A2-7C31-4D77-95A8-5645C42F8CD3 (örnek ETTN)

            #endregion

            #region [  hata kodlarının yorumlanması ve tekrar gönderim durumları  - bölüm 2  ]

            ///Bekleme sonrasında aşağıdaki hatalar gelirse, ilgili belge, sistem tarafından geçersiz olarak 
            ///işaretlenmiştir. Bu nedenle ilgili belge yeni ve farklı bir UUID(ETTN) değeri ile tekrar gönderilmek zorundadır.
            ///1)10017:Belge sistem tarafından reddedildi. - ETTN : B7E504A2-7C31-4D77-95A8-5645C42F8CD3
            ///2)10018:Belge işlenirken bir hata oluştu. - ETTN : B7E504A2-7C31-4D77-95A8-5645C42F8CD3 - HATA :???????

            #endregion

            #region [  hata kodu yok, işlem başarılı durumları  ]

            ///Entegratörün kuyruktaki mesajları işlemesi sonrasında ise sorgulama sonucunda aşağıdaki mesaj gelirse
            ///dönen değerler kullanılabilir (durum kodu, uuid, numara, vb.)

            ///yeni gonderilen bir belgeyi sorgulamak icin asagidaki satır kullanılmalıdır
            //var queryUUID = nefGuid;

            ///daha onceden gonderilmis bir belgeyi sorgula
            var queryUUID = Guid.Parse("B7E504A2-7C31-4D77-95A8-5645C42F8CD3");

            var epvStatus = client.GetEArchiveStatus(securityToken, queryUUID);
            if (epvStatus.OK)
            {
                Console.WriteLine("Belge tarihi : " + epvStatus.InvoiceDate.ToString());
                Console.WriteLine("Belge GİB No : " + epvStatus.InvoiceNumber);
                Console.WriteLine("Belge ETTN(UUID) : " + epvStatus.UUID);
                Console.WriteLine("Belge durum mesajı : " + epvStatus.ErrorDescription);
                Console.WriteLine("Belge durum kodu : " + epvStatus.RecordStatus.ToString());
                Console.WriteLine("Belge durum aciklaması : " + epvStatus.RecordStatusText);
            }

            #endregion

            #endregion


            #region [   Mustahsil makbuzlarını ara ve arama kriterlerine gore donen sonucların xml / html bilgisini indir  ]

            var eDocTrKeys = client.SearchEArchive(securityToken, new CommonWS.SearchEArcDocumentOption()
            {
                StartDateTime = DateTime.Today.AddHours(-1),
                EndDateTime = DateTime.Today.AddHours(1)
                //ReceiverTaxNumber = "16076512345",
                //InvoiceNumber = "NTL2019000000101"
            });

            if (eDocTrKeys != null && eDocTrKeys.Length > 0)
            {
                Console.WriteLine("Arama sonrasında e-mustahsil belgesine ilişkin tekil anahtar ETTN/UUID değerleri ekrana yazdırılıyor");

                ///anahtarları ekrana yazdırma - yöntem 1
                foreach (var eDocKey in eDocTrKeys)
                    Console.WriteLine("e-mustahsil belge anahtarı : " + eDocKey.ToString());

                ///////anahtarları ekrana yazdırma - yöntem 2
                ////Console.WriteLine("e-Mustahsil anahtarları : " + string.Join(" | ", eDocTrKeys));

                ///////anahtarları ekrana yazdırma - yöntem 3
                ////eDocTrKeys.ToList().ForEach(eDocKey =>
                ////{
                ////    Console.WriteLine("e-mustahsil belge anahtarı : " + eDocKey.ToString());
                ////});
            }

            #endregion

            #region [  durum kodu basarili ise html (ve gerekli ise xml) icerik al  ]

            var workingTempPath = @"C:\temp\MustahsilCalismalari";
            if (!Directory.Exists(workingTempPath))
                Directory.CreateDirectory(workingTempPath);

            ///xml block
            var signedXmlFile = Path.Combine(workingTempPath, queryUUID + " eMustahsil yasal icerik.xml");
            var xmlResult = client.GetCommonDocumentSignedXmlByUUID(securityToken, queryUUID);
            File.WriteAllText(signedXmlFile, xmlResult, Encoding.UTF8);
            System.Diagnostics.Process.Start(signedXmlFile);  //goruntuyu izlemek icin asagidaki satır kullanılabilir

            ///html block
            var htmlFileName = Path.Combine(workingTempPath, queryUUID + " eMustahsil goruntusu.html");
            var htmlResult = client.GetCommonDocumentHtmlByUUID(securityToken, queryUUID);
            File.WriteAllText(htmlFileName, htmlResult, Encoding.UTF8);
            System.Diagnostics.Process.Start(htmlFileName);  //goruntuyu izlemek icin asagidaki satır kullanılabilir

            #endregion
        }
    }

    #endregion
}
