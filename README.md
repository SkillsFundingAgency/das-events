# Events API #

Events API for the Digital Apprenticeship Service. 

The API deals with apprenticeship and provider agreement events

**Build status**

![Build Status](https://sfa-gov-uk.visualstudio.com/_apis/public/build/definitions/c39e0c0b-7aff-4606-b160-3566f3bbce23/164/badge)


## Functionality: apprenticeships ##

### Storing events (apprenticeships) ###

To store a new event:

POST http://host:port/api/events/apprenticeships/

    {
        "event": "An event",
		"apprenticeshipId": 444,
		"paymentStatus": "active",
		"agreementStatus": "agreed",
		"providerId": "1111",
		"learnerId": "9999",
		"trainingType": "framework",
		"trainingId": "3333",
		"employerAccountId": "2222",
		"trainingStartDate": "2017-01-31T00:00:00",
		"trainingEndDate": "2017-12-31T00:00:00",
		"trainingTotalCost": 123.45
	}

Where:

- **event** is an arbitrary string representation of an event (eg. "Paused")
- **apprenticeshipId** is an identifier used to aggregate apprenticeship information
- **paymentStatus** is the current status of the apprenticeship
- **agreementStatus** denotes which parties have agreed the apprenticeship
- **providerId** is the unique ID for a provider (eg. UKPRN)
- **learnerId** is the unique ID for a learner (eg. ULN)
- **trainingType** is:
	- for frameworks either "framework" or 1
	- for standards either "standard" or 2
- **trainingId** is a framework or standard identifier
- **employerAccountId** is the unique ID for an employer account
- **trainingStartDate** is the start date of the apprenticeship training
- **trainingEndDate** is the end date of the apprenticeship training
- **trainingTotalCost** is the total price for the apprenticeship training


### Retrieving events (apprenticeships) ###

Apprenticeship events can be retrieved based on either:

- in date order (events occurring between a specified date range)
- in event ID order (events following a specified event ID)

Paging of response data is supported or both.

To retrieve events:

	GET http://host:port/api/events/apprenticeships?fromDate=<fromDate>&toDate=<toDate>&pageSize=<pageSize>&pageNumber=<pageNumber>&fromEventId=<fromEventId)

Where:

- **fromDate** is the datetime range to query from (inclusive)
- **toDate** is the datetime range to query up to (not inclusive). Must be same or greater than *fromDate*
- **fromEventId** is the "starting" event identifier to be returned (inclusive)
- **pageSize** is the number of rows to be returned (eg. 100)
- **pageNumber** is the 1-based "page" of data to be returned based on the *pageSize*

Note: Datetimes passed on the querystring are expressed as yyyyMMddHHmmss, eg. 20161231235959

Note: Querying by event ID takes precedence over date based queries - dates will be ignored if an event ID is specified. 

Examples:

	http://localhost:29638/api/events/apprenticeships?fromDate=20160131000001&toDate=20171231235959&pageSize=100&pageNumber=1

	http://localhost:29638/api/events/apprenticeships?fromEventId=1&pageSize=100&pageNumber=1

In addition to the event item properties, the response payload also includes an **id** property. This is the unique internal identifier for a single event and can be used to query by event ID in subsequent API calls.


## Security (apprenticeships) ##

The API uses JWT bearer tokens to enforce authorised access to the API methods. Callers must present a bearer token that contains appropriate permissions.

Supported permissions are:

    StoreApprenticeshipEvent
    ReadApprenticeshipEvent

The token should be passed in an `authorization` header and prefixed with `bearer `, for example:

    Authorization:bearer <token>

Note that tokens can viewed using an online tool such as the one at [http://jwt.io](http://jwt.io "online JWT viewer")


## Functionality: provider agreements ##

POST and GET operations are very similar to the apprenticeship events above - differences are briefly described below.

### Storing events (agreements) ###

To store a new event:

POST http://host:port/api/events/agreements/

    {
        "event": "An event",
        "providerId": "1111",
        "employerAccountId": "2222"
    }

Where:

- **event** is an arbitrary string representation of an event (eg. "Initiated")
- **providerId** is the unique ID for a provider (eg. UKPRN)
- **employerAccountId** is the DAS employer account ID


### Retrieving events (agreements) ###

Provider agreement retrieval functionality works in the same way as for apprenticeships (see above)

Examples:

	http://localhost:29638/api/events/agreements?fromDate=20160131000001&toDate=20171231235959&pageSize=100&pageNumber=1

	http://localhost:29638/api/events/agreements?fromEventId=1&pageSize=100&pageNumber=1


## Security (agreements) ##

Supported permissions are:

    StoreAgreementEvent
    ReadAgreementEvent

## Functionality: employer accounts ##

POST and GET operations are very similar to the apprenticeship events above - differences are briefly described below.

### Storing events (accounts) ###

To store a new event:

POST http://host:port/api/events/accounts/

    {
        "event": "An event",
        "resourceUri": "/api/accounts/abc"
    }

Where:

- **event** is an arbitrary string representation of an event (eg. "Initiated")
- **resourceUri** is the Accounts API URI of the resource the event relates to


### Retrieving events (accounts) ###

Provider agreement retrieval functionality works in the same way as for apprenticeships (see above)

Examples:

	http://localhost:29638/api/events/accounts?fromDate=20160131000001&toDate=20171231235959&pageSize=100&pageNumber=1

	http://localhost:29638/api/events/accounts?fromEventId=1&pageSize=100&pageNumber=1


## Security (accounts) ##

Supported permissions are:

    StoreAccountEvent
    ReadAccountEvent
