using DrinkShop.Models;
using DrinkShop.Persistence;
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace DrinkShop.Controllers
{
    public class OrdersController : ApiController
    {
        #region Pivate Members

        private static Storage<Order> _storage = new Storage<Order>();

        #endregion


        #region Pivate Methods

        private string normalizeNameID(string name)
        {
            return Regex.Replace(name, @"[^\w]", string.Empty).ToLower();
        }

        #endregion


        #region Public Methods

        // GET: api/Orders
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _storage.GetAll());
        }

        // GET: api/Orders/Pepsi%20Cola
        public HttpResponseMessage Get(string name)
        {
            if (name != null && name.Length > 0)
            {
                var item = _storage.Get(normalizeNameID(name));
                return Request.CreateResponse(item != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, item);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/Orders
        public HttpResponseMessage Post([FromBody]Order value)
        {
            if (value != null && !String.IsNullOrWhiteSpace(value.Name))
            {
                return Request.CreateResponse(
                    _storage.Upsert(normalizeNameID(value.Name), value)
                        ? HttpStatusCode.OK
                        : HttpStatusCode.NotModified
                    );
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Orders/Coca-Cola
        public HttpResponseMessage Put([FromBody]Order value)
        {
            if (value != null && !String.IsNullOrWhiteSpace(value.Name))
            {
                return Request.CreateResponse(
                    _storage.Upsert(normalizeNameID(value.Name), value)
                        ? HttpStatusCode.OK
                        : HttpStatusCode.NotModified
                    );
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Orders/Spezi
        public HttpResponseMessage Delete(string name)
        {
            if (name != null && name.Length > 0)
            {
                return Request.CreateResponse(
                    _storage.Delete(normalizeNameID(name))
                        ? HttpStatusCode.OK
                        : HttpStatusCode.NotModified
                    );
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        #endregion
    }
}