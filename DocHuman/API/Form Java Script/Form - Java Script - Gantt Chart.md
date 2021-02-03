# Form - Java Script - Gantt Chart

This javascript method can be called from 'ButtonClick' event.

```js

/*initialize gantt chart java script object*/
var gobj={};

function drawGannt()
{
  var o = new Object();
  o["designId"]=DFSFormId;

  /*
  This workflow template can be downloaded as DocHuman WF Snipplet
  https://github.com/netleyazilim/Developer/blob/master/DocHuman/Design%20Patterns/Workflow/Gannt%20chart%20datasource%20as%20JSON%20format.xml
  */
  
  Netle.Client.WFCore.startSyncUIWF("wf.getGantt", o, function(data){
    /*check server side error */
    if (data.ErrorResult) 
    {            
        alert(datadata.ErrorResult);
        return;
    }

    if(data.ganttDS)
    {
      var ds = JSON.parse(data["ganttDS"]);

      /* Use the 'DocHuman.Form.GroupBox object' 'rpGantt' named  */
      gobj = $("#rpGantt").dxGantt({
         scaleType: 'weeks',
         allowSelection: false,
         tasks: {
                dataSource: ds,
                keyExpr: "Id",
                startExpr: "BeginDate",
                endExpr: "EndDate",
                titleExpr: "TaskDesc"
            },
        toolbar: {
                    items: [
                        "undo",
                        "redo",
                        "separator",
                        "collapseAll",
                        "expandAll",
                        "separator",
                        "addTask",
                        "deleteTask",
                        "separator",
                        "zoomIn",
                        "zoomOut"
                    ]
                },
            columns: [
          {
              dataField: "UserName",
                caption: "User",
                width: 150
            }]				    
       }).dxGantt("instance");

       /*Check auto-height paret div for the optimum gantt-view*/
       if ($("#rpGantt ").height()<100) {
        $("#rpGantt ").height("250px");
       }

      /*refres the gantt layout for repeated variant-requests*/
      gobj.repaint();

      DevExpress.ui.notify("Gantt chart is ready!","success");
    }
  });
}
```
