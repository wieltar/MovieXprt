using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieXprt.Common.Models
{
    public sealed record Show
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public DateOnly? Premiered { get; init; }
        public string Language { get; init; }
        public string Summary { get; init; }
        public ICollection<string> Genres { get; init; }
    }
}
