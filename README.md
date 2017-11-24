# GDAXClient
GDAX C# API Client Library - https://docs.gdax.com/

<h1>How to Use</h1>

<i>Generate your key at https://www.gdax.com/settings/api</i>

````
//create an authenticator with your apiKey, signature and passphrase
var authenticator = new Authenticator("<apiKey>", "<signature>", "<passphrase>");

//create the GDAX client
var gdaxClient = new GDAXClient.GDAXClient(authenticator);

//use one of the services 
var allAccounts = await gdaxClient.accountsService.GetAllAccountsAsync();
````

<h1>What services are provided?</h1>

| Accounts                | Orders                                                    | Fills | Funding | Position | Deposits | Withdrawals |
|-------------------------|-----------------------------------------------------------|-------|---------|----------|----------|-------------|
| GetAllAccountsAsync()   | PlaceMarketOrderAsync(orderSide, productType, size)       |       |         |          |          |             |
| GetAccountByIdAsync(id) | PlaceLimitOrderAsync(orderSide, productType, size, price) |       |         |          |          |             |

<h1>Sandbox Support</h1>

<i>Generate your key at https://public.sandbox.gdax.com/settings/api</i>

````
//create an authenticator with your apiKey, signature and passphrase
var authenticator = new Authenticator("<apiKey>", "<signature>", "<passphrase>");

//create the GDAX client and set the sandbox flag to true
var gdaxClient = new GDAXClient.GDAXClient(authenticator, true);

//use one of the services 
var orderResponse = await gdaxClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, 1));
````

<h1>Examples</h1>

###### Place a market order ######

`var orderResponse = await gdaxClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, 1));`

###### Place a limit order ######

`var orderResponse = await gdaxClient.OrdersService.PlaceLimitOrderAsync(OrderSide.Sell, ProductType.EthUsd, 1, 400.0M));`


