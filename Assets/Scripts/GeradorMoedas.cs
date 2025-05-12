using Unity.VisualScripting;
using UnityEngine;

namespace Lira.geradores{
    /// <summary>
    /// Classe responsável por gerar moedas em pontos específicos do jogo.
    public class GeradorMoedas : MonoBehaviour
    {
        [SerializeField] private GameObject moedaPrefab;
        [SerializeField] private Vector3[] pontosGeracao;
        [SerializeField] private float tempoGeracao = 2f;
        [SerializeField] private float tempoGeracaoMaximo = 5f;
        [SerializeField] private BoxCollider terrenoCollider;
        [SerializeField] private float alturaMinima = 1.5f;
        [SerializeField] private int quantidadeMoedas = 10;


        void Awake()
        {
            
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            pontosGeracao = CriaPontosGeracao();
            // Inicia a geração de moedas com um intervalo de tempo
            InvokeRepeating(nameof(GeraMoeda), tempoGeracao, Random.Range(tempoGeracao, tempoGeracaoMaximo));  

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        void FixedUpdate()
        {
            
        }

        private async void GeraMoeda()
        {
            // Verifica se o número máximo de moedas foi atingido
            if (GameObject.FindGameObjectsWithTag("Moeda").Length >= quantidadeMoedas)
                return;

            // Gera uma moeda em um ponto aleatório
            int indicePonto = Random.Range(0, pontosGeracao.Length);
            Vector3 pontoGeracao = pontosGeracao[indicePonto];

            // Verifica se o ponto de geração está dentro do terreno
            if (terrenoCollider.bounds.Contains(pontoGeracao))
            {
                Vector3 posicaoMoeda = new Vector3(pontoGeracao.x, pontoGeracao.y + alturaMinima, pontoGeracao.z);
                Instantiate(moedaPrefab, posicaoMoeda, moedaPrefab.transform.rotation);
            }
        }

        private Vector3[] CriaPontosGeracao()
        {
            Vector3[] pontos = new Vector3[quantidadeMoedas];
            // Cria pontos de geração em posições aleatórias dentro do terreno
            for (int i = 0; i < pontos.Length; i++)
            {
                pontos[i] = new Vector3(
                    Random.Range(terrenoCollider.bounds.min.x+1, terrenoCollider.bounds.max.x-1),
                    alturaMinima,
                    Random.Range(terrenoCollider.bounds.min.z+1, terrenoCollider.bounds.max.z-1)
                );
            }
            return pontos;
        }
    }

}