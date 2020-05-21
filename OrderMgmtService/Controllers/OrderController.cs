using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderMgmtService.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {

        /// <summary>
        /// this is to get the OrderDetas by orderId
        /// </summary>   
        /// <param name="orderId"></param>
        /// <returns></returns>
        [Route("GetOrderDetailsByOrderId")]
        [HttpGet]
        public HttpResponseMessage Get(int orderId)
        {
            if (orderId != 0)
            {
                using (var context = new ordermgmtDBEntities())
                {
                    var resultObject = context.ORDERS.Where(s => s.ORD_NUM == orderId).FirstOrDefault();
                    return Request.CreateResponse(HttpStatusCode.OK, resultObject);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, orderId);
            }

        }   
           



        [HttpPost]
        [Route("InsertOrder")]
        public HttpResponseMessage InsertOrder([FromBody]ORDER oderdata)
        {
            if (oderdata != null)
            {
                using (var context = new ordermgmtDBEntities())
                {
                    var resultObject = context.ORDERS.Add(oderdata);
                    int newlyinsertedOrderId = context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, newlyinsertedOrderId);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not Inserted");
            }
        }
        [HttpGet]
        [Route("GetOrderDetailsByCustomerId")]
        public HttpResponseMessage GetOrdersByCustomerId(string customerId)
        {
            if (customerId != null)
            {
                using (var context = new ordermgmtDBEntities())
                {
                    var resultObject = from O in context.ORDERS.Where(x=>x.CUST_CODE==customerId)                                
                                       select new
                                       {
                                           cusomerName = from c in context.CUSTOMERs.Where(x=>x.CUST_CODE== customerId) select c.CUST_NAME,
                                           csutomerId = O.CUST_CODE,
                                           Orders =O
                                       };

                    return Request.CreateResponse(HttpStatusCode.OK, resultObject.ToList());
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not valid Customer");
            }
            }

        [HttpPut]
        [Route("UpdatedOrder")]
        public HttpResponseMessage UpdatedOrder([FromBody]ORDER oderdata)
        {
            if (oderdata != null)
            {
                using (var context = new ordermgmtDBEntities())
                {
                    var newOrderData = new ORDER();
                    newOrderData= oderdata;
                    var resultObject = context.ORDERS.Add(newOrderData);
                    context.Entry(newOrderData).State = System.Data.Entity.EntityState.Modified;
                    int newlyinsertedOrderId = context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, newlyinsertedOrderId);
                }
            }
            else
            {
                  return Request.CreateResponse(HttpStatusCode.InternalServerError, "Not updated");
            }
        }

    }
    
}
