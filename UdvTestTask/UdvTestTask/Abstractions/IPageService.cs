using Calabonga.OperationResults;
using UdvTestTask.Models;

namespace UdvTestTask.Abstractions;

public interface IPageService
{
    Task<OperationResult<IList<PostModel>>> GetLastPosts(int postCount = 5);
}