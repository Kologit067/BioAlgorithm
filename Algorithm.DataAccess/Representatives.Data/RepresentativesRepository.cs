using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Algorithm.DataAccess.Representatives.Data
{
    //----------------------------------------------------------------------------------------------------------------------
    // class RepresentativesRepository
    //----------------------------------------------------------------------------------------------------------------------
    public class RepresentativesRepository
    {
        private readonly string _connectionString;
        public RepresentativesRepository(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["BioAlgorithm"].ConnectionString;
            }
            else
            {
                _connectionString = connectionString;
            }
        }

        public void DeleteRepresentativeAlgorithmGroup(RepresentativeAlgorithmGroupDimension selectedAlgorithmGroup)
        {
            Delete(selectedAlgorithmGroup.Algorithm, selectedAlgorithmGroup.NumberOfSet, selectedAlgorithmGroup.Dimension, selectedAlgorithmGroup.Step);
        }

        public string Delete(string algorithm, int? numberOfSet = null, int? dimension = null, decimal? step = null)
        {
            string error = null;


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                SqlCommand addCommand = new SqlCommand("dbo.deleteRepresentativesPerfomance", connection);
                addCommand.CommandType = CommandType.StoredProcedure;
                addCommand.CommandTimeout = 300;
                SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@Dimension", dimension);
                tvpParam2.SqlDbType = SqlDbType.Int;
                SqlParameter tvpParam3 = addCommand.Parameters.AddWithValue("@Algorithm", algorithm);
                tvpParam3.SqlDbType = SqlDbType.VarChar;
                SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@NumberOfSet", numberOfSet);
                tvpParam.SqlDbType = SqlDbType.Int;
                SqlParameter tvpParam4 = addCommand.Parameters.AddWithValue("@Step", step);
                tvpParam4.SqlDbType = SqlDbType.BigInt;
                addCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                connection.Close();
            }
            return error;

        }


        public void DeleteRepresentativeAlgorithm(RepresentativeAlgorithmGroup selectedAlgorithm)
        {
            Delete(selectedAlgorithm.Algorithm);
        }
        public async Task<List<RepresentativeAlgorithmGroupDimension>> GetRepresentativeAlgorithmGroupDimensions(string algorithmGroupListSort)
        {
            List<RepresentativeAlgorithmGroupDimension> algorithmGroups = new List<RepresentativeAlgorithmGroupDimension>();
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = $@"SELECT [Algorithm], [NumberOfSet], [Dimension], [Step], COUNT(*) as TotalCount, 
       SUM([NumberOfIteration]) as NumberOfIteration, SUM([Duration]) as TotalDuration,
	   SUM([Duration])/COUNT(*) as AverageDuration
FROM [dbo].[RepresentativesPerfomance] AS rp
INNER JOIN [dbo].[RepresentativesInput] AS ri
ON (rp.RepresentativesInputId = ri.RepresentativesInputId)
GROUP BY [Algorithm], [NumberOfSet], [Dimension], [Step]
ORDER BY {algorithmGroupListSort}
";
                algorithmGroups = (await db.QueryAsync<RepresentativeAlgorithmGroupDimension>(sql, commandTimeout: 180)).ToList();
            }
            return algorithmGroups;
        }

        public List<RepresentativeAlgorithWithDimension> GetRepresentativeAlgorithmWithDimensions()
        {
            List<RepresentativeAlgorithWithDimension> algorithmWithDimensions = new List<RepresentativeAlgorithWithDimension>();
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                algorithmWithDimensions = db.Query<RepresentativeAlgorithWithDimension>(
                    $@"SELECT [Algorithm], [NumberOfSet], [Dimension], [Step]
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance] AS rp
INNER JOIN [dbo].[RepresentativesInput] AS ri
ON (rp.RepresentativesInputId = ri.RepresentativesInputId)
GROUP BY [Algorithm], [NumberOfSet], [Dimension], [Step]
").ToList();
            }
            return algorithmWithDimensions;
        }

        public List<RepresentativeAlgorithmGroup> GetRepresentativeAlgorithmGroups()
        {
            List<RepresentativeAlgorithmGroup> algorithmGroups = new List<RepresentativeAlgorithmGroup>();
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                algorithmGroups = db.Query<RepresentativeAlgorithmGroup>(
                    @"WITH CTE AS
(
SELECT [Algorithm], [NumberOfSet], [Dimension], COUNT(*) as cnt, SUM([NumberOfIteration]) as SumNumberOfIteration, SUM([Duration]) as SumDuration
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance] AS rp
INNER JOIN [dbo].[RepresentativesInput] AS ri
ON (rp.RepresentativesInputId = ri.RepresentativesInputId)
GROUP BY [Algorithm], [NumberOfSet], [Dimension]
)
SELECT [Algorithm], 
	SUM(cnt) AS TotalCount, 
	COUNT(*) as CountByDimension, 
	SUM(SumNumberOfIteration) as NumberOfIteration, 
	SUM(SumDuration) as TotalDuration,
	SUM(SumDuration)/SUM(cnt) AS AverageDuration
FROM CTE
GROUP BY [Algorithm]").ToList();
            }
            return algorithmGroups;
        }

        public List<RepresentativesPerfomance> GetRepresentativePerformanceList(RepresentativesPerfomanceFilter representativesPerfomanceFilter, string order)
        {
            string top = "";
            if (representativesPerfomanceFilter.Top.HasValue)
            {
                top = $"TOP ({representativesPerfomanceFilter.Top})";
            }
            string where = "";
            List<string> whereList = new List<string>();
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceFilter.Algorithm))
            {
                whereList.Add($"[Algorithm] = '{representativesPerfomanceFilter.Algorithm}'");
            }
            if (representativesPerfomanceFilter.NumberOfSet.HasValue)
            {
                whereList.Add($"[NumberOfSet] = {representativesPerfomanceFilter.NumberOfSet}");
            }
            if (representativesPerfomanceFilter.Dimension.HasValue)
            {
                whereList.Add($"[Dimension] = {representativesPerfomanceFilter.Dimension}");
            }
            if (representativesPerfomanceFilter.Step.HasValue)
            {
                whereList.Add($"[Step] = {representativesPerfomanceFilter.Step}");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceFilter.InputLen))
            {
                whereList.Add($"[InputLen] = '{representativesPerfomanceFilter.InputLen}'");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceFilter.InputLenSort))
            {
                whereList.Add($"[InputLenSort] = '{representativesPerfomanceFilter.InputLenSort}'");
            }
            if (representativesPerfomanceFilter.NumberOfIterationFrom.HasValue)
            {
                whereList.Add($"[NumberOfIteration] >= {representativesPerfomanceFilter.NumberOfIterationFrom}");
            }
            if (representativesPerfomanceFilter.NumberOfIterationTo.HasValue)
            {
                whereList.Add($"[NumberOfIteration] <= {representativesPerfomanceFilter.NumberOfIterationTo}");
            }
            if (representativesPerfomanceFilter.DurationFrom.HasValue)
            {
                whereList.Add($"[Duration] >= {representativesPerfomanceFilter.DurationFrom}");
            }
            if (representativesPerfomanceFilter.DurationTo.HasValue)
            {
                whereList.Add($"[Duration] <= {representativesPerfomanceFilter.DurationTo}");
            }
            if (representativesPerfomanceFilter.CountTerminalFrom.HasValue)
            {
                whereList.Add($"[CountTerminal] >= {representativesPerfomanceFilter.CountTerminalFrom}");
            }
            if (representativesPerfomanceFilter.CountTerminalTo.HasValue)
            {
                whereList.Add($"[CountTerminal] <= {representativesPerfomanceFilter.CountTerminalTo}");
            }
            if (representativesPerfomanceFilter.BestValue.HasValue)
            {
                whereList.Add($"[BestValue] = {representativesPerfomanceFilter.BestValue}");
            }
            if (representativesPerfomanceFilter.IsComplete.HasValue)
            {
                whereList.Add($"[IsComplete] = {representativesPerfomanceFilter.IsComplete}");
            }
            if (whereList.Count > 0)
            {
                where = "WHERE " + string.Join(" AND ", whereList);
            }
            List<RepresentativesPerfomance> representativesPerfomances = new List<RepresentativesPerfomance>();
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"SELECT {top} [RepresentativesPerfomanceId]
      ,[NumberOfSet],[Dimension],[Step],[InputLen],[InputLenSort]
      ,[InputLenAvg],[InputData],[InputDataShort],[Algorithm],[NumberOfIteration]
	  ,[Duration],[DurationMilliSeconds],[DateComplete],[IsComplete]
      ,[LastRoute],[OptimalRoute],[CountTerminal],[BestValue]
      ,[UpdateOptcount],[ElemenationCount]
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance] AS rp
INNER JOIN [dbo].[RepresentativesInput] AS ri
ON (rp.RepresentativesInputId = ri.RepresentativesInputId)
{where}
ORDER BY {order}";
                representativesPerfomances = db.Query<RepresentativesPerfomance>(query, commandTimeout: 180).ToList();
            }
            return representativesPerfomances;
        }


        public List<RepresentativesPerfomanceCompare> GetRepresentativePerformanceCompareList(RepresentativesPerfomanceCompareFilter representativesPerfomanceCompareFilter)
        {
            string top = "";
            if (representativesPerfomanceCompareFilter.Top.HasValue)
            {
                top = $"TOP ({representativesPerfomanceCompareFilter.Top})";
            }
            string where = "";
            List<string> whereList = new List<string>();

            if (representativesPerfomanceCompareFilter.NumberOfSet.HasValue)
            {
                whereList.Add($"ra.[NumberOfSet] = {representativesPerfomanceCompareFilter.NumberOfSet}");
            }
            if (representativesPerfomanceCompareFilter.Dimension.HasValue)
            {
                whereList.Add($"ra.[Dimension] = {representativesPerfomanceCompareFilter.Dimension}");
            }
            if (representativesPerfomanceCompareFilter.Step.HasValue)
            {
                whereList.Add($"ra.[Step] = {representativesPerfomanceCompareFilter.Step}");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceCompareFilter.BestValueCompare) && representativesPerfomanceCompareFilter.BestValueCompare != "N/A")
            {
                whereList.Add($"(ra.BestValue {representativesPerfomanceCompareFilter.BestValueCompare.Replace("1", "").Replace("2", "")} rb.BestValue OR ra.BestValue IS NULL OR rb.BestValue IS NULL )");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceCompareFilter.NumberIterationCompare) && representativesPerfomanceCompareFilter.NumberIterationCompare != "N/A")
            {
                whereList.Add($"(ra.[NumberOfIteration] {representativesPerfomanceCompareFilter.NumberIterationCompare.Replace("1", "").Replace("2", "")} rb.[NumberOfIteration] OR ra.[NumberOfIteration] IS NULL OR rb.[NumberOfIteration] IS NULL )");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceCompareFilter.DurationCompare) && representativesPerfomanceCompareFilter.DurationCompare != "N/A")
            {
                whereList.Add($"(ra.Duration {representativesPerfomanceCompareFilter.DurationCompare.Replace("1", "").Replace("2", "")} rb.Duration OR ra.Duration IS NULL OR rb.Duration IS NULL )");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceCompareFilter.ElemenationCountCompare) && representativesPerfomanceCompareFilter.ElemenationCountCompare != "N/A")
            {
                whereList.Add($"(ra.ElemenationCount {representativesPerfomanceCompareFilter.ElemenationCountCompare.Replace("1", "").Replace("2", "")} rb.ElemenationCount OR ra.ElemenationCount IS NULL OR rb.ElemenationCount IS NULL )");
            }
            if (whereList.Count > 0)
            {
                where = "WHERE " + string.Join(" AND ", whereList);
            }
            List<RepresentativesPerfomanceCompare> representativesPerfomancesCompare = new List<RepresentativesPerfomanceCompare>();
            string query = $@"WITH ra AS
(
SELECT ria.[NumberOfSet], ria.[Dimension], ria.[InputData], ria.[InputDataShort], ria.Step,
       rpa.Algorithm, rpa.BestValue, rpa.OptimalRoute, rpa.[NumberOfIteration],rpa.[Duration], rpa.ElemenationCount
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance] AS rpa
INNER JOIN [dbo].[RepresentativesInput] AS ria
ON (rpa.RepresentativesInputId = ria.RepresentativesInputId)
),
rb AS
(
SELECT rib.[NumberOfSet], rib.[Dimension], rib.[InputData], rib.[InputDataShort], rib.Step,
       rpb.Algorithm, rpb.BestValue, rpb.OptimalRoute, rpb.[NumberOfIteration], rpb.[Duration], rpb.ElemenationCount
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance] AS rpb
INNER JOIN [dbo].[RepresentativesInput] AS rib
ON (rpb.RepresentativesInputId = rib.RepresentativesInputId)
)
SELECT ra.[NumberOfSet], ra.[Dimension], ra.[InputData], ra.[InputDataShort], ra.Step,
       ra.Algorithm as Algorithm1, rb.Algorithm as Algorithm2, 
       ra.BestValue as BestValue1, rb.BestValue as BestValue2, 
	   ra.OptimalRoute as OptimalRoute1, rb.OptimalRoute as OptimalRoute2,
	   ra.[NumberOfIteration] as NumberOfIteration1, rb.[NumberOfIteration] as NumberOfIteration2,
       ra.[Duration] as Duration1, rb.[Duration] as Duration2,
       ra.ElemenationCount as ElemenationCount1, rb.ElemenationCount as ElemenationCount2
FROM ra INNER JOIN rb
ON (ra.InputData = rb.InputData AND ra.[NumberOfSet] = rb.[NumberOfSet] AND ra.[Dimension] = rb.[Dimension] AND
ra.[Algorithm] = '{representativesPerfomanceCompareFilter.Algorithm1}' AND rb.[Algorithm] = '{representativesPerfomanceCompareFilter.Algorithm2}') 
{where}
";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                representativesPerfomancesCompare = db.Query<RepresentativesPerfomanceCompare>(query).ToList();
            }
            return representativesPerfomancesCompare;
        }
        private int updateIsomorphicBufferSize = 1000;
        private Dictionary<long, string> updateIsomorphicDict = new Dictionary<long, string>();
        public string UpdateIsomorphic(long representativesInputId, string inputData, bool isBipart = false)
        {
            string error = string.Empty;
            updateIsomorphicDict.Add(representativesInputId, inputData);

            if (updateIsomorphicDict.Count >= updateIsomorphicBufferSize)
            {
                error = SaveUpdateIsomorphic(updateIsomorphicDict, isBipart);
                updateIsomorphicDict.Clear();
            }
            return error;
        }

        public string CompleteUpdateIsomorphic(bool isBipart = false)
        {
            string error = SaveUpdateIsomorphic(updateIsomorphicDict, isBipart);
            updateIsomorphicDict.Clear();
            return error;
        }

        private string SaveUpdateIsomorphic(Dictionary<long, string> updateIsomorphicDict, bool isBipart = false)
        {
            string error = string.Empty;
            try
            {

                DataTable isonorphicTable = new DataTable();
                isonorphicTable.Columns.Add("Id", System.Type.GetType("System.Int64"));
                isonorphicTable.Columns.Add("InputData", System.Type.GetType("System.String"));


                foreach (var ui in updateIsomorphicDict)
                {
                    isonorphicTable.Rows.Add(ui.Key, ui.Value);
                }

                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                try
                {
                    SqlCommand addCommand = new SqlCommand("dbo.spUpdateIsomorphic", connection);
                    addCommand.CommandType = CommandType.StoredProcedure;
                    addCommand.CommandTimeout = 300;
                    SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@UpdateIsomorphic", isonorphicTable);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.UpdateIsomorphicType";
                    SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@Dimension", isBipart);
                    tvpParam2.SqlDbType = SqlDbType.Bit;
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

        public void ClearIsomorphic(RepresentativesPerfomanceFilter representativesPerfomanceFilter, bool isBipart = false)
        {
            string where = "";
            List<string> whereList = new List<string>();
            if (representativesPerfomanceFilter.NumberOfSet.HasValue)
            {
                whereList.Add($"[NumberOfSet] = {representativesPerfomanceFilter.NumberOfSet}");
            }
            if (representativesPerfomanceFilter.Dimension.HasValue)
            {
                whereList.Add($"[Dimension] = {representativesPerfomanceFilter.Dimension}");
            }
            if (representativesPerfomanceFilter.Step.HasValue)
            {
                whereList.Add($"[Step] = {representativesPerfomanceFilter.Step}");
            }
            if (whereList.Count > 0)
            {
                where = "WHERE " + string.Join(" AND ", whereList);
            }
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = string.Empty;
                if (!isBipart)
                {
                    query = $@"UPDATE [dbo].[RepresentativesInput]
SET [Isomorphic] = NULL
{where}
";
                }
                else
                {
                    query = $@"UPDATE [dbo].[RepresentativesInput]
SET [IsomorphicBipart] = NULL
{where}
";
                }
                db.Execute(query);
            }
        }

    }
}
