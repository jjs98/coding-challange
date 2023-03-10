using System.Threading.Tasks;

namespace ArticleStore.Services.Interfaces
{
    public interface IUpdateService
    {
        public Article[] Articles { get; set; }
    }
}
