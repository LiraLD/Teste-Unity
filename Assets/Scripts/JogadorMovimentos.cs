using UnityEngine;
using UnityEngine.InputSystem;

namespace Lira{
    public class JogadorMovimentos : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector2 direcaoMovimento;

        [SerializeField] private LayerMask camadaChao;
        [SerializeField] private float distanciaRaycast = 0.1f;
        private void Awake()
        {
            // Obtém o componente Rigidbody do jogador
            rb = GetComponent<Rigidbody>();
            // Verifica se o Rigidbody foi encontrado
            if (rb == null)
            {
                Debug.LogError("Rigidbody não encontrado no objeto " + gameObject.name);
            }
        }
        public void DefinirMovimento(InputAction.CallbackContext context)
        {
            // Obtém a direção do movimento
            direcaoMovimento = context.ReadValue<Vector2>();
        }

        public void DefinirPulo(InputAction.CallbackContext context)
        {
    // Verifica se o jogador está no chão
            if (Physics.Raycast(transform.position, Vector3.down, distanciaRaycast, camadaChao))
            {
                // Adiciona uma força para pular
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }

        }

        private void FixedUpdate()
        {
            rb.AddForce(new Vector3(direcaoMovimento.x, 0, direcaoMovimento.y) * 10f, ForceMode.Force);
        }

    }


}
