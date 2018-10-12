# coinbasepro-csharp
Coinbase Pro API C# Client Library - https://docs.pro.coinbase.com/


[![Build status](https://ci.appveyor.com/api/projects/status/maqa3anaaxhcwamt?svg=true)](https://ci.appveyor.com/project/dougdellolio/gdax-csharp)
[![NuGet](https://img.shields.io/nuget/v/GDAX.Api.ClientLibrary.svg)](https://www.nuget.org/packages/GDAX.Api.ClientLibrary/)
[![NuGet](https://img.shields.io/nuget/dt/GDAX.Api.ClientLibrary.svg)](https://www.nuget.org/packages/GDAX.Api.ClientLibrary/)
<h1>How to Install</h1>

`PM> Install-Package GDAX.Api.ClientLibrary`

<h1>How to Use</h1>

<i>Generate your key at https://pro.coinbase.com/profile/api</i>

```csharp
//create an authenticator with your apiKey, apiSecret and passphrase
var authenticator = new Authenticator("<apiKey>", "<apiSecret>", "<passphrase>");

//create the CoinbasePro client
var coinbaseProClient = new CoinbasePro.CoinbaseProClient(authenticator);

//use one of the services 
var allAccounts = await coinbaseProClient.AccountsService.GetAllAccountsAsync();
```

<h1>What services are provided?</h1>

###### Accounts ######
- GetAllAccountsAsync() - get all accounts
- GetAccountByIdAsync(id) - get account by id
- GetAccountHistoryAsync(id, limit, numberOfPages) - get account history (paged response)
- GetAccountHoldsAsync(id, limit, numberOfPages) - get all holds placed on an account (paged response)

###### CoinbaseAccounts ######
- GetAllAccountsAsync() - get all coinbase accounts

###### Orders ######
- PlaceMarketOrderAsync(orderSide, productPair, amount, MarketOrderAmountType, clientOId) - place market order by size or funds
- PlaceLimitOrderAsync(orderSide, productPair, size, price, timeInForce, postOnly, clientOId) - place limit order with time in force
- PlaceLimitOrderAsync(orderSide, productPair, size, price, cancelAfter, postOnly, clientOId) - place limit order with cancel after date
- PlaceStopOrderAsync(orderSide, productPair, size, stopPrice, clientOId) - place stop order with stop price
- PlaceStopLimitOrderAsync(orderSide, productPair, size, stopPrice, limitPrice, postOnly, clientOId) - place stop limit order
- CancelAllOrdersAsync() - cancel all orders
- CancelOrderByIdAsync(id) - cancel order by id
- GetAllOrdersAsync(orderStatus, limit, numberOfPages) - get all, active or pending orders (paged response)
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
- GetProductOrderBookAsync(productType, productLevel) - get a list of open orders for a product (specify level 1, 2, or 3)
- GetProductTickerAsync(productType) - get information about the last trade (tick), best bid/ask and 24h volume
- GetTradesAsync(productType, limit, numberOfPages) - get latest trades for a product (paged response)
- GetProductStatsAsync(productType) - get 24 hour stats for a product
- GetHistoricRatesAsync(productPair, start, end, granularity) - get historic rates for a product, auto batches requests to pull complete date range

###### Currencies ######
- GetAllCurrenciesAsync() - gets a list of known currencies

###### Fills ######
- GetAllFillsAsync(limit, numberOfPages) - gets a list of all recent fills (paged response)
- GetFillsByOrderIdAsync(orderId, limit, numberOfPages) - gets a list of all recent fills by order id (paged response)
- GetFillsByProductIdAsync(productType, limit, numberOfPages) - gets a list of all recent fills by product type (paged response)

###### Fundings ######
- GetAllFundingsAsync(limit, fundingStatus, numberOfPages) - gets a list of all orders placed with a margin profile that draws funding (paged response)

###### Reports ######
- CreateNewAccountReportAsync(startDate, endDate, accountId, productType, email, fileFormat) - generate new account report
- CreateNewFillsReportAsync(startDate, endDate, productType, accountId, email, fileFormat) - generate new fills report

###### User Account ######
- GetTrailingVolumeAsync() - get 30-day trailing volume for all products

<h1>Websocket Feed</h1>
<h2>How to use with authentication</h2>

```csharp
//create an authenticator with your apiKey, apiSecret and passphrase
var authenticator = new Authenticator("<apiKey>", "<apiSecret>", "<passphrase>");

//create the CoinbasePro client
var coinbaseProClient = new CoinbasePro.CoinbaseProClient(authenticator);

//use the websocket feed
var productTypes = new List<ProductType>() { ProductType.BtcEur, ProductType.BtcUsd };
var channels = new List<ChannelType>() { ChannelType.Full, ChannelType.User} // When not providing any channels, the socket will subscribe to all channels

var webSocket = coinbaseProClient.WebSocket;
webSocket.Start(productTypes, channels);

// EventHandler for the heartbeat response type
webSocket.OnHeartbeatReceived += WebSocket_OnHeartbeatReceived;

private static void WebSocket_OnHeartbeatReceived(object sender, WebfeedEventArgs<Heartbeat> e)
{
  throw new NotImplementedException();
}
```

<h2>How to use without authentication</h2>

```csharp
//create the CoinbasePro client without an authenticator
var coinbaseProClient = new CoinbasePro.CoinbaseProClient();

//use the websocket feed
var productTypes = new List<ProductType>() { ProductType.BtcEur, ProductType.BtcUsd };
var channels = new List<ChannelType>() { ChannelType.Full, ChannelType.User }; // When not providing any channels, the socket will subscribe to all channels

var webSocket = coinbaseProClient.WebSocket;
webSocket.Start(productTypes, channels);

// EventHandler for the heartbeat response type
webSocket.OnHeartbeatReceived += WebSocket_OnHeartbeatReceived;

private static void WebSocket_OnHeartbeatReceived(object sender, WebfeedEventArgs<Heartbeat> e)
{
  throw new NotImplementedException();
}
```

<h2>Available functions</h2>
These are the starting and stopping methods:

- Start(productTypes, channelTypes) - Starts the websocket feed based on product(s) and channel(s)
- Stop() - Stops the websocket feed

The following methods are EventHandlers:

- OnTickerReceived - EventHandler for data with response type `ticker`
- OnSnapShotReceived - EventHandler for data with response type `snapshot`
- OnLevel2UpdateReceived - EventHandler for data with response type `level2`
- OnHeartbeatReceived - EventHandler for data with response type `heartbeat`
- OnReceivedReceived - EventHandler for data with response type `received`
- OnOpenReceived - EventHandler for data with response type `open`
- OnDoneReceived - EventHandler for data with response type `done`
- OnMatchReceived - EventHandler for data with response type `match`
- OnChangeReceived - EventHandler for data with response type `change`
- OnLastMatchReceived - EventHandler for data with response type `last match`
- OnErrorReceived - EventHandler for data with response type `error`
- OnActivateReceived - Eventhandler for data with response type `activate`
- OnWebSocketError - EventHandler for web socket error
- OnWebSocketClose- EventHandler for web socket closing
- OnWebSocketOpenAndSubscribed - EventHandler for web socket being opened and subscribed

<h1>Sandbox Support</h1>

<i>Generate your key at https://public.sandbox.pro.coinbase.com/profile/api</i>

```csharp
//create an authenticator with your apiKey, signature and passphrase
var authenticator = new Authenticator("<apiKey>", "<signature>", "<passphrase>");

//create the CoinbasePro client and set the sandbox flag to true
var coinbaseProClient = new CoinbasePro.CoinbaseProClient(authenticator, true);

//use one of the services 
var response = await coinbaseProClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, 1);
```

<h1>Examples</h1>

###### Place a market order ######

```csharp
//by size
var response = await coinbaseProClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, 1);

//by funds
var response = await coinbaseProClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, 50, MarketOrderAmountType.Funds);
```

###### Place a limit order ######

```csharp
var response = await coinbaseProClient.OrdersService.PlaceLimitOrderAsync(OrderSide.Sell, ProductType.EthUsd, 1, 400.0M);
```

###### Cancel all open or un-settled orders ######

```csharp
var response = await coinbaseProClient.OrdersService.CancelAllOrdersAsync();
```

###### Getting account history (<i>paged response</i>) ######

```csharp
//the limit is the amount of items per page - in this case it would be 2 items (default is 100)
//you can also specify the number of pages to request - in this case it would be the first 5 pages (default is 0 which will request all pages)
//some routes may require the number of pages to be specified as there are rate limits
var accountHistoryResponse = await coinbaseProClient.AccountsService.GetAccountHistoryAsync("ef56a389", 2, 5);

//retrieve by page number - this would return the first page of the response (latest first)
var firstPage = accountHistoryResponse.ToList()[0];

//get the first item on the page
var firstAccountHistoryOnFirstPage = firstPage.ToList()[0];

//get the second item on the page
var secondAccountHistoryOnFirstPage = firstPage.ToList()[1];
```

###### Generate and email a report ######

```csharp
var reportDateFrom = new DateTime(2017, 1, 1);
var reportDateTo = new DateTime(2018, 1, 1);
var accountId = "29318029382";

//generate and email accounts report csv
var accountResponse = coinbaseProClient.ReportsService.CreateNewAccountReportAsync(reportDateFrom, reportDateTo, accountId, ProductType.BtcUsd, "me@email.com", FileFormat.Csv);

//generate and email fills report pdf
var fillsResponse = coinbaseProClient.ReportsService.CreateNewFillsReportAsync(reportDateFrom, reportDateTo, ProductType.BtcUsd, accountId, "me@email.com", FileFormat.Pdf);
```

###### Overriding the HttpClient behavior ######

You can gain greater control of the http requests by implementing CoinbasePro.HttpClient.IHttpClient and passing that into the CoinbasePro constructor.

```csharp
var myWay = new MyNamespace.MyHttpClient();

//create an authenticator with your apiKey, signature and passphrase
var authenticator = new Authenticator("<apiKey>", "<signature>", "<passphrase>");

//create the CoinbasePro client and set the httpClient to my way of behaving
var coinbaseProClient = new CoinbasePro.CoinbaseProClient(authenticator, myWay);
```

<h1>Logging</h1>

Logging is provided by Serilog - https://github.com/serilog/serilog

```csharp
//configure the application logging to output to console and a file called log.txt
Serilog.Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Console()
				.WriteTo.File("log.txt",
					rollingInterval: RollingInterval.Day,
					rollOnFileSizeLimit: true)
				.CreateLogger();

//create an authenticator with your apiKey, signature and passphrase
var authenticator = new Authenticator("<apiKey>", "<signature>", "<passphrase>");

//create the CoinbasePro client
var coinbaseProClient = new CoinbasePro.CoinbaseProClient(authenticator);

//use one of the services 
var response = await coinbaseProClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, 1);
```

<h1>Contributors</h1>

Thanks for contributing!

- @dgelineau
- @quin810
- @DontFretBrett
- @chrisw000
- @confessore
- @sotam
- @BradForsythe
- @zaccharles
- @BraveSirAndrew
- @alexhiggins732
- @kudobyte

<h1>Bugs or questions?</h1>

Please open an issue for any bugs or questions
