# Form - Java Script - Hide form header UI controls
```js
    /* add browser thread pool*/
    setTimeout(function(){
        /* hide insert, update, delete buttons */    
        Netle.Client.UI.HideAllButtons();

        /* hide form list navigation link */    
        Netle.Client.UI.HideFormListLink();
        
        /* hide form print-preview navigation link */    
        Netle.Client.UI.HideFormPrintLink();
    }, 0);
```
