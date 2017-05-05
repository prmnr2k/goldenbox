using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLDENBOX.Abstract
{
    interface IModel
    {
        void DoCallback(object sender, Guid[] e);
    }
}
