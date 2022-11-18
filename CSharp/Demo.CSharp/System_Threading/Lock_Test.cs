using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CSharp.System_Threading
{
    internal class Lock_Test
    {
        /// <summary>
        /// entry point
        /// </summary>
        public static void Run()
        {
            SampleMonitor();
        }

        private static object locker = new object();

        /// <summary>
        /// monitor 'Monitor' locked status
        /// </summary>
        /// <param name="threads"></param>
        static void LockStatusMonitor(params Thread[] threads)
        {
            //monitor lock status
            new Thread(() =>
            {
                while (threads.Any(t => t.ThreadState != ThreadState.Stopped))
                {
                    //1s per .
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
            }).Start();
        }

        /// <summary>
        /// write thread message info second
        /// </summary>
        static void NoMonitor()
        {
            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            LockStatusMonitor(t1, t2);

            void DoTask()
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"\n[{i}] current thread name is {Thread.CurrentThread.Name}");

                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// write thread info per second
        /// </summary>
        static void SampleMonitor()
        {
            var t1 = new Thread(DoTask) { Name = "Thread-1" };
            var t2 = new Thread(DoTask) { Name = "Thread-2" };

            t1.Start();
            t2.Start();

            LockStatusMonitor(t1, t2);

            void DoTask()
            {
                for (int i = 0; i < 10; i++)
                {
                    /*
                     * 这个在编译时会编译为
                     * try
                     * {
                     *      Monitor.Enter(locker)
                     * }
                     * finally
                     * {
                     *      Monitor.Exit(locker)
                     * }
                     * **/
                    lock (locker)
                    {
                        Console.WriteLine($"\n[{i}] current thread name is {Thread.CurrentThread.Name}");

                        Thread.Sleep(1000);
                    }
                }

                /*
                 * DoTask方法会编译为单独方法，生成的IL参考
.method assembly hidebysig static void  '<SampleMonitor>g__DoTask|4_0'() cil managed
{
  .custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  // Code size       144 (0x90)
  .maxstack  2
  .locals init (int32 V_0,
           object V_1,
           bool V_2,
           valuetype [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler V_3,
           bool V_4)
  IL_0000:  nop
  IL_0001:  ldc.i4.0
  IL_0002:  stloc.0
  IL_0003:  br.s       IL_0081
  IL_0005:  nop
  IL_0006:  ldsfld     object Demo.CSharp.System_Threading.Lock_Test::locker
  IL_000b:  stloc.1
  IL_000c:  ldc.i4.0
  IL_000d:  stloc.2
  .try
  {
    IL_000e:  ldloc.1
    IL_000f:  ldloca.s   V_2
    IL_0011:  call       void [System.Threading]System.Threading.Monitor::Enter(object,
                                                                                bool&)
    IL_0016:  nop
    IL_0017:  nop
    IL_0018:  ldc.i4.s   27
    IL_001a:  ldc.i4.2
    IL_001b:  newobj     instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::.ctor(int32,
                                                                                                                               int32)
    IL_0020:  stloc.3
    IL_0021:  ldloca.s   V_3
    IL_0023:  ldstr      "\n["
    IL_0028:  call       instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::AppendLiteral(string)
    IL_002d:  nop
    IL_002e:  ldloca.s   V_3
    IL_0030:  ldloc.0
    IL_0031:  call       instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::AppendFormatted<int32>(!!0)
    IL_0036:  nop
    IL_0037:  ldloca.s   V_3
    IL_0039:  ldstr      "] current thread name is "
    IL_003e:  call       instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::AppendLiteral(string)
    IL_0043:  nop
    IL_0044:  ldloca.s   V_3
    IL_0046:  call       class [System.Threading.Thread]System.Threading.Thread [System.Threading.Thread]System.Threading.Thread::get_CurrentThread()
    IL_004b:  callvirt   instance string [System.Threading.Thread]System.Threading.Thread::get_Name()
    IL_0050:  call       instance void [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::AppendFormatted(string)
    IL_0055:  nop
    IL_0056:  ldloca.s   V_3
    IL_0058:  call       instance string [System.Runtime]System.Runtime.CompilerServices.DefaultInterpolatedStringHandler::ToStringAndClear()
    IL_005d:  call       void [System.Console]System.Console::WriteLine(string)
    IL_0062:  nop
    IL_0063:  ldc.i4     0x3e8
    IL_0068:  call       void [System.Threading.Thread]System.Threading.Thread::Sleep(int32)
    IL_006d:  nop
    IL_006e:  nop
    IL_006f:  leave.s    IL_007c
  }  // end .try
  finally
  {
    IL_0071:  ldloc.2
    IL_0072:  brfalse.s  IL_007b
    IL_0074:  ldloc.1
    IL_0075:  call       void [System.Threading]System.Threading.Monitor::Exit(object)
    IL_007a:  nop
    IL_007b:  endfinally
  }  // end handler
  IL_007c:  nop
  IL_007d:  ldloc.0
  IL_007e:  ldc.i4.1
  IL_007f:  add
  IL_0080:  stloc.0
  IL_0081:  ldloc.0
  IL_0082:  ldc.i4.s   10
  IL_0084:  clt
  IL_0086:  stloc.s    V_4
  IL_0088:  ldloc.s    V_4
  IL_008a:  brtrue     IL_0005
  IL_008f:  ret
} // end of method Lock_Test::'<SampleMonitor>g__DoTask|4_0'
                 */
            }
        }
    }
}
