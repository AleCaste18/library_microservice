using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksManager.Entity
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
