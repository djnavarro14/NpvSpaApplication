﻿using NpvSpaApplication.Service;
using NpvSpaApplication.Models;
using System.Web.Http;

namespace NpvSpaApplication.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IComputeService _computeSevice;
        public ValuesController(IComputeService computeService)
        {
            _computeSevice = computeService;
        }

        public IHttpActionResult Post([FromBody]NpvDataModel model)
        {
            return Ok(_computeSevice.Npv(model));
        }
    }
}
