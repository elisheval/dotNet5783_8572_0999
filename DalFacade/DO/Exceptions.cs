using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public class NoFoundItemExceptions:Exception
{
    public NoFoundItemExceptions(string message):base(message){ }
}

public class ItemAlresdyExsistException:Exception
{
    public ItemAlresdyExsistException(string message):base(message){ }

}
