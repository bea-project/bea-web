﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Domain.Ads;
using System.Text.RegularExpressions;

namespace Bea.Services
{
    public class AdDataConsistencyServices : IAdDataConsistencyServices
    {
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
            if (ad.City==null)
                errors.Add("SelectedCityId", "Veuillez sélectionner une ville.");
            if (ad.Category==null)
                errors.Add("SelectedCategoryId", "Veuillez séléctionner une catégorie.");

            return errors;
        }
    }
}
