using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringTest
{
    class Program
    {
        private static Stopwatch _sw = new Stopwatch();

        static void Main(string[] args)
        {
            string theString = "";
            _sw.Start();
            for (var i = 0; i < 100000000; i++)
            {
                theString = StringContainer.NullString + @"";
            }
            _sw.Stop();
            Console.WriteLine("Verbatim");
            Console.WriteLine($"{_sw.ElapsedMilliseconds}ms");
            Console.WriteLine(theString);

            _sw.Restart();
            for (var i = 0; i < 100000000; i++)
            {
                theString = StringContainer.NullString + "";
            }
            _sw.Stop();
            Console.WriteLine("String");
            Console.WriteLine($"{_sw.ElapsedMilliseconds}ms");
            Console.WriteLine(theString);

            _sw.Restart();
            for (var i = 0; i < 100000000; i++)
            {
                theString = StringContainer.NullString + string.Empty;
            }
            _sw.Stop();
            Console.WriteLine("String Empty");
            Console.WriteLine($"{_sw.ElapsedMilliseconds}ms");
            Console.WriteLine(theString);
            Console.ReadKey();
            
        }

        /// <summary>
        /// MSIL Matches AddString()
        /// </summary>
        /// <returns></returns>
        static string AddVerbatimString()
        {
            //NullString is a static un-initialised string property
            return StringContainer.NullString + @"";
        }

        /*
            .method private hidebysig static string  AddString() cil managed
            {
              // Code size       20 (0x14)
              .maxstack  2
              .locals init ([0] string V_0)
              IL_0000:  nop
              IL_0001:  call       string StringTest.StringContainer::get_NullString()
              IL_0006:  dup                           //duplicates the value on the top of the stack
              IL_0007:  brtrue.s   IL_000f     //if the value on the top of the stack is non null - branch to this offset
              IL_0009:  pop                          //pop the value at the top of the stack
              IL_000a:  ldstr      ""                //allocates memory and pushes a new string (as expected :D)
              IL_000f:  stloc.0                      //store the value at the top of the stack into index 0
              IL_0010:  br.s       IL_0012     //unconditionally branch to the offset
              IL_0012:  ldloc.0                    //loads the local variable at index 0 to the top of the stack
              IL_0013:  ret                          //pop from our stack push to the callers
            } // end of method Program::AddString
        */
        static string AddString()
        {
            //NullString is a static un-initialised string property
            return StringContainer.NullString + "";
        }

        /*
            .method private hidebysig static string  AddStringEmpty() cil managed
            {
              // Code size       21 (0x15)
              .maxstack  2
              .locals init ([0] string V_0)
              IL_0000:  nop
              IL_0001:  call       string StringTest.StringContainer::get_NullString()
              IL_0006:  ldsfld     string [mscorlib]System.String::Empty                       //pushes the string on to the stack
              IL_000b:  call       string [mscorlib]System.String::Concat(string,
                                                                          string)
              IL_0010:  stloc.0                                                                                      //store the value at the top of the stack into index 0
              IL_0011:  br.s       IL_0013                                                                      //unconditionally branch to the offset
              IL_0013:  ldloc.0                                                                                     //loads the local variable at index 0 to the top of the stack
              IL_0014:  ret                                                                                           //pop from our stack push to the callers
            } // end of method Program::AddStringEmpty
        */
        static string AddStringEmpty()
        {
            return StringContainer.NullString + string.Empty;
        }
    }
}
