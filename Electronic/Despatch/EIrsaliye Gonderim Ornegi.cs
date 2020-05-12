Integration10 client = new Integration10();

client.Url = @" https://efaturatest.inposia.com.tr/CommonInvoice.Web.Service/Integration10.asmx";

var token = client.CreateUserToken("UserName", "Password", ModuleType.eDespatch);

var nef = new NetleEFatura();
nef.KaynakDokumanTuru = KaynakDokumanTuru.EIRSALIYE;

#region [  invoice header  ]

var uuid = Guid.NewGuid();
nef.GUID = uuid.ToString();
nef.Tedarikci = new Tedarikci()
{
	FirmaAdi = "Sender Company", // company name
	VergiNoTCKimlikNo = "4650433027", // company tax number
	WebAdresi = "www.inposia.com.tr", // web address
	Il = "izmir", // city
	IlceSemt = "konak", //district
	Ulke = "Türkiye", //country
	PostaKodu = "35000", // postal code
	VergiDairesi = "konak" // tax office

};

nef.Musteri = new Musteri()
{
	FirmaAdi = "Receiver Company", // company name
	VergiNoTCKimlikNo = "4650433027", // company tax number
	WebAdresi = "www.inposia.com.tr", // web adress
	Il = "izmir", // city
	IlceSemt = "konak", // district
	Ulke = "Türkiye", // country
	PostaKodu = "35310", // post code
	VergiDairesi = "konak" //tax office
	//HastaNo = "urn:mail:1234567801_1_pk@inposia.com.tr", // Alıcının birden fazla etiketi varsa iligl etiket bu alana atanır
};

nef.DuzenlenmeTarihi = DateTime.Now;

///format: AAA:3 Year:4 Number:9 - ardışık
///fatura  numarası
nef.No = "4TR2018210000488";
//universal currency code
nef.DovizTipi = "TRY";
//fatura tipi -irsaliye için sabit
nef.Tip = NetleEFaturaType.SEVK;
//senaryo  -irsaliye için sabit
nef.Senaryo = NetleEFaturaSenaryoType.TEMELIRSALIYE;

#endregion

#region [  items  ]
nef.FaturaKalemleri = new FaturaKalemi[]
	{
		new FaturaKalemi() {                   
			Miktar=500, 
			//BirimFiyat=100,  //irsaliye için zorunlu değil
			//ToplamTutar=200,  //irsaliye için zorunlu değil 
			StokAdi="87500344 -  LC 40W 900mA fixC SC ADV", 
			Birim="PCE", 
			DovizTipi="TRY",
			GonderilenMalAdedi=500,
			GonderilenMalAdediBirim="PCE",
			Aciklama="testetestest"
		}
	};
#endregion

//Tutar ile ilgili sahalar e-irsaliye kapsamında zorunlu değildir. İstege bağlı olarak eklenebilir.
#region [  legalmonetryfields    ]
///line extension amount
//nef.ToplamTutar = 300;

/////tax inclusive 
//nef.KDVMatrahi = 300;

/////tax amount
//nef.KDVTutari = 59;

/////payable amount
//nef.OdenecekToplamTutar = 359;

#endregion

//Zorunlu alan
#region [  Gonderi  -  Teslimat  ]

nef.Gonderi = new Gonderi()
{
	Teslimat = new Teslimat()
	{
		FiiliSevkTarihi = DateTime.Now,
		GerceklesenTeslimTarihi = DateTime.Now,
		Tasiyici = new Taraf()
		{
			Sahis = new Sahis()
			{
				Ad = "Tasıyıcı ad",
				Soyad = "Tasıyıcı soyadı"
			},
			VergiDairesi = "test"
		}

	}

};

#endregion

client.UploadInvoice(token, uuid, "", nef);

