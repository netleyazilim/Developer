/// <summary>
/// e-arşiv web servis entegrasyonu için örnek ve test kodlarını içermektedir.
/// <remarks>Bu tasarım desenleri ya da kodlar sadece "Netle Yazılım" tarafında sağlanan çözümlerde kullanılabilir.</remarks>
/// </summary>
public static class EArsivKullanimSenaryolariVeOrnekKodlari
{
    /// <summary>
    /// web servis ortamına erişim için gerekli olacak client(istemci) ara-nesnesi(proxy)
    /// </summary>
    static Integration10 client = null;

    /// <summary>
    /// web servis ortamından alınacak token (guvenlik bilgisi) değerini saklar
    /// </summary>
    static string token = null;

    /// <summary>
    /// web servis işlemlerini kullanmadan önce yapılması gereken temel / hazırlık işlemleri 
    /// </summary>
    static void CheckToken()
    {
        if (client == null)
        {
            client = new Integration10();

            ///Test sistemine bağlantı yapılacak
            client.Url = @" https://efaturatest.inposia.com.tr/CommonInvoice.Web.Service/Integration10.asmx";
        }

        ///güvenlik bilgisi alınmadıysa, oturum aç ve token değerini al (e-arşiv modülü için)
        if (string.IsNullOrEmpty(token))
            token = client.CreateUserToken(@"webServiceUserName", "webServicePassword", ModuleType.eArchive);
    }

    /// <summary>
    /// web servis yöntemlerinden dönen SoapException sınıfına özel, detail alanındaki hata kodunun ayrıştırılması
    /// </summary>
    static int GetErrorCodeFromSoapException(System.Web.Services.Protocols.SoapException sexc)
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
    /// Entegrator hata koduna gore final-State değerine karar veriz.
    /// final-state == true ==> tekrar tekrar durum sorgulama islemi yapılmamalıdır
    /// final-state == false ==> tekrar durum sorgulama islemi yapılarak güncel işlem kodu alınabilir.
    /// </summary>
    static bool CheckCodeAndGetFinalState(int code)
    {
        int[] finalStateCodes = { 10017, 10013, 10008, 1300, 1230, 1215, 1181, 1163, 1160, 1150 };

        ///using System.Linq mutlaka kullanılmalıdır
        return finalStateCodes.Contains(code);
    }

    /// <summary>
    /// Entegrator hata koduna göre yerel sistemde işlenebilir durum kodu
    /// </summary>
    static ErcStateMachineCode CheckCodeAndGetStateCode(int code)
    {
        if (code == 1300)
            return ErcStateMachineCode.OnSuccess;

        if (CheckCodeAndGetFinalState(code))
            return ErcStateMachineCode.Failed;

        return ErcStateMachineCode.Processing;
    }

    /// <summary>
    /// başarılı şekilde iletilmiş e-arşiv belgeleri için html içerik alma 
    /// </summary>
    public static string EArsivBelgesiIcinHtmlGetir(string uuid)
    {

        ErcStateMachineCode durumKodu = ErcStateMachineCode.Init;
        ///durumKodu=ExecOnDB("Select Durum from ???? Where UUID=uuid");

        ///yerel veritabanından daha fazla alan alınarak, aciklama mesajı güçlendirilebilir
        if (durumKodu != ErcStateMachineCode.OnSuccess)
            throw new Exception("Bu belge için html içerik alınamaz. Belge işleniyor olabilir ya da hatalı sonlandırıldı");

        CheckToken();

        return client.GetCommonDocumentHtmlByUUID(token, Guid.Parse(uuid));
    }

    /// <summary>
    /// •   (daha önceden güvenlik-token) yoksa, oluştur ve işlemlere hazır hale gel
    /// •	Durumu sorgulanması gereken ve süreci sonlandırılmamış kayıtları yerel veritabanı ortamından listeyi al (UUID array)
    /// •	Entegratör ortamına erişim yaparak güncel durum kodunu alarak yerel sistemde güncelle
    /// </summary>
    public static void EArsivBelgelerineIliskinDurumKodlariniGuncelleVeYerelSistemeYaz()
    {
        ///Öneri:
        ///Yerel ortam log kayıtları guncellenir
        ///AddLog("e-arsiv durum sorgulama calisiyor...", DateTime.Now, UserId, MachineId);
        string[] uuids = null;

        ///Öneri
        ///Yerel veritabanından liste alınır
        ///SQL
        ///uuids=ExecOnDB("Select UUID from ???? Where (FinalState=False) And (Durum In ('Uploaded','Processing'))");

        ///sorgulanacak kayıt yoksa çık
        if (uuids == null || uuids.Length == 0)
            return;

        try
        {
            CheckToken();

            foreach (var uuid in uuids)
            {
                try
                {
                    var earcStatus = client.GetEArchiveStatus(token, Guid.Parse(uuid));

                    if (earcStatus.OK)
                    {
                        ///e-arşiv belgesi başarılı şekilde iletildi (++++)
                        ///Öneri
                        ///SQL : Update ???? Set IslemKodu=0, IslemAciklamasi=null, FinalState=True, Durum=ErcStateMachineCode.OnSuccess Where UUID=nef.Guid
                    }
                    else
                    {
                        ///Öneri
                        ///SQL : Update ???? Set 
                        ///IslemKodu=earcStatus.RecordStatus, 
                        ///IslemAciklamasi=string.Join(" - ", earcStatus.ErrorDescription, earcStatus.RecordStatusText), 
                        ///FinalState=CheckCodeAndGetFinalState(earcStatus.RecordStatus), 
                        ///Durum=CheckCodeAndGetStateCode(earcStatus.RecordStatus)
                        ///Where UUID=nef.Guid
                    }

                }
                catch (System.Web.Services.Protocols.SoapException sexc) //entegratör tarafından SOAP tabanlı hata geliyor
                {
                    var errorCode = GetErrorCodeFromSoapException(sexc);
                    var errorMessage = sexc.Message;
                    ///Öneri
                    ///SQL : Update ???? Set 
                    ///IslemKodu=errorCode, 
                    ///IslemAciklamasi=errorMessage, 

                    /////hata koduna gore bu isleme karar verilmelidir.
                    ///FinalState=CheckCodeAndGetFinalState(errorCode), 
                    ///Durum=CheckCodeAndGetStateCode(errorCode)
                    ///Where UUID=nef.Guid
                }
                catch (Exception exc) //entegratör bağımsız olası hatalar gelebilir (ağ bağlantısı kesildi gibi)
                {
                    ///Öneri
                    ///SQL : Update ???? Set 
                    ///IslemKodu="-1",  //unknown exception code
                    ///IslemAciklamasi=exc.Message,
                    ///Where UUID=nef.Guid
                }
            }
        }
        catch (Exception exc)
        {
            ///AddLog("e-arsiv durum sorgulama işleminde hata var. Hata : " + exc.Message, DateTime.Now, UserId, MachineId);
            throw;
        }
        ///AddLog("e-arsiv durum sorgulama calisti. Sorgulanan kayıt sayısı : " + uuids.Length.ToString(), DateTime.Now, UserId, MachineId);
    }

    /// <summary>
    /// •   (daha önceden güvenlik-token) yoksa, oluştur ve işlemlere hazır hale gel
    /// •	NetleEFatura formatında bir e-arşiv hazırlama.  
    /// •	Faturayı web servise gönderme.  
    /// </summary>
    public static void NetleEFaturaNesnesiniEArsivIcinHazirlaVeGonder()
    {
        CheckToken();

        var nef = new NetleEFatura();

        #region [  invoice header  ]

        ///e-arşiv belgesi için tekil anahtar değeri 
        ///bu örnekte yeni yaratılıyor...
        ///eğer gitmemiş ya da daha önceden veri tabanına yazılan bir kayıt varsa, o değer kullanılmalıdır
        var uniqueId = Guid.NewGuid();

        nef.GUID = uniqueId.ToString();
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

        ///bu değer gönderici firma bazında tekil olmalıdır.
        ///GİB tarafından belirlenen formata uygun hazırlanmalıdır.
        ///Gönderici firma ve modül bazında (efatura, earsiv, eirsaliye, esmm, emm) PREFIX (ABC) değeri tekil olmalıdır
        ///earsiv icin "ABC" kullanılıyorsa, efatura icin kesinlikle "ABC" başlangıç (PREFIX) değeri kullanılamaz!!!
        ///yıl 2020 : bu değer fatura tarihindeki yıl bilgisidir. Fatura tarihindeki YIL ile No içindeki YIL değeri aynı olmalıdır.
        ///son 9 hane : bu alan tekil, ardışık (sıra atlamadan) üretilmelidir. Bu numara VUK sistemindeki basılı - kağıt fatura serisi numarası 
        ///gibi işlenmelidir.
        nef.No = "ABC" + nef.DuzenlenmeTarihi.Year.ToString() + "000000629";

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

        ///Öneri
        ///olusan belgeyi yerel sistemdeki (firma/desktop/web/cloud) veritabanına ekle
        ///daha onceden kontrol edilmis mi kontrol et, yoksa durumunu ='ErcStateMachineCode.Init' olarak UUID değeri ile 
        ///veri tabanına ekle
        var uuid = Guid.Parse(nef.GUID);

        ///entegratör sistemi birçok duruma özel hata kodu fırlatabilir.
        ///örnek : şematron hatası, vergi kimlik hatası, tekrarlı kayıt vb.
        ///bu durumda işlem durum kodları guncellenmelidir.
        try
        {
            client.UploadInvoice(token, uuid, "", nef);

            ///Öneri
            ///entegratör sistemine ilgili tekil anahtarlı (UUID) belgesi başarılı şekilde iletildi
            ///SQL : Update ???? Set 
            ///IslemKodu=0, 
            ///IslemAciklamasi=null, 
            ///FinalState=False, //durum sorgulama yöntemin bu belge dikkate alınacak
            ///Durum=ErcStateMachineCode.Uploaded 
            ///Where UUID=nef.Guid
        }
        catch (System.Web.Services.Protocols.SoapException sexc) //entegratör tarafından SOAP tabanlı hata geliyor
        {
            var errorCode = GetErrorCodeFromSoapException(sexc);
            var errorMessage = sexc.Message;
            ///Öneri
            ///entegratör sistemine ilgili tekil anahtarlı (UUID) belgesi iletilememiş olabilir.
            ///durum kodu ve hata yerel sistemdeki veritabanında güncellenmelidir.
            ///SQL : Update ???? Set 
            ///IslemKodu=errorCode, 
            ///IslemAciklamasi=errorMessage, 

            /////durum sorgulama yönteminde bu belge artık dikkate alınmayacak  cunku ileri dönemde bu durum kodu değişmeyecek
            ///FinalState=True, 

            ///Durum=ErcStateMachineCode.Failed 
            ///Where UUID=nef.Guid
            throw new Exception("Entegratör ortamına ilgili e-arşiv belgesi iletilemedi. Hata : " + string.Join(":", errorCode, errorMessage));
        }
        catch (Exception exc) //entegratör bağımsız olası hatalar gelebilir (ağ bağlantısı kesildi gibi)
        {
            ///Öneri
            ///entegratör sistemine ilgili tekil anahtarlı (UUID) belgesi iletilememiş olabilir.
            ///durum kodu ve hata yerel sistemdeki veritabanında güncellenmelidir.
            ///SQL : Update ???? Set 
            ///IslemKodu="-1",  //unknown exception code
            ///IslemAciklamasi=exc.Message,
            ///FinalState=False, 
            ///Durum=ErcStateMachineCode.ReSendable 
            ///Where UUID=nef.Guid
            throw new Exception("E-arşiv belgesi iletilemedi. Hata : " + exc.Message);
        }

        ///Öneri:
        ///gönderim sonrasında durum sorgulama işlemi modellenmemelidir. 
        ///E-arşiv gönderimleri e-fatura süreçelerine göre daha kısa sürede cevaplanıyor olsa da
        ///ayrı bir işlem ile durum güncelleme süreci modellenemelidir.

        ///Durum sorgulama süreci iki yöntem şeklinde olabilir
        ///Sorgulama modeli 1: 
        ///background-servis/thread/işlem gibi yerlerde durum kodlarına göre uygun durumda kayıtların tekil anahtarları alınmalı
        ///durum sorgulaması yapılmalı ve son durum guncellenmelidir
        ///
        ///Sorgulama modeli 2 :
        ///background ortamında yapılan işlem son kullanıcı tarafından anlık olarak da tetiklenebilmelidir.
        ///kullanıcı gönderilen e-arşiv listesini ekranda izledikten sonra, "refresh-durum kodlarını güncelle" gibi bir işlem ile anlık
        ///tetikleyebilir.

        ///Önerilen şekilde ve mimaride durum sorgulama yapılabilmesi icin bu kod içindeki diğer yönteme benzer adımlar tasarlanabilir.
        ///yöntem adı : EArsivBelgelerineIliskinDurumKodlariniGuncelleVeYerelSistemeYaz
    }
}
