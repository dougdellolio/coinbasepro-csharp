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
var allAccounts = await gdaxClient.accountsService.GetAllAccounts();
````

<h1>What services are provided?</h1>

| Accounts           | Orders | Fills | Funding | Position | Deposits | Withdrawals |
|--------------------|--------|-------|---------|----------|----------|-------------|
| GetAllAccounts()   |        |       |         |          |          |             |
| GetAccountById(id) |        |       |         |          |          |             |


