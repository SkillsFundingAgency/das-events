# Events API #

Events API for the Digital Apprenticeship Service

**Build status**

![Build Status](https://sfa-gov-uk.visualstudio.com/_apis/public/build/definitions/c39e0c0b-7aff-4606-b160-3566f3bbce23/164/badge)


## Functionality ##

### Storing events ###

To store a new event:

POST http://host:port/api/events/apprenticeships/

    {
        TODO
    }

Where:

- **TODO** is TODO


### Retrieving events ###

To retrieve events:

GET http://host:port/api/events/apprenticeships?from=<fromDate>&to=<toDate>&pageSize=<pageSize>&pageNumber=<pageNumber>

Where:

- **fromDate** is the datetime range to query from (inclusive)
- **toDate** is the datetime range to query up to (not inclusive). Must be same or greater than *fromDate*
- **pageSize** is the number of rows to be returned (eg. 100)
- **pageNumber** is the "page" of data to be returned based on the *pageSize*

Datetimes are expressed as yyyyMMddHHmmss, eg. 20161231235959

Example:
http://localhost:29638/api/events/apprenticeships?from=20160131000001&to=20171231235959&pageSize=100&pageNumber=1


## Security ##

The API uses JWT bearer tokens to enforce authorised access to the API methods. Callers must present a bearer token that contains appropriate permissions.

Supported permissions are:

    StoreApprenticeshipEvent
    ReadApprenticeshipEvent

The token should be passed in an `authorization` header and prefixed with `bearer `, for example:

    Authorization:bearer <token>

Note that tokens can viewed using an online tool such as the one at [http://jwt.io](http://jwt.io "online JWT viewer")
