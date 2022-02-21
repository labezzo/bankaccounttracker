namespace BAT.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal static class GuidHelpers
    {
        /// <summary>
        /// Returns the Guid format used for directories: 00000000-0000-0000-0000-000000000000
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        internal static string GetDirectoryFormat(this Guid guid)
        {
            return guid.ToString(Consts.GuidDirectoryFormat);
        }
    }
}
