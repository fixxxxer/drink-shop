# drink-shop

### Requirements

VS 2015 or later with .Net Framework 4.6 and later

### How to use the REST API

The solution is ready to be compiled and ran directly without any additional configurations.

The implementation is done using the ASP.NET WebAPI and can send/receive both XML and JSON payloads, depending on the client preferences and settings.

CRUD operations are implemented using REST over the HTTP verbs as listed below.

The drink order consists of the drink name and a quantity, the name will be internally normalized to represent a key, the current implementation removed any non-alphanumerics and uses the last updated name to represent the formatted name for any specific product. Hence the following spelling alternatives would all represent the same drink:

- "Coca Cola"
- "Cocacola"
- "coca    cola"
- " COCA-COLA *"
- "COCA/cola"

**JSON payload sample**:

```{"Name":"pepsi", "Quantity":3}```

**XML payload sample**:
```
<Order>
  <Name>pepsi</Name>
  <Quantity>3</Quantity>
</Order>
```

### Supported operations

**GET** *http://localhost:12345/api/orders* : Lists all available orders in the system as an XML or a JSON array

**GET** *http://localhost:12345/api/orders/Pepsi%20Cola* : Allocates an order for the requested drink name, if not found returns an HTTP status code 404

**POST**: http://localhost:12345/api/orders : Adds a drink requested with the provided quantity to the system, the current implementation is an Upsert, so if the drink exists (even under a different name) it would update its quantity and the name spelling

**PUT** *http://localhost:12345/api/orders/Coca-Cola* : Although this is supposed to be an update operation but for more flexibility and usability it's currently implemented an an Upsert similar to the POST described above and it ignores the name used in the URL and uses the name in the payload instead.

**DELETE** *http://localhost:12345/api/orders/Spezi* : Tries to allocate the order by the drink name provided in the URL and deletes it, if the record didn't exist it return an HTTP status code 304