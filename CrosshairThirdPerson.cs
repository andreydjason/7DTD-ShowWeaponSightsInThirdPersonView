using UnityEngine;

namespace CrosshairThirdPerson
{
    public class CrosshairThirdPersonMod : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            GameObject go = new GameObject("CrosshairThirdPersonGO");
            go.AddComponent<CrosshairThirdPersonBehaviour>();
            UnityEngine.Object.DontDestroyOnLoad(go);
            Debug.Log("[CrosshairThirdPerson] Mod carregado.");
        }
    }

    public class CrosshairThirdPersonBehaviour : MonoBehaviour
    {
        void OnGUI()
        {
            try
            {
                EntityPlayerLocal lp = GameManager.Instance?.World?.GetPrimaryPlayer() as EntityPlayerLocal;
                if (lp == null) return;

                // Só em terceira pessoa
                if (lp.bFirstPersonView) return;

                // Só quando mirando
                if (!lp.AimingGun) return;

                // Pega o ItemClass direto, sem conversão de ID
                ItemClass ic = lp.inventory?.holdingItem;
                if (ic == null) return;

                string nome = ic.Name;
                if (string.IsNullOrEmpty(nome)) return;

                // Exclui arcos e bestas
                if (nome.Contains("Bow") || nome.Contains("Crossbow")) return;

                // Só armas de fogo
                if (!nome.StartsWith("gun")) return;

                // Desenha crosshair no centro da tela
                float cx = Screen.width * 0.5f;
                float cy = Screen.height * 0.5f;
                float tam = 12f;
                float esp = 4f;

                GUI.color = new Color(1f, 1f, 1f, 0.8f);
                GUI.DrawTexture(new Rect(cx - tam - esp, cy - 1f, tam, 2f), Texture2D.whiteTexture);
                GUI.DrawTexture(new Rect(cx + esp, cy - 1f, tam, 2f), Texture2D.whiteTexture);
                GUI.DrawTexture(new Rect(cx - 1f, cy - tam - esp, 2f, tam), Texture2D.whiteTexture);
                GUI.DrawTexture(new Rect(cx - 1f, cy + esp, 2f, tam), Texture2D.whiteTexture);
                GUI.color = Color.white;
            }
            catch (System.Exception e)
            {
                Debug.LogError("[CrosshairThirdPerson] Erro: " + e.Message);
            }
        }
    }
}