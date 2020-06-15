//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ExamTask.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrdersController : ControllerBase
//    {
//    }
//}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ExamTask.MyAwDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExamTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;

        private readonly MyAdWorkContext _context;

        public OrdersController(MyAdWorkContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Orders> Get()
        {
            return _context.Orders.ToList().Select(index => new Orders
            {
                SalesOrderId = index.SalesOrderId,
                RevisionNumber = index.RevisionNumber,
                InvoiceOrderNumber = index.InvoiceOrderNumber
            })
            .ToArray();
        }

        [Route("status")]
        [HttpPut]
        public IActionResult Status(string status, string id)
        {
            var order = _context.Orders.Where(a => a.SalesOrderId == id).FirstOrDefault();
            order.Status = status;
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [Route("filter")]
        [HttpGet]
        public Orders Filter(string id)
        {
            Orders order = _context.Orders.Where(a => a.SalesOrderId == id).FirstOrDefault();
            return order;
        }

        [Route("invoice")]
        [HttpPut]
        public IActionResult InvoiceNum(string invoiceNum, string id)
        {
            var order = _context.Orders.Where(a => a.SalesOrderId == id).FirstOrDefault();
            int num = 0;
            Int32.TryParse(invoiceNum, out num);
            order.InvoiceOrderNumber = num;
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post()
        {
            IFormFile file = Request.Form.Files[0];
            var content = file.OpenReadStream();
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLineAsync().Result);
            }


            OrdersDto xmls = null;
            XmlSerializer serializer = new XmlSerializer(typeof(OrdersDto));
            using (TextReader reader = new StringReader(result.ToString()))
            {
                xmls = (OrdersDto)serializer.Deserialize(reader);
            }

            foreach (var item in xmls.Orders)
            {
                var ordEnt = new Orders();
                ordEnt.SalesOrderId = item.SalesOrderId;
                ordEnt.RevisionNumber = item.RevisionNumber;
                int num = 0;
                Int32.TryParse(item.InvoiceOrderNumber, out num);
                ordEnt.InvoiceOrderNumber = num;
                _context.Orders.Add(ordEnt);
            }
            _context.SaveChanges();

            //XmlDocument doc = new XmlDocument();

            //doc.Load(;
            // return result.ToString();
            return Ok();
        }
    }


    [XmlRoot("Orders")]
    public class OrdersDto
    {
        [XmlElement("Order")]
        public List<OrderDto> Orders { get; set; }
    }

    [XmlRoot("Order")]
    public class OrderDto
    {
        [XmlElement("SalesOrderId")]
        public string SalesOrderId { get; set; }
        [XmlElement("RevisionNumber")]
        public string RevisionNumber { get; set; }

        //[XmlElement("SalesOrderNumber")]
        //public string SalesOrderNumber { get; set; }

        //public string Status { get; set; }
        //public string OnlineOrderFlag { get; set; }
        //public string SalesOrderNumber { get; set; }
        [XmlElement("InvoiceOrderNumber")]
        public string InvoiceOrderNumber { get; set; }
        //public string AccountNumber { get; set; }
        //public string CreditCardApprovalCode { get; set; }
        //public string SubTotal { get; set; }
        //public string TaxAmt { get; set; }
        //public string Freight { get; set; }
        //public string TotalDue { get; set; }
    }
}
