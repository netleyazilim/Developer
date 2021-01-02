# Form - Java Script - Variables

## Form Mode "NEW"
```js
    console.log(DFSUserId);                         /*get active user id - integer - example : 12 */
    console.log(DFSFirstDepartmentId);              /*get active user org. department-id - integer - example : -1 */
    console.log(DFSFirstPositionId);                /*get active user oeg. position-id - integer - example : -1 */
    console.log(DFSIsRuntimeAdminUser);             /*is user Admin? - bool - example : true */
    console.log(DFSIsNewRecord);                    /*new record? - bool - true */
    console.log(DFSFormId);                         /*design / form id - integer - example : 14542 */
    console.log(DFSRecordId);                       /*record id - integer - 0 */
    console.log(DFSRecordWfState);                  /*record workflow state - integer - 0 */
    console.log(AppPath);                           /*web app root path - string - example : '/DocHuman/' */
```

## Form Mode "Browse"
```js
    console.log(DFSUserId);                         /*get active user id - integer - example : 12 */
    console.log(DFSFirstDepartmentId);              /*get active user org. department-id - integer - example : -1 */
    console.log(DFSFirstPositionId);                /*get active user oeg. position-id - integer - example : -1 */
    console.log(DFSIsRuntimeAdminUser);             /*is user Admin? - bool - example : true */
    console.log(DFSIsNewRecord);                    /*new record? - bool - false */
    console.log(DFSFormId);                         /*design / form id - integer - example : 14542 */
    console.log(DFSRecordId);                       /*record id - integer - example : 113 */
    console.log(DFSRecordWfState);                  /*record workflow state - integer - example : 0,1,2,3,4 */
    console.log(AppPath);                           /*web app root path - string - example : '/DocHuman/' */
```
