using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform jogadorTransform; // Referência ao transform do jogador
    [SerializeField] private Vector3 offset; // Offset da câmera em relação ao jogador
    [SerializeField] private float suavidade = 0.1f; // Suavidade do movimento da câmera
    [SerializeField] private LayerMask camadaObstaculos; // Camada para detectar obstáculos
    [SerializeField] private float raioColisao = 0.5f; // Raio da esfera para detectar colisões
    //[SerializeField] private float ajusteCamera = 10f; // Distância máxima da câmera ao jogador
    private Vector3 velocity = Vector3.zero; // Velocidade atual da câmera
    private RaycastHit hit = new RaycastHit(); // Variável para armazenar informações do Raycast
    private void FixedUpdate()
    {
        // Calcula a posição alvo da câmera com base no offset
        Vector3 targetPosition = jogadorTransform.position + offset;

        // Direção do jogador para a posição alvo
        //Vector3 direction = (targetPosition - jogadorTransform.position).normalized;

        // Distância máxima da câmera ao jogador
        float maxDistance = offset.magnitude;

        // Realiza um Cast para verificar obstáculos
        if (Physics.Linecast(jogadorTransform.position, transform.position, out hit ))
        {
            // Ajusta a posição da câmera para o ponto de impacto, recuando um pouco para evitar sobreposição
            targetPosition = hit.point +transform.forward  * raioColisao;
            // Move a câmera suavemente em direção à posição alvo ajustada
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, suavidade*2); // Recuo baseado no raio da esfera
        }
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, suavidade);

        // Faz a câmera olhar para o jogador
        transform.LookAt(jogadorTransform);
    }
}