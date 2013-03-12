using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bea.Core.Services;

namespace Bea.Web.Controllers.API
{
    public class CategoryAPIController : ApiController
    {
        private ICategoryServices _categoryServices;

        public CategoryAPIController(ICategoryServices categoryServices)
        {
            if (categoryServices == null)
                throw new ArgumentNullException("categoryServices");

            _categoryServices = categoryServices;
        }

        public HttpResponseMessage GetAllCategories()
        {
            HttpResponseMessage response;

            var list = _categoryServices.GetAllCategories().ToList().Select(x => new { Id = x.Id, Label = x.Label });

            response = Request.CreateResponse(HttpStatusCode.OK, list);

            return response;
        }

        public HttpResponseMessage GetAllCategoriesAndGroups()
        {
            HttpResponseMessage response;

            var list = _categoryServices.GetAllCategoriesAndGroups();

            response = Request.CreateResponse(HttpStatusCode.OK, list);

            return response;
        }

        public HttpResponseMessage GetAllCategoryChildrenLabels(int parentId)
        {
            HttpResponseMessage response;

            List<String> list = _categoryServices.GetCategoryChildrenLabelFromParentId(parentId).ToList();

            response = Request.CreateResponse(HttpStatusCode.OK, list);

            return response;
        }
    }
}
