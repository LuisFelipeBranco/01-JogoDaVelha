using System.Xml;

namespace _01_Jogo_da_velha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cont = 0, opc = 0, final, jogIA;
            char[,] campo = new char[3, 3];
            bool vez = true;//Controla quem esta na vez de jogar;

            mostrar(campo);

            jogIA = contajogIA();//
            iniciarTabuleiro(campo);

            while (jogIA >= 2)
            {
                Console.ReadLine();
                Console.Clear();
                mostrar(campo);
                jogIA = contajogIA();//
            }

            if (jogIA == 0)
            {
                while (opc == 0)
                {
                    vez = jogador(vez, campo);
                    cont++;
                    if (cont >= 4)
                    {
                        final = vencedor(campo, jogIA);//0-empate;1-vencedor;2-Segue o jogo;
                        if (final == 1)
                        {
                            Console.WriteLine("Deseja reiniciar o jogo: (0-Sim; 1-Não)");
                            opc = int.Parse(Console.ReadLine());
                            iniciarTabuleiro(campo);
                            mostrar(campo);
                        }
                        else if (final == 0)
                        {
                            Console.WriteLine("Jogo deu empate!!");
                            Console.WriteLine("Deseja reiniciar o jogo: (0-Sim; 1-Não)");
                            opc = int.Parse(Console.ReadLine());
                            iniciarTabuleiro(campo);
                            mostrar(campo);
                        }
                    }//a partir da 3 vez, já pode ter um vencedor;
                }
            }
            else
            {
                iniciarTabuleiro(campo);
                mostrar(campo);
                gameIA(campo);
            }
        }
        //Jogo no modo IA;
        public static void gameIA(char[,] campo)
        {
            int linha, coluna, jogovazio = 0;
            char mark = 'X';
            Random rand = new Random();

            Console.WriteLine("Informe as coordenadas que deseja jogar: ");
            Console.WriteLine("Linha: ");
            linha = int.Parse(Console.ReadLine());
            while (linha > 3 || linha == 0)
            {
                Console.WriteLine("Linha digitada não pode ser maior que 3 ou igual a 0, digite novamente uma coordenada: ");
                linha = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Coluna: ");
            coluna = int.Parse(Console.ReadLine());
            if (coluna > 3 || coluna == 0)
            {
                Console.WriteLine("Coluna digitada não pode ser maior que 3 ou igual a 0, digite novamente uma coordenada: ");
                coluna = int.Parse(Console.ReadLine());
            }

            if (campo[(linha - 1), (coluna - 1)] != ' ') //verifica se for preencher um campo que já esteja preenchido;
            {
                Console.WriteLine("Atenção, não pode preencher uma coordenada já preenchida!!");
                Console.WriteLine("Informe novamente as coordenadas que deseja jogar: ");
                Console.WriteLine("Linha: ");
                linha = int.Parse(Console.ReadLine());
                while (linha > 3 || linha == 0)
                {
                    Console.WriteLine("Linha digitada não pode ser maior que 3 ou igual a 0, digite novamente uma coordenada: ");
                    linha = int.Parse(Console.ReadLine());
                }

                Console.WriteLine("Coluna: ");
                coluna = int.Parse(Console.ReadLine());
                if (coluna > 3 || coluna == 0)
                {
                    Console.WriteLine("Coluna digitada não pode ser maior que 3 ou igual a 0, digite novamente uma coordenada: ");
                    coluna = int.Parse(Console.ReadLine());
                }
            }
            else { campo[linha, coluna] = mark; }

            for (int i = 0; i < campo.GetLength(0); i++)
            {
                for (int j = 0; j < campo.GetLength(1); j++)
                {
                    if (campo[i, j] == ' ')
                    {//achou um campo vazio, a IA pode jogar;
                        jogovazio = 1;
                        break;
                    }
                }
            }
            if (jogovazio == 1)
            {
                linha = rand.Next(1, 4);
                coluna = rand.Next(1, 4);
                while (campo[linha, coluna] != ' ')
                {
                    linha = rand.Next(1, 4);
                    coluna = rand.Next(1, 4);
                }//Ia jogou 
            }
            else
            {
                Console.WriteLine("Jogo deu empate!!");
                return;
            }

        }

        public static int vencedor(char[,] campo, int opcJogo)
        {//checar possibilidades de vitoria;
            int i, j, linha = campo.GetLength(0), coluna = campo.GetLength(1);
            string player = " ";

            //checa as colunas
            for (i = 0; i < coluna; i++)
            {
                if (campo[i, 0] == campo[i, 1] && campo[i, 1] == campo[i, 2] && campo[i, 0] != ' ')
                {
                    if (campo[i, 0] == 'X') { player = "Player 1"; }
                    else { player = "Player 2"; }
                    Console.WriteLine($"Vencedor na linha {i + 1}, jogador: {player}");
                    return 1;
                }
            }
            //checa as linhas;
            for (j = 0; j < linha; j++)
            {
                if (campo[0, j] == campo[1, j] && campo[1, j] == campo[2, j] && campo[0, j] != ' ')
                {
                    if (campo[0, j] == 'X') { player = "Player 1"; }
                    else { player = "Player 2"; }
                    Console.WriteLine($"Vencedor na coluna {j + 1}, jogador: {player}");
                    return 1;
                }
            }

            if (campo[0, 0] == campo[1, 1] && campo[1, 1] == campo[2, 2] && campo[0, 0] != ' ') //diagonal
            {
                if (campo[0, 0] == 'X') { player = "Player 1"; }
                else { player = "Player 2"; }
                Console.WriteLine($"Vencedor na diagonal principal, jogador: {player}");
                return 1;
            }
            if (campo[2, 0] == campo[1, 1] && campo[1, 1] == campo[0, 2] && campo[2, 0] != ' ') //diagonal2
            {
                if (campo[2, 0] == 'X') { player = "Player 1"; }
                else { player = "Player 2"; }
                Console.WriteLine($"Vencedor na diagonal principal inversa, jogador: {player}");
                return 1;
            }
            //0 - empate; 1 - vencedor; 2 - Segue o jogo;
            for (i = 0; i < campo.GetLength(0); i++)
            {
                for (j = 0; j < campo.GetLength(1); j++)
                {
                    if (campo[i, j] == ' ')
                    { //ainda pode jogar
                        return 2;
                    }
                }
            }
            return 0;
        }

        public static void mostrar(char[,] campo)
        {
            Console.Clear();

            for (int i = 0; i < campo.GetLength(0); i++)
            {
                Console.WriteLine($"{campo[i, 0]} - {campo[i, 1]} - {campo[i, 2]}");
                if (i != 2)
                {
                    Console.WriteLine("---------");
                }
            }

        }

        public static bool jogador(bool player, char[,] campo)
        {
            int linha, coluna;
            char mark;

            if (player == true) { Console.WriteLine("Player 1"); mark = 'X'; }
            else { Console.WriteLine("Player 2"); mark = 'O'; }

            Console.WriteLine("Informe as coordenadas que deseja jogar: ");
            Console.WriteLine("Linha: ");
            linha = int.Parse(Console.ReadLine());
            while (linha > 3 || linha == 0)
            {
                Console.WriteLine("Linha digitada não pode ser maior que 3 ou igual a 0, digite novamente uma coordenada: ");
                linha = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Coluna: ");
            coluna = int.Parse(Console.ReadLine());
            if (coluna > 3 || coluna == 0)
            {
                Console.WriteLine("Coluna digitada não pode ser maior que 3 ou igual a 0, digite novamente uma coordenada: ");
                coluna = int.Parse(Console.ReadLine());
            }
            if (campo[(linha - 1), (coluna - 1)] != ' ')
            { //verifica se for preencher um campo que já esteja preenchido;
                Console.WriteLine("Atenção, não pode preencher uma coordenada já preenchida!!");
                Console.WriteLine("Informe novamente as coordenadas que deseja jogar: ");
                Console.WriteLine("Linha: ");
                linha = int.Parse(Console.ReadLine());
                while (linha > 3 || linha == 0)
                {
                    Console.WriteLine("Linha digitada não pode ser maior que 3 ou igual a 0, digite novamente uma coordenada: ");
                    linha = int.Parse(Console.ReadLine());
                }

                Console.WriteLine("Coluna: ");
                coluna = int.Parse(Console.ReadLine());
                if (coluna > 3 || coluna == 0)
                {
                    Console.WriteLine("Coluna digitada não pode ser maior que 3 ou igual a 0, digite novamente uma coordenada: ");
                    coluna = int.Parse(Console.ReadLine());
                }
            }
            campo[(linha - 1), (coluna - 1)] = mark;
            if (player == true)
            {
                player = false;
            }
            else
            {
                player = true;
            }
            mostrar(campo);
            return player;
        }

        public static void iniciarTabuleiro(char[,] campo)
        {
            for (int i = 0; i < campo.GetLength(0); i++)
            {
                for (int j = 0; j < campo.GetLength(1); j++)
                {
                    campo[i, j] = ' ';
                }
            }
        }
        public static int contajogIA()
        {
            int opc;
            Console.WriteLine("Deseja jogar conta outro jogador ou IA: ");
            Console.WriteLine("0-jogador; 1-IA");
            opc = int.Parse(Console.ReadLine());
            if (opc == 0)
            {
                return 0;
            }
            else if (opc == 1)
            {
                return 1;
            }
            else
            {
                Console.WriteLine("Opção inválida, favor inserir uma opção válida");
                return 2;
            }
        }

    }
}