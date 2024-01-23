﻿using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text.Json.Serialization;

namespace Codeblaze.SemanticKernel.Connectors.Ollama.ChatCompletion
{
    internal class OllamaChatResponseMessageContent
    {
        /// <summary>
        /// The role of the author of the message either "system", "user", or "assistant".
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;

        /// <summary>
        /// The message content
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null!;

    }
    internal class OllamaChatResponseMessage : OllamaResponseMessage
    {
        /// <summary>
        /// The message generated by the chat model.
        /// </summary>
        [JsonPropertyName("message")]
        public OllamaChatResponseMessageContent Message { get; set; } = null!;

        internal ChatMessageContent ToChatMessageContent()
        {
            var metadata = new Dictionary<string, object>() {
                    { "total_duration", TotalDuration },
                    { "load_duration", LoadDuration },
                    { "prompt_eval_count", PromptEvalCount },
                    { "prompt_eval_duration", PromptEvalDuration },
                    { "eval_count", EvalCount },
                    { "eval_duration", EvalDuration}
                };

            return new ChatMessageContent(
                role: new AuthorRole(Message.Role),
                content: Message.Content,
                modelId: Model,
                metadata: metadata!);
        }

        internal StreamingChatMessageContent ToStreamingChatMessageContent()
        {
            var metadata = new Dictionary<string, object>() {
                    { "total_duration", TotalDuration },
                    { "load_duration", LoadDuration },
                    { "prompt_eval_count", PromptEvalCount },
                    { "prompt_eval_duration", PromptEvalDuration },
                    { "eval_count", EvalCount },
                    { "eval_duration", EvalDuration}
                };

            return new StreamingChatMessageContent(
                role: new AuthorRole(Message.Role),
                content: Message.Content,
                modelId: Model,
                metadata: metadata!);
        }
    }
}
