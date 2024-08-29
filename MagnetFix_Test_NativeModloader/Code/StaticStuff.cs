using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnetFix_Test_NativeModloader
{
    public static class StaticStuff
    {
        public static bool isInsideSomething(Actor tActor)
        {
            return tActor.is_inside_boat;
        }
    }
}
