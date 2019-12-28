/// <summary>
/// Netle tabanlı "Özel Entegratör" çözümleriyle yapılacak entegrasyonlarda
/// karşılaşılabilecek hata kodları ve kısa açıklamaları
/// 
/// Hata kodlarına ilişkin baz (alt) değeri 10.000 olarak tanımlanmıştır.
/// Her bir hata kodu 10.000 + function kod değerine göre işlenmektedir.
/// Örnek : Record Not Found (RecordNotFound) (10006)
/// </summary>
public partial struct SystemStatusCode
{
	/// <summary>
	/// Invalid system / recod id
	/// </summary>
	public static int InvalidId { get { return 10000 + 0; } }

	/// <summary>
	/// Test integration account is locked
	/// </summary>
	public static int TestIntegLock { get { return 10000 + 1; } }

	/// <summary>
	/// Live integration account is locked
	/// </summary>
	public static int LiveIntegLock { get { return 10000 + 2; } }

	/// <summary>
	/// Data or parameter can not be null
	/// </summary>
	public static int CanNotBeNull { get { return 10000 + 3; } }

	/// <summary>
	/// Invalid session or token Id / value
	/// </summary>
	public static int InvalidSession { get { return 10000 + 4; } }

	/// <summary>
	/// The operation is not applicable in active context
	/// </summary>
	public static int NotApplicable { get { return 10000 + 5; } }

	/// <summary>
	/// Record not found in database
	/// </summary>
	public static int RecordNotFound { get { return 10000 + 6; } }

	/// <summary>
	/// Current object is locked
	/// </summary>
	public static int ObjectIsLock { get { return 10000 + 7; } }

	/// <summary>
	/// Invalid xsd (xml schema) or semantic schema (schematron)
	/// </summary>
	public static int NXmlSchemaInvalid { get { return 10000 + 8; } }

	/// <summary>
	/// This method is not runnable! (Not Implemented / by design / reserved)
	/// </summary>
	public static int MethodIsNotRunable { get { return 10000 + 9; } }

	/// <summary>
	/// Object seralization or deseriazation error on structured class
	/// </summary>
	public static int SerializationError { get { return 10000 + 10; } }

	/// <summary>
	/// Document format not supported
	/// </summary>
	public static int NotSupportedDocumentFormat { get { return 10000 + 11; } }

	/// <summary>
	/// No active workflow / ESB / BPM thread on context processing
	/// </summary>
	public static int WFNotStarted { get { return 10000 + 12; } }

	/// <summary>
	/// Invalid implementation (Internal error) (Setup Problem)
	/// </summary>
	public static int InvalidImplementation { get { return 10000 + 13; } }

	/// <summary>
	/// No active thread in queue for document processing
	/// </summary>
	public static int DocInQueu { get { return 10000 + 14; } }

	/// <summary>
	/// Active thread is processing document
	/// </summary>
	public static int DocIsProcessing { get { return 10000 + 15; } }

	/// <summary>
	/// Document accepted by given process
	/// </summary>
	public static int DocIsAccepted { get { return 10000 + 16; } }

	/// <summary>
	/// Document rejected by given process
	/// </summary>
	public static int DocIsRejected { get { return 10000 + 17; } }

	/// <summary>
	/// Document terminated by given process
	/// </summary>
	public static int DocIsUnCommitted { get { return 10000 + 18; } }

	/// <summary>
	/// User or company based data violation 
	/// </summary>
	public static int PrivacyViolation { get { return 10000 + 19; } }

	/// <summary>
	/// TAXNumber (VKN) is not listed in e-invoice tax payer list (GİB List)
	/// </summary>
	public static int TaxNumberNotInRAList { get { return 10000 + 20; } }

	/// <summary>
	/// Document parsing error
	/// </summary>
	public static int DataParsingError { get { return 10000 + 21; } }

	/// <summary>
	/// Security token expired or invalid
	/// </summary>
	public static int TokenIsNotRegistered { get { return 10000 + 22; } }

	/// <summary>
	/// Sender / Reciever tax id validation
	/// </summary>
	public static int SenderViolation { get { return 10000 + 23; } }

	/// <summary>
	/// Invalid or duplicated object/record id
	/// </summary>
	public static int DuplicateId { get { return 10000 + 24; } }

	/// <summary>
	/// Ambiguous record to process or access
	/// </summary>
	public static int AmbiguousRecord { get { return 10000 + 25; } }

	/// <summary>
	/// Invalid data category
	/// </summary>
	public static int InvalidDataCategory { get { return 10000 + 26; } }

	/// <summary>
	/// Signed-XML based document not allowed
	/// </summary>
	public static int SignedUblNotAllowed { get { return 10000 + 27; } }

	/// <summary>
	/// Invalid document hash / digest value 
	/// </summary>
	public static int InvalidHashValue { get { return 10000 + 28; } }

	/// <summary>
	/// Unzip failed
	/// </summary>
	public static int UnZipFailed { get { return 10000 + 29; } }

	/// <summary>
	/// Invalid signature checksum value, root certificate or digest value 
	/// </summary>
	public static int InvalidSignature { get { return 10000 + 30; } }

	/// <summary>
	/// Applying changes failed in database
	/// </summary>
	public static int DBPostError { get { return 10000 + 31; } }

	/// <summary>
	/// Invalid xml character or codepage-value
	/// </summary>
	public static int IllegalData { get { return 10000 + 32; } }

	/// <summary>
	/// File not found in short/medium/long archive storage
	/// </summary>
	public static int FileNotFound { get { return 10000 + 33; } }

	/// <summary>
	/// Web service, http, https or socket based communication error
	/// </summary>
	public static int CommunicationError { get { return 10000 + 34; } }

	/// <summary>
	/// Re-Trying count exceeded for the specific class method
	/// </summary>
	public static int MaxCallExceeded { get { return 10000 + 35; } }


	/// <summary>
	/// CPU inbound object not found 
	/// </summary>
	public static int ObjectNotFound { get { return 10000 + 36; } }

    /// <summary>
	/// Access denied (AAA) (Authorization, Authentication, Account)
	/// </summary>
	public static int AccessDenied { get { return 10000 + 37; } }

    /// <summary>
	/// Invalida data range for the specific domain
	/// </summary>
	public static int DataRangeError { get { return 10000 + 38; } }

    /// <summary>
	/// Invalid business workflow state 
	/// </summary>
	public static int InvalidWFStateForProcess { get { return 10000 + 39; } }

    /// <summary>
	/// Business logic layer (BLL) based process failed
	/// </summary>
	public static int BLLProcessError { get { return 10000 + 40; } }

    /// <summary>
	/// Electronic based document not found
	/// </summary>
	public static int DocNotFound { get { return 10000 + 41; } }

    /// <summary>
	/// Generic not found (Mutex, Semaphore, Internal class, caller id, etc)
	/// </summary>
	public static int NotFound { get { return 10000 + 42; } }

	#region [  this areas reserved for the new error code definition   ]

	#endregion

	/// <summary>
	/// Unhandled exception in business code layer / block
	/// </summary>
	public static int UnexpectedError { get { return 10000 + 9999; } }
}
