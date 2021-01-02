# Form - Button - netleEventClick(s,e) - startSyncUIWF - 01

```js
/*initialize object for parameters*/
var o = new Object();

/*get active record id*/
o["recordId"] = DFSRecordId;

/*get active form meta data designer id*/
o["designId"] = DFSFormId;

/*
start server-side compiled-code via worfklow object
netleGlobal.wf.DoSomething : workflow name
*/
Netle.Client.WFCore.startSyncUIWF("netleGlobal.wf.DoSomething", o, function(data)
{
        /*server-side-code-executed-check callback status*/

        /*check error (returned by workflow object)*/
        if (data.ErrorResult){
            alert("Operation failed. Error messaage(s): " + data.ErrorResult);
            return;
        }
    
        /*operation sucessfull*/
        alert("Operation sucessfully completed.");

        /*
        Grid object in form named by 'idcFeature'.
        Refresh grid data
        */
        idcFeature.PerformCallback();
});
```
