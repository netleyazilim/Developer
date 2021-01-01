namespace MPIP.Common.Entity.GlobalInvoice.HU
{
    public static class Constants
    {
        public const byte MONETARY_DECIMAL_POINT = 2;
        public const byte RATE_DECIMAL_POINT = 4;
        public const byte QUANTITY_DECIMAL_POINT = 6;
        public const byte EXCHANGE_RATE_DECIMAL_POINT = 6;

        public const string NEF_INVOICE_ISANNULMENT_KEY = "NEF.HU.Header.IsAnnulment";
        public const string NEF_INVOICE_CATEGORY_KEY = "NEF.HU.Header.Category";

        public const string NEF_INVOICE_SUPPLIER_ADDRESS_REGION_KEY = "NEF.HU.Header.SupplierAddressRegion";
        public const string NEF_INVOICE_SUPPLIER_GROUP_MEMBER_TAXNUMBER_KEY = "NEF.HU.Header.SupplierGroupMemberTaxNumber";
        public const string NEF_INVOICE_SUPPLIER_COMMUNITY_VATNUMBER_KEY = "NEF.HU.Header.SupplierCommunityVatNumber";
        public const string NEF_INVOICE_SUPPLIER_BANKACCOUNTNUMBER_KEY = "NEF.HU.Header.SupplierBankAccountNumber";
        public const string NEF_INVOICE_SUPPLIER_VATCODE_KEY = "NEF.HU.Header.SupplierVatCode";
        public const string NEF_INVOICE_SUPPLIER_COUNTYCODE_KEY = "NEF.HU.Header.SupplierCountyCode";
        public const string NEF_INVOICE_SUPPLIER_ADDRESS_PUBLICPLACECATEGORY_KEY = "NEF.HU.Header.SupplierPublicPlaceCategory";
        public const string NEF_INVOICE_SUPPLIER_ADDRESS_STREETNUMBER_KEY = "NEF.HU.Header.SupplierStreetNumber";

        public const string NEF_INVOICE_CUSTOMER_ADDRESS_REGION_KEY = "NEF.HU.Header.CustomerAddressRegion";
        public const string NEF_INVOICE_CUSTOMER_GROUP_MEMBER_TAXNUMBER_KEY = "NEF.HU.Header.CustomerGroupMemberTaxNumber";
        public const string NEF_INVOICE_CUSTOMER_COMMUNITY_VATNUMBER_KEY = "NEF.HU.Header.CustomerCommunityVatNumber";
        public const string NEF_INVOICE_CUSTOMER_BANKACCOUNTNUMBER_KEY = "NEF.HU.Header.CustomerBankAccountNumber";
        public const string NEF_INVOICE_CUSTOMER_VATCODE_KEY = "NEF.HU.Header.CustomerVatCode";
        public const string NEF_INVOICE_CUSTOMER_COUNTYCODE_KEY = "NEF.HU.Header.CustomerCountyCode";
        public const string NEF_INVOICE_CUSTOMER_ADDRESS_PUBLICPLACECATEGORY_KEY = "NEF.HU.Header.CustomerPublicPlaceCategory";
        public const string NEF_INVOICE_CUSTOMER_ADDRESS_STREETNUMBER_KEY = "NEF.HU.Header.CustomerStreetNumber";

        public const string NEF_INVOICE_FISCALREPRESENTATIVE_ADDRESS_REGION_KEY = "NEF.HU.Header.FiscalRepresentativeAddressRegion";
        public const string NEF_INVOICE_FISCALREPRESENTATIVE_BANKACCOUNTNUMBER_KEY = "NEF.HU.Header.FiscalRepresentativeBankAccountNumber";
        public const string NEF_INVOICE_FISCALREPRESENTATIVE_VATCODE_KEY = "NEF.HU.Header.FiscalRepresentativeVatCode";
        public const string NEF_INVOICE_FISCALREPRESENTATIVE_COUNTYCODE_KEY = "NEF.HU.Header.FiscalRepresentativeCountyCode";
        public const string NEF_INVOICE_FISCALREPRESENTATIVE_ADDRESS_PUBLICPLACECATEGORY_KEY = "NEF.HU.Header.FiscalRepresentativePublicPlaceCategory";
        public const string NEF_INVOICE_FISCALREPRESENTATIVE_ADDRESS_STREETNUMBER_KEY = "NEF.HU.Header.FiscalRepresentativeStreetNumber";

        public const string NEF_INVOICE_PAYMENTMETHOD_KEY = "NEF.HU.Header.PaymentMethod";
        public const string NEF_INVOICE_PAYMENTDATE_KEY = "NEF.HU.Header.PaymentDate";
        public const string NEF_INVOICE_DELIVERYDATE_KEY = "NEF.HU.Header.DeliveryDate";
        public const string NEF_INVOICE_APPEARANCE_KEY = "NEF.HU.Header.Appearance";
        public const string NEF_INVOICE_EIHASH_KEY = "NEF.HU.Header.ElectronicInvoiceHash";

        public const string NEF_ORIGINAL_INVOICE_NUMBER_KEY = "NEF.HU.Header.OriginalInvoiceNumber";
        public const string NEF_MODIFICATION_ISSUEDATE_KEY = "NEF.HU.Header.ModificationIssueDate";
        public const string NEF_MODIFICATION_TIMESTAMP_KEY = "NEF.HU.Header.ModificationTimestamp";
        public const string NEF_LAST_MODIFICATION_REFERENCE_KEY = "NEF.HU.Header.LastModificationReference";
        public const string NEF_MODIFY_WITHOUT_MASTER_KEY = "NEF.HU.Header.ModifyWithoutMaster";

        public const string NEF_ANNULMENT_REF_KEY = "NEF.Header.AnnulmentReference";
        public const string NEF_ANNULMENT_TIMESTAMP_KEY = "NEF.Header.AnnulmentTimestamp";
        public const string NEF_ANNULMENT_CODE_KEY = "NEF.Header.AnnulmentCode";
        public const string NEF_ANNULMENT_REASON_KEY = "NEF.Header.AnnulmentReason";

        public const string NEF_INVOICE_PRODUCTFEESUMMARYCOUNT_KEY = "NEF.HU.Header.ProductFeeSummaryCount";

        public const string NEF_UUID_KEY = "A00001_NEF_HEADER_UUID";
        public const string NEF_UUID_KEY_DESC = "Netle Universal Unique Invoice Id (UUID)";
        
        public const string NEF_ORDER_NUMBERANDDATE_KEY = "O00001_NEF_HEADER_ORDERNUMBERANDDATE";
        public const string NEF_ORDER_NUMBERANDDATE_KEY_DESC = "Netle Universal Order Number and Date";
        
        public const string NEF_DESPATCH_NUMBERANDDATE_KEY_PREFIX = "D";
        public const string NEF_DESPATCH_NUMBERANDDATE_KEY_SUFFIX = "_NEF_HEADER_DESPATCHNUMBERANDDATE";
        public const string NEF_DESPATCH_NUMBERANDDATE_KEY_DESC = "Netle Universal Despatch Advice Number and Date";

        public const string NEF_INVOCE_DESC_KEY_PREFIX = "D";
        public const string NEF_INVOCE_DESC_KEY_SUFFIX = "_NEF_HEADER_DESC";
        public const string NEF_INVOCE_DESC_KEY_DESC = "Netle Universal Invoice Description";

        public const string NEF_INVOICE_OPERATIONTYPE_KEY = "NEF.HU.Header.OperationType";
        public const string NEF_OPERATIONTYPE_KEY = "T00001_NEF_HEADER_OPERATIONTYPE";
        public const string NEF_OPERATIONTYPE_DESC = "Netle Operation Type";

        public const string NEF_INVOICE_TAXTOTALAMOUNTHUF_KEY = "NEF.HU.Header.TaxTotalAmountHUF";
        public const string NEF_INVOICE_TAXAMOUNTHUF_KEY = "NEF.HU.Header.TaxAmountHUF_";
        public const string NEF_INVOICE_NETAMOUNT_KEY = "NEF.HU.Header.NetAmount_";

        public const string NEF_INVOICE_ISVATDOMESTICREVERSECHARGE_KEY = "NEF.HU.Header.IsVatDomesticReverseCharge_";
    }

    public static class LineConstants
    {
        public const string NEF_LINE_NUMBER_KEY = "NEF.HU.Line.LineNumber";
        public const string NEF_LINE_NUMBER_REFERENCE_KEY = "NEF.HU.Line.LineNumberReference";
        public const string NEF_LINE_OPERATION_KEY = "NEF.HU.Line.LineOperation";
        public const string NEF_LINE_TAXAMOUNTHUF_KEY = "NEF.HU.Line.TaxAmountHUF";
        public const string NEF_LINE_ISVATDOMESTICREVERSECHARGE_KEY = "NEF.HU.Line.IsVatDomesticReverseCharge";
        public const string NEF_LINE_ADVANCEINDICATOR_KEY = "NEF.HU.Line.AdvanceIndicator";
        public const string NEF_LINE_EXPRESSIONINDICATOR_KEY = "NEF.HU.Line.ExpressionIndicator";
    }
}
