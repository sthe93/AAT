// Updated SQL query with a LIMIT clause for better cross-database support
string sql = $"SELECT * FROM received WHERE status = 1 ORDER BY re_ref LIMIT 1000000";

// List to store results
List<received> results = new List<received>();

// ConcurrentBag to safely collect results in parallel
ConcurrentBag<received> concurrentResults = new ConcurrentBag<received>();

// Parallel query for querying SQL nodes in parallel
Parallel.ForEach(SqlNodes, Node =>
{
    received[] result = DBQuery<received>.Query(Node.Value, sql);
    concurrentResults.AddRange(result);
});

// Use a connection pool to efficiently manage database connections
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
    connection.Open();

    // Use a bulk insert to insert data efficiently
    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
    {
        bulkCopy.DestinationTableName = "received_total";
        bulkCopy.BatchSize = 1000; // Adjust batch size based on performance testing

        // Map the source columns to destination columns
        bulkCopy.ColumnMappings.Add("re_fromnum", "rt_msisdn");
        bulkCopy.ColumnMappings.Add("re_message", "rt_message");

        bulkCopy.WriteToServer(concurrentResults);
    }

    connection.Close();
}
