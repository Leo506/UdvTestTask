using UdvTestTask.Models;

namespace UdvTestTask.Abstractions;

public interface IPageService
{
    Task<IList<PostModel>> GetLastPosts(int postCount = 5);
}