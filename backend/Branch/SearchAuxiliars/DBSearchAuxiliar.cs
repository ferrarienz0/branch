using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.SearchAuxiliars
{
    public static class DBSearchAuxiliar
    {
        private const string ConnectionString = "server=tuffi.db.elephantsql.com;" +
                                                "User Id=vlvhqsdd;Database=vlvhqsdd;" +
                                                "Port=5432;" +
                                                "Password=0pBsOXETTteJOJt6Ysf0jq_135BK--N3";

        public static List<dynamic> RecommendedProDiscounts(int ProId)
        {
            var Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();

            var RecommendedDiscounts = new List<dynamic>();

            using (var SQLCommand = new NpgsqlCommand($"SELECT * FROM ApplyDiscount({ ProId })", Connection))
            using (var Reader = SQLCommand.ExecuteReader())
            {
                while (Reader.Read())
                {
                    var Value = new 
                    { 
                        ProductId = Reader.GetValue(0), 
                        RecommendedDiscount = Reader.GetValue(1) 
                    };

                    RecommendedDiscounts.Add(Value);
                }
            }

            Connection.Close();

            return RecommendedDiscounts;
        }

        public static void IncreaseAffinityOnFollow(int FollowerId, int FollowedId)
        {
            var Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();

            using (var SQLCommand = new NpgsqlCommand($"call IncreaseAffinityFollow({ FollowerId }, {FollowedId})", Connection))
            {
                SQLCommand.ExecuteNonQuery();
            }

            Connection.Close();
        }

        public static void IncreaseAffinityOnSubject(int UserId, int SubjectId)
        {
            var Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();
            
            using (var SQLCommand = new NpgsqlCommand($"call IncreaseAffinityUserSubjects({ UserId }, { SubjectId })", Connection))
            {
                SQLCommand.ExecuteNonQuery();
            }

            Connection.Close();
        }
    }
}