﻿using System;
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

        public HttpResponseMessage GetAllCategoriesAndGroups()
        {
            HttpResponseMessage response;

            var list = _categoryServices.GetAllCategoriesAndGroups();

            response = Request.CreateResponse(HttpStatusCode.OK, list);

            return response;
        }

        public HttpResponseMessage GetAllCategoriesOfAGroup(int categoryId)
        {
            HttpResponseMessage response;

            var list = _categoryServices.GetAllCategoriesOfAGroup(categoryId);

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
