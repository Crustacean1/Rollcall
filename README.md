# Simple attendance manager
## Endpoints
### User
- POST 
 Provides {"Login": "","Password : ""} in request body\\
 Receives jwt if authorization succeeds
- PUT
 Prodvides {"Login" :"", "Password" :""} in request body\\
 Receives 201 code if user is created
### Child
- GET /{Group|}/{Id|}
 Receives array of children for specified target group, group with no id means all children\\
 \[ Name : name, Surname: surname, GroupId: Id, defaultAttendance: \[meal name: true|false,...\] \]
- POST 
 Provides array of children: {Name:name,Surname:surname,GroupId:Id,Defaults:[]|}\\
 Receives: 2001 code if operation suceeds
- DELETE
 Provides Id
 Receives some code, idk
### Group
- Standard CRUD, nothing to see here 
### Attendance
- GET/{child|group}/{id}/{year}/{month}/{day | }
 Receives attendance record for given child(or group of children) in a given month or day
- GET/total/{child|group}}/{id}/{year}/{month}/{day|}
 Receives cumulative attendance record for given child(or group of children) in a given month or day
- POST/{child | group}/{Id}/{year}/{month}/{day}
 Sets attendance, or attendance mask in case of a group