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
                                .OrderByDescending(AffinityPost => (double) AffinityPost.TopicAffinity)
                                .ThenByDescending(AffinityPost => (double) AffinityPost.UserAffinity)
                                .Select(AffinityPost => (Post) AffinityPost.Post)
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
                lock(SQLContext)
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
    }
}