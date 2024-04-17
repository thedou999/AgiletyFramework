using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace test
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddTransient<Son>(x => new Son());
            services.AddTransient<Father>(x => new Father(new Son()).CreateFather(3));


            using (ServiceProvider provider = services.BuildServiceProvider())
            {

                var father = provider.GetService<Father>();
                Console.WriteLine(Father.age);
                father.sonSaid();

            }
        }
    }

    public class Father
    {
        public static int age = 0;
        private Son son;


        public Father(Son Son)
        {
            son = Son;
        }

        public Father CreateFather(int Age)
        {
            age = Age;
            return this;
        } 
        public void sonSaid()
        {
            son.said();
        }


    }
    public class Son
    {
        private int age = 1;
        public void said()
        {
            Console.WriteLine(age);
        }

    }
}
