using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Digite os números sorteados (6 números entre 1 e 60, separados por espaço):");
        var entradaSorteio = Console.ReadLine();

        var numerosSorteados = ParseNumeros(entradaSorteio);

        if (numerosSorteados.Count != 6)
        {
            Console.WriteLine("Erro: o sorteio deve conter exatamente 6 números válidos.");
            return;
        }

        string caminhoApostas = @"C:\apostas.csv";

        if (!File.Exists(caminhoApostas))
        {
            Console.WriteLine("Arquivo de apostas não encontrado em C:\\apostas.csv");
            return;
        }

        var linhas = File.ReadAllLines(caminhoApostas);
        var resultadoFinal = new StringBuilder();

        bool ganhouSena = false;
        bool ganhouQuadra = false;
        bool ganhouQuina = false;

        int linhaAtual = 1;

        Console.WriteLine("\n===== RESULTADO DAS APOSTAS =====\n");
        resultadoFinal.AppendLine("===== RESULTADO DAS APOSTAS =====\n");

        foreach (var linha in linhas)
        {
            var limpa = linha.Trim();

            if (string.IsNullOrWhiteSpace(limpa))
            {
                linhaAtual++;
                continue;
            }

            var aposta = ParseNumeros(limpa);

            // Agora aceita apostas com 6 ou mais números
            if (aposta.Count < 6 || aposta.Any(n => n < 1 || n > 60))
            {
                linhaAtual++;
                continue;
            }

            var acertos = aposta
                .Intersect(numerosSorteados)
                .OrderBy(n => n)
                .ToList();

            string tipoPremio = ObterTipoPremio(acertos.Count);

            Console.WriteLine($"Aposta linha {linhaAtual}:");
            resultadoFinal.AppendLine($"Aposta linha {linhaAtual}:");

            Console.WriteLine($"Números da aposta ({aposta.Count}): {Formatar(aposta)}");
            resultadoFinal.AppendLine($"Números da aposta ({aposta.Count}): {Formatar(aposta)}");

            Console.WriteLine($"Acertos ({acertos.Count}): {Formatar(acertos)}");
            resultadoFinal.AppendLine($"Acertos ({acertos.Count}): {Formatar(acertos)}");

            Console.WriteLine($"Resultado: {tipoPremio}\n");
            resultadoFinal.AppendLine($"Resultado: {tipoPremio}\n");

            if (acertos.Count == 6)
                ganhouSena = true;
            else if (acertos.Count == 4)
                ganhouQuadra = true;
            else if (acertos.Count == 5)
                ganhouQuina = true;

            linhaAtual++;
        }

        Console.WriteLine("=================================");

        if (ganhouSena)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PARABÉNS! VOCÊ GANHOU A MEGA DA VIRADA!");
            resultadoFinal.AppendLine("PARABÉNS! VOCÊ GANHOU A MEGA DA VIRADA!");
        }
        else if (ganhouQuadra)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PARABÉNS! Você acertou pelo menos uma QUADRA!");
            resultadoFinal.AppendLine("PARABÉNS! Você acertou pelo menos uma QUADRA!");
        }
        else if (ganhouQuina)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PARABÉNS! Você acertou pelo menos uma QUINA!");
            resultadoFinal.AppendLine("PARABÉNS! Você acertou pelo menos uma QUINA!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nenhuma aposta foi premiada.");
            resultadoFinal.AppendLine("Nenhuma aposta foi premiada.");
        }

        Console.ResetColor();

        string downloadsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads"
        );

        string caminhoResultado = Path.Combine(downloadsPath, "resultado_mega.txt");

        File.WriteAllText(caminhoResultado, resultadoFinal.ToString());

        Console.WriteLine($"\nResultado exportado para: {caminhoResultado}");
        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    #region MÉTODOS AUXILIARES 
    static List<int> ParseNumeros(string entrada)
    {
        return entrada
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .Distinct()
            .OrderBy(n => n)
            .ToList();
    }

    static string ObterTipoPremio(int acertos)
    {
        return acertos switch
        {
            >= 6 => "SENA",
            5 => "QUINA",
            4 => "QUADRA",
            _ => "Não premiada"
        };
    }

    static string Formatar(List<int> numeros)
    {
        if (!numeros.Any())
            return "Nenhum";

        return string.Join(" ", numeros.Select(n => n.ToString("D2")));
    }
    #endregion
}
