using System;
using System.Drawing;
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

            //A criação dos objetos foi feita fora do loop "while" para ele serem instanciados apenas uma vez.
            FileStream fileStr;
            FileInfo fileInf;
            while(true)
            {
                //Nova altura da imagem em pixels
                int tamanhoAltura = 200;
                
                //lendo ArquivosEntrada
                var arquivosEntrada = Directory.EnumerateFiles(diretorioEntrada);

                foreach (var arquivo in arquivosEntrada)
                {
                    fileStr = new FileStream(arquivo, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fileInf = new FileInfo(arquivo);

                    //Diretório Local(pasta atual)\Pasta de Destino\Nome do Arquivo Original
                    string caminho = Environment.CurrentDirectory + @"\" + diretorioRedimensionados + @"\" + fileInf.Name;

                    //Redimensionando
                    Redimensionador(Image.FromStream(fileStr), tamanhoAltura, caminho);

                    //FECHA o arquivo para liberar a memória
                    fileStr.Close();

                    //Mover os arquivos originais para a pasta Finalizados
                    string caminhoFinalizados = Environment.CurrentDirectory + @"\" + diretorioFinalizados + @"\" + fileInf.Name;
                    fileInf.MoveTo(caminhoFinalizados);
                }

                //Com o TimeSpan não é preciso calcular os milissegundos.
                //Nesse formato temos (horas, minutos, segundos)                
                Thread.Sleep(new TimeSpan(0, 0, 5));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagem">Imagem Original a ser redimensionada</param>
        /// <param name="altura">Altura que desejamos redimensionar</param>
        /// <param name="caminho">caminho aonde será salva a nova imagem redimencionada</param>
        /// <returns></returns>
        static void Redimensionador(Image imagem, int altura, string caminho)
        {
            //(double) converte explicitamente os valores para double
            double ratio = (double)altura / imagem.Height;
            int novaLargura = (int)(imagem.Width * ratio);
            int novaAltura = (int)(imagem.Height * ratio);

            Bitmap novaImage = new Bitmap(novaLargura, novaAltura);

            //Desenhando a imagem com os novos parametros
            using(Graphics grafico = Graphics.FromImage(novaImage))
            {
                grafico.DrawImage(imagem, 0, 0, novaLargura, novaAltura);
            }
            
            novaImage.Save(caminho);

            //liberando toda a memória usada no processo de criação da nova imagem
            imagem.Dispose();
        }
    }
}
