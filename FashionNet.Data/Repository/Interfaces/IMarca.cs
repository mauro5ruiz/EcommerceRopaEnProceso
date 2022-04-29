﻿using FashionNet.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionNet.Data.Repository.Interfaces
{
    public interface IMarca : IRepository<Marca>
    {
        IEnumerable<SelectListItem> GetListaMarcas();
        void Update(Marca marca);
    }
}
