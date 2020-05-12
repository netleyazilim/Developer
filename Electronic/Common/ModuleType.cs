/*
Web servis ile oturum açmak için kullanılan module type değerleri.

SAP sisteminde, enum veri türleri 'string' olarak yazılmalıdır ve case-sensitive olarak bu değerler kullanılmalıdır.
*/

public enum ModuleType
{
    //e-arşiv
    eArchive,

    //e-fatura
    eInvoice,

    //e-irsaliye
    eDispatch,

    //e-bilet
    eTicket,

    //e-smm (Serbest Meslek Makbuzu)
    eSEVoucher,

    //e-mm (Müstahsil Makbuzu)
    EProducerReceipt
}
