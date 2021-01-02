# Form - Java Script - Form keyboard shurtcut

> This code should be placed in 'form beforeAllScripts event'

```js
/*short cut*/
    $(document).keydown(function (e) {
        /*example : alt-s */
        if (e.altKey && e.keyCode=="65") 
            console.log("alt-s");
            
        /*example : alt-m */
        if (e.altKey && e.keyCode=="77") 
            console.log("alt-m");
            
        /*example : alt-l */
        if (e.altKey && e.keyCode=="76")
            console.log("alt-l");
            
        /*example : alt-g */
        if (e.altKey && e.keyCode=="71") 
            console.log("alt-g");
    });
    
 ```
