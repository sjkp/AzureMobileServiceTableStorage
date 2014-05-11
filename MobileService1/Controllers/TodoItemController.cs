using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using Microsoft.WindowsAzure.Mobile.Service;
using MobileService1.DataObjects;
using MobileService1.Models;

namespace MobileService1.Controllers
{
    public class TodoItemController : TableController<TodoItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileService1Context context = new MobileService1Context(Services.Settings.Schema);
            DomainManager = new StorageDomainManager<TodoItem>("AzureTable", "Todo", Request, Services);


        }

        // GET tables/TodoItem
        public Task<IEnumerable<TodoItem>> GetAllTodoItems()
        {
            var sm = DomainManager as StorageDomainManager<TodoItem>;
            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<TodoItem>("TodoItems");
            var opts = new ODataQueryOptions(new ODataQueryContext(modelBuilder.GetEdmModel(), typeof(TodoItem)), Request);
            return DomainManager.QueryAsync(opts);
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SingleResult<TodoItem>> GetTodoItem(string id)
        {
            return DomainManager.LookupAsync(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostTodoItem(TodoItem item)
        {
            try
            {
                //item.PartitionKey = Guid.NewGuid().ToString();
                
                //item.RowKey = Guid.NewGuid().ToString();
                //item.UpdatedAt = DateTimeOffset.Now;

                TodoItem current = await InsertAsync(item);

                return CreatedAtRoute("Tables", new {id = current.Id}, current);
            }
            catch (Exception ex)
            {
                throw;
            }   
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}