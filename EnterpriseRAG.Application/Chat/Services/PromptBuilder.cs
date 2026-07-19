using EnterpriseRAG.Application.Chat.DTO;
using EnterpriseRAG.Application.Chat.Interfaces;
using EnterpriseRAG.Application.Retrieval.DTO;
using System.Text;

namespace EnterpriseRAG.Application.Chat.Services
{
    public class PromptBuilder:IPromptBuilder
    {
    public string BuildPrompt(string question, List<SearchResultDTO> chunks)
        {
            var sb = new StringBuilder();

            sb.AppendLine(""" You are an AI Assistant. Answer ONLY from the supplied context. If the answer is unavailable, reply "I don't know." """);
    
             sb.AppendLine("Context");
            foreach (var chunk in chunks)
            {
                sb.AppendLine("----------------");
                sb.AppendLine(chunk.Content);
            }
            sb.AppendLine();
            sb.AppendLine("Question");
            sb.AppendLine(question);
            sb.AppendLine();
            sb.AppendLine("Answer");
            return sb.ToString();
        }

        public string BuildPrompt(PromptRequest request)
        {
            var builder = new StringBuilder();

            builder.AppendLine(""" You are an AI Assistant. Answer ONLY from the supplied context. If the answer is unavailable, reply "I don't know." """);

            builder.AppendLine();
            builder.AppendLine();
           foreach (var message in request.History)
            {
                builder.AppendLine(
                    $"{message.Role}: {message.Message}");
            }


            builder.AppendLine("Context");
            foreach (var chunk in request.RetrievedChunks)
            {
                builder.AppendLine("----------------");
                builder.AppendLine(chunk.Content);
            }
            builder.AppendLine();
            builder.AppendLine("Question");
            builder.AppendLine(request.Question);
            builder.AppendLine();
            builder.AppendLine("Answer");
            return builder.ToString();
        }
    }
}
