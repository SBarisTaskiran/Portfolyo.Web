using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities.Infrastructure
{
    public interface IEntityTypeSeed<T> where T : class
    {
        void SeedData(EntityTypeBuilder<T> builder);
    }
}
