using System.Collections.Generic;
using System.Linq;

namespace Ione.Framework.Core
{
    public static class EnumSet
    {
        public static ISet<TEnum> Range<TEnum>(TEnum from, TEnum to)
            where TEnum : EnumPolymorphic<int, TEnum>, new()
        {
            return new HashSet<TEnum>(
                EnumPolymorphic<int, TEnum>
                    .GetValues()
                    .Where(e => e.Ordinal >= from.Ordinal && e.Ordinal <= to.Ordinal));
        }
    }
}
