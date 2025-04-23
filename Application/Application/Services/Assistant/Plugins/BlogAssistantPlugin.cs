using Infrastructure.Dtos.Twitter;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Application.Services.Assistant.Plugins
{
    public class BlogAssistantPlugin
    {
        [KernelFunction,Description("Gives example blog about given topic.")]
        public string GenerateExampleBlog([Description("Blog Topic")] string topic)
        {
            return $"Preparing example blog about {topic} ...";  
        }
        [KernelFunction,Description("Analyzes the blog post and gives feedback.")]
        public string AnalyzeBlog([Description("Given blog from user")] string blogContent)
        {
            return $"Analyzing blog content and giving feedbacks ...";
        }
        [KernelFunction, Description("Evaluates the blog's writing style.")]
        public string AnalyzeStyle([Description("Blog Content")] string content)
        {
            return $"I'm analyzing blog writing style ...";
        }

        [KernelFunction, Description("Suggests SEO tags based on content.")]
        public string SuggestSeoTags([Description("Blog içeriği")] string content)
        {
            return $"I recommend SEO tags that are appropriate for the content...";
        }
    }
}
