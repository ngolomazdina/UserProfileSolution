using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Common.Extentions;

namespace UserProfile.Common.Enums
{
    public enum Langs
    {
        [EnumDisplay("PHP")]
        php,

        [EnumDisplay("JavaScript")]
        js,

        [EnumDisplay("C")]
        c,

        [EnumDisplay("C++")]
        c2plus,

        [EnumDisplay("Java")]
        lang4,

        [EnumDisplay("C#")]
        cSh,

        [EnumDisplay("Python")]
        pthn,

        [EnumDisplay("Ruby")]
        ruby
    }
}
