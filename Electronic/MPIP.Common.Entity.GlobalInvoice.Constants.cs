namespace MPIP.Common.Entity.GlobalInvoice
{
    public static class Constants
    {
        public const byte MONETARY_DECIMAL_POINT = 2;
        public const byte RATE_DECIMAL_POINT = 4;
        public const byte QUANTITY_DECIMAL_POINT = 6;
        public const byte EXCHANGE_RATE_DECIMAL_POINT = 6;

        public const string NEF_INVOICE_TYPE_CODE_KEY = "NEF.Global.Header.InvoiceTypeCode"; //380
        public const string NEF_INVOICE_PROFILE_ID_KEY = "NEF.Global.Header.ProfileID"; //urn:www.nesubl.eu:profiles:profile5:ver2.0
        public const string NEF_INVOICE_ACCOUNTINGCOST_KEY = "NEF.Global.Header.AccountingCost";
        public const string NEF_INVOICE_UUID_KEY = "NEF.Global.Header.UUID"; //B2G, B2B, B2C        

        public const string NEF_INVOICE_SUPPLIER_ENDPOINTID_KEY = "NEF.Global.Header.SupplierEndpointID"; //DK16356706
        public const string NEF_INVOICE_SUPPLIER_ENDPOINT_SCHEMEID_KEY = "NEF.Global.Header.SupplierEndpointSchemeID"; //DK:CVR
        public const string NEF_INVOICE_SUPPLIER_ENDPOINT_SCHEMEAGENCYID_KEY = "NEF.Global.Header.SupplierEndpointSchemeAgencyID"; //320
        public const string NEF_INVOICE_SUPPLIER_PARTYIDENTIFICATION_ID_KEY = "NEF.Global.Header.SupplierPartyIdentificationID"; //DK16356706
        public const string NEF_INVOICE_SUPPLIER_PARTYIDENTIFICATION_SCHEMEID_KEY = "NEF.Global.Header.SupplierPartyIdentificationSchemeID";//DK:CVR
        public const string NEF_INVOICE_SUPPLIER_PARTYIDENTIFICATION_SCHEMEAGENCYID_KEY = "NEF.Global.Header.SupplierPartyIdentificationSchemeAgencyID";//320
        public const string NEF_INVOICE_SUPPLIER_ADDRESS_COUNTRY_CODE_KEY = "NEF.Global.Header.SupplierAddressCountryCode"; //DK
        public const string NEF_INVOICE_SUPPLIER_ADDRESS_POST_OFFICE_KEY = "NEF.Global.Header.SupplierAddressPostOffice";
        public const string NEF_INVOICE_SUPPLIER_PARTYTAXSCHEME_COMPANY_ID_KEY = "NEF.Global.Header.SupplierPartyTaxSchemeCompanyID"; //DK16356706
        public const string NEF_INVOICE_SUPPLIER_PARTYTAXSCHEME_COMPANY_SCHEMEID_KEY = "NEF.Global.Header.SupplierPartyTaxSchemeCompanySchemeID"; //DK:SE
        public const string NEF_INVOICE_SUPPLIER_PARTYTAXSCHEME_COMPANY_SCHEMEAGENCYID_KEY = "NEF.Global.Header.SupplierPartyTaxSchemeCompanySchemeAgencyID"; //320
        public const string NEF_INVOICE_SUPPLIER_PARTYTAXSCHEME_ID_KEY = "NEF.Global.Header.SupplierPartyTaxID";//63
        public const string NEF_INVOICE_SUPPLIER_PARTYTAXSCHEME_SCHEMEID_KEY = "NEF.Global.Header.SupplierPartyTaxSchemeID"; //urn:oioubl:id:taxschemeid-1.1
        public const string NEF_INVOICE_SUPPLIER_PARTYTAXSCHEME_SCHEMEAGENCYID_KEY = "NEF.Global.Header.SupplierPartyTaxSchemeAgencyID"; //320
        public const string NEF_INVOICE_SUPPLIER_PARTYTAXSCHEME_NAME_KEY = "NEF.Global.Header.SupplierPartyTaxName"; //Moms
        public const string NEF_INVOICE_SUPPLIER_PARTYLEGALENTITY_SCHEMEID_KEY = "NEF.Global.Header.SupplierPartyLegalEntitySchemeID"; //DK:CVR
        public const string NEF_INVOICE_SUPPLIER_PARTYLEGALENTITY_SCHEMEAGENCYID_KEY = "NEF.Global.Header.SupplierPartyLegalEntitySchemeAgencyID"; //320
        public const string NEF_INVOICE_SUPPLIER_CONTACT_ID_KEY = "NEF.Global.Header.SupplierContactID"; //9876
        public const string NEF_INVOICE_SUPPLIER_CONTACT_NAME_KEY = "NEF.Global.Header.SupplierContactName"; 
        public const string NEF_INVOICE_SUPPLIER_ORGANIZATION_NUMBER_KEY = "NEF.Global.Header.SupplierOrganizationNumber";
        public const string NEF_INVOICE_SUPPLIER_INTERMEDIATOR_KEY = "NEF.Global.Header.SupplierIntermediator";

        public const string NEF_INVOICE_SUPPLIER_BANKNAME_KEY = "NEF.Global.Header.SupplierBankName";
        public const string NEF_INVOICE_SUPPLIER_BANKSWIFTCODE_KEY = "NEF.Global.Header.SupplierBankSwiftCode"; 
        public const string NEF_INVOICE_SUPPLIER_BANKACCOUNTNUMBER_KEY = "NEF.Global.Header.SupplierBankAccountNumber";
        public const string NEF_INVOICE_SUPPLIER_BANKIBAN_KEY = "NEF.Global.Header.SupplierBankIBAN";

        public const string NEF_INVOICE_CUSTOMER_CATEGORY_KEY = "NEF.Global.Header.CustomerCategory"; //B2G, B2B, B2C        
        public const string NEF_INVOICE_CUSTOMER_FORMATTYPE_KEY = "NEF.Global.Header.CustomerFormatType"; //OIOUBL202, TEAPPSXML272, TEAPPSXML30, Finvoice201, Finvoice30, EHF20, Svefaktura10, SvefakturaBIS5A20, PEPPOLBISBilling30       
        public const string NEF_INVOICE_CUSTOMER_ENDPOINTID_KEY = "NEF.Global.Header.CustomerEndpointID";
        public const string NEF_INVOICE_CUSTOMER_ENDPOINT_SCHEMEID_KEY = "NEF.Global.Header.CustomerEndpointSchemeID";
        public const string NEF_INVOICE_CUSTOMER_ENDPOINT_SCHEMEAGENCYID_KEY = "NEF.Global.Header.CustomerEndpointSchemeAgencyID";
        public const string NEF_INVOICE_CUSTOMER_PARTYIDENTIFICATION_ID_KEY = "NEF.Global.Header.CustomerPartyIdentificationID";
        public const string NEF_INVOICE_CUSTOMER_PARTYIDENTIFICATION_SCHEMEID_KEY = "NEF.Global.Header.CustomerPartyIdentificationSchemeID";
        public const string NEF_INVOICE_CUSTOMER_PARTYIDENTIFICATION_SCHEMEAGENCYID_KEY = "NEF.Global.Header.CustomerPartyIdentificationSchemeAgencyID";
        public const string NEF_INVOICE_CUSTOMER_ADDRESS_COUNTRY_CODE_KEY = "NEF.Global.Header.CustomerAddressCountryCode";
        public const string NEF_INVOICE_CUSTOMER_ADDRESS_POST_OFFICE_KEY = "NEF.Global.Header.CustomerAddressPostOffice";
        public const string NEF_INVOICE_CUSTOMER_PARTYTAXSCHEME_COMPANY_ID_KEY = "NEF.Global.Header.CustomerPartyTaxSchemeCompanyID";
        public const string NEF_INVOICE_CUSTOMER_PARTYTAXSCHEME_COMPANY_SCHEMEID_KEY = "NEF.Global.Header.CustomerPartyTaxSchemeCompanySchemeID";
        public const string NEF_INVOICE_CUSTOMER_PARTYTAXSCHEME_COMPANY_SCHEMEAGENCYID_KEY = "NEF.Global.Header.CustomerPartyTaxSchemeCompanySchemeAgencyID";
        public const string NEF_INVOICE_CUSTOMER_PARTYTAXSCHEME_ID_KEY = "NEF.Global.Header.CustomerPartyTaxID";
        public const string NEF_INVOICE_CUSTOMER_PARTYTAXSCHEME_SCHEMEID_KEY = "NEF.Global.Header.CustomerPartyTaxSchemeID";
        public const string NEF_INVOICE_CUSTOMER_PARTYTAXSCHEME_SCHEMEAGENCYID_KEY = "NEF.Global.Header.CustomerPartyTaxSchemeAgencyID";
        public const string NEF_INVOICE_CUSTOMER_PARTYTAXSCHEME_NAME_KEY = "NEF.Global.Header.CustomerPartyTaxName";
        public const string NEF_INVOICE_CUSTOMER_PARTYLEGALENTITY_SCHEMEID_KEY = "NEF.Global.Header.CustomerPartyLegalEntitySchemeID";
        public const string NEF_INVOICE_CUSTOMER_PARTYLEGALENTITY_SCHEMEAGENCYID_KEY = "NEF.Global.Header.CustomerPartyLegalEntitySchemeAgencyID";
        public const string NEF_INVOICE_CUSTOMER_CONTACT_ID_KEY = "NEF.Global.Header.CustomerContactID";
        public const string NEF_INVOICE_CUSTOMER_CONTACT_NAME_KEY = "NEF.Global.Header.CustomerContactName";
        public const string NEF_INVOICE_CUSTOMER_ORGANIZATION_NUMBER_KEY = "NEF.Global.Header.CustomerOrganizationNumber";
        public const string NEF_INVOICE_CUSTOMER_INTERMEDIATOR_KEY = "NEF.Global.Header.CustomerIntermediator";

        public const string NEF_INVOICE_PARTY_ENDPOINTID_KEY = "NEF.Global.Header.{0}EndpointID"; //DK16356706
        public const string NEF_INVOICE_PARTY_ENDPOINT_SCHEMEID_KEY = "NEF.Global.Header.{0}EndpointSchemeID"; //DK:CVR
        public const string NEF_INVOICE_PARTY_ENDPOINT_SCHEMEAGENCYID_KEY = "NEF.Global.Header.{0}EndpointSchemeAgencyID"; //320
        public const string NEF_INVOICE_PARTY_PARTYIDENTIFICATION_ID_KEY = "NEF.Global.Header.{0}PartyIdentificationID"; //DK16356706
        public const string NEF_INVOICE_PARTY_PARTYIDENTIFICATION_SCHEMEID_KEY = "NEF.Global.Header.{0}PartyIdentificationSchemeID";//DK:CVR
        public const string NEF_INVOICE_PARTY_PARTYIDENTIFICATION_SCHEMEAGENCYID_KEY = "NEF.Global.Header.{0}PartyIdentificationSchemeAgencyID";//320
        public const string NEF_INVOICE_PARTY_ADDRESS_COUNTRY_CODE_KEY = "NEF.Global.Header.{0}AddressCountryCode"; //DK
        public const string NEF_INVOICE_PARTY_ADDRESS_POST_OFFICE_KEY = "NEF.Global.Header.{0}AddressPostOffice";
        public const string NEF_INVOICE_PARTY_PARTYTAXSCHEME_COMPANY_ID_KEY = "NEF.Global.Header.{0}PartyTaxSchemeCompanyID"; //DK16356706
        public const string NEF_INVOICE_PARTY_PARTYTAXSCHEME_COMPANY_SCHEMEID_KEY = "NEF.Global.Header.{0}PartyTaxSchemeCompanySchemeID"; //DK:SE
        public const string NEF_INVOICE_PARTY_PARTYTAXSCHEME_COMPANY_SCHEMEAGENCYID_KEY = "NEF.Global.Header.{0}PartyTaxSchemeCompanySchemeAgencyID"; //320
        public const string NEF_INVOICE_PARTY_PARTYTAXSCHEME_ID_KEY = "NEF.Global.Header.{0}PartyTaxID";//63
        public const string NEF_INVOICE_PARTY_PARTYTAXSCHEME_SCHEMEID_KEY = "NEF.Global.Header.{0}PartyTaxSchemeID"; //urn:oioubl:id:taxschemeid-1.1
        public const string NEF_INVOICE_PARTY_PARTYTAXSCHEME_SCHEMEAGENCYID_KEY = "NEF.Global.Header.{0}PartyTaxSchemeAgencyID"; //320
        public const string NEF_INVOICE_PARTY_PARTYTAXSCHEME_NAME_KEY = "NEF.Global.Header.{0}PartyTaxName"; //Moms
        public const string NEF_INVOICE_PARTY_PARTYLEGALENTITY_SCHEMEID_KEY = "NEF.Global.Header.{0}PartyLegalEntitySchemeID"; //DK:CVR
        public const string NEF_INVOICE_PARTY_PARTYLEGALENTITY_SCHEMEAGENCYID_KEY = "NEF.Global.Header.{0}PartyLegalEntitySchemeAgencyID"; //320
        public const string NEF_INVOICE_PARTY_CONTACT_ID_KEY = "NEF.Global.Header.{0}ContactID"; //9876
        public const string NEF_INVOICE_PARTY_CONTACT_NAME_KEY = "NEF.Global.Header.{0}ContactName"; // Hugo Jensen
        public const string NEF_INVOICE_PARTY_ORGANIZATION_NUMBER_KEY = "NEF.Global.Header.{0}OrganizationNumber";
        public const string NEF_INVOICE_PARTY_INTERMEDIATOR_KEY = "NEF.Global.Header.{0}Intermediator";

        public const string NEF_INVOICE_PARTY_APPROVEDFORFTAX_KEY = "NEF.Global.Header.{0}ApprovedForFTax"; //Sweden - Godkänd för F-skatt / F-skattebevis finns        
        public const string NEF_INVOICE_APPROVEDFORFTAXDESCFORPEPPOL = "Godkänd för F-skatt"; //Sweden for PEPPOL format - Godkänd för F-skatt
        public const string NEF_INVOICE_APPROVEDFORFTAXDESCFORSVEFAKTURA = "F-skattebevis finns"; //Sweden for Svefaktura format -  F-skattebevis finns        

        public const string NEF_INVOICE_TAXCATEGORY_ID_KEY = "NEF.Global.Header.TaxCategoryID"; //StandardRated
        public const string NEF_INVOICE_TAXCATEGORY_SCHEMEID_KEY = "NEF.Global.Header.TaxCategorySchemeID"; //urn:oioubl:id:taxcategoryid-1.1
        public const string NEF_INVOICE_TAXCATEGORY_SCHEMEAGENCYID_KEY = "NEF.Global.Header.TaxCategorySchemeAgencyID";//320

        public const string NEF_INVOICE_TAXSCHEME_ID_KEY = "NEF.Global.Header.TaxSchemeID";//63
        public const string NEF_INVOICE_TAXSCHEME_SCHEMEID_KEY = "NEF.Global.Header.TaxSchemeSchemeID"; //urn:oioubl:id:taxschemeid-1.1
        public const string NEF_INVOICE_TAXSCHEME_SCHEMEAGENCYID_KEY = "NEF.Global.Header.TaxSchemeSchemeAgencyID";//320
        public const string NEF_INVOICE_TAXSCHEME_NAME_KEY = "NEF.Global.Header.TaxSchemeName";//Moms

        public const string NEF_INVOICE_PAYMENTMEANS_CODE_KEY = "NEF.Global.Header.PaymentMeansCode";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYMENTDUEDATE_KEY = "NEF.Global.Header.PaymentDueDate";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYMENTCHANNELCODE_KEY = "NEF.Global.Header.PaymentChannelCode";
        public const string NEF_INVOICE_PAYMENTMEANS_INSTRUCTIONID_KEY = "NEF.Global.Header.PaymentInstructionID";
        public const string NEF_INVOICE_PAYMENTMEANS_INSTRUCTIONNOTE_KEY = "NEF.Global.Header.PaymentInstructionNote";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYMENTID_KEY = "NEF.Global.Header.PaymentID";
        public const string NEF_INVOICE_PAYMENTMEANS_CREDITACCOUNTID_KEY = "NEF.Global.Header.PaymentCreditAccountID";

        public const string NEF_INVOICE_PAYMENTMEANS_PAYEEFINANCIALACCOUNT_ID_KEY = "NEF.Global.Header.PayeeFinancialAccountID";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYEEFINANCIALACCOUNT_PAYMENTNOTE_KEY = "NEF.Global.Header.PayeeFinancialAccountPaymentNote";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYEEFINANCIALACCOUNT_FIBRANCHID_KEY = "NEF.Global.Header.PayeeFinancialAccountFIBranchID";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYEEFINANCIALACCOUNT_FIBRANCHNAME_KEY = "NEF.Global.Header.PayeeFinancialAccountFIBranchName";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYEEFINANCIALACCOUNT_FIBRANCHBANKID_KEY = "NEF.Global.Header.PayeeFinancialAccountFIBranchBankID";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYEEFINANCIALACCOUNT_FIBRANCHBANKNAME_KEY = "NEF.Global.Header.PayeeFinancialAccountFIBranchBankName";

        public const string NEF_INVOICE_PAYMENTMEANS_PAYERFINANCIALACCOUNT_ID_KEY = "NEF.Global.Header.PayerFinancialAccountID";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYERFINANCIALACCOUNT_PAYMENTNOTE_KEY = "NEF.Global.Header.PayerFinancialAccountPaymentNote";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYERFINANCIALACCOUNT_FIBRANCHID_KEY = "NEF.Global.Header.PayerFinancialAccountFIBranchID";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYERFINANCIALACCOUNT_FIBRANCHNAME_KEY = "NEF.Global.Header.PayerFinancialAccountFIBranchName";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYERFINANCIALACCOUNT_FIBRANCHBANKID_KEY = "NEF.Global.Header.PayerFinancialAccountFIBranchBankID";
        public const string NEF_INVOICE_PAYMENTMEANS_PAYERFINANCIALACCOUNT_FIBRANCHBANKNAME_KEY = "NEF.Global.Header.PayerFinancialAccountFIBranchBankName";
                
        public const string NEF_INVOICE_PAYMENTTERMS_PAYMENTMEANSID_KEY = "NEF.Global.Header.PaymentTermsPaymentMeansID";
        public const string NEF_INVOICE_PAYMENTTERMS_PAYMENTNOTE_KEY = "NEF.Global.Header.PaymentTermsPaymentNote";
        public const string NEF_INVOICE_PAYMENTTERMS_AMOUNT_KEY = "NEF.Global.Header.PaymentTermsAmount";
        public const string NEF_INVOICE_PAYMENTTERMS_PENALTYSURCHARGEPERCENT_KEY = "NEF.Global.Header.PenaltySurchargePercent";
        public const string NEF_INVOICE_PAYMENTTERMS_PENALTYPERIODSTARTDATE_KEY = "NEF.Global.Header.PenaltyPeriodStartDate";

        public const string NEF_INVOICE_DELIVERY_ACTUALDELIVERYDATE_KEY = "NEF.Global.Header.ActualDeliveryDate";
        public const string NEF_INVOICE_DELIVERY_LOSSRISKRESPONSIBILITYCODE_KEY = "NEF.Global.Header.LossRiskResponsibilityCode"; //FOB, CIF
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_COMPANYNAME_KEY = "NEF.Global.Header.DeliveryLocationCompanyName";
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_STREETNAME_KEY = "NEF.Global.Header.DeliveryLocationStreetName";
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_BUILDINGNAME_KEY = "NEF.Global.Header.DeliveryLocationBuildingName";
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_BUILDINGNUMBER_KEY = "NEF.Global.Header.DeliveryLocationBuildingNumber";
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_DISTRICT_KEY = "NEF.Global.Header.DeliveryLocationDistrict";
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_CITY_KEY = "NEF.Global.Header.DeliveryLocationCity";
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_POSTALZONE_KEY = "NEF.Global.Header.DeliveryLocationPostalZone";
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_COUNTRYNAME_KEY = "NEF.Global.Header.DeliveryLocationCountryName";
        public const string NEF_INVOICE_DELIVERY_DELIVERYLOCATION_COUNTRYCODE_KEY = "NEF.Global.Header.DeliveryLocationCountryCode";

        public const string NEF_INVOICE_ORDER_DOCUMENTREFERENCE_KEY = "NEF.Global.Header.OrderDocumentReference";
        public const string NEF_INVOICE_BUYER_REFERENCE_KEY = "NEF.Global.Header.BuyerReference";
        public const string NEF_INVOICE_BILLING_INVOICE_DOCUMENT_REFERENCE_KEY = "NEF.Global.Header.BillingInvoiceDocumentReference";
        public const string NEF_INVOICE_BILLING_CREDITNOTE_DOCUMENT_REFERENCE_KEY = "NEF.Global.Header.BillingCreditNoteDocumentReference";

        public const string NEF_INVOICE_TOTAL_NET_WEIGHT_MEASURE_KEY = "NEF.Global.Header.TotalNetWeightMeasure";
        public const string NEF_INVOICE_TOTAL_NET_WEIGHT_MEASURE_UNIT_KEY = "NEF.Global.Header.TotalNetWeightMeasureUnit";
        public const string NEF_INVOICE_TOTAL_GROSS_WEIGHT_MEASURE_KEY = "NEF.Global.Header.TotalGrossWeightMeasure";
        public const string NEF_INVOICE_TOTAL_GROSS_WEIGHT_MEASURE_UNIT_KEY = "NEF.Global.Header.TotalGrossWeightMeasureUnit";

        public const string NEF_INVOICE_TRANSPORT_MODE_OF_TRANSPORT_KEY = "NEF.Global.Header.ModeOfTransport";        
    }

    public static class LineConstants
    {
        public const string NEF_LINE_TAXCATEGORY_ID_KEY = "NEF.Global.Line.TaxCategoryID"; //StandardRated
        public const string NEF_LINE_TAXCATEGORY_SCHEMEID_KEY = "NEF.Global.Line.TaxCategorySchemeID";//urn:oioubl:id:taxcategoryid-1.1
        public const string NEF_LINE_TAXCATEGORY_SCHEMEAGENCYID_KEY = "NEF.Global.Line.TaxCategorySchemeAgencyID";//320

        public const string NEF_LINE_TAXSCHEME_ID_KEY = "NEF.Global.Line.TaxSchemeID";//63
        public const string NEF_LINE_TAXSCHEME_SCHEMEID_KEY = "NEF.Global.Line.TaxSchemeSchemeID";//urn:oioubl:id:taxschemeid-1.1
        public const string NEF_LINE_TAXSCHEME_SCHEMEAGENCYID_KEY = "NEF.Global.Line.TaxSchemeSchemeAgencyID";//320
        public const string NEF_LINE_TAXSCHEME_NAME_KEY = "NEF.Global.Line.TaxSchemeName";

        public const string NEF_LINE_DELIVERY_DATE_KEY = "NEF.Global.Line.DeliveryDate";

        public const string NEF_LINE_NET_WEIGHT_MEASURE_KEY = "NEF.Global.Line.NetWeightMeasure";
        public const string NEF_LINE_NET_WEIGHT_MEASURE_UNIT_KEY = "NEF.Global.Line.NetWeightMeasureUnit";
        public const string NEF_LINE_GROSS_WEIGHT_MEASURE_KEY = "NEF.Global.Line.GrossWeightMeasure";
        public const string NEF_LINE_GROSS_WEIGHT_MEASURE_UNIT_KEY = "NEF.Global.Line.GrossWeightMeasureUnit";

        public const string NEF_LINE_BASE_PRICE_PER_UNIT_KEY = "NEF.Global.Line.BasePricePerUnit";
    }
}
