# Form - Java Script - Electronic Signature - NES - Qualified
## Netle  - Client Based Electronic Signature JavaScript Example (InMemory Case)
* DocHuman form script object for electronic signature.
* OS must be 64bit
* Signature method can be BES, EST, ESXLong
* Netle Sign service must installed in local / client machine.
* Required electronic signature token must be installed (like Alaaddin 64bit)

```js
/* Prepare the class which will be us ing in pop-up form */
NetleESign={};

/* Set electronic signature opetion type or method for so many different operations */
NetleESign.OperationType="Audit2021";

/* Set db-related reference key (like table primary key Id values) */
NetleESign.MasterRecordId=310;

/*For TR regulations, Signature user can see the description before NES-Qualified signature (Regulation 5070  */
NetleESign.OperationShortDesc="3 internal audit notes documents must be signed.BES (Attached-CADES)";

/* Set InMemory document content array */
NetleESign.Docs=[];
NetleESign.Docs.push({"docName":"note01.txt", "docMimeType":"text/plain", "docContent":[1,2,3,4,5,6]})
NetleESign.Docs.push({"docName":"note02.txt", "docMimeType":"text/plain", "docContent":[22,22,22,22,22,1,2,3,4,5,6]})
NetleESign.Docs.push({"docName":"note03.txt", "docMimeType":"text/plain", "docContent":[22,22,22,22,22,1,2,3,4,5,6,33,44,55,66,66]})

/* Attach the  call back function for sucessfull signing */
NetleESign.OnSuccess = function(netleESign, attachmentIds) {
	/* show signed documents 'AttachmentSet' ID values */
	console.table(attachmentIds);
	fpuSign.Hide();
}

/* Attach the  call back function for failed signing */
NetleESign.OnFailed = function(netleESign) {
	fpuSign.Hide();
	alert("Signing failed....");
}

/*
Prepare DocHuman Form - PopUp object
    Width:1200
    Heigh:800
    Formname : ntl.common.esign (DocHuman E-Signature System Form)
*/
fpuSign.Show();
```
