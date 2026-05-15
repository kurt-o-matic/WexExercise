Thank you for taking the time to review this product brief submission. The following documentation is intended to describe some of the choices made filling in the gaps of the project requirements.

# What Is Implemented

The bare requirements in the brief are all implemented here, with some additional infrastructure provided in the spirit of approximating a production-like service application.

A small service library is present to provide storage persistence, US Treasury web API calls, as well as the purchase and conversion functions outlined in the brief requirements.

- **Non-volatile Persistent Storage**: LiteDb was chosen as an ideal open-source imbedded NoSQL database framework. While this is working well here, in a real production service support scenario, additional thought may need to be given to file access and concurrency risks.

- **US Treasury Exchange Rates API**: A simple client class based on HttpClient provides the exchange rate lookup, respecting both the purchase transaction date and the 6-month rate expiration requirements.

- **Currency Exchange Service**: All the specified business functions are contained in a currency exchange class that provides interfaces for storage of USD purchase records, and subsequent retrieval with specified foreign currency conversion.

- **Purchase Record Validation**: The various input field validation requirements are implemented:
  - Descriptions are strictly limited to 50 characters and will throw an exception if exceeded. Additionally, a 5 character minimum was also added in the spirit of a more realistic business record.
  - TransactionDate is stored in DateOnly form at all times, and future dates are prohibited, again in the spirit of a realistic use-case.
  - PurchaseAmount is stored in Decimal; negative values and greater than 2-digits of scale will throw an exception.

- **Foreign Currency Conversion**: The exchange service class will attempt to match the given purchase record against a date-validated, non-expired conversion rate, and return the required output fields containing the results.

- **Minimal Web API Host**: The required business functions need to be accessible in some way, so a simple self-hosted web API host has been provided to express the business requirements. This was implemented in a stripped-down implementation of the C# Minimal API pattern in ASP.NET.

- **Automated Test Cases**: Some targeted unit and integration test cases have been provided, with an excess of 80% code coverage. All specified validation and business functions are subject to testing.

# What Is Not Implemented

Due to unknown hypotheticals left unspecified in the brief, and ultimately a lack to time to chase speculative concerns too far, not every single element of a completely realistic production system is provided.

- **Performance Optimizations**: Specifically called out in the brief, no effort has been made to test or optimize performance, including DB optimizations, caching (either the Treasury API or this one), memory management, or asynchronous threading.

- **Configuration**: While a more realistic application would undoubtedly require some form of region-specific configuration, this brief only contained a bare handful of static values, most of which it can be argued should be managed as part of the code base.

- **Logging**: Also very high on the production support stack, logging would be essential in any actual deployment. But the form and purpose of that logging would be highly speculative without having more concrete requirements.

- **Gold Standard Testing**: While an effort was made to provide sufficient testing to demonstrate the brief requirements, and good code coverage is provided, the testing strategy remains a little undercooked. In particular, data storage mocking is lacking and many of the test cases are polluting the LiteDB file. Also, in a more robust business system with more endpoints, it would make sense to have automated endpoint testing in place. The underlying code is covered, but only a manual .http test file is currently provided for testing the endpoints directly.

# API Documentation

There are two endpoints provided: /purchase accepts a POST to add purchase records to the data store, and /convert accepts a GET call to look up a transaction with the targeted foreign currency conversion.

## /purchase

This endpoint expects a POST body containing a Purchase record in application/json format:

```
{
  "description":"Example Purchase",
  "transactionDate":"2020-12-31",
  "purchaseAmount":"78.42"
}
```
This endpoint returns an object containing the resulting record ID value in the data store:

```
{
  "id": "019e2929-090a-74b5-ae6b-edff75a1cc07"
}
```
## /convert

This GET endpoint takes the following parameters:
- **id**: a Guid value identifying the purchase record.
- **country**: a string containing the name of the target conversion country.
- **currency**: a string containing the name of the target countries' currency.

```
/convert?id=019e2929-090a-74b5-ae6b-edff75a1cc07&country=Canada&currency=Dollar
```

A result object formatted in application/json is returned:

```
{
  "id": "019e2929-090a-74b5-ae6b-edff75a1cc07",
  "description": "Testing web post...",
  "transactionDate": "2020-12-31",
  "purchaseAmount": 78.42,
  "countryCurrency": "Canada-Dollar",
  "exchangeRate": 1.275,
  "convertedAmount": 99.99
}
```

A .http file is included in the WebApi source project containing working examples.

# Thank You

Thanks again for taking a look at my submission! I welcome the opportunity to work on a fun little project like this.

Cheers,
Kurtis
