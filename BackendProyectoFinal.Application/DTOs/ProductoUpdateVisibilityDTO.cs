﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ProductoUpdateVisibilityDTO
    {
        public int IdProducto { get; set; }
        public bool NoVisible { get; set; }

    }
}
