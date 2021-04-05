# Form - Java Script - Electronic Signature - NES - Qualified
## Netle - Client Based Electronic Signature JavaScript Example - DB Table Case - By Using SyncWF
* DocHuman form script object for electronic signature.
* Signature method can be BES, EST, ESXLong
* Netle Sign service must installed in local / client machine.
* Required electronic signature token must be install (like Alaaddin 64bit)
* This script can be run with 'wf.test.getAttachmentAsDocArray' sync wf object 

```js
/* Prepare javascript sync wf property bag */
var o = new Object();

/* set current design / form id */
o["designId"] = DFSFormId;

/* set current db table id */
o["recordId"] = DFSRecordId;

/* call sync wf with object property bag */
Netle.Client.WFCore.startSyncUIWF("wf.test.getAttachmentAsDocArray", o, function(data)
{
  /* Check possible error from wf based (internal message) */
  if (data.err) 
  {
    alert(datadata.err);
    return;
  }

  /* 
    'wf.test.getAttachmentAsDocArray' sync workflow must return 'ds' out argument value as json compatible string.
    Convert JSONStrin to JSON Object 
  */
  var dsObj = eval(data.ds);
    
  /* Prepare the class which will be us ing in pop-up form */
  NetleESign = {};
    
  /* Set electronic signature opetion type or method for so many different operations */
  NetleESign.OperationType = "Audit2021";

  /* Set db-related reference key (like table primary key Id values) */
  NetleESign.MasterRecordId = 404;

  /*For TR regulations, Signature user can see the description before NES-Qualified signature (Regulation 5070  */
  NetleESign.OperationShortDesc = "3 internal audit notes documents must be signed.BES (Attached-CADES)";
  
  /*Set json object to document array*/
  NetleESign.Docs = dsObj;
	
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
});
```
