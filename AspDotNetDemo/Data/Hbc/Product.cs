﻿using Clio.Demo.Core.Component.Master.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clio.Demo.Domain.Data.Entity
{
    [Table("[Hbc].[dbo].[Customer]")]
    public sealed class Product : EntityMaster
    {
        [Column("Id")]    public int     Id    { get; set; }
        [Column("Name")]  public string  Name  { get; set; }
        [Column("Price")] public decimal Price { get; set; }
    }
}