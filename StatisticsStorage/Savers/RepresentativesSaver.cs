using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using StatisticsStorage.Accumulators.Objects;
using Representatives.Data;
using System.Linq;

namespace StatisticsStorage.Savers
{
    public class RepresentativesSaver
    {
        private string _connectionString;
        private readonly RepresentativesRepository _representativesRepository;
        public RepresentativesSaver()
        {
 //           _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["bioalgorithm"].ConnectionString;
            _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=BioAlgorithm;Integrated Security=true";
            _representativesRepository = new RepresentativesRepository(_connectionString);
        }

        public string Save(List<RepresentativesPerfomance> representativesPerfomances)
        {
            var rp = representativesPerfomances.GroupBy(r => r.InputData).Select(g => new { g.Key, Count = g.Count() }).ToList();
            var rps = representativesPerfomances.GroupBy(r => r.InputDataShort).Select(g => new { g.Key, Count = g.Count() }).ToList();
            string error = null;

            try
            {

                DataTable performance = new DataTable();
                performance.Columns.Add("NumberOfSet", System.Type.GetType("System.Int32"));
                performance.Columns.Add("Dimension", System.Type.GetType("System.Int32"));
                performance.Columns.Add("Step", System.Type.GetType("System.Decimal"));
                performance.Columns.Add("InputLen", System.Type.GetType("System.String"));
                performance.Columns.Add("InputLenSort", System.Type.GetType("System.String"));
                performance.Columns.Add("InputLenAvg", System.Type.GetType("System.Decimal"));
                performance.Columns.Add("InputData", System.Type.GetType("System.String"));
                performance.Columns.Add("InputDataShort", System.Type.GetType("System.String"));
                performance.Columns.Add("Algorithm", System.Type.GetType("System.String"));
                performance.Columns.Add("NumberOfIteration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("Duration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DurationMilliSeconds", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DateComplete", System.Type.GetType("System.DateTime"));
                performance.Columns.Add("IsComplete", System.Type.GetType("System.Boolean"));
                performance.Columns.Add("LastRoute", System.Type.GetType("System.String"));
                performance.Columns.Add("OptimalRoute", System.Type.GetType("System.String"));
                performance.Columns.Add("CountTerminal", System.Type.GetType("System.Int64"));
                performance.Columns.Add("BestValue", System.Type.GetType("System.Int64"));
                performance.Columns.Add("UpdateOptcount", System.Type.GetType("System.Int64"));
                performance.Columns.Add("ElemenationCount", System.Type.GetType("System.Int64"));

                DataTable solutions = new DataTable();
                solutions.Columns.Add("NumberOfSet", System.Type.GetType("System.Int32"));
                solutions.Columns.Add("Dimension", System.Type.GetType("System.Int32"));
                solutions.Columns.Add("Step", System.Type.GetType("System.Decimal"));
                solutions.Columns.Add("InputData", System.Type.GetType("System.String"));
                solutions.Columns.Add("Algorithm", System.Type.GetType("System.String"));
                solutions.Columns.Add("OutputPresentation", System.Type.GetType("System.String"));

                foreach (var ps in representativesPerfomances)
                {
                    performance.Rows.Add(
                        ps.NumberOfSet, 
                        ps.Dimension,
                        ps.Step,
                        ps.InputLen,
                        ps.InputLenSort,
                        (decimal)ps.InputLenAvg,
                        ps.InputData,
                        ps.InputDataShort, 
                        ps.Algorithm,
                        ps.IterationCount, 
                        ps.Duration, 
                        ps.DurationMilliSeconds, 
                        ps.DateComplete, 
                        ps.IsComplete,
                        ps.LastRoute,
                        ps.OptimalRoute,
                        ps.CountTerminal, 
                        ps.BestValue, 
                        ps.UpdateOptcount, 
                        ps.ElemenationCount
                        );

                    for (int i = 0; i < ps.OptimalSets.Count; i++)
                    {
                        solutions.Rows.Add(ps.NumberOfSet, ps.Dimension, ps.Step, ps.InputData, ps.Algorithm, ps.OptimalSets[i]);
                    }

                }

                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                try
                {
                    SqlCommand addCommand = new SqlCommand("addRepresentativesPerfomance", connection);
                    addCommand.CommandType = CommandType.StoredProcedure;
                    addCommand.CommandTimeout = 300;
                    SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@RepresentativesPerfomances", performance);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.RepresentativesPerfomanceType";
                    SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@RepresentativesSolutions", solutions);
                    tvpParam2.SqlDbType = SqlDbType.Structured;
                    tvpParam2.TypeName = "dbo.RepresentativesSolutionType";
                    addCommand.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
 
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return error;

        }

        public string Delete(string algorithm, int? numberOfSet = null, int? dimension = null, decimal? step = null)
        {
            return _representativesRepository.Delete(algorithm, numberOfSet, dimension, step);
        }

    }
}
