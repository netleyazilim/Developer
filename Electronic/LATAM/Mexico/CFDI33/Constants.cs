namespace MPIP.Mexico.CFDI33
{
    public static class Constants
    {
        public const string DIGITALTAXSTAMP_XMLELEMENT_LOCALNAME = "TimbreFiscalDigital";
        public const string PAYMENT_XMLELEMENT_LOCALNAME = "Pagos";
        public const string PAYROLL_XMLELEMENT_LOCALNAME = "Nomina";
        public const string ADDITIONALFIELDS_XMLELEMENT_LOCALNAME = "ComercioExterior";

        public const byte MONETARY_DECIMAL_POINT = 2;
        public const byte RATE_DECIMAL_POINT = 4;
        public const byte QUANTITY_DECIMAL_POINT = 6;
        public const byte EXCHANGE_RATE_DECIMAL_POINT = 6;
        public const byte BASEUNIT_DECIMAL_POINT = 6;

        //Invoice
        public const string NEF_INVOICE_DOCUMENT_TYPE_KEY = "NEF.MX.Header.DocumentType";
        public const string NEF_INVOICE_PAYMENT_METHOD_KEY = "NEF.MX.Header.PaymentMethod";
        public const string NEF_INVOICE_TYPE_KEY = "NEF.MX.Header.Type";
        public const string NEF_ADDENDA_CUSTOMERNO = "ERP_MUSTERI_NO";
        public const string NEF_INVOICE_PAYMENT_TYPE_KEY = "NEF.MX.Header.PaymentType";
        public const string NEF_INVOICE_EXPEDITION_PLACE_KEY = "NEF.MX.Header.ExpeditionPlace";

        public const string NEF_INVOICE_CUSTOMER_USAGE_KEY = "NEF.MX.Header.CustomerUsage";

        public const string NEF_INVOICE_DIGITAL_TAX_STAMP_UUID_KEY = "NEF.MX.Header.DigitalTaxStampUUID";

        public const string NEF_INVOICE_INCOTERM1 = "NEF.MX.Header.IncoTerm1";
        public const string NEF_INVOICE_INCOTERM1TEXT = "NEF.MX.Header.IncoTerm1Text";
        public const string NEF_INVOICE_INCOTERM2 = "NEF.MX.Header.IncoTerm2";

        public const string NEF_INVOICE_PAYMENTTERM = "NEF.MX.Header.PaymentTerm";
        public const string NEF_INVOICE_PAYMENTTERMTEXT = "NEF.MX.Header.PaymentTermText";
        public const string NEF_INVOICE_SERIES = "NEF.MX.Header.Serie";


        // Payment
        public const string NEF_INVOICE_PAYMENT_COUNT_KEY = "NEF.MX.Header.PaymentCount";
        public const string NEF_INVOICE_PAYMENT_CARD_PAYMENT_KEY = "NEF.MX.Header.PaymentCardPayment";
        public const string NEF_INVOICE_PAYMENT_DAY_KEY = "NEF.MX.Header.PaymentDay";
        public const string NEF_INVOICE_PAYMENT_FORMAT_KEY = "NEF.MX.Header.PaymentFormatType";
        public const string NEF_INVOICE_PAYMENT_CURRENCY_KEY = "NEF.MX.Header.PaymentCurrencyType";

        // .1 .2 (with line index indicator)
        public const string NEF_INVOICE_PAYMENT_AMOUNT_KEY = "NEF.MX.Header.PaymentAmount";
        public const string NEF_INVOICE_PAYMENT_CHANGE_TYPE_KEY = "NEF.MX.Header.PaymentChangeType";
        public const string NEF_INVOICE_PAYMENT_NUMBER_OPERATION_KEY = "NEF.MX.Header.PaymentNumberOperation";
        public const string NEF_INVOICE_PAYMENT_RFC_KEY = "NEF.MX.Header.PaymentRFC";
        public const string NEF_INVOICE_PAYMENT_BANK_NAME_KEY = "NEF.MX.Header.PaymentBankName";
        public const string NEF_INVOICE_PAYMENT_CARD_TYPE_KEY = "NEF.MX.Header.PaymentCardType";
        public const string NEF_INVOICE_PAYMENT_CERTIFICATE_KEY = "NEF.MX.Header.PaymentCertificate";
        public const string NEF_INVOICE_PAYMENT_VOUCHER = "NEF.MX.Header.PaymentVoucher";
        public const string NEF_INVOICE_PAYMENT_SEAL_KEY = "NEF.MX.Header.PaymentSeal";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_RELATION_COUNT_KEY = "NEF.MX.Header.PaymentDocumentRelationCount";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_ID_KEY = "NEF.MX.Header.PaymentDocumentId";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_SERIE_KEY = "NEF.MX.Header.PaymentDocumentSerie";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_FOLIO_KEY = "NEF.MX.Header.PaymentDocumentFolio";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_CURRENCY_KEY = "NEF.MX.Header.PaymentDocumentCurrency";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_PPD_KEY = "NEF.MX.Header.PaymentPPD";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_METODOPAGO_KEY = "NEF.MX.Payment.Document.MetodoPago";

        public const string NEF_INVOICE_PAYMENT_DOCUMENT_TAXUBALANCE_KEY = "NEF.MX.Header.PaymentTaxUBalance";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_TAXBALANCE_KEY = "NEF.MX.Header.PaymentTaxBalance";
        public const string NEF_INVOICE_PAYMENT_DOCUMENT_UBALANCE_KEY = "NEF.MX.Header.PaymentUBalance";


        //Invoice Lines
        public const string NEF_LINEITEM_PONUMBER = "NEF.MX.LineItem.PONumber";
        public const string NEF_LINEITEM_PODATE = "NEF.MX.LineItem.PODate";

        //ComplementoConcepto
        public const string NEF_LINEITEM_NOTECOUNT = "NEF_LineItem.NoteCount";
        public const string NEF_LINEITEM_CUSTOMID = "NEF.MX.Header.CustomBrokerNumber";

        public const string NEF_LINEITEM_CUSTOMBROKERNO = "NEF.Hedaer.Mx.RequiredCustomsID"; // CustomsID
        public const string NEF_LINEITEM_LOTENO = "NEF.MX.LineItem.LotNo";
        public const string NEF_LINEITEM_COMMODITYCODE = "NEF.MX.LineItem.ComodityCode";
        public const string NEF_LINEITEM_MATERIALNO = "NEF.MX.LineItem.OldMaterialNumber";
        public const string NEF_LINEITEMCUSTOMERMATERIALNO = "NEF.MX.LineItem.CustomerMaterialNumber";
        public const string NEF_LINEITEM_SERIALNO = "NEF.MX.LineItem.SerialNumber";
        public const string NEF_LINEITEM_SURCHARGEAMOUNT = "NEF.MX.LineItem.SurchargeAmount";



        //Extras && Addenda
        public const string NEF_INVOICE_SHOWADDENDA = "NEF.MX.Header.ShowAddenda";
        public const string NEF_INVOICE_SHIPPINGCONDITION = "NEF.MX.Header.ShippingCondition";
        public const string NEF_INVOICE_NETWEIGHT = "NEF.MX.Header.NetWeight";
        public const string NEF_INVOICE_GROSSWEIGHT = "NEF.MX.Header.GrossWeight";
        public const string NEF_ADDENDA_COUNTRYOFORIGIN = "NEF.MX.Header.CountryofOrigin";
        public const string NEF_ADDENDA_PALLETT = "NEF.MX.Header.Paletts";
        public const string NEF_ADDENDA_CUSTOMUNIT = "NEF.MX.Header.UnidadAduana";
        public const string NEF_ADDENDA_CUSTOMQUANTITY = "NEF.MX.Header.CandidadAduana";
        public const string NEF_ADDENDA_CUSTOMTARIFF = "NEF.MX.Header.FraccionArancelaria";

        //Cancelled invoice UUID in  re-created invoice
        public const string NEF_INVOICE_REFGUIDCOUNT = "NEF.MX.Header.RefGUIDCount";
        public const string NEF_INVOICE_REFGUIDID = "NEF.MX.Header.RefGUIDId";


    }

    public static class LineConstants
    {
        public const string NEF_LINE_NUMBER_KEY = "NEF.MX.Line.LineNumber";

    }

    public static class PaymentConstants
    {
        public const string NEF_PAYMENT_TOTALAMOUNT_KEY = "NEF.MX.Payment.TotalAmount";

    }

}
