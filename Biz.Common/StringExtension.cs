using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Biz.Common
{
    public static class StringExtension
    {
        public static Stream ToStream(this string v)
            => new MemoryStream(Encoding.UTF8.GetBytes(v));
    }
}
