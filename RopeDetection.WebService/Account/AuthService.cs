﻿using Newtonsoft.Json;
using RopeDetection.CommonData.ViewModels.Base;
using RopeDetection.CommonData.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RopeDetection.WebService.Account
{
    public class AuthService
    {
        private static HttpClient _httpClient;

        /// <summary>
        /// Инициализация экземпляра клиента
        /// </summary>
        /// <param name="client"></param>
        public static void InitializeClient(HttpClient client)
        {
            _httpClient = client;
        }


        /// <summary>
        /// Выполнение входа.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<UserShortModel> Login(string username, string userPassword)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "username", username },
                        { "userpassword", userPassword},
                    });

                var password = WebUtility.UrlEncode(userPassword);
                HttpResponseMessage response = await _httpClient.PostAsync($"auth/loginuser?username={username}&userpassword={userPassword}", formContent);
                string s_result;
                using (HttpContent responseContent = response.Content)
                {
                    s_result = await responseContent.ReadAsStringAsync();
                }

                UserShortModel o_data = new UserShortModel();

                switch (response.StatusCode)
                {
                    //case HttpStatusCode.InternalServerError:
                    //    {
                    //        //ErrorResponseObject error = new ErrorResponseObject();
                    //        //o_data.Status = response.StatusCode;
                    //        //o_data.Message = "Внутренняя ошибка сервера 500";
                    //        //return o_data;
                    //    }

                    //case HttpStatusCode.NotFound:
                    //    {
                    //        //ErrorResponseObject error = new ErrorResponseObject();
                    //        //o_data.Status = response.StatusCode;
                    //        //o_data.Message = "Ресурс не найден 404";
                    //        //return o_data;
                    //    }
                    case HttpStatusCode.OK:
                        {
                            o_data = JsonConvert.DeserializeObject<UserShortModel>(s_result);
                            return o_data;
                        }
                    default:
                        {
                            throw new Exception(response.StatusCode.ToString() + " Server Error");
                        }
                }

            }
            catch (Exception ex)
            {
                UserShortModel o_data = new UserShortModel();
                return o_data;
            }
        }

        public static async Task<UserShortModel> LogOut()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"auth/logoutuser");
                string s_result;
                using (HttpContent responseContent = response.Content)
                {
                    s_result = await responseContent.ReadAsStringAsync();
                }

                MessageResponse o_data = new MessageResponse();
                UserShortModel model = new UserShortModel();

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            o_data = JsonConvert.DeserializeObject<MessageResponse>(s_result);
                            model.SuccessInfo = o_data.message;
                            model.Result = CommonData.DefaultEnums.Result.OK;
                            return model;
                        }
                    default:
                        {
                            throw new Exception("Ошибка выхода. Попробуйте снова.");
                        }
                }
            }
            catch (Exception ex)
            {
                UserShortModel o_data = new UserShortModel();
                o_data.Error = ex;
                o_data.Result = CommonData.DefaultEnums.Result.Error;
                return o_data;
            }
        }

        /// <summary>
        /// Вход с использованием токена.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        //public static async Task Login(string token)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await _httpClient.GetAsync($"login?login={token}&password=");
        //        string s_result;
        //        using (HttpContent responseContent = response.Content)
        //        {
        //            s_result = await responseContent.ReadAsStringAsync();
        //        }

        //        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        //        {
        //            ErrorResponseObject error = new ErrorResponseObject();
        //            error = JsonConvert.DeserializeObject<ErrorResponseObject>(s_result);
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        /// <summary>
        /// Регистрация физического лица.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<UserShortModel> Register(UserModel model)
        {
            try
            {
                //status & message
                //var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
                //    {
                //        { "login", model.Login },
                //        { "password", model.Password},
                //        { "email", model.Email},
                //        { "phone", model.Phone},
                //        { "client_type", model.ClientType},
                //        { "client_last_name", model.ClientLastName},
                //        { "client_name", model.ClientName},
                //        { "client_patronymic", model.ClientPatronymic},
                //        { "client_birthday", model.ClientBirthday},
                //        { "client_passport_serie", model.ClientPassportSerie},
                //        { "client_passport_id", model.ClientPassportId},
                //        { "client_passport_code", model.ClientPassportCode}
                //    });

                HttpResponseMessage response = await _httpClient.PostAsync("auth/registeruser", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                string s_result;
                using (HttpContent responseContent = response.Content)
                {
                    s_result = await responseContent.ReadAsStringAsync();
                }

                UserShortModel o_data = new UserShortModel();

                switch (response.StatusCode)
                {
                    //case HttpStatusCode.InternalServerError:
                    //    {
                    //        //ErrorResponseObject error = new ErrorResponseObject();
                    //        //o_data.Status = response.StatusCode;
                    //        //o_data.Message = "Внутренняя ошибка сервера 500";
                    //        //return o_data;
                    //    }

                    //case HttpStatusCode.NotFound:
                    //    {
                    //        //ErrorResponseObject error = new ErrorResponseObject();
                    //        //o_data.Status = response.StatusCode;
                    //        //o_data.Message = "Ресурс не найден 404";
                    //        //return o_data;
                    //    }
                    case HttpStatusCode.OK:
                        {
                            o_data = JsonConvert.DeserializeObject<UserShortModel>(s_result);
                            return o_data;
                        }
                    default:
                        {
                            throw new Exception(response.StatusCode.ToString() + " Server Error");
                        }
                }

            }
            catch (Exception ex)
            {
                UserShortModel o_data = new UserShortModel();
                return o_data;
            }
        }


        /// <summary>
        /// Регистрация юридического лица.
        /// </summary>r
        /// <param name="model"></param>
        /// <returns></returns>
        //public static async Task<ServiceResponseObject<RegisterResponse>> RegisterLegal(RegisterLegalModel model)
        //{
        //    try
        //    {
        //        //status & message
        //        var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
        //            {
        //                { "login", model.Login },
        //                { "password", model.Password},
        //                { "email", model.Email},
        //                { "org_phone", model.OrgPhone},
        //                { "client_type", model.ClientType},
        //                { "client_last_name", model.ClientLastName},
        //                { "client_name", model.ClientName},
        //                { "client_patronymic", model.ClientPatronymic},
        //                { "org_postal_address", model.OrgPostalAddress},
        //                { "org_name", model.OrgName},
        //                { "org_kpp", model.OrgKpp},
        //                { "org_inn", model.OrgInn},
        //                { "org_ogrn", model.OrgOgrn},
        //                { "org_bank", model.OrgBank},
        //                { "org_bank_payment", model.OrgBankpayment},
        //                { "org_bank_correspondent", model.OrgBankCorrespondent},
        //                { "org_bank_bik", model.OrgBankBik},
        //                { "org_legal_address", model.OrgLegalAddress}
        //            });

        //        HttpResponseMessage response = await _httpClient.PostAsync($"client", formContent);

        //        string s_result;
        //        using (HttpContent responseContent = response.Content)
        //        {
        //            s_result = await responseContent.ReadAsStringAsync();
        //        }

        //        ServiceResponseObject<RegisterResponse> o_data = new ServiceResponseObject<RegisterResponse>();

        //        switch (response.StatusCode)
        //        {
        //            case HttpStatusCode.BadRequest:
        //                {
        //                    ErrorResponseObject error = new ErrorResponseObject();
        //                    error = JsonConvert.DeserializeObject<ErrorResponseObject>(s_result);
        //                    o_data.Status = response.StatusCode;
        //                    o_data.Message = error.Errors[0];
        //                    return o_data;
        //                }
        //            case HttpStatusCode.InternalServerError:
        //                {
        //                    ErrorResponseObject error = new ErrorResponseObject();
        //                    o_data.Status = response.StatusCode;
        //                    o_data.Message = "Внутренняя ошибка сервера 500";
        //                    return o_data;
        //                }

        //            case HttpStatusCode.NotFound:
        //                {
        //                    ErrorResponseObject error = new ErrorResponseObject();
        //                    o_data.Status = response.StatusCode;
        //                    o_data.Message = "Ресурс не найден 404";
        //                    return o_data;
        //                }
        //            case HttpStatusCode.OK:
        //                {
        //                    var register = JsonConvert.DeserializeObject<RegisterResponse>(s_result);
        //                    o_data.Message = register.message;
        //                    o_data.Status = response.StatusCode;
        //                    o_data.ResponseData = new RegisterResponse
        //                    {
        //                        message = register.message,
        //                        user = register.user
        //                    };
        //                    return o_data;
        //                }
        //            default:
        //                {
        //                    throw new Exception(response.StatusCode.ToString() + " Server Error");
        //                }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceResponseObject<RegisterResponse> o_data = new ServiceResponseObject<RegisterResponse>();
        //        o_data.Message = ex.Message;
        //        return o_data;
        //    }
        //}

        /// <summary>
        /// Выход.
        /// </summary>
        /// <returns></returns>
        //public static async Task LogOut()
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await _httpClient.GetAsync($"logout");

        //        string s_result;
        //        using (HttpContent responseContent = response.Content)
        //        {
        //            s_result = await responseContent.ReadAsStringAsync();
        //        }


        //        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        //        {
        //            ErrorResponseObject error = new ErrorResponseObject();
        //            error = JsonConvert.DeserializeObject<ErrorResponseObject>(s_result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorResponseObject error = new ErrorResponseObject();
        //        error.Errors[0] = ex.Message;
        //    }
        //}
    }
}
