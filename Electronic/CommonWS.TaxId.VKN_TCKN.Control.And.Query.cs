///Lang.Code:EN
///Before using this sample, you may update the web reference to re-build web service client proxy code
            
///Lang.Code:TR 
///Bu örneği kullanmadan önce web servis referanslarınızı ilgili projede güncellemeniz gerekebilir.
void Netle_Common_Web_Service_User_Operations()
{
    ///Lang.Code:EN
    ///Create connection and get security token by using user name and password provided service provider
            
    ///Lang.Code:TR 
    ///Özel entegratör tarafından sağlanan web servis kullanıcı isim ve şifre bilgisini kullanarak güvenlik anahtarını al
    var eInvSecurityToken = client.CreateUserToken(test_user_name, test_password, CommonWS.ModuleType.eInvoice);
    Console.WriteLine("Electronic Invoice Security Token : " + eInvSecurityToken);

    ///Lang.Code:EN
    ///Get the tax payer list from cache server (updated from GIB/TRA online user services) by using security token module (eInvoice)

    ///Lang.Code:TR 
    ///Bağlatı anahtar token değerineki modül için
    ///GİB tarafından yayınlanan kullanıcı mükellef listesini getir (bu örnekte e-fatura)
    var list = client.GetFullUserList(eInvSecurityToken);


    ///Lang.Code:EN
    ///Get the specified user post-box list (aliases) by using security token 
    ///Supported tokens : eInvoice, eArchive (same-as-eInvoice), eDespatch

    ///Lang.Code:TR 
    ///Bağlantı anahtar (token) değerindeki modül için 
    ///kullanıcı posta kutusu adres(ler)ini getir (etiket)
    ///GİB tarafından yayınlanan kullanıcı mükellef listesini getir (bu örnekte e-fatura)
    var list2 = client.GetUserInfoByTaxId(eInvSecurityToken, "6310545918");
    foreach (var item in list2)
        Console.WriteLine(item.TaxNo + "\t" + item.TaxPayerType + "\t" + item.Alias + "\t" + item.Title);

    ///Lang.Code:EN
    ///Query tax-payer(user) information from service provider by tax-identifier
    ///If method returns true==> tax-id (vkn/tckn) compatible with related module (like e-invoice, e-archive, e-despatch)

    ///Lang.Code:TR 
    ///VKN/TCKN değerlerini kullanarak kullanıcı bazında sorgulama yap
    ///eğer yöntem True dönerse, ilgili modül için geçerli bir kullanıcıdır. 
    ///Örnek 1 : e-arşiv token değeri için vkn/tckn değeri true dönerse, e-arşiv düzenlenmelidir
    var res1 = client.CheckUserInfoByTaxId(eInvSecurityToken, "2950307055");

    ///Örnek 2 : e-fatura token değeri için vkn/tckn değeri true dönerse, e-fatura düzenlenmelidir
    var res2 = client.CheckUserInfoByTaxId(eInvSecurityToken, "16011978878");
}
