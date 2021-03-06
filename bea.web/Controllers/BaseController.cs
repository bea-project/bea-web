﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bea.Domain.Categories;
using Bea.Domain.Ads;
using Bea.Core.Services;

namespace Bea.Web.Controllers
{
    public class BaseController : Controller
    {
        private ILocationServices _locationServices;
        protected IReferenceServices _referenceServices;

        public BaseController(ILocationServices locationServices,IReferenceServices referenceServices) 
        {
            _locationServices = locationServices;
            _referenceServices = referenceServices;
        }

        public void FillViewLists(Category category)
        {
            if (category == null)
                return;

            switch (category.Type)
            {
                case AdTypeEnum.CarAd:
                    ViewBag.KmBrackets = _referenceServices.GetAllKmBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.AgeBrackets = _referenceServices.GetAllAgeBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.Brands = _referenceServices.GetAllCarBrands().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.Fuels = _referenceServices.GetAllCarFuels().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.MotoAd:
                    ViewBag.Brands = _referenceServices.GetAllMotoBrands().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.KmBrackets = _referenceServices.GetAllKmBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.AgeBrackets = _referenceServices.GetAllAgeBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.EngineSizeBrackets = _referenceServices.GetAllEngineSizeBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    break;
                case AdTypeEnum.OtherVehiculeAd:
                    ViewBag.KmBrackets = _referenceServices.GetAllKmBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.AgeBrackets = _referenceServices.GetAllAgeBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.Fuels = _referenceServices.GetAllCarFuels().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.VehiculeAd:
                    ViewBag.AgeBrackets = _referenceServices.GetAllAgeBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.KmBrackets = _referenceServices.GetAllKmBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    break;
                case AdTypeEnum.RealEstateAd:
                    ViewBag.Rooms = _referenceServices.GetAllRealEstateNbRoomsBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() }).ToList();
                    ViewBag.SurfaceAreas = _referenceServices.GetAllSurfaceAreaBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() }).ToList();
                    ViewBag.Types = _referenceServices.GetAllRealEstateTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.Districts = _locationServices.GetAllDistricts().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.FurnishedList = new List<SelectListItem>() { new SelectListItem { Text = "Meublé", Value = "true" }, new SelectListItem { Text = "Non meublé", Value = "false" } };
                    break;
                case AdTypeEnum.MotorBoatAd:
                    ViewBag.AgeBrackets = _referenceServices.GetAllAgeBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.Types = _referenceServices.GetAllMotorBoatTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.MotorTypes = _referenceServices.GetAllMotorBoatEngineTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.SailingBoatAd:
                    ViewBag.AgeBrackets = _referenceServices.GetAllAgeBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.Types = _referenceServices.GetAllSailingBoatTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    ViewBag.HullTypes = _referenceServices.GetAllSailingBoatHullTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.MotorBoatEngineAd:
                    ViewBag.AgeBrackets = _referenceServices.GetAllAgeBrackets().Select(x => new SelectListItem { Text = x.Value.Label, Value = x.Key.ToString() });
                    ViewBag.Types = _referenceServices.GetAllMotorBoatEngineTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
                case AdTypeEnum.WaterSportAd:
                    ViewBag.Types = _referenceServices.GetAllWaterSportTypes().Select(x => new SelectListItem { Text = x.Label, Value = x.Id.ToString() }).ToList();
                    break;
            }
        }
    }
}
