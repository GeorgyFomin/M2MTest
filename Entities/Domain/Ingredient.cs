﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ProductIngredient>? ProductsIngredients { get; set; }
    }
}
