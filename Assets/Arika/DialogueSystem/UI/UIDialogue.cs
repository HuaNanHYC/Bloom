﻿using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DialogueSystem
{
    public sealed class UIDialogue : MonoBehaviour
    {
        [Header("Actor Name")] [SerializeField]
        private TextMeshProUGUI tmpFirstActorName;

        [SerializeField] private TextMeshProUGUI tmpSecondActorName;
        [SerializeField] private TextMeshProUGUI tmpThirdActorName;

        [Header("Actor Image")] [SerializeField]
        private Image imgFirstActor;

        [SerializeField] private Image imgSecondActor;
        [SerializeField] private Image imgThirdActor;


        [Header("Text")] [SerializeField] private TextMeshProUGUI tmpDialogueText;
        [Header("Button")] [SerializeField] private Button btnNext;
        [SerializeField] private Button btnQuit;

        public bool IsActive => DialogueManager.IsInDialogue;

        private DialogueManager DialogueManager => DialogueManager.Instance;

        private void Awake()
        {
            if (btnNext)
                btnNext.onClick.AddListener(() => { DialogueManager.Next(); });
            if (btnQuit)
                btnQuit.onClick.AddListener(() => DialogueManager.QuitDialogue());
        }

        private void Start()
        {
            DialogueManager.OnConversationStarted += ConversationStarted;
            DialogueManager.OnConversationUpdated += ConversationUpdated;
            DialogueManager.OnConversationEnded += ConversationEnded;
        }


        private void ConversationStarted()
        {
            gameObject.SetActive(true);
        }

        private void ConversationUpdated()
        {
            RefreshPanel();
        }

        private void ConversationEnded()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            RefreshPanel();
        }

        private void OnDestroy()
        {
            if (DialogueManager)
                DialogueManager.OnConversationUpdated -= RefreshPanel;
            if (btnNext) btnNext.onClick.RemoveAllListeners();
            if (btnQuit) btnQuit.onClick.RemoveAllListeners();
        }

        private void RefreshPanel()
        {
            if (!DialogueManager)
                return;
            transform.gameObject.SetActive(DialogueManager.IsInDialogue);
            if (!DialogueManager.IsInDialogue) return;
            RefreshDialogue();
        }

        private void RefreshDialogue()
        {
            tmpDialogueText.text = DialogueManager.CurrentNodeText;
            // TODO: Add support for multiple actors
        }
    }
}