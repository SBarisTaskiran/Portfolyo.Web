using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public interface IHasEnabled
    {
        bool Enabled { get; set; }
    }
}
