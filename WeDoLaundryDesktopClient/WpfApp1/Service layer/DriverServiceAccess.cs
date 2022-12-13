﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ServiceAccess
{
    public class DriverServiceAccess
    {
        static readonly string restUrl = "https://localhost:7091/api/drivers";
        readonly HttpClient _httpClient;
        public HttpStatusCode CurrentHttpStatusCode { get; set; }

        public  DriverServiceAccess()
        {
            _httpClient = new();
        }

        public async Task<List<Driver>?> GetDriversAsync(int id = -1)
        {
            List<Driver> driverList = null;

            // /api/drivers/{id}
            string useRestUrl = restUrl;
            bool hasValidId = (id > 0);
            if (hasValidId)
            {
                useRestUrl += id;
            }
            var uri = new Uri(string.Format(useRestUrl));

            // Perform and evaluate the request
            try
            {
                var response = await _httpClient.GetAsync(uri);
                CurrentHttpStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (hasValidId)
                    {
                        Driver foundDriver = JsonConvert.DeserializeObject<Driver>(content);
                        if (foundDriver != null)
                        {
                            driverList = new List<Driver>() { foundDriver };
                        }
                    }
                    else
                    {
                        driverList = JsonConvert.DeserializeObject<List<Driver>>(content);
                    }
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        driverList = new List<Driver>();
                    }
                    else
                    {
                        driverList = null;
                    }
                }
            }
            catch
            {
                driverList = null;
            }
            return driverList;
        }

    }
}

