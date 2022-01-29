using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Enumerator
{
    public enum ResponseMessages
    {
        [Description("The price must be greater than or equal to 0")]
        PRICE_LENGTH_INVALID,
        [Description("Please specify a name for the item")]
        ITEM_NAME_INVALID
    }
}
