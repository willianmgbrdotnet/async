using System;
using System.IO;
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


            Console.ReadKey();

        }
                
        static void Redimensionar()
        {
            #region "Diretórios"
            var diretorioEntrada = "ArquivosEntrada";
            var diretorioRedimensionados = "ArquivosRedimensionados";
            var diretorioFinalizados = "ArquivosFinalizados";

            //As pastas serão criadas caso não existam quando o método for executado.
            if (!Directory.Exists(diretorioEntrada))
            {
                Directory.CreateDirectory(diretorioEntrada);
            }
            if (!Directory.Exists(diretorioRedimensionados))
            {
                Directory.CreateDirectory(diretorioRedimensionados);
            }
            if (!Directory.Exists(diretorioFinalizados))
            {
                Directory.CreateDirectory(diretorioFinalizados);
            }
            #endregion            

            /*while(true)
            {

                //Com o TimeSpan não é preciso calcular os milissegundos.
                //Nesse formato temos (horas, minutos, segundos)                
                Thread.Sleep(new TimeSpan(0, 0, 3));
            }*/
        }


    }
}
