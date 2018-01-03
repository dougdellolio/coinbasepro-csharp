# gdax-csharp
GDAX API C# Client Library - https://docs.gdax.com/

<a href="https://travis-ci.org/dougdellolio/gdax-csharp"><img src="https://travis-ci.org/dougdellolio/gdax-csharp.svg?branch=master"></a> [![NuGet](https://img.shields.io/nuget/v/GDAX.Api.ClientLibrary.svg)](https://www.nuget.org/packages/GDAX.Api.ClientLibrary/)
[![NuGet](https://img.shields.io/nuget/dt/GDAX.Api.ClientLibrary.svg)](https://www.nuget.org/packages/GDAX.Api.ClientLibrary/)
<h1>How to Install</h1>

`PM> Install-Package GDAX.Api.ClientLibrary`

<h1>How to Use</h1>

<i>Generate your key at https://www.gdax.com/settings/api</i>

````
//create an authenticator with your apiKey, signature and passphrase
var authenticator = new Authenticator("<apiKey>", "<signature>", "<passphrase>");

//create the GDAX client
var gdaxClient = new GDAXClient.GDAXClient(authenticator);

//use one of the services 
var allAccounts = await gdaxClient.AccountsService.GetAllAccountsAsync();
````

<h1>What services are provided?</h1>

###### Accounts ######
- GetAllAccountsAsync() - get all accounts
- GetAccountByIdAsync(id) - get account by id
- GetAccountHistoryAsync(id, limit) - get account history (paged response)
- GetAccountHoldsAsync(id, limit) - get all holds placed on an account (paged response)

###### CoinbaseAccounts ######
- GetAllAccountsAsync() - get all coinbase accounts

###### Orders ######
- PlaceMarketOrderAsync(orderSide, productType, size) - place market order
- PlaceLimitOrderAsync(orderSide, productType, size, price) - place limit order
- CancelAllOrdersAsync() - cancel all orders
- CancelOrderByIdAsync(id) - cancel order by id
- GetAllOrdersAsync(limit) - get all open or un-settled orders (paged response)
- GetOrderByIdAsync(id) - get order by id

###### Payments ######
- GetAllPaymentMethodsAsync() - get all payment methods

###### Withdrawals ######
- WithdrawFundsAsync(paymentMethodId, amount, currency) - withdraw funds to a payment method
- WithdrawToCoinbaseAsync(coinbaseAccountId, amount, currency) - withdraw funds to a coinbase account
- WithdrawToCryptoAsync(cryptoAddress, amount, currency) - withdraw funds to a crypto address

###### Deposits ######
- DepositFundsAsync(paymentMethodId, amount, currency) - deposits funds from a payment method
- DepositCoinbaseFundsAsync(coinbaseAccountId, amount, currency) - deposits funds from a coinbase account

###### Products ######
- GetAllProductsAsync() - get a list of available currency pairs for trading
- GetProductOrderBookAsync(productType) - get a list of open orders for a product
- GetProductTickerAsync(productType) - get information about the last trade (tick), best bid/ask and 24h volume
- GetProductStatsAsync(productType) - get 24 hour stats for a product

###### Currencies ######
- GetAllCurrenciesAsync() - gets a list of known currencies

###### Fills ######
- GetAllFillsAsync(limit) - gets a list of all recent fills (paged response)
- GetFillsByOrderIdAsync(orderId, limit) - gets a list of all recent fills by order id (paged response)
- GetFillsByProductIdAsync(productType, limit) - gets a list of all recent fills by product type (paged response)

###### Fundings ######
- GetAllFundingsAsync(limit, fundingStatus) - gets a list of all orders placed with a margin profile that draws funding (paged response)

<h1>Sandbox Support</h1>

<i>Generate your key at https://public.sandbox.gdax.com/settings/api</i>

````
//create an authenticator with your apiKey, signature and passphrase
var authenticator = new Authenticator("<apiKey>", "<signature>", "<passphrase>");

//create the GDAX client and set the sandbox flag to true
var gdaxClient = new GDAXClient.GDAXClient(authenticator, true);

//use one of the services 
var response = await gdaxClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, 1));
````

<h1>Examples</h1>

###### Place a market order ######

`var response = await gdaxClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, 1));`

###### Place a limit order ######

`var response = await gdaxClient.OrdersService.PlaceLimitOrderAsync(OrderSide.Sell, ProductType.EthUsd, 1, 400.0M));`

###### Cancel all open or un-settled orders ######

`var response = await gdaxClient.OrdersService.CancelAllOrdersAsync();`

###### Getting account history (<i>paged response</i>) ######

````
//the limit is the amount of items per page - in this case it would be 2 items (default is 100)
var accountHistoryResponse = await gdaxClient.AccountsService.GetAccountHistoryAsync("ef56a389", 2);

//retrieve by page number - this would return the first page of the response (latest first)
var firstPage = accountHistoryResponse.ToList()[0];

//get the first item on the page
var firstAccountHistoryOnFirstPage = firstPage.ToList()[0];

//get the second item on the page
var secondAccountHistoryOnFirstPage = firstPage.ToList()[1];
````

<h1>Contributors</h1>

Thanks for contributing!

- @dgelineau

<h1>Bugs or questions?</h1>

Please open an issue for any bugs or questions

