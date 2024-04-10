using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Server.Entry.Controllers;

public class ODataDemoController : ODataController
{
    public class Product
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }

    private List<Product> products =
        [
            new Product()
            {
                ID = 1,
                Name = "Bread",
            }
        ];

    [EnableQuery,HttpGet("[controller]/[action]")]
    public List<Product> Get(CancellationToken token)
    {
        return products;
    }
}
