# İrsaliye Şematron Düzenlemeleri - 19.07.2021

GİB, şematronda denetiminde irsaliye ile ilgili aşağıdaki düzenlemeleri yapmıştır. 

UBL ve NetleEfatura üzerinden gönderilen e-irsaliye belgeleri için yapılması ya da kontrol edilmesi gereken maddeler aşağıda sıralanmıştır. 

GİB bu düzenlemeleri 19 Temmuz tarihinden itibaren uygulamaya alacak. 
Bu düzenlemenin öncesinde entegratör sistemlerimiz de 13.07.2021 tarihinde güncellenecektir.

Bu düzenleme ve güncelleme planlarına e-irsaliye entegrasyonlarında  gerekli düzenlemelerin ve kontrollerin yapılması gerekmektedir.

## LicensePlateIDSchemeIDCheck kontrolü eklendi
* Karayolu taşımacılığı yapılıyorsa LicensePlateID alanı dolduruluyorsa, schemeId değeri DORSE veya PLAKA olarak düzenlenmelidir.

### UBL
* cac:Shipment/cac:ShipmentStage/cac:TransportMeans/cac:RoadTransport/cbc:LicensePlateID schemeID alanı 

### NetleEFatura
* NetleEFatura/Gonderi/GonderiFazlari/GonderiFazi/TasimaSekli/KarayoluTasimaciligi/PlakaNo 
alanı doldurulduğunda schemeId değeri otomatik doldurulur.

## DespatchAddressCheck kontrolü eklendi
* Teslimat adresinde İlçe/Semt, İl, Ülke ve Posta kodu bilgileri zorunlu ve verilen formata uygun olmalıdır.

### UBL
* cac:Shipment/cac:Delivery/cac:DeliveryAddress/cbc:CitySubdivisionName
* cac:Shipment/cac:Delivery/cac:DeliveryAddress/cbc:CityName
* cac:Shipment/cac:Delivery/cac:DeliveryAddress/cac:Country/cbc:Name
* cac:Shipment/cac:Delivery/cac:DeliveryAddress/cbc:PostalZone

### NetleEFatura
* NetleEFatura/Gonderi/Teslimat/TeslimatYapilacak/IlceSemt
* NetleEFatura/Gonderi/Teslimat/TeslimatYapilacak/Il
* NetleEFatura/Gonderi/Teslimat/TeslimatYapilacak/Ulke
* NetleEFatura/Gonderi/Teslimat/TeslimatYapilacak/PostaKodu

## DespatchTimeCheck kontrolü eklendi
* Teslimat tarihi yanında zamanın da bildirilmesi zorunludur.
### UBL
* cac:Shipment/cac:Delivery/cac:Despatch/cbc:ActualDespatchTime
### NetleEFatura
* NetleEFatura/Gonderi/Teslimat/FiiliSevkTarihi
 alanından otomatik olarak saat bilgisi doldurulmaktadır.

## DespatchCarrierDriverCheck kontrolü eklendi
* Taşıyıcı veya sürücü bilgilerinden en az birini içermelidir. Sürücü bilgileri belirtilecekse sürücüye ait Ad, Soyad, Uyruk bilgileri zorunludur.
### UBL
* cac:Shipment/cac:ShipmentStage/cac:DriverPerson veya cac:Shipment/cac:Delivery/cac:CarrierParty alanlarından en az birini içermelidir

#### cac:Shipment/cac:ShipmentStage/cac:DriverPerson alanı doluysa
* cac:Shipment/cac:ShipmentStage/cac:DriverPerson/cbc:FirstName
* cac:Shipment/cac:ShipmentStage/cac:DriverPerson/cbc:FamilyName
* *cac:Shipment/cac:ShipmentStage/cac:DriverPerson/cbc:NationalityID 
alanları dolu olmalıdır    

### NetleEFatura:
* NetleEFatura/Gonderi/Teslimat/Tasiyici veya NetleEFatura/Gonderi/GonderiFazlari/GonderiFazi/Suruculer alanlarından en az birini içermelidir

#### NetleEFatura/Gonderi/GonderiFazlari/GonderiFazi/Suruculer/Surucu alanı doluysa
* NetleEFatura/Gonderi/GonderiFazlari/GonderiFazi/Suruculer/Surucu/Ad
* NetleEFatura/Gonderi/GonderiFazlari/GonderiFazi/Suruculer/Surucu/ Soyad
* NetleEFatura/Gonderi/GonderiFazlari/GonderiFazi/Suruculer/Surucu/Uyruk

## CarrierParty  PartyIdentification kontrolleri eklendi.
* Taşıyıcı bilgilerinde TCKN/VKN bilgisi ve uzunluk kontrolü yapılmaktadır.
### UBL
* cac:Shipment/cac:Delivery/cac:CarrierParty/cac:PartyIdentification
* cac:Shipment/cac:Delivery/cac:CarrierParty/cac:PartyIdentification/cbc:Id schemeId
### NetleEFatura
* NetleEFatura/Gonderi/Teslimat/Tasiyici/VergiNoTCKimlikNo
Alanının değeri kontrol edilmelidir



