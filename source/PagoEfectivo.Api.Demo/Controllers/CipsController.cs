﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PagoEfectivo.Api.Demo.Models.Entity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PagoEfectivo.Api.Demo.Controllers
{
    public class CipsController : Controller
    {
        // GET: /<controller>/

        public IActionResult Index(CipViewModel model)
        {
            if (model.Authenticate == null && model.Data == null)
                return View();
            else
            {
                var _url = UrlConfig();

                var date = Convert.ToDateTime(DateTime.Now);
                var DateNow = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, new TimeSpan(-5, 0, 0));

                Authenticate authorization = new Authenticate()
                {
                    accessKey = model.Authenticate.AccessKey,
                    idService = model.Authenticate.IdService,
                    dateRequest = DateNow.ToString("yyyy-MM-ddTHH:mm:sszzz")
                };

                var authRequest = authorization.idService.ToString() + "." + authorization.accessKey + "." + model.Authenticate.SecretKey + "." + authorization.dateRequest;

                authorization.hashString = Hash.HashString(authRequest);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    string stringData = JsonConvert.SerializeObject(authorization);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("v1/authorizations", contentData).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                        ViewBag.status = response.StatusCode;
                        ViewBag.response = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                        ViewBag.status = response.StatusCode;
                        ViewBag.response = response.Content.ReadAsStringAsync().Result;
                    }
                }

                return View();
            }
        }

        [Route("Cips/GenerarCip")]
        public IActionResult GenerarCip(CipViewModel model)
        {
            if (model.Authenticate == null && model.Data == null)
                return View();
            else
            {
                var responseAuthz = AuthenticatePostAsync(model.Authenticate);

                Type type = responseAuthz.GetType();

                if (type.Name == "AuthenticateResponse")
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(responseAuthz.url);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuthz.Data.Token);

                        var cip = new CipGenerate
                        {
                            Currency = model.Currency,
                            Amount = model.Amount,
                            TransactionCode =model.TransactionCode,
                            UserEmail = model.UserEmail,
                            AdminEmail = model.AdminEmail,
                            DateExpiry = model.DateExpiry,
                            PaymentConcept = model.PaymentConcept,
                            AdditionalData = model.AdditionalData,
                            UserName = model.UserName,
                            UserLastName = model.UserLastName,
                            UserUbigeo = model.UserUbigeo,
                            UserCountry = model.UserCountry,
                            UserDocumentType = model.UserDocumentType,
                            UserDocumentNumber = model.UserDocumentNumber,
                            UserPhone = model.UserPhone,
                            UserCodeCountry = model.UserCodeCountry
                        };

                        string stringData = JsonConvert.SerializeObject(cip);
                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("v1/cips", contentData).Result;
                        
     

                        if (response.IsSuccessStatusCode)
                        {
                            var UrlCip = JsonConvert.DeserializeObject<GenerateResponse>(response.Content.ReadAsStringAsync().Result);
                            ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                            ViewBag.status = response.StatusCode;
                            ViewBag.response = response.Content.ReadAsStringAsync().Result;
                            ViewBag.Url = UrlCip.Data.CipUrl;
                        }
                        else
                        {
                            ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                            ViewBag.status = response.StatusCode;
                            ViewBag.response = response.Content.ReadAsStringAsync().Result;
                        }
                    }

                    return View();
                }
                else
                {
                    ViewBag.code = StatusCode((int)responseAuthz.StatusCode).StatusCode;
                    ViewBag.status = responseAuthz.StatusCode;
                    ViewBag.response = responseAuthz.Content.ReadAsStringAsync().Result;                    
                    return View();
                }
            }
        }

        [Route("Cips/ConsultarCip")]
        public IActionResult ConsultarCip(CipViewModel model)
        {
            if (model.Authenticate == null && model.Data == null)
                return View();
            else
            {
                var responseAuthz = AuthenticatePostAsync(model.Authenticate);

                Type type = responseAuthz.GetType();

                if (type.Name == "AuthenticateResponse")
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(responseAuthz.url);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuthz.Data.Token);

                        var cip = new CipSearch
                        {
                            Data = model.Data
                        };

                        string stringData = JsonConvert.SerializeObject(cip);
                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("v1/cips/search", contentData).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                            ViewBag.status = response.StatusCode;
                            ViewBag.response = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                            ViewBag.status = response.StatusCode;
                            ViewBag.response = response.Content.ReadAsStringAsync().Result;
                        }
                    }

                    return View();
                }
                else
                {
                    ViewBag.code = StatusCode((int)responseAuthz.StatusCode).StatusCode;
                    ViewBag.status = responseAuthz.StatusCode;
                    ViewBag.response = responseAuthz.Content.ReadAsStringAsync().Result;
                    return View();
                }
            }
        }

        [Route("Cips/ActualizarCip")]
        public IActionResult ActualizarCip(CipViewModel model)
        {
            if (model.Authenticate == null && model.Data == null)
                return View();
            else
            {
                var responseAuthz = AuthenticatePostAsync(model.Authenticate);

                Type type = responseAuthz.GetType();

                if (type.Name == "AuthenticateResponse")
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(responseAuthz.url);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuthz.Data.Token);

                        var cip= new CipUpdate
                        {
                            DateExpiry = model.DateExpiry
                        };

                        string stringData = JsonConvert.SerializeObject(cip);
                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                        var request = new HttpRequestMessage(new HttpMethod("PATCH"), responseAuthz.url + "v1/cips/" + model.Data[0].Cip) { Content = contentData };
                        HttpResponseMessage response = client.SendAsync(request).Result;
                        
                        if (response.IsSuccessStatusCode)
                        {
                            ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                            ViewBag.status = response.StatusCode;
                            ViewBag.response = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                            ViewBag.status = response.StatusCode;
                            ViewBag.response = response.Content.ReadAsStringAsync().Result;
                        }

                    }

                    return View();
                }
                else
                {
                    ViewBag.code = StatusCode((int)responseAuthz.StatusCode).StatusCode;
                    ViewBag.status = responseAuthz.StatusCode;
                    ViewBag.response = responseAuthz.Content.ReadAsStringAsync().Result;
                    return View();
                }
            }
        }

        [Route("Cips/EliminarCip")]
        public IActionResult EliminarCip(CipViewModel model)
        {
            if (model.Authenticate == null && model.Data == null)
                return View();
            else
            {
                var responseAuthz = AuthenticatePostAsync(model.Authenticate);

                Type type = responseAuthz.GetType();
                                
                if (type.Name == "AuthenticateResponse")
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(responseAuthz.url);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuthz.Data.Token);
                                                
                        HttpResponseMessage response = client.DeleteAsync("v1/cips/"+ model.Data[0].Cip).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                            ViewBag.status = response.StatusCode;
                            ViewBag.response = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            ViewBag.code = StatusCode((int)response.StatusCode).StatusCode;
                            ViewBag.status = response.StatusCode;
                            ViewBag.response = response.Content.ReadAsStringAsync().Result;
                        }
                    }

                    return View();
                }
                else
                {
                    ViewBag.code = StatusCode((int)responseAuthz.StatusCode).StatusCode;
                    ViewBag.status = responseAuthz.StatusCode;
                    ViewBag.response = responseAuthz.Content.ReadAsStringAsync().Result;
                    return View();
                }
            }
        }

        public dynamic AuthenticatePostAsync(AuthenticateViewModel authenticateViewModel)
        {
            try
            {
                var _url = UrlConfig();

                var date = Convert.ToDateTime(DateTime.Now);
                var DateNow = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, new TimeSpan(-5, 0, 0));

                Authenticate authorization = new Authenticate()
                {
                    accessKey = authenticateViewModel.AccessKey,
                    idService = authenticateViewModel.IdService,
                    dateRequest = DateNow.ToString("yyyy-MM-ddTHH:mm:sszzz")
                };

                var authRequest = authorization.idService.ToString() + "." + authorization.accessKey + "." + authenticateViewModel.SecretKey + "." + authorization.dateRequest;

                authorization.hashString = Hash.HashString(authRequest);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    string stringData = JsonConvert.SerializeObject(authorization);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("v1/authorizations", contentData).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic responseAuth = JsonConvert.DeserializeObject<AuthenticateResponse>(response.Content.ReadAsStringAsync().Result);
                        responseAuth.url = _url;
                        return responseAuth;
                    }
                    else
                        return response;
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }

        }

        public string UrlConfig()
        {
            var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false)
                    .Build();

            return configBuilder["Url"];
        }

    }
}
