using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public class NoFoundItemExceptions:Exception
{
    public NoFoundItemExceptions(string message):base(message){ }
}

public class ItemAlresdyExsistException:Exception
{
    public ItemAlresdyExsistException(string message):base(message){ }

}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

