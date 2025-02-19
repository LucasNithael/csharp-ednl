using System;
using System.Collections.Generic;
using System.IO;

class Algoritmos
{
    private int[,] matrizLabirinto;
    private List<(int, int)> saidas;
    private (int, int) partida;

    public Algoritmos(int[,] matrizLabirinto)
    {
        this.matrizLabirinto = matrizLabirinto;

        int linhasLabirinto = matrizLabirinto.GetLength(0);
        int colunasLabirinto = matrizLabirinto.GetLength(1);
        
        // Encontra as saídas, onde o valor é 3
        saidas = new List<(int, int)>();
        for (int i = 0; i < linhasLabirinto; i++)
        {
            for (int j = 0; j < colunasLabirinto; j++)
            {
                if (matrizLabirinto[i, j] == 3)
                {
                    saidas.Add((i, j));
                }
            }
        }

        // Encontra a partida, onde o valor é 2
        for (int i = 0; i < linhasLabirinto; i++)
        {
            for (int j = 0; j < colunasLabirinto; j++)
            {
                if (matrizLabirinto[i, j] == 2)
                {
                    partida = (i, j);
                }
            }
        }
    }

    public void MostrarMatriz()
    {
        for (int i = 0; i < matrizLabirinto.GetLength(0); i++)
        {
            for (int j = 0; j < matrizLabirinto.GetLength(1); j++)
            {
                Console.Write(matrizLabirinto[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void MostrarSaidas()
    {
        foreach (var saida in saidas)
        {
            Console.WriteLine($"({saida.Item1}, {saida.Item2})");
        }
    }

    public void MostrarPartida()
    {
        Console.WriteLine($"({partida.Item1}, {partida.Item2})");
    }

    public List<(int, int)> Dijkstra()
    {
        int linhas = matrizLabirinto.GetLength(0);
        int colunas = matrizLabirinto.GetLength(1);

        // Inicializa as distâncias com infinito
        int[,] distancias = new int[linhas, colunas];
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                distancias[i, j] = int.MaxValue;
            }
        }

        // A distância do ponto de partida para ele mesmo é 0
        distancias[partida.Item1, partida.Item2] = 0;

        // Fila de prioridade para os vértices a serem visitados
        var fila = new PriorityQueue<(int, int), int>();
        fila.Enqueue(partida, 0);

        // Direções possíveis: cima, baixo, esquerda, direita
        var direcoes = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        // Enquanto houver vértices para visitar
        while (fila.Count > 0)
        {
            var (x, y) = fila.Dequeue();

            // Se chegamos a uma saída, retornamos o caminho
            if (matrizLabirinto[x, y] == 3)
            {
                return ReconstruirCaminho1(distancias, (x, y));
            }

            // Explora os vizinhos
            foreach (var (dx, dy) in direcoes)
            {
                int nx = x + dx;
                int ny = y + dy;

                // Verifica se o vizinho está dentro dos limites do labirinto e não é uma parede
                if (nx >= 0 && nx < linhas && ny >= 0 && ny < colunas && matrizLabirinto[nx, ny] != 1)
                {
                    int novaDistancia = distancias[x, y] + 1;

                    // Se encontramos um caminho mais curto
                    if (novaDistancia < distancias[nx, ny])
                    {
                        distancias[nx, ny] = novaDistancia;
                        fila.Enqueue((nx, ny), novaDistancia);
                    }
                }
            }
        }

        // Se não encontramos uma saída
        return new List<(int, int)>();
    }

    private List<(int, int)> ReconstruirCaminho1(int[,] distancias, (int, int) saida)
    {
        var caminho = new List<(int, int)>();
        var (x, y) = saida;

        // Adiciona a saída ao caminho
        caminho.Add((x, y));

        // Direções possíveis: cima, baixo, esquerda, direita
        var direcoes = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        // Reconstrói o caminho até o ponto de partida
        while (distancias[x, y] != 0)
        {
            foreach (var (dx, dy) in direcoes)
            {
                int nx = x + dx;
                int ny = y + dy;

                if (nx >= 0 && nx < distancias.GetLength(0) && ny >= 0 && ny < distancias.GetLength(1) && distancias[nx, ny] == distancias[x, y] - 1)
                {
                    caminho.Add((nx, ny));
                    x = nx;
                    y = ny;
                    break;
                }
            }
        }

        // Inverte o caminho para começar do ponto de partida
        caminho.Reverse();
        return caminho;
    }

    private double HeuristicaEuclidiana((int, int) a, (int, int) b)
    {
        int dx = a.Item1 - b.Item1;
        int dy = a.Item2 - b.Item2;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public List<(int, int)> Aestrela()
    {
        int linhas = matrizLabirinto.GetLength(0);
        int colunas = matrizLabirinto.GetLength(1);

        // Inicializa as distâncias com infinito
        double[,] distancias = new double[linhas, colunas];
        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                distancias[i, j] = double.MaxValue;
            }
        }

        // A distância do ponto de partida para ele mesmo é 0
        distancias[partida.Item1, partida.Item2] = 0;

        // Fila de prioridade para os vértices a serem visitados
        var fila = new PriorityQueue<(int, int), double>();
        fila.Enqueue(partida, HeuristicaEuclidiana(partida, saidas[0]));

        // Direções possíveis: cima, baixo, esquerda, direita, diagonais
        var direcoes = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1), (-1, -1), (-1, 1), (1, -1), (1, 1) };

        // Dicionário para armazenar o caminho
        var caminho = new Dictionary<(int, int), (int, int)>();

        // Variável para armazenar a melhor saída encontrada
        (int, int) melhorSaida = (-1, -1);
        double menorDistancia = double.MaxValue;

        // Enquanto houver vértices para visitar
        while (fila.Count > 0)
        {
            var (x, y) = fila.Dequeue();

            // Se chegamos a uma saída, verificamos se é a melhor
            if (matrizLabirinto[x, y] == 3)
            {
                double distanciaAtual = distancias[x, y];
                if (distanciaAtual < menorDistancia)
                {
                    melhorSaida = (x, y);
                    menorDistancia = distanciaAtual;
                }
                continue; // Continua procurando outras saídas
            }

            // Explora os vizinhos
            foreach (var (dx, dy) in direcoes)
            {
                int nx = x + dx;
                int ny = y + dy;

                // Verifica se o vizinho está dentro dos limites do labirinto e não é uma parede
                if (nx >= 0 && nx < linhas && ny >= 0 && ny < colunas && matrizLabirinto[nx, ny] != 1)
                {
                    double novaDistancia = distancias[x, y] + 1;

                    // Se encontramos um caminho mais curto
                    if (novaDistancia < distancias[nx, ny])
                    {
                        distancias[nx, ny] = novaDistancia;

                        // Calcula a heurística para a saída mais próxima
                        double heuristica = double.MaxValue;
                        foreach (var saida in saidas)
                        {
                            double h = HeuristicaEuclidiana((nx, ny), saida);
                            if (h < heuristica)
                            {
                                heuristica = h;
                            }
                        }

                        fila.Enqueue((nx, ny), novaDistancia + heuristica);

                        // Atualiza o caminho
                        caminho[(nx, ny)] = (x, y);
                    }
                }
            }
        }

        // Se encontramos uma saída, retorna o caminho até ela
        if (melhorSaida != (-1, -1))
        {
            return ReconstruirCaminho2(caminho, melhorSaida);
        }

        // Se não encontramos nenhuma saída
        return new List<(int, int)>();
    }

    private List<(int, int)> ReconstruirCaminho2(Dictionary<(int, int), (int, int)> caminho, (int, int) saida)
    {
        var caminhoFinal = new List<(int, int)>();
        var (x, y) = saida;

        // Reconstrói o caminho até o ponto de partida
        while (caminho.ContainsKey((x, y)))
        {
            caminhoFinal.Add((x, y));
            (x, y) = caminho[(x, y)];
        }

        // Adiciona o ponto de partida
        caminhoFinal.Add((x, y));

        // Inverte o caminho para começar do ponto de partida
        caminhoFinal.Reverse();
        return caminhoFinal;
    }
}