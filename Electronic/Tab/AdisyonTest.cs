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
    public static class AdisyonTest
    {
        /// <summary>
        /// Senaryo:
        /// •   Oturum Açma 
        /// •   E-Adisyon Yükleme 
        /// •   Adisyon Durumunu Sorgulama 
        /// •   Adisyon Pdf Alma 
        /// •   Adisyon Html Alma  
        /// •   Adisyon Arama 
        /// •   Adisyon İptal Etme 
        /// </summary>
        public static void PrepareEAdisyonByNetleEFatura()
        {
            try
            {
                var client = new Integration10();
                client.Url = "http://localhost:51257/CommonInvoice.Web.Service/Integration10.asmx";

                // Modül tipi olarak eTab kullanılması önemlidir 
                var adisyonToken = client.CreateUserToken(@"username", "password", ModuleType.eTab);

                /// adisyonNEF, NetleEFatura tipinde oluşturulmuş adisyon belgesidir                 
                /// documentUUID değeri, o belgeye has tekil bir değer olacaktır ve gelecek sorgulamalarda kullanılacaktır, kullanıcı tarafından oluşturulup verilir                
                /// prefix değeri, belgenin numarasıyla ilgilidir, şu aşamada boş bırakılmalıdır 
                var uuid = Guid.NewGuid();
                NetleEFatura netleAdisyonWS = GetAdisyonInvoice(uuid);
                DFS.Common.Entity.NetleEFatura netleAdisyon = ConvertNetleEFatura(netleAdisyonWS);
                string nefXml = DFS.Form.Bll.Serialization.SerializeObject(netleAdisyon);
                string creditNoteXml = DFS.Form.Bll.Serialization.SerializeObject(DFS.Form.Bll.CreditNoteHelper.NetleEInvoiceToECreditNote(netleAdisyon));
                
                client.UploadCommonDocument(adisyonToken, uuid, "", netleAdisyonWS);

                Console.WriteLine("Adisyon gönderildi");
                System.Threading.Thread.Sleep(5000); // Gönderim gerçekleştirildikten sonra, arka planda asenkron oluşturma işleminin bitmesi beklenmelidir. 
                // Ardından, durumu sorgulanarak gönderimin durumu sorgulanmalıdır. Başarılı olan makbuzlar için pdf alma işlemine geçilebilir. 

                var status = client.GetEArchiveStatus(adisyonToken, uuid);

                // gonderim durumu sorgulanabilir 
                Console.WriteLine("Gönderildi mi ? :" + status.OK);

                var pdf = client.GetCommonDocumentPDFByUUIDs(adisyonToken, new UbltrPDFInput[] { new UbltrPDFInput { UUID = uuid } });

                //uuid değerine göre pdf elde edilir 
                if (pdf.Length > 0)
                    File.WriteAllBytes(@"C:\temp\" + uuid.ToString() + ".pdf", pdf[0].PDFContent);
                else
                    Console.WriteLine("Pdf alınamadı");

                // uuid değerine göre html elde edilir 
                var html = client.GetCommonDocumentHtmlByUUID(adisyonToken, uuid); if (html.Length > 0)
                    File.WriteAllText(@"c:\temp\test.html", html);


                var seado = new SearchEArcDocumentOption()
                {
                    //Döküman numarası, doldurulmassa filtreye uyan tüm dökümanlar 
                    InvoiceNumber = "ADS2022000000315",

                    // Tarih aralığı başlangıç değeri 
                    StartDateTime = DateTime.Parse("2018-06-03"),

                    //Tarih aralığı bitiş değeri 
                    EndDateTime = DateTime.Parse("2018-06-22")
                };

                var search = client.SearchEArchive(adisyonToken, seado);

                //uuid değerine göre makbuz iptal edilir 
                client.DeleteEArchive(adisyonToken, uuid);

            }
            catch (Exception exc)
            {
                Console.WriteLine("Gönderim sürecinde hata oluştu. Hata detayı : " + exc.Message);
            }
        }

        static DFS.Common.Entity.Vergi[] ConvertNetleVergiler(Vergi[] vergiler)
        {
            DFS.Common.Entity.Vergi[] retVal = null;
            if (vergiler != null)
            {
                List<DFS.Common.Entity.Vergi> vergiList = new List<DFS.Common.Entity.Vergi>();
                foreach (Vergi v in vergiler)
                {
                    DFS.Common.Entity.Vergi vt = new DFS.Common.Entity.Vergi();
                    vt.Matrah = v.Matrah;
                    vt.MuafiyetKodu = (DFS.Common.Entity.TaxExemptionReasonCodeContentType)((int)v.MuafiyetKodu);
                    vt.MuafiyetNedeni = v.MuafiyetNedeni;
                    vt.Oran = v.Oran;
                    vt.Tur = (DFS.Common.Entity.TaxCodeContentType)((int)v.Tur);
                    vt.Tutar = v.Tutar;
                    vergiList.Add(vt);
                }
                retVal = vergiList.ToArray();
            }
            return retVal;
        }

        static DFS.Common.Entity.EkSaha[] ConvertNetleEkSahalar(EkSaha[] ekSahalar)
        {
            DFS.Common.Entity.EkSaha[] retVal = null;
            if (ekSahalar != null)
            {
                List<DFS.Common.Entity.EkSaha> ekSahaList = new List<DFS.Common.Entity.EkSaha>();
                foreach (EkSaha s in ekSahalar)
                {
                    DFS.Common.Entity.EkSaha es = new DFS.Common.Entity.EkSaha();
                    es.Anahtar = s.Anahtar;
                    es.Deger = es.Deger;
                    ekSahaList.Add(es);
                }
                retVal = ekSahaList.ToArray();
            }
            return retVal;
        }

        static DFS.Common.Entity.VergiTemsilcisi ConvertNetleTaraf(VergiTemsilcisi t)
        {
            DFS.Common.Entity.VergiTemsilcisi r = null;
            if (t != null)
            {
                r = new DFS.Common.Entity.VergiTemsilcisi();
                r.AboneNo = t.AboneNo;
                //r.Adres = t.Add;
                r.AraciKurumEtiket = t.AraciKurumEtiket;
                r.AraciKurumVergiNo = t.AraciKurumVergiNo;
                r.BayiNo = t.BayiNo;
                r.BinaAdi = t.BinaAdi;
                r.BlokAdi = t.BlokAdi;
                r.CiftciNo = t.CiftciNo;
                r.DistributorNo = t.DistributorNo;
                r.DosyaNo = t.DosyaNo;
                r.Eposta = t.Eposta;
                r.Fax = t.Fax;
                r.FirmaAdi = t.FirmaAdi;
                r.HastaNo = t.HastaNo;
                r.HizmetNo = t.HizmetNo;
                r.Il = t.Il;
                r.Ilce = t.Ilce;
                r.IlceSemt = t.IlceSemt;
                r.ImalatciNo = t.ImalatciNo;
                r.KapiNo = t.KapiNo;
                r.KurumKayitNumarasi = t.KurumKayitNumarasi;
                r.KurumResmiUnvan = t.KurumResmiUnvan;
                r.MersisNo = t.MersisNo;
                r.MusteriNo = t.MusteriNo;
                //r.MusteriTuru = t.M
                r.NACEKodu = t.NACEKodu;
                r.PostaKodu = t.PostaKodu;
                r.SayacNo = t.SayacNo;
                r.Sokak = t.Sokak;
                r.SubeNo = t.SubeNo;
                r.TAPDKNo = t.TAPDKNo;
                r.TarafTuru = t.TarafTuru;
                r.Telefon = t.Telefon;
                r.TelefonNo = t.TelefonNo;
                r.TesisatNo = t.TesisatNo;
                r.TicaretSicilNo = t.TicaretSicilNo;
                r.Ulke = t.Ulke;
                r.UreticiNo = t.UreticiNo;
                r.VergiDairesi = t.VergiDairesi;
                r.VergiNoTCKimlikNo = t.VergiNoTCKimlikNo;
                r.VergiTipiKodu = t.VergiTipiKodu;
                r.WebAdresi = t.WebAdresi;
                if (t.Sahis != null)
                {
                    r.Sahis = new DFS.Common.Entity.Sahis();
                    r.Sahis.Ad = t.Sahis.Ad;
                    r.Sahis.Soyad = t.Sahis.Soyad;
                    r.Sahis.Unvan = t.Sahis.Unvan;
                    r.Sahis.Uyruk = t.Sahis.Uyruk;
                    if (t.Sahis.BankaHesap != null)
                    {
                        r.Sahis.BankaHesap = new DFS.Common.Entity.BankaHesap();
                        r.Sahis.BankaHesap.Aciklama = t.Sahis.BankaHesap.Aciklama;
                        r.Sahis.BankaHesap.BankaAdi = t.Sahis.BankaHesap.BankaAdi;
                        r.Sahis.BankaHesap.No = t.Sahis.BankaHesap.No;
                        r.Sahis.BankaHesap.SubeAdi = t.Sahis.BankaHesap.SubeAdi;
                    }
                    if (t.Sahis.Pasaport != null)
                    {
                        r.Sahis.Pasaport = new DFS.Common.Entity.Pasaport();
                        r.Sahis.Pasaport.No = t.Sahis.Pasaport.No;
                        r.Sahis.Pasaport.Tarih = t.Sahis.Pasaport.Tarih;
                    }
                }
            }
            return r;
        }

        static DFS.Common.Entity.SaticiTedarikcisi ConvertNetleTaraf(SaticiTedarikcisi t)
        {
            DFS.Common.Entity.SaticiTedarikcisi r = null;
            if (t != null)
            {
                r = new DFS.Common.Entity.SaticiTedarikcisi();
                r.AboneNo = t.AboneNo;
                //r.Adres = t.Add;
                r.AraciKurumEtiket = t.AraciKurumEtiket;
                r.AraciKurumVergiNo = t.AraciKurumVergiNo;
                r.BayiNo = t.BayiNo;
                r.BinaAdi = t.BinaAdi;
                r.BlokAdi = t.BlokAdi;
                r.CiftciNo = t.CiftciNo;
                r.DistributorNo = t.DistributorNo;
                r.DosyaNo = t.DosyaNo;
                r.Eposta = t.Eposta;
                r.Fax = t.Fax;
                r.FirmaAdi = t.FirmaAdi;
                r.HastaNo = t.HastaNo;
                r.HizmetNo = t.HizmetNo;
                r.Il = t.Il;
                r.Ilce = t.Ilce;
                r.IlceSemt = t.IlceSemt;
                r.ImalatciNo = t.ImalatciNo;
                r.KapiNo = t.KapiNo;
                r.KurumKayitNumarasi = t.KurumKayitNumarasi;
                r.KurumResmiUnvan = t.KurumResmiUnvan;
                r.MersisNo = t.MersisNo;
                r.MusteriNo = t.MusteriNo;
                //r.MusteriTuru = t.M
                r.NACEKodu = t.NACEKodu;
                r.PostaKodu = t.PostaKodu;
                r.SayacNo = t.SayacNo;
                r.Sokak = t.Sokak;
                r.SubeNo = t.SubeNo;
                r.TAPDKNo = t.TAPDKNo;
                r.TarafTuru = t.TarafTuru;
                r.Telefon = t.Telefon;
                r.TelefonNo = t.TelefonNo;
                r.TesisatNo = t.TesisatNo;
                r.TicaretSicilNo = t.TicaretSicilNo;
                r.Ulke = t.Ulke;
                r.UreticiNo = t.UreticiNo;
                r.VergiDairesi = t.VergiDairesi;
                r.VergiNoTCKimlikNo = t.VergiNoTCKimlikNo;
                r.VergiTipiKodu = t.VergiTipiKodu;
                r.WebAdresi = t.WebAdresi;
                if (t.Sahis != null)
                {
                    r.Sahis = new DFS.Common.Entity.Sahis();
                    r.Sahis.Ad = t.Sahis.Ad;
                    r.Sahis.Soyad = t.Sahis.Soyad;
                    r.Sahis.Unvan = t.Sahis.Unvan;
                    r.Sahis.Uyruk = t.Sahis.Uyruk;
                    if (t.Sahis.BankaHesap != null)
                    {
                        r.Sahis.BankaHesap = new DFS.Common.Entity.BankaHesap();
                        r.Sahis.BankaHesap.Aciklama = t.Sahis.BankaHesap.Aciklama;
                        r.Sahis.BankaHesap.BankaAdi = t.Sahis.BankaHesap.BankaAdi;
                        r.Sahis.BankaHesap.No = t.Sahis.BankaHesap.No;
                        r.Sahis.BankaHesap.SubeAdi = t.Sahis.BankaHesap.SubeAdi;
                    }
                    if (t.Sahis.Pasaport != null)
                    {
                        r.Sahis.Pasaport = new DFS.Common.Entity.Pasaport();
                        r.Sahis.Pasaport.No = t.Sahis.Pasaport.No;
                        r.Sahis.Pasaport.Tarih = t.Sahis.Pasaport.Tarih;
                    }
                }
            }
            return r;
        }

        static DFS.Common.Entity.AliciMusteri ConvertNetleTaraf(AliciMusteri t)
        {
            DFS.Common.Entity.AliciMusteri r = null;
            if (t != null)
            {
                r = new DFS.Common.Entity.AliciMusteri();
                r.AboneNo = t.AboneNo;
                //r.Adres = t.Add;
                r.AraciKurumEtiket = t.AraciKurumEtiket;
                r.AraciKurumVergiNo = t.AraciKurumVergiNo;
                r.BayiNo = t.BayiNo;
                r.BinaAdi = t.BinaAdi;
                r.BlokAdi = t.BlokAdi;
                r.CiftciNo = t.CiftciNo;
                r.DistributorNo = t.DistributorNo;
                r.DosyaNo = t.DosyaNo;
                r.Eposta = t.Eposta;
                r.Fax = t.Fax;
                r.FirmaAdi = t.FirmaAdi;
                r.HastaNo = t.HastaNo;
                r.HizmetNo = t.HizmetNo;
                r.Il = t.Il;
                r.Ilce = t.Ilce;
                r.IlceSemt = t.IlceSemt;
                r.ImalatciNo = t.ImalatciNo;
                r.KapiNo = t.KapiNo;
                r.KurumKayitNumarasi = t.KurumKayitNumarasi;
                r.KurumResmiUnvan = t.KurumResmiUnvan;
                r.MersisNo = t.MersisNo;
                r.MusteriNo = t.MusteriNo;
                //r.MusteriTuru = t.M
                r.NACEKodu = t.NACEKodu;
                r.PostaKodu = t.PostaKodu;
                r.SayacNo = t.SayacNo;
                r.Sokak = t.Sokak;
                r.SubeNo = t.SubeNo;
                r.TAPDKNo = t.TAPDKNo;
                r.TarafTuru = t.TarafTuru;
                r.Telefon = t.Telefon;
                r.TelefonNo = t.TelefonNo;
                r.TesisatNo = t.TesisatNo;
                r.TicaretSicilNo = t.TicaretSicilNo;
                r.Ulke = t.Ulke;
                r.UreticiNo = t.UreticiNo;
                r.VergiDairesi = t.VergiDairesi;
                r.VergiNoTCKimlikNo = t.VergiNoTCKimlikNo;
                r.VergiTipiKodu = t.VergiTipiKodu;
                r.WebAdresi = t.WebAdresi;
                if (t.Sahis != null)
                {
                    r.Sahis = new DFS.Common.Entity.Sahis();
                    r.Sahis.Ad = t.Sahis.Ad;
                    r.Sahis.Soyad = t.Sahis.Soyad;
                    r.Sahis.Unvan = t.Sahis.Unvan;
                    r.Sahis.Uyruk = t.Sahis.Uyruk;
                    if (t.Sahis.BankaHesap != null)
                    {
                        r.Sahis.BankaHesap = new DFS.Common.Entity.BankaHesap();
                        r.Sahis.BankaHesap.Aciklama = t.Sahis.BankaHesap.Aciklama;
                        r.Sahis.BankaHesap.BankaAdi = t.Sahis.BankaHesap.BankaAdi;
                        r.Sahis.BankaHesap.No = t.Sahis.BankaHesap.No;
                        r.Sahis.BankaHesap.SubeAdi = t.Sahis.BankaHesap.SubeAdi;
                    }
                    if (t.Sahis.Pasaport != null)
                    {
                        r.Sahis.Pasaport = new DFS.Common.Entity.Pasaport();
                        r.Sahis.Pasaport.No = t.Sahis.Pasaport.No;
                        r.Sahis.Pasaport.Tarih = t.Sahis.Pasaport.Tarih;
                    }
                }
            }
            return r;
        }

        static DFS.Common.Entity.Musteri ConvertNetleTaraf(Musteri t)
        {
            DFS.Common.Entity.Musteri r = null;
            if (t != null)
            {
                r = new DFS.Common.Entity.Musteri();
                r.Alias = t.Alias;
                r.AboneNo = t.AboneNo;
                //r.Adres = t.Add;
                r.AraciKurumEtiket = t.AraciKurumEtiket;
                r.AraciKurumVergiNo = t.AraciKurumVergiNo;
                r.BayiNo = t.BayiNo;
                r.BinaAdi = t.BinaAdi;
                r.BlokAdi = t.BlokAdi;
                r.CiftciNo = t.CiftciNo;
                r.DistributorNo = t.DistributorNo;
                r.DosyaNo = t.DosyaNo;
                r.Eposta = t.Eposta;
                r.Fax = t.Fax;
                r.FirmaAdi = t.FirmaAdi;
                r.HastaNo = t.HastaNo;
                r.HizmetNo = t.HizmetNo;
                r.Il = t.Il;
                r.Ilce = t.Ilce;
                r.IlceSemt = t.IlceSemt;
                r.ImalatciNo = t.ImalatciNo;
                r.KapiNo = t.KapiNo;
                r.KurumKayitNumarasi = t.KurumKayitNumarasi;
                r.KurumResmiUnvan = t.KurumResmiUnvan;
                r.MersisNo = t.MersisNo;
                r.MusteriNo = t.MusteriNo;
                //r.MusteriTuru = t.M
                r.NACEKodu = t.NACEKodu;
                r.PostaKodu = t.PostaKodu;
                r.SayacNo = t.SayacNo;
                r.Sokak = t.Sokak;
                r.SubeNo = t.SubeNo;
                r.TAPDKNo = t.TAPDKNo;
                r.TarafTuru = t.TarafTuru;
                r.Telefon = t.Telefon;
                r.TelefonNo = t.TelefonNo;
                r.TesisatNo = t.TesisatNo;
                r.TicaretSicilNo = t.TicaretSicilNo;
                r.Ulke = t.Ulke;
                r.UreticiNo = t.UreticiNo;
                r.VergiDairesi = t.VergiDairesi;
                r.VergiNoTCKimlikNo = t.VergiNoTCKimlikNo;
                r.VergiTipiKodu = t.VergiTipiKodu;
                r.WebAdresi = t.WebAdresi;
                if (t.Sahis != null)
                {
                    r.Sahis = new DFS.Common.Entity.Sahis();
                    r.Sahis.Ad = t.Sahis.Ad;
                    r.Sahis.Soyad = t.Sahis.Soyad;
                    r.Sahis.Unvan = t.Sahis.Unvan;
                    r.Sahis.Uyruk = t.Sahis.Uyruk;
                    if (t.Sahis.BankaHesap != null)
                    {
                        r.Sahis.BankaHesap = new DFS.Common.Entity.BankaHesap();
                        r.Sahis.BankaHesap.Aciklama = t.Sahis.BankaHesap.Aciklama;
                        r.Sahis.BankaHesap.BankaAdi = t.Sahis.BankaHesap.BankaAdi;
                        r.Sahis.BankaHesap.No = t.Sahis.BankaHesap.No;
                        r.Sahis.BankaHesap.SubeAdi = t.Sahis.BankaHesap.SubeAdi;
                    }
                    if (t.Sahis.Pasaport != null)
                    {
                        r.Sahis.Pasaport = new DFS.Common.Entity.Pasaport();
                        r.Sahis.Pasaport.No = t.Sahis.Pasaport.No;
                        r.Sahis.Pasaport.Tarih = t.Sahis.Pasaport.Tarih;
                    }
                }
            }
            return r;
        }

        static DFS.Common.Entity.Tedarikci ConvertNetleTaraf(Tedarikci t)
        {
            DFS.Common.Entity.Tedarikci r = null;
            if (t != null)
            {
                r = new DFS.Common.Entity.Tedarikci();
                r.AboneNo = t.AboneNo;
                //r.Adres = t.Add;
                r.AraciKurumEtiket = t.AraciKurumEtiket;
                r.AraciKurumVergiNo = t.AraciKurumVergiNo;
                r.BayiNo = t.BayiNo;
                r.BinaAdi = t.BinaAdi;
                r.BlokAdi = t.BlokAdi;
                r.CiftciNo = t.CiftciNo;
                r.DistributorNo = t.DistributorNo;
                r.DosyaNo = t.DosyaNo;
                r.Eposta = t.Eposta;
                r.Fax = t.Fax;
                r.FirmaAdi = t.FirmaAdi;
                r.HastaNo = t.HastaNo;
                r.HizmetNo = t.HizmetNo;
                r.Il = t.Il;
                r.Ilce = t.Ilce;
                r.IlceSemt = t.IlceSemt;
                r.ImalatciNo = t.ImalatciNo;
                r.KapiNo = t.KapiNo;
                r.KurumKayitNumarasi = t.KurumKayitNumarasi;
                r.KurumResmiUnvan = t.KurumResmiUnvan;
                r.MersisNo = t.MersisNo;
                r.MusteriNo = t.MusteriNo;
                //r.MusteriTuru = t.M
                r.NACEKodu = t.NACEKodu;
                r.PostaKodu = t.PostaKodu;
                r.SayacNo = t.SayacNo;
                r.Sokak = t.Sokak;
                r.SubeNo = t.SubeNo;
                r.TAPDKNo = t.TAPDKNo;
                r.TarafTuru = t.TarafTuru;
                r.Telefon = t.Telefon;
                r.TelefonNo = t.TelefonNo;
                r.TesisatNo = t.TesisatNo;
                r.TicaretSicilNo = t.TicaretSicilNo;
                r.Ulke = t.Ulke;
                r.UreticiNo = t.UreticiNo;
                r.VergiDairesi = t.VergiDairesi;
                r.VergiNoTCKimlikNo = t.VergiNoTCKimlikNo;
                r.VergiTipiKodu = t.VergiTipiKodu;
                r.WebAdresi = t.WebAdresi;
                if (t.Sahis != null)
                {
                    r.Sahis = new DFS.Common.Entity.Sahis();
                    r.Sahis.Ad = t.Sahis.Ad;
                    r.Sahis.Soyad = t.Sahis.Soyad;
                    r.Sahis.Unvan = t.Sahis.Unvan;
                    r.Sahis.Uyruk = t.Sahis.Uyruk;
                    if (t.Sahis.BankaHesap != null)
                    {
                        r.Sahis.BankaHesap = new DFS.Common.Entity.BankaHesap();
                        r.Sahis.BankaHesap.Aciklama = t.Sahis.BankaHesap.Aciklama;
                        r.Sahis.BankaHesap.BankaAdi = t.Sahis.BankaHesap.BankaAdi;
                        r.Sahis.BankaHesap.No = t.Sahis.BankaHesap.No;
                        r.Sahis.BankaHesap.SubeAdi = t.Sahis.BankaHesap.SubeAdi;
                    }
                    if (t.Sahis.Pasaport != null)
                    {
                        r.Sahis.Pasaport = new DFS.Common.Entity.Pasaport();
                        r.Sahis.Pasaport.No = t.Sahis.Pasaport.No;
                        r.Sahis.Pasaport.Tarih = t.Sahis.Pasaport.Tarih;
                    }
                }
            }
            return r;
        }


        static DFS.Common.Entity.Taraf ConvertNetleTaraf(Taraf t)
        {
            DFS.Common.Entity.Taraf r = null;
            if (t != null)
            {
                r = new DFS.Common.Entity.Taraf();
                r.AboneNo = t.AboneNo;
                //r.Adres = t.Add;
                r.AraciKurumEtiket = t.AraciKurumEtiket;
                r.AraciKurumVergiNo = t.AraciKurumVergiNo;
                r.BayiNo = t.BayiNo;
                r.BinaAdi = t.BinaAdi;
                r.BlokAdi = t.BlokAdi;
                r.CiftciNo = t.CiftciNo;
                r.DistributorNo = t.DistributorNo;
                r.DosyaNo = t.DosyaNo;
                r.Eposta = t.Eposta;
                r.Fax = t.Fax;
                r.FirmaAdi = t.FirmaAdi;
                r.HastaNo = t.HastaNo;
                r.HizmetNo = t.HizmetNo;
                r.Il = t.Il;
                r.Ilce = t.Ilce;
                r.IlceSemt = t.IlceSemt;
                r.ImalatciNo = t.ImalatciNo;
                r.KapiNo = t.KapiNo;
                r.KurumKayitNumarasi = t.KurumKayitNumarasi;
                r.KurumResmiUnvan = t.KurumResmiUnvan;
                r.MersisNo = t.MersisNo;
                r.MusteriNo = t.MusteriNo;
                //r.MusteriTuru = t.M
                r.NACEKodu = t.NACEKodu;
                r.PostaKodu = t.PostaKodu;
                r.SayacNo = t.SayacNo;
                r.Sokak = t.Sokak;
                r.SubeNo = t.SubeNo;
                r.TAPDKNo = t.TAPDKNo;
                r.TarafTuru = t.TarafTuru;
                r.Telefon = t.Telefon;
                r.TelefonNo = t.TelefonNo;
                r.TesisatNo = t.TesisatNo;
                r.TicaretSicilNo = t.TicaretSicilNo;
                r.Ulke = t.Ulke;
                r.UreticiNo = t.UreticiNo;
                r.VergiDairesi = t.VergiDairesi;
                r.VergiNoTCKimlikNo = t.VergiNoTCKimlikNo;
                r.VergiTipiKodu = t.VergiTipiKodu;
                r.WebAdresi = t.WebAdresi;
                if (t.Sahis != null)
                {
                    r.Sahis = new DFS.Common.Entity.Sahis();
                    r.Sahis.Ad = t.Sahis.Ad;
                    r.Sahis.Soyad = t.Sahis.Soyad;
                    r.Sahis.Unvan = t.Sahis.Unvan;
                    r.Sahis.Uyruk = t.Sahis.Uyruk;
                    if (t.Sahis.BankaHesap != null)
                    {
                        r.Sahis.BankaHesap = new DFS.Common.Entity.BankaHesap();
                        r.Sahis.BankaHesap.Aciklama = t.Sahis.BankaHesap.Aciklama;
                        r.Sahis.BankaHesap.BankaAdi = t.Sahis.BankaHesap.BankaAdi;
                        r.Sahis.BankaHesap.No = t.Sahis.BankaHesap.No;
                        r.Sahis.BankaHesap.SubeAdi = t.Sahis.BankaHesap.SubeAdi;
                    }
                    if (t.Sahis.Pasaport != null)
                    {
                        r.Sahis.Pasaport = new DFS.Common.Entity.Pasaport();
                        r.Sahis.Pasaport.No = t.Sahis.Pasaport.No;
                        r.Sahis.Pasaport.Tarih = t.Sahis.Pasaport.Tarih;
                    }
                }
            }
            return r;
        }

        static DFS.Common.Entity.NetleEFatura ConvertNetleEFatura(NetleEFatura f)
        {
            DFS.Common.Entity.NetleEFatura r = new DFS.Common.Entity.NetleEFatura();
            r.Aciklama = f.Aciklama;
            r.AlkolluIcecekOTVMatrahi = f.AlkolluIcecekOTVMatrahi;
            r.AlkolluIcecekOTVMuafiyetNedeni = f.AlkolluIcecekOTVMuafiyetNedeni;
            r.AlkolluIcecekOTVTutari = f.AlkolluIcecekOTVTutari;
            r.ArtirimTutari = f.ArtirimTutari;
            r.BarkodUrl = f.BarkodUrl;
            r.BelediyeTuketimVergisiMatrahi = f.BelediyeTuketimVergisiMatrahi;
            r.BelediyeTuketimVergisiMuafiyetNedeni = f.BelediyeTuketimVergisiMuafiyetNedeni;
            r.BelediyeTuketimVergisiTutari = f.BelediyeTuketimVergisiTutari;
            r.BorsaTescilUcretMatrahi = f.BorsaTescilUcretMatrahi;
            r.BorsaTescilUcretMuafiyetNedeni = f.BorsaTescilUcretMuafiyetNedeni;
            r.BorsaTescilUcretTutari = f.BorsaTescilUcretTutari;
            r.BSMVMatrahi = f.BSMVMatrahi;
            r.BSMVMuafiyetNedeni = f.BSMVMuafiyetNedeni;
            r.BSMVTutari = f.BSMVTutari;
            r.CevreTemizlikVergisiMatrahi = f.CevreTemizlikVergisiMatrahi;
            r.CevreTemizlikVergisiMuafiyetNedeni = f.CevreTemizlikVergisiMuafiyetNedeni;
            r.CevreTemizlikVergisiTutari = f.CevreTemizlikVergisiTutari;
            r.DayanikliTuketimOTVMatrahi = f.DayanikliTuketimOTVMatrahi;
            r.DayanikliTuketimOTVMuafiyetNedeni = f.DayanikliTuketimOTVMuafiyetNedeni;
            r.DayanikliTuketimOTVTutari = f.DayanikliTuketimOTVTutari;
            r.DovizTipi = f.DovizTipi;
            r.DuzenlenmeTarihi = f.DuzenlenmeTarihi;
            r.DVKanun5035Matrahi = f.DVKanun5035Matrahi;
            r.DVKanun5035MuafiyetNedeni = f.DVKanun5035MuafiyetNedeni;
            r.DVKanun5035Tutari = f.DVKanun5035Tutari;
            r.DVMatrahi = f.DVMatrahi;
            r.DVMuafiyetNedeni = f.DVMuafiyetNedeni;
            r.DVTutari = f.DVTutari;
            r.EkAciklamalar = f.EkAciklamalar;
            r.ElektrikTuketimVergisiMatrahi = f.ElektrikTuketimVergisiMatrahi;
            r.ElektrikTuketimVergisiMuafiyetNedeni = f.ElektrikTuketimVergisiMuafiyetNedeni;
            r.ElektrikTuketimVergisiTutari = f.ElektrikTuketimVergisiTutari;
            r.EnerjiFonuMatrahi = f.EnerjiFonuMatrahi;
            r.EnerjiFonuMuafiyetNedeni = f.EnerjiFonuMuafiyetNedeni;
            r.EnerjiFonuTutari = f.EnerjiFonuTutari;
            r.ERPFatNo = f.ERPFatNo;
            r.GUID = f.GUID;
            r.IrsaliyeNo = f.IrsaliyeNo;
            r.IrsaliyeTarihi = f.IrsaliyeTarihi;
            r.IskontoTutari = f.IskontoTutari;
            //r.KalemlerToplamTutar = 
            r.KDVMatrahi = f.KDVMatrahi;
            r.KDVMuafiyetNedeni = f.KDVMuafiyetNedeni;
            r.KDVTevkifatMatrahi = f.KDVTevkifatMatrahi;
            r.KDVTevkifatMuafiyetNedeni = f.KDVTevkifatMuafiyetNedeni;
            r.KDVTevkifatTutari = f.KDVTevkifatTutari;
            r.KDVTutari = f.KDVTutari;
            r.KKDFKesintiMatrahi = f.KKDFKesintiMatrahi;
            r.KKDFKesintiMuafiyetNedeni = f.KKDFKesintiMuafiyetNedeni;
            r.KKDFKesintiTutari = f.KKDFKesintiTutari;
            r.KolaliGazozOTVMatrahi = f.KolaliGazozOTVMatrahi;
            r.KolaliGazozOTVMuafiyetNedeni = f.KolaliGazozOTVMuafiyetNedeni;
            r.KolaliGazozOTVTutari = f.KolaliGazozOTVTutari;
            //r.MeraFonuTutari =f.me
            r.MotorluTasitlarOTVMatrahi = f.MotorluTasitlarOTVMatrahi;
            r.MotorluTasitlarOTVMuafiyetNedeni = f.MotorluTasitlarOTVMuafiyetNedeni;
            r.MotorluTasitlarOTVTutari = f.MotorluTasitlarOTVTutari;
            r.No = f.No;
            r.OdemeDovizKuru = f.OdemeDovizKuru;
            r.OdemeDovizTipi = f.OdemeDovizTipi;
            r.OdenecekToplamTutar = f.OdenecekToplamTutar;
            r.OIVKanun5035Matrahi = f.OIVKanun5035Matrahi;
            r.OIVKanun5035MuafiyetNedeni = f.OIVKanun5035MuafiyetNedeni;
            r.OIVKanun5035Tutari = f.OIVKanun5035Tutari;
            r.OIVMatrahi = f.OIVMatrahi;
            r.OIVMuafiyetNedeni = f.OIVMuafiyetNedeni;
            r.OIVTutari = f.OIVTutari;
            r.PetrolDogalgazOTVMatrahi = f.PetrolDogalgazOTVMatrahi;
            r.PetrolDogalgazOTVMuafiyetNedeni = f.PetrolDogalgazOTVMuafiyetNedeni;
            r.PetrolDogalgazOTVTutari = f.PetrolDogalgazOTVTutari;
            r.SaticiSiparisNo = f.SaticiSiparisNo;
            //r.SGKPrimTutari =f.S
            r.SiparisNo = f.SiparisNo;
            r.SiparisTarihi = f.SiparisTarihi;
            r.SiparisTuruKodu = f.SiparisTuruKodu;
            //r.StopajDahilToplamVergi=f.Sto
            r.StopajMatrahi = f.StopajMatrahi;
            r.StopajMuafiyetNedeni = f.StopajMuafiyetNedeni;
            r.StopajTutari = f.StopajTutari;
            r.TelsizKullanimAylikTaksitMatrahi = f.TelsizKullanimAylikTaksitMatrahi;
            r.TelsizKullanimAylikTaksitMuafiyetNedeni = f.TelsizKullanimAylikTaksitMuafiyetNedeni;
            r.TelsizKullanimAylikTaksitTutari = f.TelsizKullanimAylikTaksitTutari;
            r.TelsizRuhsatUcretiMatrahi = f.TelsizRuhsatUcretiMatrahi;
            r.TelsizRuhsatUcretiMuafiyetNedeni = f.TelsizRuhsatUcretiMuafiyetNedeni;
            r.TelsizRuhsatUcretiTutari = f.TelsizRuhsatUcretiTutari;
            //r.TevkifatDusulmemisToplamVergi=f.tev
            r.ToplamTutar = f.ToplamTutar;
            r.ToplamTutarDovizKuru = f.ToplamTutarDovizKuru;
            r.ToplamTutarDovizTipi = f.ToplamTutarDovizTipi;
            //r.ToplamVergi=f.
            r.TRTPayiMatrahi = f.TRTPayiMatrahi;
            r.TRTPayiMuafiyetNedeni = f.TRTPayiMuafiyetNedeni;
            r.TRTPayiTutari = f.TRTPayiTutari;
            r.TutunMamulleriOTVMatrahi = f.TutunMamulleriOTVMatrahi;
            r.TutunMamulleriOTVMuafiyetNedeni = f.TutunMamulleriOTVMuafiyetNedeni;
            r.TutunMamulleriOTVTutari = f.TutunMamulleriOTVTutari;
            r.VergilendirilecekToplamTutar = f.VergilendirilecekToplamTutar;
            r.VergilerDahilTutar = f.VergilerDahilTutar;
            r.VergilerHaricTutar = f.VergilerHaricTutar;
            r.VergiMuafiyetNedeni = f.VergiMuafiyetNedeni;
            r.XsltPath = f.XsltPath;
            r.YuvarlamaTutari = f.YuvarlamaTutari;

            r.AliciMusteri = ConvertNetleTaraf(f.AliciMusteri);
            r.VergiTemsilcisi = ConvertNetleTaraf(f.VergiTemsilcisi);
            r.SaticiTedarikcisi = ConvertNetleTaraf(f.SaticiTedarikcisi);
            r.EkSahalar = ConvertNetleEkSahalar(f.EkSahalar);
            r.Vergiler = ConvertNetleVergiler(f.Vergiler);
            r.Musteri = ConvertNetleTaraf(f.Musteri);
            r.Tedarikci = ConvertNetleTaraf(f.Tedarikci);
 
            if (f.SGKOzelFaturaAlanlari != null)
            {
                r.SGKOzelFaturaAlanlari.DosyaNo = f.SGKOzelFaturaAlanlari.DosyaNo;
                if (f.SGKOzelFaturaAlanlari.FaturaDonemi != null)
                {
                    r.SGKOzelFaturaAlanlari.FaturaDonemi = new DFS.Common.Entity.FaturaDonemi();
                    r.SGKOzelFaturaAlanlari.FaturaDonemi.BaslangicTarihi = f.SGKOzelFaturaAlanlari.FaturaDonemi.BaslangicTarihi;
                    r.SGKOzelFaturaAlanlari.FaturaDonemi.BitisTarihi = f.SGKOzelFaturaAlanlari.FaturaDonemi.BitisTarihi;
                }
                r.SGKOzelFaturaAlanlari.IlaveFaturaTipi = (DFS.Common.Entity.SGKIlaveFaturaTipi) ((int) f.SGKOzelFaturaAlanlari.IlaveFaturaTipi);
                r.SGKOzelFaturaAlanlari.MukellefAdi = f.SGKOzelFaturaAlanlari.MukellefAdi;
                r.SGKOzelFaturaAlanlari.MukellefKodu = f.SGKOzelFaturaAlanlari.MukellefKodu;
            }

            if (f.SevkIrsaliye != null)
            {
                r.SevkIrsaliye = new DFS.Common.Entity.SevkIrsaliye();
                r.SevkIrsaliye.ETTN = f.SevkIrsaliye.ETTN;
                r.SevkIrsaliye.No = f.SevkIrsaliye.No;
                r.SevkIrsaliye.Tarih = f.SevkIrsaliye.Tarih;
            }
            
            if (f.OKCBilgiFisleri != null)
            {
                List<DFS.Common.Entity.OKCBilgiFisi> okcBilgiFisList = new List<DFS.Common.Entity.OKCBilgiFisi>();
                foreach (OKCBilgiFisi o in f.OKCBilgiFisleri)
                {
                    DFS.Common.Entity.OKCBilgiFisi ot = new DFS.Common.Entity.OKCBilgiFisi();
                    ot.No = o.No;
                    ot.OKCSeriNo = o.OKCSeriNo;
                    ot.Tarih = o.Tarih;
                    ot.Tip = (DFS.Common.Entity.OKCBilgiFisiTipi) ((int)o.Tip);
                    ot.ZRaporuNo = o.ZRaporuNo;
                    okcBilgiFisList.Add(ot);
                }
                r.OKCBilgiFisleri = okcBilgiFisList.ToArray();
            }
            
            if (f.Irsaliyeler != null)
            {
                List<DFS.Common.Entity.Irsaliye> irsaliyeList = new List<DFS.Common.Entity.Irsaliye>();
                foreach (Irsaliye i in f.Irsaliyeler)
                {
                    DFS.Common.Entity.Irsaliye it = new DFS.Common.Entity.Irsaliye();
                    it.No = i.No;
                    it.Tarih = i.Tarih;
                    irsaliyeList.Add(it);
                }
                r.Irsaliyeler = irsaliyeList.ToArray();
            }

            if (f.FaturaKalemleri != null)
            {
                List<DFS.Common.Entity.FaturaKalemi> kalemList = new List<DFS.Common.Entity.FaturaKalemi>();
                foreach (FaturaKalemi k in f.FaturaKalemleri)
                {
                    DFS.Common.Entity.FaturaKalemi kt = new DFS.Common.Entity.FaturaKalemi();
                    kt.Aciklama = k.Aciklama;
                    kt.AlkolluIcecekOTVMatrahi = k.AlkolluIcecekOTVMatrahi;
                    kt.AlkolluIcecekOTVMuafiyetNedeni = k.AlkolluIcecekOTVMuafiyetNedeni;
                    kt.AlkolluIcecekOTVOrani = k.AlkolluIcecekOTVOrani;
                    kt.AlkolluIcecekOTVTutari = k.AlkolluIcecekOTVTutari;
                    kt.BelediyeTuketimVergisiMatrahi = k.BelediyeTuketimVergisiMatrahi;
                    kt.BelediyeTuketimVergisiMuafiyetNedeni = k.BelediyeTuketimVergisiMuafiyetNedeni;
                    kt.BelediyeTuketimVergisiOrani = k.BelediyeTuketimVergisiOrani;
                    kt.BelediyeTuketimVergisiTutari = k.BelediyeTuketimVergisiTutari;
                    kt.Birim = k.Birim;
                    kt.BirimFiyat = k.BirimFiyat;
                    kt.BorsaTescilUcretMatrahi = k.BorsaTescilUcretMatrahi;
                    kt.BorsaTescilUcretMuafiyetNedeni = k.BorsaTescilUcretMuafiyetNedeni;
                    kt.BorsaTescilUcretOrani = k.BorsaTescilUcretOrani;
                    kt.BorsaTescilUcretTutari = k.BorsaTescilUcretTutari;
                    kt.BSMVMatrahi = k.BSMVMatrahi;
                    kt.BSMVMuafiyetNedeni = k.BSMVMuafiyetNedeni;
                    kt.BSMVOrani = k.BSMVOrani;
                    kt.BSMVTutari = k.BSMVTutari;
                    kt.CevreTemizlikVergisiMatrahi = k.CevreTemizlikVergisiMatrahi;
                    kt.CevreTemizlikVergisiMuafiyetNedeni = k.CevreTemizlikVergisiMuafiyetNedeni;
                    kt.CevreTemizlikVergisiOrani = k.CevreTemizlikVergisiOrani;
                    kt.CevreTemizlikVergisiTutari = k.CevreTemizlikVergisiTutari;
                    kt.CevrimDovizKuru = k.CevrimDovizKuru;
                    kt.CevrimDovizTipi = k.CevrimDovizTipi;
                    kt.DayanikliTuketimOTVMatrahi = k.DayanikliTuketimOTVMatrahi;
                    kt.DayanikliTuketimOTVMuafiyetNedeni = k.DayanikliTuketimOTVMuafiyetNedeni;
                    kt.DayanikliTuketimOTVOrani = k.DayanikliTuketimOTVOrani;
                    kt.DayanikliTuketimOTVTutari = k.DayanikliTuketimOTVTutari;
                    kt.DovizTipi = k.DovizTipi;
                    kt.DVKanun5035Matrahi = k.DVKanun5035Matrahi;
                    kt.DVKanun5035MuafiyetNedeni = k.DVKanun5035MuafiyetNedeni;
                    kt.DVKanun5035Orani = k.DVKanun5035Orani;
                    kt.DVKanun5035Tutari = k.DVKanun5035Tutari;
                    kt.DVMatrahi = k.DVMatrahi;
                    kt.DVMuafiyetNedeni = k.DVMuafiyetNedeni;
                    kt.DVOrani = k.DVOrani;
                    kt.DVTutari = k.DVTutari;
                    kt.EkSahalar = ConvertNetleEkSahalar(k.EkSahalar);
                    kt.EksikMalAdedi = k.EksikMalAdedi;
                    kt.EksikMalAdediBirim = k.EksikMalAdediBirim;
                    kt.ElektrikTuketimVergisiMatrahi = k.ElektrikTuketimVergisiMatrahi;
                    kt.ElektrikTuketimVergisiMuafiyetNedeni = k.ElektrikTuketimVergisiMuafiyetNedeni;
                    kt.ElektrikTuketimVergisiOrani = k.ElektrikTuketimVergisiOrani;
                    kt.ElektrikTuketimVergisiTutari = k.ElektrikTuketimVergisiTutari;
                    kt.EnerjiFonuMatrahi = k.EnerjiFonuMatrahi;
                    kt.EnerjiFonuMuafiyetNedeni = k.EnerjiFonuMuafiyetNedeni;
                    kt.EnerjiFonuOrani = k.EnerjiFonuOrani;
                    kt.EnerjiFonuTutari = k.EnerjiFonuTutari;
                    kt.FazlaMalAdedi = k.FazlaMalAdedi;
                    kt.FazlaMalAdediBirim = k.FazlaMalAdediBirim;
                    kt.GecTeslimSikayetAciklamasi = k.GecTeslimSikayetAciklamasi;
                    kt.GonderilenMalAdedi = k.GonderilenMalAdedi;
                    kt.GonderilenMalAdediBirim = k.GonderilenMalAdediBirim;
                    kt.IlerikiTarihteGonderilecekMalAdedi = k.IlerikiTarihteGonderilecekMalAdedi;
                    kt.IlerikiTarihteGonderilecekMalAdediBirim = k.IlerikiTarihteGonderilecekMalAdediBirim;
                    kt.IlerikiTarihteMalGondermeNedenleri = k.IlerikiTarihteMalGondermeNedenleri;
                    kt.IskontoAciklama = k.IskontoAciklama;
                    kt.IskontoOrani = k.IskontoOrani;
                    kt.IskontoTutari = k.IskontoTutari;
                    kt.KabulEdilmeyenMalAdedi = k.KabulEdilmeyenMalAdedi;
                    kt.KabulEdilmeyenMalAdediBirim = k.KabulEdilmeyenMalAdediBirim;
                    kt.KDVMatrahi = k.KDVMatrahi;
                    kt.KDVMuafiyetNedeni = k.KDVMuafiyetNedeni;
                    kt.KDVOrani = k.KDVOrani;
                    kt.KDVTevkifatMatrahi = k.KDVTevkifatMatrahi;
                    kt.KDVTevkifatMuafiyetNedeni= k.KDVTevkifatMuafiyetNedeni;
                    kt.KDVTevkifatOrani = k.KDVTevkifatOrani;
                    kt.KDVTevkifatTutari = k.KDVTevkifatTutari;
                    kt.KDVTutari = k.KDVTutari;
                    kt.KKDFKesintiMatrahi = k.KKDFKesintiMatrahi;
                    kt.KKDFKesintiMuafiyetNedeni = k.KKDFKesintiMuafiyetNedeni;
                    kt.KKDFKesintiOrani = k.KKDFKesintiOrani;
                    kt.KKDFKesintiTutari = k.KKDFKesintiTutari;
                    kt.KolaliGazozOTVMatrahi = k.KolaliGazozOTVMatrahi;
                    kt.KolaliGazozOTVMuafiyetNedeni = k.KolaliGazozOTVMuafiyetNedeni;
                    kt.KolaliGazozOTVOrani = k.KolaliGazozOTVOrani;
                    kt.KolaliGazozOTVTutari = k.KolaliGazozOTVTutari;
                    kt.MaliReddetmeNedenleri = k.MaliReddetmeNedenleri;
                    kt.Miktar = k.Miktar;
                    kt.Model = k.Model;
                    kt.MotorluTasitlarOTVMatrahi = k.MotorluTasitlarOTVMatrahi;
                    kt.MotorluTasitlarOTVMuafiyetNedeni = k.MotorluTasitlarOTVMuafiyetNedeni;
                    kt.MotorluTasitlarOTVOrani = k.MotorluTasitlarOTVOrani;
                    kt.MotorluTasitlarOTVTutari = k.MotorluTasitlarOTVTutari;
                    kt.MusteriStokNo = k.MusteriStokNo;
                    kt.OIVKanun5035Matrahi = k.OIVKanun5035Matrahi;
                    kt.OIVKanun5035MuafiyetNedeni = k.OIVKanun5035MuafiyetNedeni;
                    kt.OIVKanun5035Orani = k.OIVKanun5035Orani;
                    kt.OIVKanun5035Tutari = k.OIVKanun5035Tutari;
                    kt.OIVMatrahi = k.OIVMatrahi;
                    kt.OIVMuafiyetNedeni = k.OIVMuafiyetNedeni;
                    kt.OIVOrani = k.OIVOrani;
                    kt.OIVTutari = k.OIVTutari;
                    kt.PetrolDogalgazOTVMatrahi = k.PetrolDogalgazOTVMatrahi;
                    kt.PetrolDogalgazOTVMuafiyetNedeni = k.PetrolDogalgazOTVMuafiyetNedeni;
                    kt.PetrolDogalgazOTVOrani = k.PetrolDogalgazOTVOrani;
                    kt.PetrolDogalgazOTVTutari = k.PetrolDogalgazOTVTutari;
                    kt.SiparisSiraNo = k.SiparisSiraNo;
                    kt.StokAdi = k.StokAdi;
                    kt.StopajMatrahi = k.StopajMatrahi;
                    kt.StopajMuafiyetNedeni = k.StopajMuafiyetNedeni;
                    kt.StopajOrani = k.StopajOrani;
                    kt.StopajTutari = k.StopajTutari;
                    kt.TedarikciStokNo = k.TedarikciStokNo;
                    kt.TelsizKullanimAylikTaksitMatrahi = k.TelsizKullanimAylikTaksitMatrahi;
                    kt.TelsizKullanimAylikTaksitMuafiyetNedeni = k.TelsizKullanimAylikTaksitMuafiyetNedeni;
                    kt.TelsizKullanimAylikTaksitOrani = k.TelsizKullanimAylikTaksitOrani;
                    kt.TelsizKullanimAylikTaksitTutari = k.TelsizKullanimAylikTaksitTutari;
                    kt.TelsizRuhsatUcretiMatrahi = k.TelsizRuhsatUcretiMatrahi;
                    kt.TelsizRuhsatUcretiMuafiyetNedeni = k.TelsizRuhsatUcretiMuafiyetNedeni;
                    kt.TelsizRuhsatUcretiOrani = k.TelsizRuhsatUcretiOrani;
                    kt.TelsizRuhsatUcretiTutari = k.TelsizRuhsatUcretiTutari;
                    kt.TeslimAlinanMalAdedi = k.TeslimAlinanMalAdedi;
                    kt.TeslimAlinanMalAdediBirim = k.TeslimAlinanMalAdediBirim;
                    //kt.TevkifatDusulmemisToplamVergi = k.TeslimAlinanMalAdediBirim;
                    kt.ToplamTutar = k.ToplamTutar;
                    kt.TRTPayiMatrahi = k.TRTPayiMatrahi;
                    kt.TRTPayiMuafiyetNedeni = k.TRTPayiMuafiyetNedeni;
                    kt.TRTPayiOrani = k.TRTPayiOrani;
                    kt.TRTPayiTutari = k.TRTPayiTutari;
                    kt.TutunMamulleriOTVMatrahi = k.TutunMamulleriOTVMatrahi;
                    kt.TutunMamulleriOTVMuafiyetNedeni = k.TutunMamulleriOTVMuafiyetNedeni;
                    kt.TutunMamulleriOTVOrani = k.TutunMamulleriOTVOrani;
                    kt.TutunMamulleriOTVTutari = k.TutunMamulleriOTVTutari;
                    kt.UreticiStokNo = k.UreticiStokNo;
                    kt.Vergiler = ConvertNetleVergiler(k.Vergiler);
                    kalemList.Add(kt);
                }
                r.FaturaKalemleri = kalemList.ToArray();
            }

            switch (f.GonderimTipi)
            {
                case GonderimTipi.KAGIT:
                    r.GonderimTipi = DFS.Common.Entity.GonderimTipi.KAGIT;
                    break;
                case GonderimTipi.ELEKTRONIK:
                default:
                    r.GonderimTipi = DFS.Common.Entity.GonderimTipi.ELEKTRONIK;
                    break;
            }
            switch (f.KaynakDokumanTuru)
            {
                case KaynakDokumanTuru.EADISYON:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.EADISYON;
                    break;
                case KaynakDokumanTuru.EARSIV:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.EARSIV;
                    break;
                case KaynakDokumanTuru.EFATURA:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.EFATURA;
                    break;
                case KaynakDokumanTuru.EGIDERPUSULASI:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.EGIDERPUSULASI;
                    break;
                case KaynakDokumanTuru.EIHRACAT:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.EIHRACAT;
                    break;
                case KaynakDokumanTuru.EIRSALIYE:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.EIRSALIYE;
                    break;
                case KaynakDokumanTuru.EIRSALIYEYANIT:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.EIRSALIYEYANIT;
                    break;
                case KaynakDokumanTuru.EMUSTAHSIL:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.EMUSTAHSIL;
                    break;
                case KaynakDokumanTuru.ESERBESTMESLEKMAKBUZU:
                    r.KaynakDokumanTuru = DFS.Common.Entity.KaynakDokumanTuru.ESERBESTMESLEKMAKBUZU;
                    break;
            }
            switch (f.Senaryo)
            {
                case NetleEFaturaSenaryoType.EARSIVBELGE:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.EARSIVBELGE;
                    break;
                case NetleEFaturaSenaryoType.EARSIVFATURA:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.EARSIVFATURA;
                    break;
                case NetleEFaturaSenaryoType.HKS:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.HKS;
                    break;
                case NetleEFaturaSenaryoType.IHRACAT:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.IHRACAT;
                    break;
                case NetleEFaturaSenaryoType.KAMU:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.KAMU;
                    break;
                case NetleEFaturaSenaryoType.OZELFATURA:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.OZELFATURA;
                    break;
                case NetleEFaturaSenaryoType.TEMELFATURA:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.TEMELFATURA;
                    break;
                case NetleEFaturaSenaryoType.TEMELIRSALIYE:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.TEMELIRSALIYE;
                    break;
                case NetleEFaturaSenaryoType.TICARIFATURA:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.TICARIFATURA;
                    break;
                case NetleEFaturaSenaryoType.YOLCUBERABERFATURA:
                    r.Senaryo = DFS.Common.Entity.NetleEFaturaSenaryoType.YOLCUBERABERFATURA;
                    break;
            }
            switch (f.Tip)
            {
                case NetleEFaturaType.HKSKOMISYONCU:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.HKSKOMISYONCU;
                    break;
                case NetleEFaturaType.HKSSATIS:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.HKSSATIS;
                    break;
                case NetleEFaturaType.IADE:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.IADE;
                    break;
                case NetleEFaturaType.IHRACKAYITLI:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.IHRACKAYITLI;
                    break;
                case NetleEFaturaType.ISTISNA:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.ISTISNA;
                    break;
                case NetleEFaturaType.KOMISYONCU:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.KOMISYONCU;
                    break;
                case NetleEFaturaType.MATBUDAN:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.MATBUDAN;
                    break;
                case NetleEFaturaType.OZELMATRAH:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.OZELMATRAH;
                    break;
                case NetleEFaturaType.SATIS:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.SATIS;
                    break;
                case NetleEFaturaType.SEVK:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.SEVK;
                    break;
                case NetleEFaturaType.SGK:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.SGK;
                    break;
                case NetleEFaturaType.TEVKIFAT:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.TEVKIFAT;
                    break;
                case NetleEFaturaType.TEVKIFATIADE:
                    r.Tip = DFS.Common.Entity.NetleEFaturaType.TEVKIFATIADE;
                    break;
            }

            if (f.Gonderi != null)
            {
                r.Gonderi = new DFS.Common.Entity.Gonderi();
                r.Gonderi.KargoNo = f.Gonderi.KargoNo;
                r.Gonderi.UrunToplamTutar = f.Gonderi.UrunToplamTutar;
                if (f.Gonderi.GonderiFazlari != null)
                {
                    List<DFS.Common.Entity.GonderiFazi> gonderiFazList = new List<DFS.Common.Entity.GonderiFazi>();
                    foreach (GonderiFazi gf in f.Gonderi.GonderiFazlari)
                    {
                        DFS.Common.Entity.GonderiFazi gft = new DFS.Common.Entity.GonderiFazi();
                        if (gf.TasimaSekli != null)
                        {
                            DFS.Common.Entity.TasimaSekli gftst = new DFS.Common.Entity.TasimaSekli();
                            if (gf.TasimaSekli.DemiryoluTasimaciligi != null)
                            {
                                gftst.DemiryoluTasimaciligi = new DFS.Common.Entity.DemiryoluTasimaciligi();
                                gftst.DemiryoluTasimaciligi.TrenNo = gf.TasimaSekli.DemiryoluTasimaciligi.TrenNo;
                                gftst.DemiryoluTasimaciligi.VagonNo = gf.TasimaSekli.DemiryoluTasimaciligi.VagonNo;
                            }
                            if (gf.TasimaSekli.DenizTasimaciligi != null)
                            {
                                gftst.DenizTasimaciligi = new DFS.Common.Entity.DenizTasimaciligi();
                                gftst.DenizTasimaciligi.GemiAdi = gf.TasimaSekli.DenizTasimaciligi.GemiAdi;
                                gftst.DenizTasimaciligi.IMONo = gf.TasimaSekli.DenizTasimaciligi.IMONo;
                            }
                            if (gf.TasimaSekli.HavaTasimaciligi != null)
                            {
                                gftst.HavaTasimaciligi = new DFS.Common.Entity.HavaTasimaciligi();
                                gftst.HavaTasimaciligi.HavaAraciNo = gf.TasimaSekli.HavaTasimaciligi.HavaAraciNo;
                            }
                            if (gf.TasimaSekli.KarayoluTasimaciligi != null)
                            {
                                gftst.KarayoluTasimaciligi = new DFS.Common.Entity.KarayoluTasimaciligi();
                                gftst.KarayoluTasimaciligi.PlakaNo = gf.TasimaSekli.KarayoluTasimaciligi.PlakaNo;
                            }
                            gft.TasimaSekli = gftst;
                        }
                        gonderiFazList.Add(gft);
                    }
                    r.Gonderi.GonderiFazlari = gonderiFazList.ToArray();
                }
                if (f.Gonderi.TasimaUniteleri != null)
                {
                    List<DFS.Common.Entity.TasimaUnitesi> tasimaUniteList = new List<DFS.Common.Entity.TasimaUnitesi>();
                    foreach (TasimaUnitesi tu in f.Gonderi.TasimaUniteleri)
                    {
                        DFS.Common.Entity.TasimaUnitesi tut = new DFS.Common.Entity.TasimaUnitesi();
                        tut.DorsePlakalari = tu.DorsePlakalari;
                    }
                }
                if (f.Gonderi.Teslimat != null)
                {
                    r.Gonderi.Teslimat = new DFS.Common.Entity.Teslimat();
                    r.Gonderi.Teslimat.FiiliSevkTarihi = f.Gonderi.Teslimat.FiiliSevkTarihi;
                    r.Gonderi.Teslimat.GerceklesenTeslimTarihi = f.Gonderi.Teslimat.GerceklesenTeslimTarihi;
                    r.Gonderi.Teslimat.Tasiyici = ConvertNetleTaraf(f.Gonderi.Teslimat.Tasiyici);
                    r.Gonderi.Teslimat.TeslimatYapilacak = ConvertNetleTaraf(f.Gonderi.Teslimat.TeslimatYapilacak);
                }

            }


            return r;
        }

        static NetleEFatura GetAdisyonInvoice(Guid uuid)
        {
            var nef = new NetleEFatura();
            nef.GUID = Guid.NewGuid().ToString();
            ///  Belge numarası, özel bir yapıdadır, ilk 3 karakter fatura serisi(Burada seri ADS olarak belirlendi), 
            /// sonraki dört karakter yıl (2018) sonraki 9 karakter, müteselsil şekilde devam eden numara olmalıdır. 
            /// Fatura numarası ve fatura tarihi birbiriyle sıralı olmalıdır.              
            nef.DuzenlenmeTarihi = DateTime.Now; // Belge düzenlenme zamanı             
            nef.Tip = NetleEFaturaType.SATIS; // Bu şekilde sabit belirtilmelidir.             
            nef.Aciklama = "Yalnız : DörtBinYüz TL"; // Belgede not olarak eklenecek bir açıklama varsa yazılır. 
            nef.No = "ADS2022000000315";

            // Bu şekilde sabit olmalıdır. 
            nef.Senaryo = NetleEFaturaSenaryoType.EARSIVBELGE;

            // Belgeyi kesen kişi / firmanın bilgileri. Belge bir şahıs tarafından kesiliyorsa Sahis bölümü doldurulmalıdır.  
            nef.Tedarikci = new Tedarikci
            {
                FirmaAdi = "Adisyonİsim AdisyonSoyisim ",
                Il = "test",
                IlceSemt = "test",
                Sokak = "Test adres 03",
                Sahis = new Sahis { Ad = "Adisyonİsim", Soyad = "AdisyonSoyisim" },
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
            fatKalem.BirimFiyat = 350;
            fatKalem.ToplamTutar = 1750;
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
            vergi.Tutar = 140;
            vergi.Matrah = 1750;
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
            vergi.Tutar = 140;
            vergi.Matrah = 1750;
            vergiler.Add(vergi);

            vergi = new Vergi();
            vergi.Tur = TaxCodeContentType.Item0003;
            vergi.Oran = 20;
            vergi.Tutar = 931.82;
            vergi.Matrah = 4659.1;
            vergiler.Add(vergi);
            nef.Vergiler = vergiler.ToArray();

            nef.ToplamTutar = 1750;
            nef.OdenecekToplamTutar = 1890; 
            nef.DovizTipi = "TRY";
            nef.VergilerDahilTutar = 1890;
            nef.KaynakDokumanTuru = KaynakDokumanTuru.EADISYON;

            /// Belgeler alıcıya iki şekilde gönderilir, kağıt çıktısı alınarak ya da elektonik yolla e-posta gönderilerek. 
            /// Bu gönderim tipi belgede belirtilmelidir çünkü bu bilgi entegratör tarafından ay sonunda oluşturulup 
            /// GIB'e gönderilecek raporda doğru biçimde bildirilmelidir.
            nef.GonderimTipi = GonderimTipi.KAGIT;

            return nef;
        }
    }
}
