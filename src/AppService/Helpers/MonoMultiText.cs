using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.AppService.Helpers
{
    public static class MonoMultiText
    { 
        public static string GenerateText<T>(List<T> list, string MultiText, string MonoText)
        {
            if (list.Count > 0 && MultiText != null && MonoText != null)
            {
                if (list.Count > 1)
                    return MultiText;
                return MonoText;
                
            }
            return null;

        }
    }
}
