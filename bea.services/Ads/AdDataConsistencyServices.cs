using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Domain.Ads;
using System.Text.RegularExpressions;
using Bea.Domain.Ads.WaterSport;
using Bea.Models;
using Bea.Core.Services.Ads;
using Bea.Models.Create;

namespace Bea.Services.Ads
{
    public class AdDataConsistencyServices : IAdDataConsistencyServices
    {
        private Regex _phoneRegex = new Regex(@"^[0-9]{6}$");
        private Regex _emailRegex = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");

        public IDictionary<string, string> GetAdDataConsistencyErrors(AdvancedAdCreateModel model)
        {
            IDictionary<string, string> errors = new Dictionary<string, string>();

            // Check base ad data consistency
            GetBaseAdDataConsistencyErrors(model, errors);

            // Check user data consistency
            GetUserDataConsistencyErrors(model, errors);

            if (model.Type == AdTypeEnum.Undefined)
                return errors;

            // Check specifyc ad data consistency
            switch (model.Type)
            {
                case AdTypeEnum.CarAd:
                    GetCarAdDataConsistencyErrors(model, errors);
                    break;

                case AdTypeEnum.MotoAd:
                    GetMotoAdDataConsistencyErrors(model, errors);
                    break;

                case AdTypeEnum.OtherVehiculeAd:
                    GetOtherVehicleAdDataConsistencyErrors(model, errors);
                    break;

                case AdTypeEnum.VehiculeAd:
                    GetVehicleAdDataConsistencyErrors(model, errors);
                    break;

                case AdTypeEnum.MotorBoatAd:
                    GetMotorBoatAdDataConsistencyErrors(model, errors);
                    break;

                case AdTypeEnum.SailingBoatAd:
                    GetSailingBoatAdDataConsistencyErrors(model, errors);
                    break;

                case AdTypeEnum.MotorBoatEngineAd:
                    GetMotorBoatEngineAdDataConsistencyErrors(model, errors);
                    break;

                case AdTypeEnum.WaterSportAd:
                    GetWaterSportAdDataConsistencyErrors(model, errors);
                    break;

                case AdTypeEnum.RealEstateAd:
                    GetRealEstateAdDataConsistencyErrors(model, errors);
                    break;
                default:
                    break;
            }

            return errors;
        }

        public IDictionary<string, string> GetBaseAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (String.IsNullOrEmpty(model.Title))
                errors.Add("Title", "Veuillez donner un titre à votre annonce.");

            if (String.IsNullOrEmpty(model.Body))
                errors.Add("Body", "Veuillez rédiger un texte d'annonce.");

            if (model.Price < 0)
                errors.Add("Price", "Veuillez saisir un prix positif.");

            if (!String.IsNullOrEmpty(model.Telephone) && !_phoneRegex.IsMatch(model.Telephone))
                errors.Add("Telephone", "Telephone invalide.");

            if (model.SelectedCityId == null)
                errors.Add("SelectedCityId", "Veuillez sélectionner une ville.");

            if (model.SelectedCategoryId == null)
                errors.Add("SelectedCategoryId", "Veuillez séléctionner une catégorie.");

            return errors;
        }

        public IDictionary<string, string> GetWaterSportAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.SelectedWaterSportTypeId == null)
                errors.Add("SelectedWaterSportTypeId", "Veuillez sélectionner une discipline.");

            return errors;
        }

        public IDictionary<string, string> GetMotorBoatEngineAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.SelectedMotorBoatEngineTypeId == null)
                errors.Add("SelectedMotorBoatEngineTypeId", "Veuillez sélectionner un type de moteur.");

            if (model.SelectedYearId == null)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            if (model.Hp == null)
                errors.Add("Hp", "Veuillez saisir une puissance.");

            return errors;
        }

        public IDictionary<string, string> GetRealEstateAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.SelectedRealEstateTypeId == null)
                errors.Add("SelectedRealEstateTypeId", "Veuillez sélectionner un type de bien.");

            if (model.RoomNb == null)
                errors.Add("RoomNb", "Veuillez sélectionner un nombre de pieces.");

            if (model.SurfaceArea == null)
                errors.Add("SurfaceArea", "Veuillez saisir une superficie.");

            if (model.IsFurnished == null)
                errors.Add("IsFurnished", "Veuillez selectionner une option.");

            return errors;
        }

        public IDictionary<string, string> GetSailingBoatAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.SelectedHullTypeId == null)
                errors.Add("SelectedHullTypeId", "Veuillez sélectionner une coque.");

            if (model.SelectedSailingBoatTypeId == null)
                errors.Add("SelectedSailingBoatTypeId", "Veuillez sélectionner un matériaux.");

            if (model.SelectedYearId == null)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            if (model.Length == null)
                errors.Add("Length", "Veuillez saisir une longueur.");

            return errors;
        }

        public IDictionary<string, string> GetMotorBoatAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.SelectedMotorTypeId == null)
                errors.Add("SelectedMotorTypeId", "Veuillez sélectionner un type de moteur.");

            if (model.SelectedMotorBoatTypeId == null)
                errors.Add("SelectedMotorBoatTypeId", "Veuillez sélectionner un type.");

            if (model.SelectedYearId == null)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            if (model.Hp == null)
                errors.Add("Hp", "Veuillez saisir une puissance.");

            if (model.Length == null)
                errors.Add("Length", "Veuillez saisir une longueur.");

            return errors;
        }

        public IDictionary<string, string> GetCarAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.Km==null)
                errors.Add("Km", "Veuillez séléctionner un kilométrage.");

            if (model.SelectedFuelId == null)
                errors.Add("SelectedFuelId", "Veuillez sélectionner un type.");

            if (model.SelectedCarBrandId == null)
                errors.Add("SelectedCarBrandId", "Veuillez sélectionner une marque.");

            if (model.SelectedYearId == null)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            return errors;
        }

        public IDictionary<string, string> GetVehicleAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.Km == null)
                errors.Add("Km", "Veuillez séléctionner un kilométrage.");
            if (model.SelectedYearId == null)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            return errors;
        }

        public IDictionary<string, string> GetOtherVehicleAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.Km == null)
                errors.Add("Km", "Veuillez séléctionner un kilométrage.");

            if (model.SelectedFuelId == null)
                errors.Add("SelectedFuelId", "Veuillez sélectionner un type.");

            if (model.SelectedYearId == null)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            return errors;
        }

        public IDictionary<string, string> GetMotoAdDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (model.Km == null)
                errors.Add("Km", "Veuillez séléctionner un kilométrage.");

            if (model.SelectedYearId == null)
                errors.Add("SelectedYearId", "Veuillez sélectionner une annee-modele.");

            if (model.EngineSize == null)
                errors.Add("EngineSize", "Veuillez sélectionner une cylindrée.");

            if (model.SelectedMotoBrandId == null)
                errors.Add("SelectedMotoBrandId", "Veuillez sélectionner une marque.");

            return errors;
        }

        public IDictionary<string, string> GetUserDataConsistencyErrors(AdvancedAdCreateModel model, IDictionary<string, string> errors)
        {
            if (String.IsNullOrEmpty(model.Name))
                    errors.Add("Name", "Veuillez saisir un nom.");
            if (String.IsNullOrEmpty(model.Email))
                    errors.Add("Email", "Veuillez insérer une adresse email.");
            else
                if (!_emailRegex.IsMatch(model.Email))
                            errors.Add("Email", "Email invalide.");
            return errors;
        }

        public Dictionary<string, string> GetDataConsistencyErrors(ContactUserFormModel model)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(model.Name))
                errors.Add("Name", "Veuillez saisir votre nom.");
            if (String.IsNullOrEmpty(model.Email))
                errors.Add("Email", "Veuillez saisir votre adresse email.");
            else
                if (!String.IsNullOrEmpty(model.Email) && !_emailRegex.IsMatch(model.Email))
                    errors.Add("Email", "Email invalide.");
            if (String.IsNullOrEmpty(model.EmailBody))
                errors.Add("EmailBody", "Veuillez saisir un texte.");
            return errors;
        }

        public Boolean IsEmailValid(String email)
        {
            if (!String.IsNullOrEmpty(email) && _emailRegex.IsMatch(email))
                return true;
            return false;
        }
    }
}
