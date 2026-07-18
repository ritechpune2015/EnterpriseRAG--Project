using EnterpriseRAG.Application.Retrieval.DTO;
using System.Text;

namespace EnterpriseRAG.Application.Chat.Services
{
    public class PromptBuilder
    {
    public string BuildPrompt(string question, List<SearchResultDto> chunks)
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

    }
}
