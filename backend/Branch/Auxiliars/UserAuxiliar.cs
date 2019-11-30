using Branch.Models;
using Branch.Models.NoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.SearchAuxiliars
{
    public static class UserAuxiliar
    {
        /// <summary>
        /// Returns the subjects followed by a certain User 
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<Subject> FollowedSubjects(int UserId, SQLContext SQLContext)
        {
            return SQLContext.UserSubjects
                                          .Where(x => x.UserId == UserId)
                                          .Select(x => x.Subject)
                                          .ToList();
        }

        /// <summary>
        /// Returns the subjects not followed by a certain User 
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<Subject> UnfollowedSubjects(int UserId, SQLContext SQLContext)
        {
            var AllSubjects = SQLContext.Subjects.ToList();
            
            return AllSubjects
                              .Except(FollowedSubjects(UserId, SQLContext))
                              .ToList();
        }

        /// <summary>
        /// Returns the user's follows 
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<User> Follows(int UserId, SQLContext SQLContext)
        {
            return SQLContext.Follows
                                     .Where(x => x.FollowerId == UserId)
                                     .Select(x => x.Followed)
                                     .ToList();
        }

        /// <summary>
        /// Returns the users not followed by a certain user 
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<User> Unfolloweds(int UserId, SQLContext SQLContext)
        {
            var UserFollows = Follows(UserId, SQLContext);

            return SQLContext.Users
                                   .Except(UserFollows)
                                   .ToList();
        }

        /// <summary>
        /// Returns the user's followers
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<User> Followers(int UserId, SQLContext SQLContext)
        {
            return SQLContext.Follows
                                     .Where(x => x.FollowedId == UserId)
                                     .Select(x => x.Follower)
                                     .ToList();
        }

        /// <summary>
        /// Returns the user's media (profile photo)
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static Media Media(int UserId, SQLContext SQLContext)
        {
            return SQLContext.Users
                                   .Find(UserId)
                                   .Media;
        }

        /// <summary>
        /// Returns the pro's products (profile photo)
        /// </summary>
        /// <param name="UserId">The pro's id</param>
        public static List<Product> Products(int UserId, SQLContext SQLContext)
        {
            return SQLContext.Products
                                      .Where(x => x.ProId == UserId)
                                      .ToList();
        }

        /// <summary>
        /// Returns the carts of a certain user (profile photo)
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<Cart> UserCarts(int UserId, SQLContext SQLContext)
        {
            return SQLContext.Carts
                                   .Where(x => x.UserId == UserId)
                                   .ToList();
        }

        /// <summary>
        /// Returns the carts of a certain pro (profile photo)
        /// </summary>
        /// <param name="UserId">The pro's id</param>
        public static List<Cart> ProCarts(int UserId, SQLContext SQLContext)
        {
            return SQLContext.Carts
                                   .Where(x => x.ProId == UserId)
                                   .ToList();
        }

        /// <summary>
        /// Returns the cart of a certain store and a certain user (profile photo)
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="ProId">The pro's id</param>
        public static Cart StoreCart(int UserId, int ProId, SQLContext SQLContext)
        {
            return SQLContext.Carts
                                   .FirstOrDefault(x => x.UserId == UserId && x.ProId == ProId);
        }

        /// <summary>
        /// Returns the carts of a certain user with it's products (profile photo)
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<ProductCart> UserProductCarts(int UserId, SQLContext SQLContext)
        {
            var _UserCarts = UserCarts(UserId, SQLContext);

            var CartsId = _UserCarts.Select(x => x.Id);

            return SQLContext.ProductCarts
                                          .Where(x => CartsId.Contains(x.CartId))
                                          .ToList();
        }

        /// <summary>
        /// Returns the carts of a certain pro with it's products (profile photo)
        /// </summary>
        /// <param name="UserId">The pro's id</param>
        public static List<ProductCart> ProProductCarts(int UserId, SQLContext SQLContext)
        {
            var _ProCarts = ProCarts(UserId, SQLContext);
            var CartsId = _ProCarts.Select(x => x.Id);

            return SQLContext.ProductCarts
                                          .Where(x => CartsId.Contains(x.CartId))
                                          .ToList();
        }
    }
}