using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MyStockViewApp;
using NUnit.Gui;
using System.Reflection;
using UnitTests.Mocks;

namespace UnitTests
{

    static class UniTestsStartup
    {
        [STAThread]
        static void Main()
        {
            AppEntry.Main(new[] { Assembly.GetExecutingAssembly().Location });
        }
    }

}
