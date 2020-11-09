using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ModelLib.Model;
using FilterItem = ModelLib.Model.FilterItem;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestItemService.Controllers
{
    [Route("localItems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<Item> items = new List<Item>()
        {
            new Item(1,"Bread","Low",33),
            new Item(2,"Bread","Middle",21),
            new Item(3,"Beer","low",70.5),
            new Item(4,"Soda","High",21.4),
            new Item(5,"Milk","Low",55.8)
        };
        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return items;
        }

        // GET api/<ItemsController>/5
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return items.Find(i => i.Id == id);
        }


        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            items.Add(value);
        }

        // PUT api/<ItemsController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody] Item value)
        {
            Item item = Get(id);
            if (item != null)
            {
                item.Id = value.Id;
                item.Name = value.Name;
                item.Quality = value.Quality;
                item.Quantity = value.Quantity;
            }

        }

        // DELETE api/<ItemsController>/5
        //[HttpDelete("{id}")]
        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            Item item = Get(id);
            items.Remove(item);
        }

        [HttpGet]
        [Route("name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(String substring)
        {
            return items.FindAll(i => i.Name.ToLower().Contains(substring.ToLower()));
        }

        [HttpGet]
        [Route("quality/{substring}")]
        public IEnumerable<Item> GetQuality(String substring)
        {
            return items.FindAll(i => i.Quality.ToLower().Contains(substring.ToLower()));
        }

        [HttpGet]
        [Route("quantity/")]
        public IEnumerable<Item> GetWithFilter([FromQuery] FilterItem filter)
        {

            return items.FindAll(i => (i.Quantity < filter.HighQuantity) && (i.Quantity > filter.LowQuantity));
            // http://localhost:54952/localitems/quantity/?LowQuantity=2&&HighQuantity=50 det man skal søge på i URL for at bruge denne funktion
        }
    }
}
