namespace GDAXClient.Specs.JsonFixtures.Products
{
    public static class ProductHistoryFixture
    {
        public static string Create()
        {

            var json = @"
[
    [
        1512691200,
        16777,
        17777.69,
        17390.01,
        17210.99,
        7650.386033540894
    ],
    [
        1512633600,
        14487.8,
        19697,
        14487.8,
        17390.01,
        65581.82529800163
    ],
    [
        1512576000,
        13500,
        14499.89,
        14056.78,
        14487.8,
        12303.76923928093
    ]
]";

            return json;
        }
    }
}
