using UnityEngine;
using RhythmSystem;
using AudioSystem;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UISystem
{
    public class RhythmInterface : MonoBehaviour
    {
        [Header("Characters Interface")]
        [SerializeField] List<CharacterInterface> characterInterfaces;
        Dictionary<MUSIC, CharacterInterface> characterDictionary;

        [Header("UI Settings")]
        [SerializeField] Image background;
        [SerializeField] Image track;
        [SerializeField] Image blur;

        [Header("Character Left Settings")]
        [SerializeField] Image characterLeft;
        [SerializeField] Image nameCharacterLeft;
        [SerializeField] Image backgroundCharacterLeft;

        [Header("Character Right Settings")]
        [SerializeField] Image characterRight;
        [SerializeField] Image nameCharacterRight;
        [SerializeField] Image backgroundCharacterRight;

        [Header("Progress Bar Settings")]
        [SerializeField] Image iconProgressBar;
        [SerializeField] Image backgroundProgressBar;
        [SerializeField] Image fillProgressBar;

        Lane[] lanes;

        private void Awake()
        {
            // inicializando o dicionario
            characterDictionary = new Dictionary<MUSIC, CharacterInterface>();
            foreach (var character in characterInterfaces) { characterDictionary[character.musicType] = character; }
        }

        private void Start()
        {
            MUSIC musicID = RhythmManager.instance.musicID;
            lanes = RhythmManager.instance.lanes;

            // atualizando a interface com base no genero musical
            if (characterDictionary.TryGetValue(musicID, out CharacterInterface character))
            {
                background.sprite = character.background;
                track.sprite = character.track;
                blur.sprite = character.blur;

                characterLeft.sprite = character.characterLeft;
                nameCharacterLeft.sprite = character.nameCharacterLeft;
                backgroundCharacterLeft.sprite = character.backgroundCharacterLeft;

                characterRight.sprite = character.characterRight;
                nameCharacterRight.sprite = character.nameCharacterRight;
                backgroundCharacterRight.sprite = character.backgroundCharacterRight;

                // iconProgressBar.sprite = character.iconProgressBar;
                backgroundProgressBar.sprite = character.backgroundProgressBar;
                fillProgressBar.sprite = character.fillProgressBar;

                foreach (Lane lane in lanes)
                {
                    KeyCode input = lane.input;
                    Arrow arrow = lane.GetComponentInChildren<Arrow>();
                    switch (input)
                    {
                        case KeyCode.UpArrow:
                            lane.notePrefab = character.prefabUpArrow;
                            arrow.image = character.buttonUpArrow;
                            break;
                        case KeyCode.DownArrow:
                            lane.notePrefab = character.prefabDownArrow;
                            arrow.image = character.buttonDownArrow;
                            break;
                        case KeyCode.LeftArrow:
                            lane.notePrefab = character.prefabLeftArrow;
                            arrow.image = character.buttonLeftArrow;
                            break;
                        case KeyCode.RightArrow:
                            lane.notePrefab = character.prefabRightArrow;
                            arrow.image = character.buttonRightArrow;
                            break;
                    }
                }
            }
        }

    }
}