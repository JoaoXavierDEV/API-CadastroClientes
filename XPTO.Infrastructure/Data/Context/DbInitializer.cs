//using XPTO.Infrastructure.Data.Context;
//using HeavyApps.Blog.Domain.Model;

//namespace HeavyApps.Blog.Infrastructure.Data.Context;

//public static class DbInitializer
//{

//    public static readonly List<Autor> Autores = new List<Autor>
//        {
//            new Autor { Nome = "João", Email = "joao@outlook.com", Biografia = "biografia", Posts = new List<Post>
//                {
//                    new Post
//                    {
//                        Titulo = "Primeiro Post",
//                        Corpo = "Conteúdo do primeiro post",
//                        DataUltimaAtualizacao = DateTime.Now,
//                        DataPublicacao = DateTime.Now,
//                        Status = Domain.Enum.StatusPostEnum.Publicado

//                    },
//                    new Post
//                    {
//                        Titulo = "Segundo Post",
//                        Corpo = "Conteúdo do segundo post",
//                        DataUltimaAtualizacao = DateTime.Now.AddDays(-5),
//                        DataPublicacao = DateTime.Now.AddDays(-80),
//                        Status = Domain.Enum.StatusPostEnum.Programado
//                    }
//                }},
//            new Autor { Nome = "Alexandre", Email = "", Biografia = "" },
//            new Autor { Nome = "Caio", Email = "caio@outlook.com" , Biografia = ""},
//        };

//    public static readonly List<Post> Posts = new List<Post>
//        {
//            new Post
//            {
//                Titulo = "Terceiro Post",
//                Corpo = "Conteúdo do terceiro post",

//                DataUltimaAtualizacao = DateTime.Now.AddDays(-10),
//                DataPublicacao = DateTime.Now.AddDays(-20),

//            },
//            new Post
//            {
//                Titulo = "Quarto Post",
//                Corpo = "Conteúdo do quarto post",
//                DataUltimaAtualizacao = DateTime.Now.AddDays(-15),
//                DataPublicacao = DateTime.Now.AddDays(-30),

//            }
//        };

//    public static void Initialize(ApplicationDbContext context)
//    {

//    }
//}
