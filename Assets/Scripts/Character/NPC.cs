using DialogueSystem;
using UnityEngine;

namespace CharacterSystem
{
    public class NPC : Character
    {
        [SerializeField] Dialogue dialogues;
        [SerializeField] private GameObject dialogueBox;

        public override void CharacterMove()
        {

        }


        public void Interact()
        {
            if (dialogueBox != null)
            {
                dialogueBox.SetActive(true); // ativa a caixa }
            }
        }
    }
}