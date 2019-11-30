using Branch.Models;
using Branch.Models.NoSQL;
using Branch.SearchAuxiliars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Branch.Auxiliars
{
    public static class GraphAuxiliar
    {
        public static dynamic CalculateSubjectSimilarity(int SubjectId, SQLContext SQLContext)
        {
            var UserSubject = SQLContext.UserSubjects
                                                     .Where(x => x.SubjectId == SubjectId)
                                                     .Select(x => x.UserId)
                                                     .ToList();

            var FollowsFollowSubjects = SQLContext.UserSubjects
                                                               .Where(x => UserSubject.Contains(x.UserId) && x.SubjectId != SubjectId)
                                                               .Select(x => x.Subject)
                                                               .ToList();

            var Result = FollowsFollowSubjects
                                              .Select(x => new { Topic = x, Similarity = (double)FollowsFollowSubjects.Count(y => y == x) / SQLContext.UserSubjects.Where(y => y.SubjectId == x.Id).Count() })
                                              .ToList();

            Result = Result
                           .DistinctBy(x => x.Topic.Id)
                           .ToList();

            return Result;
        }

        public static void IncreaseFollowAffinity(int FollowerId, int FollowedId, SQLContext SQLContext)
        {
            var Follow = GetFollowById(FollowerId, FollowedId, SQLContext);

            if (Follow == default)
            {
                return;
            }

            DBAuxiliar.IncreaseAffinityOnFollow(FollowerId, FollowedId);
        }

        public static void IncreaseFollowAffinity(int FollowerId, List<int> Followeds, SQLContext SQLContext)
        {
            foreach (var FollowedId in Followeds)
            {
                IncreaseFollowAffinity(FollowerId, FollowedId, SQLContext);
            }
        }

        public static void IncreaseFollowAffinityAsync(int FollowerId, int FollowedId, SQLContext SQLContext)
        {
            var Function = new Task(() => IncreaseTopicAffinity(FollowerId, FollowedId, SQLContext));
            Function.Start();
        }

        public static void IncreaseFollowAffinityAsync(int FollowerId, List<int> Followeds, SQLContext SQLContext)
        {
            var Function = new Task(() => IncreaseFollowAffinity(FollowerId, Followeds, SQLContext));
            Function.Start();
        }

        public static void IncreaseTopicAffinity(int UserId, int TopicId, SQLContext SQLContext)
        {
            var UserSubject = GetUserSubjectById(UserId, TopicId, SQLContext);

            if (UserSubject == default)
            {
                return;
            }

            DBAuxiliar.IncreaseAffinityOnSubject(UserId, TopicId);
        }

        public static void IncreaseTopicAffinity(int UserId, List<int> Topics, SQLContext SQLContext)
        {
            foreach (var TopicId in Topics)
            {
                IncreaseTopicAffinity(UserId, TopicId, SQLContext);
            }
        }

        public static void IncreaseTopicAffinityAsync(int UserId, int TopicId, SQLContext SQLContext)
        {
            var Function = new Task(() => IncreaseTopicAffinity(UserId, TopicId, SQLContext));
            Function.Start();
        }

        public static void IncreaseTopicAffinityAsync(int UserId, List<int> Topics, SQLContext SQLContext)
        {
            var Function = new Task(() => IncreaseTopicAffinity(UserId, Topics, SQLContext));
            Function.Start();
        }

        public static List<Post> OrderPostsByAffinity(int UserId, List<Post> Posts, SQLContext SQLContext)
        {
            var AffinityPosts = CreateAffinityPosts(UserId, Posts, SQLContext);

            return AffinityPosts
                                .OrderByDescending(AffinityPost => (double)AffinityPost.TopicAffinity)
                                .ThenByDescending(AffinityPost => (double)AffinityPost.UserAffinity)
                                .Select(AffinityPost => (Post)AffinityPost.Post)
                                .ToList();
        }

        private static List<dynamic> CreateAffinityPosts(int UserId, List<Post> Posts, SQLContext SQLContext)
        {
            var AffinityPosts = new List<dynamic>();
            var Tasks = new List<Task>();

            foreach (var Post in Posts)
            {
                var NewTask = Task.Run
                (
                    () =>
                    {
                        var AffinityPost = CreateAffinityPost(UserId, Post, SQLContext);
                        AffinityPosts.Add(AffinityPost.First());
                    }
                );

                Tasks.Add(NewTask);
            }

            Task.WaitAll(Tasks.ToArray());

            return AffinityPosts;
        }

        private static List<dynamic> CreateAffinityPost(int UserId, Post Post, SQLContext SQLContext)
        {
            double UserAffinity = 0;
            double TopicAffinity = 0;

            Follow Follow;

            lock (SQLContext)
            {
                Follow = GetFollowById(UserId, Post.UserId, SQLContext);
            }

            UserAffinity += Follow != default ? Follow.Affinity : 0;

            foreach (var Mention in Post.Mentions)
            {
                lock (SQLContext)
                {
                    Follow = GetFollowById(UserId, Mention, SQLContext);
                }

                UserAffinity += Follow != default ? Follow.Affinity / Post.Mentions.Count : 0;
            }

            foreach (var Topic in Post.Hashtags)
            {
                UserSubject UserSubject;

                lock (SQLContext)
                {
                    UserSubject = GetUserSubjectById(UserId, Topic, SQLContext);
                }

                TopicAffinity += UserSubject != default ? UserSubject.Affinity / Post.Hashtags.Count : 0;
            }

            var AffinityPost = new { Post, UserAffinity, TopicAffinity };

            return new List<dynamic> { AffinityPost };
        }

        public static List<Product> OrderProductsByAffinity(int UserId, List<Product> Products, SQLContext SQLContext)
        {
            var ProductRecommendation = CreateAffinityProducts(UserId, Products, SQLContext);

            return ProductRecommendation
                                        .OrderByDescending(x => (double)x.SubjectAffinity)
                                        .ThenByDescending(x => (double)x.FollowsAffinity)
                                        .ThenByDescending(x => (double)x.OwnerAffinity)
                                        .Select(x => (Product)x.Product)
                                        .ToList();
        }

        //TA MT FEIO HORROROSO DESCULPA
        private static List<dynamic> CreateAffinityProducts(int UserId, List<Product> Products, SQLContext SQLContext)
        {
            var ProductRecommendation = new List<dynamic>();
            var TaskArray = new List<Task>();

            foreach (var Product in Products)
            {
                var NewTask = Task.Run
                (
                  () =>
                  {
                      double OwnerAffinity = 0;
                      double SubjectAffinity = 0;
                      double FollowsAffinity = 0;

                      var OwnerId = Product.ProId;

                      IEnumerable<int> SubjectsFollowedByOwner;
                      IEnumerable<int> UsersFollowedByOwner;
                      IEnumerable<int> OwnerFollowers;

                      lock (SQLContext)
                      {
                          SubjectsFollowedByOwner = UserAuxiliar.FollowedSubjects((int)OwnerId, SQLContext).Select(x => x.Id);
                          UsersFollowedByOwner = UserAuxiliar.Follows((int)OwnerId, SQLContext).Select(x => x.Id);
                          OwnerFollowers = UserAuxiliar.Followers((int)OwnerId, SQLContext).Select(x => x.Id);
                      }

                      var Follows = UsersFollowedByOwner.Union(OwnerFollowers);

                      Follow OwnerFollow;

                      lock (SQLContext)
                      {
                          OwnerFollow = SQLContext.Follows.FirstOrDefault(x => x.FollowerId == UserId && x.FollowedId == OwnerId);
                      }

                      if (OwnerFollow != default)
                      {
                          OwnerAffinity += OwnerFollow.Affinity;
                      }

                      int RawSubjectAffinity = 0;

                      lock (SQLContext)
                      {
                          var SameSubjects = SQLContext.UserSubjects.Where(x => x.UserId == UserId && SubjectsFollowedByOwner.Contains(x.SubjectId));
                          if (SameSubjects.Any())
                          {
                              RawSubjectAffinity = SameSubjects.Sum(x => x.Affinity);
                          }
                      }

                      if (SubjectsFollowedByOwner.Any())
                      {
                          SubjectAffinity = RawSubjectAffinity / SubjectsFollowedByOwner.Count();
                      }

                      IQueryable<Follow> EqualFollows;
                      lock (SQLContext)
                      {
                          EqualFollows = SQLContext.Follows.Where(x => Follows.Contains(x.FollowedId) && x.FollowerId == UserId);
                          if (EqualFollows.Any())
                          {
                              FollowsAffinity = EqualFollows.Sum(x => x.Affinity) / EqualFollows.Count();
                          }
                      }

                      if (OwnerAffinity != 0 || FollowsAffinity != 0 || SubjectAffinity != 0)
                      {
                          ProductRecommendation.Add(new { Product, OwnerAffinity, SubjectAffinity, FollowsAffinity });
                      }

                  }
                );

                TaskArray.Add(NewTask);
            }

            Task.WaitAll(TaskArray.ToArray());

            return ProductRecommendation;
        }

        public static UserSubject GetUserSubjectById(int UserId, int TopicId, SQLContext SQLContext)
        {
            var UserSubject = SQLContext.UserSubjects.FirstOrDefault(x => x.UserId == UserId
                                                                       && x.SubjectId == TopicId);

            return UserSubject;
        }

        public static Follow GetFollowById(int FollowerId, int FollowedId, SQLContext SQLContext)
        {
            var Follow = SQLContext.Follows.FirstOrDefault(x => x.FollowerId == FollowerId
                                                             && x.FollowedId == FollowedId);

            return Follow;
        }

        private static IEnumerable<TSource> DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}