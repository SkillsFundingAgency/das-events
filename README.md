# Events API #

Events API for the Digital Apprenticeship Service

**Build status**

TODO

## Functionality ##

### Storing events ###

To store a new event:

POST http://host:port/api/events/

    {
        TODO
    }

Where:

- **TODO** is TODO


### Retrieving events ###

To retrieve events:

GET http://host:port/api/events?from=<fromDate>&to=<toDate>

    {
        TODO
    }

Where:

- **fromDate** is the datetime range to query from (inclusive)
- **toDate** is the datetime range to query up to (not inclusive)
- **TODO** is TODO

#### Example ####

TODO


## Security ##

The API uses JWT bearer tokens to enforce authorised access to the API methods. Callers must present a bearer token that contains appropriate permissions.

Supported permissions are:

    StoreEvent
    ReadEvent

The token should be passed in an `authorization` header and prefixed with `bearer `, for example:

    Authorization:bearer <token>

Note that tokens can viewed using an online tool such as the one at [http://jwt.io](http://jwt.io "online JWT viewer")
