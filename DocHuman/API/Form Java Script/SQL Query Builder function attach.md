# Form - Java Script - Attach the SQL Query Builder Event

> This code should be placed in 'form ready event'

```js
    /* prepare the attach function for SQL Query Builder */
    function listenMessages(event){
        /* signal is coming from QueryBuilder?*/    
        if (!event.data.queryBuilderObject) return; 
        
        /* Get the signaled value from SQL Query Builder */    
        var qbo=event.data.queryBuilderObject;
        
        /* log the the last prepared SQL statements by Query Builder Form */
        console.log(qbo.sql);
    }

    /* now we can hook the Browser window listener event by custom function */
    window.addEventListener('message', listenMessages, false);
 ```
