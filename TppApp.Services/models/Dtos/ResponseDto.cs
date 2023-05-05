﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TppApp.Services.ProductAPI.models.Dtos
{
    public class ResponseDto
    {
        // Whether the request is successful or not 
        public bool isSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}