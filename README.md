# Simple attendance manager
Web application for meal attendance management in school
## Endpoints
### `/user`
- `POST`\
 Endpoint for user authentication, receives Login and Password in body, returns JWT if authorization was successfull, 401 otherwise
### `/child`
- `GET /childId` \
Returns child with specified id
- `GET /group/groupId` \
 Returns array of children for specified group, group with no id returns all children
- `POST` \
Creates child with  name, surname, group and default meals specified in request body
- `DELETE /childId` \
Deletes child with specified id
- `PATCH /childId` \
Updates default meals with values from body
- `PUT /childId` \
Updates personal info with values from body
### `/group`
- `GET /groupId`\
Gets group with specified id or all groups if no id was specified
- `PUT /groupId`\
Updates group name
### `/attendance/child`
- `GET /childId/year/month/day`\
 Returns attendance record for given child at given date
- `GET /total/childId/year/month/`\
 Returns cumulative attendance record for child at given month
- `POST /childId/year/month/day`\
 Sets attendance defined in body on specified date