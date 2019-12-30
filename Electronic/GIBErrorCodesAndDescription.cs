switch (TRAStatusCode)
{
    case 0: return "Internal.Netle.STate.0.ZARF OLUSTURULDU";

    case 1: return "Internal.Netle.STate.1.ZARF OLUSTURULDU";

    case 1000: return "ZARF KUYRUGA EKLENDI";

    case 1100: return "ZARF ISLENIYOR";

    case 1110: return "ZIP DOSYASI DEGIL";

    case 1111: return "ZARF ID UZUNLUGU GECERSIZ";

    case 1120: return "ZARF ARSIVDEN_KOPYALANAMADI";

    case 1130: return "ZIP ACILAMADI";

    case 1131: return "ZIP BIR DOSYA ICERMELI";

    case 1132: return "XML DOSYASI DEGIL";

    case 1133: return "ZARF ID VE XML DOSYASININ ADI AYNI OLMALI";

    case 1140: return "DOKUMAN AYRISTIRILAMADI";

    case 1141: return "ZARF ID YOK";

    case 1142: return "ZARF ID VE ZIP DOSYASI ADI AYNI OLMALI";

    case 1143: return "GECERSIZ_VERSIYON";

    case 1145: return "UBLTR SÜRÜMÜ 1.2 DEĞİL";

    case 1150: return "SCHEMATRON KONTROL SONUCU HATALI";

    case 1160: return "XML SEMA KONTROLUNDEN GECEMEDI";

    case 1161: return "IMZA SAHIBI TCKN VKN ALINAMADI";

    case 1162: return "IMZA KAYDEDILEMEDI";

    case 1163: return "GONDERILEN ZARF SISTEMDE DAHA ONCE KAYITLI OLAN BIR FATURAYI ICERMEKTEDIR.";

    case 1164: return "GONDERILEN ZARF SISTEMDE DAHA ONCE KAYITLI OLAN BIR BELGEYİ ICERMEKTEDIR.";

    case 1170: return "YETKI KONTROL EDILEMEDI";

    case 1171: return "GONDERICI BIRIM YETKISI YOK";

    case 1172: return "POSTA KUTUSU YETKISI YOK";

    case 1175: return "IMZA YETKISI KONTROL EDILEMEDI";

    case 1176: return "IMZA SAHIBI YETKISIZ";

    case 1177: return "GEÇERSİZ İMZA";

    case 1180: return "ADRES KONTROL EDILEMEDI";

    case 1181: return "ADRES BULUNAMADI";

    case 1182: return "KULLANICI EKLENEMEDİ";

    case 1183: return "KULLANICI SİLİNEMEDİ";

    case 1190: return "SISTEM YANITI HAZIRLANAMADI";

    case 1195: return "SISTEM HATASI";

    case 1200: return "ZARF BASARIYLA ISLENDI";

    case 1210: return "DOKUMAN BULUNAN ADRESE GONDERILEMEDI";

    case 1215: return "DOKUMAN GONDERIMI BASARISIZ. TERKAR GONDERME SONLANDI";

    case 1220: return "HEDEFTEN SISTEM YANITI GELMEDI";

    case 1230: return "HEDEFTEN SISTEM YANITI BASARISIZ GELDI";

    case 1235: return "İPTAL EDİLMİŞ ZARF";

    case 1300: return "BASARIYLA TAMAMLANDI";

    case 2000: return "ZARF HASH DEĞERİ GEÇERSİZ (MD5)";

    case 2004: return "ZARF BULUNAMADI";

    case 2005: return "SİSTEM HATASI";

    default: return "Not-Defined. Kod: " + responseCode;
}
