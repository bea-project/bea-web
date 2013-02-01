using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Domain.Ads;
using System.Text.RegularExpressions;
using Bea.Domain.Ads.WaterSport;

namespace Bea.Services
{
    public class AdDataConsistencyServices : IAdDataConsistencyServices
    {
        private Regex _phoneRegex = new Regex(@"^[0-9]{6}$");
        private Regex _emailRegex = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");

        public IDictionary<string, string> GetAdDataConsistencyErrors(BaseAd ad)
        {
            IDictionary<string, string> errors = new Dictionary<string, string>();
            
            // Check base ad data consistency
            GetBaseAdDataConsistencyErrors(ad, errors);

            // Check user data consistency
            GetUserDataConsistencyErrors(ad, errors);

            if (ad.Category == null)
                return errors;

            // Check specifyc ad data consistency
            switch (ad.Category.Type)
            {
                case AdTypeEnum.CarAd:
                    GetCarAdDataConsistencyErrors(ad as CarAd, errors);
                    break;

                case AdTypeEnum.MotoAd:
                    GetMotoAdDataConsistencyErrors(ad as MotoAd, errors);
                    break;

                case AdTypeEnum.OtherVehiculeAd:
                    GetOtherVehicleAdDataConsistencyErrors(ad as OtherVehicleAd, errors);
                    break;

                case AdTypeEnum.VehiculeAd:
                    GetVehicleAdDataConsistencyErrors(ad as VehicleAd, errors);
                    break;

                case AdTypeEnum.MotorBoatAd:
                    GetMotorBoatAdDataConsistencyErrors(ad as MotorBoatAd, errors);
                    break;

                case AdTypeEnum.SailingBoatAd:
                    GetSailingBoatAdDataConsistencyErrors(ad as SailingBoatAd, errors);
                    break;

                case AdTypeEnum.MotorBoatEngineAd:
                    GetMotorBoatEngineAdDataConsistencyErrors(ad as MotorBoatEngineAd, errors);
                    break;

                case AdTypeEnum.WaterSportAd:
                    GetWaterSportAdDataConsistencyErrors(ad as WaterSportAd, errors);
                    break;

                default:
                    break;
            }

            return errors;
        }

        public IDictionary<string, string> GetBaseAdDataConsistencyErrors(BaseAd ad, IDictionary<string, string> errors)
        {
            if (String.IsNullOrEmpty(ad.Title))
                errors.Add("Title", "Veuillez donner un titre à votre annonce.");

            if (String.IsNullOrEmpty(ad.Body))
                errors.Add("Body", "Veuillez rédiger un texte d'annonce.");

            if (ad.Price < 0)
                errors.Add("Price", "Veuillez saisir un prix positif.");

            if (!String.IsNullOrEmpty(ad.PhoneNumber) && !_phoneRegex.IsMatch(ad.PhoneNumber))
                errors.Add("Telephone", "Telephone invalide.");

            if (ad.City == null)
                errors.Add("SelectedCityId", "Veuillez sélectionner une ville.");

            if (ad.Category == null)
                errors.Add("SelectedCategoryId", "Veuillez séléctionner une catégorie.");

            return errors;
        }

        public IDictionary<string, string> GetWaterSportAdDataConsistencyErrors(WaterSportAd waterSportAd, IDictionary<string, string> errors)
        {
            if (waterSportAd.Type == null)
                errors.Add("SelectedTypeId", "Veuillez sélectionner une discipline.");

            return errors;
        }

        public IDictionary<string, string> GetMotorBoatEngineAdDataConsistencyErrors(MotorBoatEngineAd motorBoatEngineAd, IDictionary<string, string> errors)
        {
            

            if (motorBoatEngineAd.Type == null)
                errors.Add("SelectedMotorTypeId", "Veuillez sélectionner un type de moteur.");

            if (motorBoatEngineAd.Type == null)
                errors.Add("SelectedTypeId", "Veuillez sélectionner un type.");

            if (motorBoatEngineAd.Year == 0)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            if (motorBoatEngineAd.Hp == 0)
                errors.Add("Hp", "Veuillez saisir une puissance.");

            return errors;
        }

        public IDictionary<string, string> GetSailingBoatAdDataConsistencyErrors(SailingBoatAd sailingBoatAd, IDictionary<string, string> errors)
        {
            if (sailingBoatAd.HullType == null)
                errors.Add("SelectedHullTypeId", "Veuillez sélectionner une coque.");

            if (sailingBoatAd.Type == null)
                errors.Add("SelectedTypeId", "Veuillez sélectionner un matériaux.");

            if (sailingBoatAd.Year == 0)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            if (sailingBoatAd.Length == 0)
                errors.Add("Length", "Veuillez saisir une longueur.");

            return errors;
        }

        public IDictionary<string, string> GetMotorBoatAdDataConsistencyErrors(MotorBoatAd motorBoatAd, IDictionary<string, string> errors)
        {
            if (motorBoatAd.MotorType == null)
                errors.Add("SelectedMotorTypeId", "Veuillez sélectionner un type de moteur.");

            if (motorBoatAd.Type == null)
                errors.Add("SelectedTypeId", "Veuillez sélectionner un type.");

            if (motorBoatAd.Year == 0)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            if (motorBoatAd.Hp == 0)
                errors.Add("Hp", "Veuillez saisir une puissance.");

            if (motorBoatAd.Length == 0)
                errors.Add("Length", "Veuillez saisir une longueur.");

            return errors;
        }

        public IDictionary<string, string> GetCarAdDataConsistencyErrors(CarAd carAd, IDictionary<string, string> errors)
        {
            if (carAd.Kilometers == 0)
                errors.Add("Km", "Veuillez séléctionner un kilométrage.");

            if (carAd.Fuel == null)
                errors.Add("SelectedFuelId", "Veuillez sélectionner un type.");

            if (carAd.Brand == null)
                errors.Add("SelectedBrandId", "Veuillez sélectionner une marque.");

            if (carAd.Year == 0)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            return errors;
        }

        public IDictionary<string, string> GetVehicleAdDataConsistencyErrors(VehicleAd vehicleAd, IDictionary<string, string> errors)
        {
            if (vehicleAd.Kilometers == 0)
                errors.Add("Km", "Veuillez séléctionner un kilométrage.");
            if (vehicleAd.Year == 0)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            return errors;
        }

        public IDictionary<string, string> GetOtherVehicleAdDataConsistencyErrors(OtherVehicleAd carAd, IDictionary<string, string> errors)
        {
            if (carAd.Kilometers == 0)
                errors.Add("Km", "Veuillez séléctionner un kilométrage.");

            if (carAd.Fuel == null)
                errors.Add("SelectedFuelId", "Veuillez sélectionner un type.");

            if (carAd.Year == 0)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            return errors;
        }

        public IDictionary<string, string> GetMotoAdDataConsistencyErrors(MotoAd motoAd, IDictionary<string, string> errors)
        {
            if (motoAd.Kilometers == 0)
                errors.Add("Km", "Veuillez séléctionner un kilométrage.");

            if (motoAd.Year == 0)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            if (motoAd.EngineSize == 0)
                errors.Add("EngineSize", "Veuillez sélectionner une cylindrée.");

            if (motoAd.Brand == null)
                errors.Add("SelectedBrandId", "Veuillez sélectionner une marque.");

            return errors;
        }

        public IDictionary<string,string> GetUserDataConsistencyErrors(BaseAd ad, IDictionary<string, string> errors)
        {
            if (ad.CreatedBy != null && String.IsNullOrEmpty(ad.CreatedBy.Firstname))
                    errors.Add("Name", "Veuillez saisir un nom.");
            if (ad.CreatedBy != null && String.IsNullOrEmpty(ad.CreatedBy.Email))
                    errors.Add("Email", "Veuillez insérer une adresse email.");
            else
                if (ad.CreatedBy != null && !_emailRegex.IsMatch(ad.CreatedBy.Email))
                            errors.Add("Email", "Email invalide.");
            return errors;
        }


        public Dictionary<string, string> GetAdDataConsistencyErrors(BaseAd ad, int? selectedProvinceId)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(ad.Title))
                errors.Add("Title", "Veuillez donner un titre à votre annonce.");
            if (String.IsNullOrEmpty(ad.Body))
                errors.Add("Body", "Veuillez rédiger un texte d'annonce.");
            if (ad.Price < 0)
                errors.Add("Price", "Veuillez saisir un prix positif.");
            //if (ad.CreatedBy!= null && String.IsNullOrEmpty(ad.CreatedBy.Firstname))
            //    errors.Add("Name", "Veuillez saisir un nom.");
            //if (ad.CreatedBy != null && String.IsNullOrEmpty(ad.CreatedBy.Email))
            //    errors.Add("Email", "Veuillez insérer une adresse email.");
            //else
            //{
            //    Regex emailRegex = new Regex(@"^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$");
            //    if (ad.CreatedBy != null && !emailRegex.IsMatch(ad.CreatedBy.Email))
            //        errors.Add("Email", "Email invalide.");
            //}
            if (!String.IsNullOrEmpty(ad.PhoneNumber))
            {
                Regex phoneRegex = new Regex(@"^[0-9]{6}$");
                if (!phoneRegex.IsMatch(ad.PhoneNumber))
                    errors.Add("Telephone", "Telephone invalide.");
            }
            if (!selectedProvinceId.HasValue)
                errors.Add("SelectedProvinceId", "Veuillez sélectionner une province.");
            if (ad.City == null)
                errors.Add("SelectedCityId", "Veuillez sélectionner une ville.");
            if (ad.Category == null)
                errors.Add("SelectedCategoryId", "Veuillez séléctionner une catégorie.");
            if (ad is CarAd)
            {
                CarAd carAd = ad as CarAd;
                if (carAd.Kilometers == 0)
                    errors.Add("Km", "Veuillez séléctionner un kilométrage.");
                if (carAd.Fuel == null)
                    errors.Add("SelectedFuelId", "Veuillez sélectionner un type.");
                if (carAd.Brand == null)
                    errors.Add("SelectedBrandId", "Veuillez sélectionner une marque.");
                if (carAd.Year == 0)
                    errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");
            }
            if (ad is MotoAd)
            {
                MotoAd motoAd = ad as MotoAd;
                if (motoAd.Kilometers == 0)
                    errors.Add("Km", "Veuillez séléctionner un kilométrage.");
                if (motoAd.Year == 0)
                    errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");
                if (motoAd.EngineSize == 0)
                    errors.Add("EngineSize", "Veuillez sélectionner une cylindrée.");
            }
            return errors;
        }
    }
}
