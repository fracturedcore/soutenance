using UnityEngine;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

public class HighScoreManager : MonoBehaviour
{
    private static string _connectionString;
    private static List<HighScore> _lstHighScores= new List<HighScore>();
    [SerializeField]
    public GameObject scorePrefab;
    [SerializeField]
    public Transform scoreParent;
    [SerializeField]
    private int _topRanks;
    [SerializeField]
    public static int _saveScores;
  
    void Start()
    {
        _connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/DataBase.db";
        _saveScores = 100;

        DeleteExtraScore();
        ShowScores();

    }
    public static void GetScore()
    {
        _lstHighScores.Clear();
        _connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/DataBase.db";
        using (IDbConnection dbConnection = new SqliteConnection(_connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                var sqlQuery = "SELECT * FROM score";
                dbCommand.CommandText = sqlQuery;
                using (IDataReader readerDb = dbCommand.ExecuteReader())
                {
                    while (readerDb.Read())
                    {
                        _lstHighScores.Add(new HighScore(readerDb.GetInt32(0), readerDb.GetString(1), readerDb.GetInt32(2), readerDb.GetDateTime(3), readerDb.GetString(4)));
                    }
                    dbConnection.Close();
                    readerDb.Close();
                }
            }
        }
        _lstHighScores.Sort();
    }
    
    public static void AddScore(string pNom, int pScore, string pType)
    {
        GetScore();
        _saveScores = 100;
       var hsCount = _lstHighScores.Count;
     
        if (hsCount <= _saveScores)
        {
            _connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/DataBase.db";
            using (IDbConnection dbConnection = new SqliteConnection(_connectionString))
            {
                dbConnection.Open();
                using (IDbCommand dbCommand = dbConnection.CreateCommand())
                {
                    var sqlQuery = "INSERT INTO score (pseudo, score, ship_type) VALUES ('" + pNom + "' , '" + pScore + "' , '" + pType + "')";
                    dbCommand.CommandText = sqlQuery;
                    dbCommand.ExecuteScalar();
                    dbConnection.Close();
                }
            }
        }
        
        if (_lstHighScores.Count > 0)
        {
            HighScore lowestScore = _lstHighScores[_lstHighScores.Count - 1];
            if (lowestScore != null && _saveScores > 0 && _lstHighScores.Count >= _saveScores && pScore > lowestScore.Score)
            {
                DeleteScoreFromDataBase(lowestScore.Id);
                hsCount--;
            }
        }
    }

    public static void DeleteScoreFromDataBase(int pId)
    {
        _connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/DataBase.db";
        using (IDbConnection dbConnection = new SqliteConnection(_connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                var sqlQuery = string.Format("DELETE FROM score WHERE id_score = \"{0}\"", pId);
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    public void ShowScores()
    {
       GetScore();
        for (int i = 0; i < _topRanks; i++)
        {
            if (i <= _lstHighScores.Count - 1)
            {
                var tmpGameObject = Instantiate(scorePrefab);
                var tmpScore = _lstHighScores[i];

                tmpGameObject.GetComponent<HighScoreScript>().SetScore("#" + (i + 1), tmpScore.Name, tmpScore.Score.ToString(), tmpScore.Type);
                tmpGameObject.transform.SetParent(scoreParent);
            
                tmpGameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void DeleteExtraScore()
    {
        GetScore();
        if (_saveScores <= _lstHighScores.Count)
        { 
            int deletedCount = _lstHighScores.Count - _saveScores;
            _lstHighScores.Reverse();
           
            for (int i = 0; i < deletedCount; i++)
            {
                DeleteScoreFromDataBase(_lstHighScores[i].Id);
            }
        }
    }
}
