using Calabonga.OperationResults;
using UdvTestTask.Abstractions;
using UdvTestTask.Models;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;

namespace UdvTestTask.Services;

public class PageService : IPageService
{
    private readonly IVkApi _vkApi;

    public PageService(IVkApi vkApi)
    {
        _vkApi = vkApi;
    }

    public async Task<OperationResult<IList<PostModel>>> GetLastPosts(int postCount = 5)
    {
        var result = OperationResult.CreateResult<IList<PostModel>>();

        try
        {
            var posts = await _vkApi.Wall.GetAsync(new WallGetParams()
            {
                Count = 5
            });

            result.Result = posts.WallPosts.Select(post => new PostModel() { Content = post.Text }).ToList();
        }
        catch (Exception e)
        {
            result.AddError(e);
        }

        return result;
    }
}