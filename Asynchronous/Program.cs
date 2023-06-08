using System;
using System.Threading;

namespace Asynchronous
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando o Redimensionador!");

            //Note que aqui o "Redimensionar" está sem parenteses.
            //A thread está apenas apontando para o método que estará sob seu efeito,
            //não está chamando o método.
            Thread thread = new Thread(Redimensionar);
            thread.Start();


            //Redimensionar();

            System.Console.WriteLine("Tecle para fechar");
            Console.ReadKey();

        }
        
        static void Redimensionar()
        {
            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine(i);

                //Com o TimeSpan não é preciso calcular os milissegundos.
                //Nesse formato temos (horas, minutos, segundos)                
                Thread.Sleep(new TimeSpan(0, 0, 3));
            }
        }


    }
}
