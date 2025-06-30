using System; //Comando usar pra simplicar. Exemplo: System.Console.WriteLine("Olá, Mundo!");

class Program
{
    static Random random = new Random();

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(@"=/\= MASMORRA SOMBRIA =/\=");
        Console.ResetColor();

        int habilidade = Rolard6() + 6;
        int energia = Rolard6() + Rolard6() + 12;
        int sorte = Rolard6() + 6;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Seus Atributos - Habilidade: {habilidade}, Energia: {energia}, Sorte: {sorte}\n");
        Console.ResetColor();

        var criaturas = new[]
        {
            new Criatura("Lobo Cinzento", 3, 3),
            new Criatura("Lobo Branco", 3, 3),
            new Criatura("Goblin", 5, 4),
            new Criatura("Orc Vesgo", 5, 5),
            new Criatura("Orc Barbudo", 5, 5),
            new Criatura("Zumbi Manco", 6, 7),
            new Criatura("Zumbi Balofo", 6, 7),
            new Criatura("Troll", 8, 7),
            new Criatura("Ogro", 8, 9),
            new Criatura("Ogro Furioso", 10, 9),
            new Criatura("Necromante Maligno", 12, 12)
        };

        foreach (var criatura in criaturas)
        {
            Console.Clear();
            Console.WriteLine($"\nUm {criatura.Nome} Apareceu (Habilidade: {criatura.Habilidade}, Energia: {criatura.Energia})");

            while (energia > 0 && criatura.Energia > 0)
            {
                Console.Clear();

                Console.WriteLine($"\nSeu turno: Energia {energia}, Sorte {sorte}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{criatura.Nome}: Energia {criatura.Energia}");
                Console.ResetColor();

                int ataqueHeroi = habilidade + Rolard6() + Rolard6();
                int ataqueCriatura = criatura.Habilidade + Rolard6() + Rolard6();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Combate: Você {ataqueHeroi} vs {criatura.Nome} {ataqueCriatura}");
                Console.ResetColor();

                if (ataqueHeroi == ataqueCriatura)
                {
                    Console.WriteLine("Você e a Criatura colidem seus golpes (nenhum dano causado).");
                }
                else if (ataqueHeroi > ataqueCriatura)
                {
                    int dano = 2;

                    if (sorte > 0)
                    {
                        string input = PerguntarSimNao("Tentar a sorte? (s/n) ");
                        if (input == "s")
                        {
                            bool sorteOk = (Rolard6() + Rolard6()) <= sorte;
                            sorte--;

                            if (sorteOk)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("Acerto Crítico!");
                                dano = 4;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("A criatura desvia parcialmente. Dano reduzido.");
                                dano = 1;
                            }
                            Console.ResetColor();
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Você acertou! Criatura recebe {dano} de dano!");
                    Console.ResetColor();
                    criatura.Energia -= dano;
                }
                else
                {
                    int dano = 2;

                    if (sorte > 0)
                    {
                        string input = PerguntarSimNao("Tentar a sorte? (s/n) ");
                        if (input == "s")
                        {
                            bool sorteOk = (Rolard6() + Rolard6()) <= sorte;
                            sorte--;

                            if (sorteOk)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Você desvia parcialmente. Dano reduzido.");
                                dano = 1;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Você falha e leva um golpe forte! Dano aumentado.");
                                dano = 3;
                            }
                            Console.ResetColor();
                        }
                    }

                    energia -= dano;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Você recebe {dano} de dano!");
                    Console.ResetColor();
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

            if (energia <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nVocê é finalizado pela Criatura.");
                Console.WriteLine("\n=== GAME OVER ===");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\nVocê conseguiu matar o {criatura.Nome}!");
            Console.ResetColor();
            Console.WriteLine("\nPressione qualquer tecla para seguir para o próximo combate...");
            Console.ReadKey();
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n=== Todos os Monstros foram derrotados, A masmorra foi Concluída! ===");
        Console.ResetColor();
        Console.ReadKey();
    }

    // Função utilitária para validar entrada de "s" ou "n"
    static string PerguntarSimNao(string mensagem)
    {
        string resposta;
        do
        {
            Console.Write(mensagem);
            resposta = Console.ReadLine()?.Trim().ToLower() ?? "n";

            if (resposta != "s" && resposta != "n")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Entrada inválida. Digite apenas 's' para sim ou 'n' para não.");
                Console.ResetColor();
            }

        } while (resposta != "s" && resposta != "n");

        return resposta;
    }

    static int Rolard6() => random.Next(1, 7);
}

class Criatura
{
    public string Nome { get; }
    public int Habilidade { get; }
    public int Energia { get; set; }

    public Criatura(string nome, int habilidade, int energia)
    {
        Nome = nome;
        Habilidade = habilidade;
        Energia = energia;
    }
}