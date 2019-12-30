# Özel Entegratörlerin Listesi ve Entegrasyon Adresleri

## Genel Bilgilendirme
Türkiye'deki GİB sistemine dahil olan ve hizmet veren tüm entegratör çözümleri Netle Yazılım tarafından tasarlanmıştır. Çekirdek (Core) katmanlar ve bunlara bağlı entegrasyonlar için aşağıdaki modüller ve ilgili entegrasyon adreslerine yönelik projeler geliştirilebilir.
Gelir İdaresi Başkanlığı'na dahil olan özel entegratörlerine https://ebelge.gib.gov.tr/efaturaozelentegratorlerlistesi.html adresinden erişim yapılabilir.

## Notlar
- Barındırma ortamı 'Netle' olan sistemler iş ortaklarına özel geliştirme sürecine katkı sağlaması için tasarlanmıştır.
- Diğer entegratörlere ilişkin (Protel, Oytek ve Erciyes) hizmet veren web servis adresleri ilgili özel entegratörlerin destek birimleri tarafından sağlanmaktadır.

## Modüller
- e-fatura 
- e-arşiv
- e-defter
- e-irsaliye
- e-Bilet
- e-SMM
- e-MM
- e-Belge

## Entegrasyon adresleri ve Canlı/Test ortamlar
|Barındırma Ortamı|Modül|Ortam|Url
|---|---|---|---|
|IBM|e-invoice|TEST|https://efaturatest.seriltd.com.tr/entegrasyon10/EFaturaEntegrasyonu.asmx
|IBM|e-archive|TEST|https://efaturatest.seriltd.com.tr/EArcWebService/EArcIntegration10.asmx
|IBM|e-invoice|PROD|https://efatura.seriltd.com.tr/entegrasyon10/EFaturaEntegrasyonu.asmx
|IBM|e-archive|PROD|https://efatura.seriltd.com.tr/EArcWebService/EArcIntegration10.asmx
|IBM|(CommonWS) e-invoice, e-archive, e-dispatch, e-ticket, e-smm, e-mm|PROD|https://efatura.seriltd.com.tr/CommonInvoice.Web.Service/Integration10.asmx
|IBM|e-ledger|PROD|https://efatura.seriltd.com.tr/eledgerintegration/integration10.asmx
|Türk Telekom|(CommonWS) e-invoice, e-archive, e-dispatch, e-ticket, e-smm, e-mm|PROD|https://efaturaent.turktelekom.com.tr:10443/CommonInvoice.Web.Service/Integration10.asmx
|Türk Telekom|(CommonWS) e-invoice, e-archive, e-dispatch, e-ticket, e-smm, e-mm|TEST|http://efaturatest.turktelekom.com.tr/CommonInvoice.Web.Service/Integration10.asmx
|Inposia|KSG|PROD|https://e-inposia.com/KSG.Web.Service/KSGIntegration10.asmx
|Inposia|(CommonWS)e-invoice, e-archive, e-dispatch, e-ticket, e-smm, e-mm|PROD|https://e-inposia.com/commonInvoice.web.service/integration10.asmx
|Inposia|e-ledger|PROD|https://e-inposia.com/eledgerIntegration/Integration10.asmx
|Netle|emutabakat (ereconciliation)|PROD|https://emutabakat.netle.com.tr/CommonWebService/Reconciliation10.asmx
|Netle|emutabakat (ereconciliation)|TEST|https://payment.netle.com.tr/CommonInvoice.Web.Service/reconciliation10.asmx
|Netle|KSG|TEST|https://payment.netle.com.tr/KSG.Web.Service/KSGIntegration10.asmx
