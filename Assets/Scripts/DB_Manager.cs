using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;

public class DB_Manager : MonoBehaviour
{
    public static string connectionString;
    //private string path;

    private List<Cell> cellDB = new List<Cell>();

    public GameObject cellPrefab;

    public Transform cellParent;

    public GameObject ContentBox;

    private int t;

    public Text F5, F6, F7, F8, F9, F10;

    List<string> type, Optionsfield5, Optionsfield6;
    
    
    // Convert string like "1?5" in string "1.5"
    public static string TryGetDD(string str)
    {
        int k = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (System.Char.IsDigit(str[i]) == false) 
            {
                k = i;
                break;
            }
        }
        string OUT = str.Replace(str[k], '.');
        return OUT;
    }

    // Start is called before the first frame update
    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/Data.db";
        type = new List<string>();
        type.Add("");
        type.Add("Процессор");
        type.Add("Материнская_плата");
        type.Add("Видеокарта");
        type.Add("Оперативная_память");
        Optionsfield5 = new List<string>();
        Optionsfield6 = new List<string>();
        producer = new List<string>();
    }


    private void InsertINProcessor(string producer, string model, int year, int cores, int max_memory, string chastota, int amount)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Процессор(Производитель,Модель,Год_выпуска,Число_ядер,Макс_память,Частота,Количество) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\")", producer, model, year, cores, max_memory, chastota, amount);
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void InsertINMother(string producer, string model, int year, string type_memory, int max_memory, string height, string width, int amount)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Материнская_плата(Производитель,Модель,Год_выпуска,Тип_памяти,Объём_памяти,Высота,Ширина,Количество) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\")", producer, model, year, type_memory, max_memory, height, width, amount);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void InsertINVideo(string producer, string model, int year, string type_memory, int max_memory, string type_cooling, string length, int amount)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Видеокарта(Производитель,Модель,Год_выпуска,Тип_памяти,Объём_памяти,Тип_охлаждения,Длина,Количество) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\")", producer, model, year, type_memory, max_memory, type_cooling, length, amount);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void InsertINMemory(string producer, string model, int year, string type_memory, int max_memory, string chastota, string radiator, string height, int amount)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Оперативная_память(Производитель,Модель,Год_выпуска,Тип_памяти,Объём_памяти,Частота,Радиатор,Высота,Количество) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\")", producer, model, year, type_memory, max_memory, chastota, radiator, height, amount);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }
    private void GetProcessor()
    {
        cellDB.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Процессор";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetInt32(7).ToString(), "", ""));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void GetMother()
    {
        cellDB.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Материнская_плата";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void GetVideo()
    {
        cellDB.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Видеокарта";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetString(6), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void GetMemory()
    {
        cellDB.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Оперативная_память";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetString(7), reader.GetDouble(8).ToString(), reader.GetInt32(9).ToString()));
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void GetField5(string name0, string table, int tuggle)
    {
        Optionsfield5.Clear();
        Optionsfield5.Add(name0);

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                if (table == "Тип_памяти" & tuggle == 1)
                {
                    string sqlQuery = String.Format("SELECT * FROM \"{0}\" WHERE Type LIKE 'DD%'", table);
                    dbCmd.CommandText = sqlQuery;
                }
                else if (table == "Тип_памяти" & tuggle == 2)
                {
                    string sqlQuery = String.Format("SELECT * FROM \"{0}\" WHERE Type LIKE 'GD%'", table);
                    dbCmd.CommandText = sqlQuery;
                }
                else
                {
                    string sqlQuery = String.Format("SELECT * FROM \"{0}\"", table);
                    dbCmd.CommandText = sqlQuery;
                }

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (table == "Тип_памяти") { Optionsfield5.Add(reader.GetString(1)); }
                        else { Optionsfield5.Add(reader.GetInt32(1).ToString()); }
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private void GetField6(string name0, string table)
    {
        Optionsfield6.Clear();
        Optionsfield6.Add(name0);

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("SELECT * FROM \"{0}\"", table);

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(table == "Тип_памяти") { Optionsfield6.Add(reader.GetString(1)); }
                        else { Optionsfield6.Add(reader.GetInt32(1).ToString()); }
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    public void DeleteData(string table, int id)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("DELETE FROM \"{0}\" WHERE Id = \"{1}\"", table, id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    private void ShowData(int toggle)
    {
        int numChildren = cellParent.childCount;
        if (numChildren > 1)
        {
            for (int i = 1; i < numChildren; i++)
            {
                Destroy(cellParent.GetChild(i).gameObject);
            }
        }


        if (toggle == 1) 
        {
            t = 8;
            F5.text = "Число ядер";
            F6.text = "Макс память";
            F7.text = "Частота";
            F8.text = "Количество";
            F9.text = "";
            F10.text = "";
        }
        else if (toggle == 2) 
        {
            t = 9;
            F5.text = "Тип памяти";
            F6.text = "Объём памяти";
            F7.text = "Высота";
            F8.text = "Ширина";
            F9.text = "Количество";
            F10.text = "";
        }
        else if (toggle == 3) 
        {
            t = 9;
            F5.text = "Тип памяти";
            F6.text = "Объём памяти";
            F7.text = "Тип охлаждения";
            F8.text = "Длина";
            F9.text = "Количество";
            F10.text = "";
        }
        else if (toggle == 4) 
        {
            t = 10;
            F5.text = "Тип памяти";
            F6.text = "Объём памяти";
            F7.text = "Частота";
            F8.text = "Радиатор";
            F9.text = "Высота";
            F10.text = "Количество";
        }

        ContentBox.GetComponent<RectTransform>().sizeDelta = new Vector2(1056 * t / 10f, 365 * cellDB.Count / 11f);


        for (int i = 0; i < cellDB.Count; i++)
        {
            GameObject tmpObject = Instantiate(cellPrefab);

            Cell tmpCell = cellDB[i];

            tmpObject.GetComponent<CellScript>().SetData(tmpCell.Field1.ToString(), tmpCell.Field2, tmpCell.Field3, tmpCell.Field4.ToString(), tmpCell.Field5, tmpCell.Field6, tmpCell.Field7, tmpCell.Field8, tmpCell.Field9, tmpCell.Field10);

            tmpObject.transform.SetParent(cellParent);

            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    private void SearchNameData(string table, string name)
    {
        cellDB.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            if (name != "")
            {
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    string sqlQuery = String.Format("SELECT * FROM {0} WHERE Модель LIKE  \"%{1}%\"", table, name);

                    dbCmd.CommandText = sqlQuery;
                    
                    if (table == "Процессор")
                    {
                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetInt32(7).ToString(), "", ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Материнская_плата")
                    {
                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Видеокарта")
                    {
                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetString(6), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Оперативная_память")
                    {
                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetString(7), reader.GetDouble(8).ToString(), reader.GetInt32(9).ToString()));
                            }

                            reader.Close();
                        }
                    }
                }
            }
            else
            {
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    string sqlQuery = String.Format("SELECT * FROM \"{0}\"", table);

                    dbCmd.CommandText = sqlQuery;

                    if (table == "Процессор")
                    {
                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetInt32(7).ToString(), "", ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Материнская_плата")
                    {
                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Видеокарта")
                    {
                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetString(6), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Оперативная_память")
                    {
                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetString(7), reader.GetDouble(8).ToString(), reader.GetInt32(9).ToString()));
                            }

                            reader.Close();
                        }
                    }
                }
            }
            
            dbConnection.Close();
        }
    }

    private void ExtendedSearchData(string table, string field2, string field3, string field4, string field5, string field6, string field7, string field8, string field9, string field10)
    {
        cellDB.Clear();
        string sqlParam = "";

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            if (field3 != "")
            {
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    if (table == "Процессор")
                    {
                        if (field4 != "") { sqlParam += String.Format(" AND Год_выпуска LIKE \"%{0}%\"", field4); }
                        if (field5 != "Число ядер...") { sqlParam += String.Format(" AND Число_ядер LIKE \"%{0}%\"", field5); }
                        if (field6 != "Макс память...") { sqlParam += String.Format(" AND Макс_память LIKE \"%{0}%\"", field6); }
                        if (field7 != "") { sqlParam += String.Format(" AND Частота LIKE \"%{0}%\"", TryGetDD(field7)); }
                        if (field8 != "") { sqlParam += String.Format(" AND Количество LIKE \"%{0}%\"", field8); }
                        if (field2 != "Производители") { sqlParam += String.Format(" AND Производитель LIKE \"%{0}%\"", field2); }

                        string sqlQuery = String.Format("SELECT * FROM \"{0}\" WHERE Модель LIKE \"%{1}%\"{2}", table, field3, sqlParam);
                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetInt32(7).ToString(), "", ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Материнская_плата")
                    {
                        if (field4 != "") { sqlParam += String.Format(" AND Год_выпуска LIKE \"%{0}%\"", field4); }
                        if (field5 != "Тип памяти...") { sqlParam += String.Format(" AND Тип_памяти LIKE \"%{0}%\"", field5); }
                        if (field6 != "Объём памяти...") { sqlParam += String.Format(" AND Объём_памяти LIKE \"%{0}%\"", field6); }
                        if (field7 != "") { sqlParam += String.Format(" AND Высота LIKE \"%{0}%\"", TryGetDD(field7)); }
                        if (field8 != "") { sqlParam += String.Format(" AND Ширина LIKE \"%{0}%\"", TryGetDD(field8)); }
                        if (field9 != "") { sqlParam += String.Format(" AND Количество LIKE \"%{0}%\"", field9); }
                        if (field2 != "Производители") { sqlParam += String.Format(" AND Производитель LIKE \"%{0}%\"", field2); }

                        string sqlQuery = String.Format("SELECT * FROM \"{0}\" WHERE Модель LIKE \"%{1}%\"{2}", table, field3, sqlParam);
                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Видеокарта")
                    {
                        if (field4 != "") { sqlParam += String.Format(" AND Год_выпуска LIKE \"%{0}%\"", field4); }
                        if (field5 != "Тип памяти...") { sqlParam += String.Format(" AND Тип_памяти LIKE \"%{0}%\"", field5); }
                        if (field6 != "Объём памяти...") { sqlParam += String.Format(" AND Объём_памяти LIKE \"%{0}%\"", field6); }
                        if (field7 != "") { sqlParam += String.Format(" AND Тип_охлаждения LIKE \"%{0}%\"", field7); }
                        if (field8 != "") { sqlParam += String.Format(" AND Длина LIKE \"%{0}%\"", TryGetDD(field8)); }
                        if (field9 != "") { sqlParam += String.Format(" AND Количество LIKE \"%{0}%\"", field9); }
                        if (field2 != "Производители") { sqlParam += String.Format(" AND Производитель LIKE \"%{0}%\"", field2); }

                        string sqlQuery = String.Format("SELECT * FROM \"{0}\" WHERE Модель LIKE \"%{1}%\"{2}", table, field3, sqlParam);
                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetString(6), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Оперативная_память")
                    {
                        if (field4 != "") { sqlParam += String.Format(" AND Год_выпуска LIKE \"%{0}%\"", field4); }
                        if (field5 != "Тип памяти...") { sqlParam += String.Format(" AND Тип_памяти LIKE \"%{0}%\"", field5); }
                        if (field6 != "Объём памяти...") { sqlParam += String.Format(" AND Объём_памяти LIKE \"%{0}%\"", field6); }
                        if (field7 != "") { sqlParam += String.Format(" AND Частота LIKE \"%{0}%\"", field7); }
                        if (field8 != "") { sqlParam += String.Format(" AND Радиатор LIKE \"%{0}%\"", field8); }
                        if (field9 != "") { sqlParam += String.Format(" AND Высота LIKE \"%{0}%\"", TryGetDD(field9)); }
                        if (field10 != "") { sqlParam += String.Format(" AND Количество LIKE \"%{0}%\"", field10); }
                        if (field2 != "Производители") { sqlParam += String.Format(" AND Производитель LIKE \"%{0}%\"", field2); }

                        string sqlQuery = String.Format("SELECT * FROM \"{0}\" WHERE Модель LIKE \"%{1}%\"{2}", table, field3, sqlParam);
                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetString(7), reader.GetDouble(8).ToString(), reader.GetInt32(9).ToString()));
                            }

                            reader.Close();
                        }
                    }
                }
            }
            else
            {
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    if (table == "Процессор")
                    {
                        if (field4 != "") { sqlParam += String.Format("WHERE Год_выпуска LIKE \"%{0}%\"", field4); }
                        if (field5 != "Число ядер..." && field4 != "") { sqlParam += String.Format(" AND Число_ядер LIKE \"%{0}%\"", field5); }
                        else if (field5 != "Число ядер..." && field4 == "") { sqlParam += String.Format("WHERE Число_ядер LIKE \"%{0}%\"", field5); }
                        if (field6 != "Макс память...") { sqlParam += String.Format(" AND Макс_память LIKE \"%{0}%\"", field6); }
                        if (field7 != "") { sqlParam += String.Format(" AND Частота LIKE \"%{0}%\"", TryGetDD(field7)); }
                        if (field8 != "") { sqlParam += String.Format(" AND Количество LIKE \"%{0}%\"", field8); }
                        if (field2 != "Производители" && sqlParam != "") { sqlParam += String.Format(" AND Производитель LIKE \"%{0}%\"", field2); }
                        else if (field2 != "Производители" && sqlParam == "") { sqlParam += String.Format("WHERE Производитель LIKE \"%{0}%\"", field2); }

                        string sqlQuery = String.Format("SELECT * FROM \"{0}\" {1}", table, sqlParam);
                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetInt32(7).ToString(), "", ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Материнская_плата")
                    {
                        if (field4 != "") { sqlParam += String.Format("WHERE Год_выпуска LIKE \"%{0}%\"", field4); }
                        if (field5 != "Тип памяти..." && field4 != "") { sqlParam += String.Format(" AND Тип_памяти LIKE \"%{0}%\"", field5); }
                        else if (field5 != "Тип памяти..." && field4 == "") { sqlParam += String.Format("WHERE Тип_памяти LIKE \"%{0}%\"", field5); }
                        if (field6 != "Объём памяти...") { sqlParam += String.Format(" AND Объём_памяти LIKE \"%{0}%\"", field6); }
                        if (field7 != "") { sqlParam += String.Format(" AND Высота LIKE \"%{0}%\"", TryGetDD(field7)); }
                        if (field8 != "") { sqlParam += String.Format(" AND Ширина LIKE \"%{0}%\"", TryGetDD(field8)); }
                        if (field9 != "") { sqlParam += String.Format(" AND Количество LIKE \"%{0}%\"", field9); }
                        if (field2 != "Производители" && sqlParam != "") { sqlParam += String.Format(" AND Производитель LIKE \"%{0}%\"", field2); }
                        else if (field2 != "Производители" && sqlParam == "") { sqlParam += String.Format("WHERE Производитель LIKE \"%{0}%\"", field2); }

                        string sqlQuery = String.Format("SELECT * FROM \"{0}\" {1}", table, sqlParam);
                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Видеокарта")
                    {
                        if (field4 != "") { sqlParam += String.Format("WHERE Год_выпуска LIKE \"%{0}%\"", field4); }
                        if (field5 != "Тип памяти..." && field4 != "") { sqlParam += String.Format(" AND Тип_памяти LIKE \"%{0}%\"", field5); }
                        else if (field5 != "Тип памяти..." && field4 == "") { sqlParam += String.Format("WHERE Тип_памяти LIKE \"%{0}%\"", field5); }
                        if (field6 != "Объём памяти...") { sqlParam += String.Format(" AND Объём_памяти LIKE \"%{0}%\"", field6); }
                        if (field7 != "") { sqlParam += String.Format(" AND Тип_охлаждения LIKE \"%{0}%\"", field7); }
                        if (field8 != "") { sqlParam += String.Format(" AND Длина LIKE \"%{0}%\"", TryGetDD(field8)); }
                        if (field9 != "") { sqlParam += String.Format(" AND Количество LIKE \"%{0}%\"", field9); }
                        if (field2 != "Производители" && sqlParam != "") { sqlParam += String.Format(" AND Производитель LIKE \"%{0}%\"", field2); }
                        else if (field2 != "Производители" && sqlParam == "") { sqlParam += String.Format("WHERE Производитель LIKE \"%{0}%\"", field2); }

                        string sqlQuery = String.Format("SELECT * FROM \"{0}\" {1}", table, sqlParam);
                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetString(6), reader.GetDouble(7).ToString(), reader.GetInt32(8).ToString(), ""));
                            }

                            reader.Close();
                        }
                    }
                    else if (table == "Оперативная_память")
                    {
                        if (field4 != "") { sqlParam += String.Format("WHERE Год_выпуска LIKE \"%{0}%\"", field4); }
                        if (field5 != "Тип памяти..." && field4 != "") { sqlParam += String.Format(" AND Тип_памяти LIKE \"%{0}%\"", field5); }
                        else if (field5 != "Тип памяти..." && field4 == "") { sqlParam += String.Format("WHERE Тип_памяти LIKE \"%{0}%\"", field5); }
                        if (field6 != "Объём памяти...") { sqlParam += String.Format(" AND Объём_памяти LIKE \"%{0}%\"", field6); }
                        if (field7 != "") { sqlParam += String.Format(" AND Частота LIKE \"%{0}%\"", field7); }
                        if (field8 != "") { sqlParam += String.Format(" AND Радиатор LIKE \"%{0}%\"", field8); }
                        if (field9 != "") { sqlParam += String.Format(" AND Высота LIKE \"%{0}%\"", TryGetDD(field9)); }
                        if (field10 != "") { sqlParam += String.Format(" AND Количество LIKE \"%{0}%\"", field10); }
                        if (field2 != "Производители" && sqlParam != "") { sqlParam += String.Format(" AND Производитель LIKE \"%{0}%\"", field2); }
                        else if (field2 != "Производители" && sqlParam == "") { sqlParam += String.Format("WHERE Производитель LIKE \"%{0}%\"", field2); }

                        string sqlQuery = String.Format("SELECT * FROM \"{0}\" {1}", table, sqlParam);
                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cellDB.Add(new Cell(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetDouble(6).ToString(), reader.GetString(7), reader.GetDouble(8).ToString(), reader.GetInt32(9).ToString()));
                            }

                            reader.Close();
                        }
                    }
                }
            }

            dbConnection.Close();
        }
    }

    public void EditData(string table, int ID, string field3, string field4, string field5, string field6, string field7, string field8, string field9, string field10)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            if (table == "Процессор")
            {
                string sqlQuery = String.Format("UPDATE Процессор SET Модель = \"{0}\", Год_выпуска = \"{1}\", Число_ядер = \"{2}\", Макс_память = \"{3}\", Частота = \"{4}\", Количество = \"{5}\" WHERE Id = \"{6}\"", field3, System.Convert.ToInt32(field4), System.Convert.ToInt32(field5), System.Convert.ToInt32(field6), TryGetDD(field7),  System.Convert.ToInt32(field8), ID);
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
            else if (table == "Материнская_плата")
            {
                string sqlQuery = String.Format("UPDATE Материнская_плата SET Модель = \"{0}\", Год_выпуска = \"{1}\", Тип_памяти = \"{2}\", Объём_памяти = \"{3}\", Высота = \"{4}\", Ширина = \"{5}\", Количество = \"{6}\" WHERE Id = \"{7}\"", field3, System.Convert.ToInt32(field4), field5, System.Convert.ToInt32(field6), TryGetDD(field7), TryGetDD(field8), System.Convert.ToInt32(field9), ID);
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
            else if (table == "Видеокарта")
            {
                string sqlQuery = String.Format("UPDATE Видеокарта SET Модель = \"{0}\", Год_выпуска = \"{1}\", Тип_памяти = \"{2}\", Объём_памяти = \"{3}\", Тип_охлаждения = \"{4}\", Длина = \"{5}\", Количество = \"{6}\" WHERE Id = \"{7}\"", field3, System.Convert.ToInt32(field4), field5, System.Convert.ToInt32(field6), field7, TryGetDD(field8), System.Convert.ToInt32(field9), ID);
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
            else if (table == "Оперативная_память")
            {
                string sqlQuery = String.Format("UPDATE Оперативная_память SET Модель = \"{0}\", Год_выпуска = \"{1}\", Тип_памяти = \"{2}\", Объём_памяти = \"{3}\", Частота = \"{4}\", Радиатор = \"{5}\", Высота = \"{6}\", Количество = \"{7}\" WHERE Id = \"{8}\"", field3, System.Convert.ToInt32(field4), field5, System.Convert.ToInt32(field6), TryGetDD(field7), field8, TryGetDD(field9), System.Convert.ToInt32(field10), ID);
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
        }
    }

    public void ShowAllProcessor()
    {
        GetProcessor();
        ShowData(1);
    }

    public void ShowAllMother()
    {
        GetMother();
        ShowData(2);
    }

    public void ShowAllVideo()
    {
        GetVideo();
        ShowData(3);
    }

    public void ShowAllMemory()
    {
        GetMemory();
        ShowData(4);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public Dropdown DD_type1, DD_type2, DD_prod1, DD_prod2;
    List<string> producer;

    public GameObject field31, field41, field51, field61, field71, field81, field91, field101;
    public Dropdown DD_field51, DD_field61;
    public Text Tf_place71, Tf_place81, Tf_place91, Tf_place101;

    public void Type1(int r)
    {
        producer.Clear();
        if (r == 0)
        {
            producer.Add("Производители");
            field51.SetActive(false);
            field61.SetActive(false);
            field71.SetActive(false);
            field81.SetActive(false);
            field91.SetActive(false);
            field101.SetActive(false);
        }
        // Процессор
        else if (r == 1)
        {
            producer.Add("Производители");
            producer.Add("AMD");
            producer.Add("Intel");
            field51.SetActive(true);
            GetField5("Число ядер...", "Число_ядер", 0);
            field61.SetActive(true);
            GetField6("Макс память...", "Объём_памяти");
            field71.SetActive(true);
            Tf_place71.text = "Частота...";
            field81.SetActive(true);
            Tf_place81.text = "Количество...";
            field91.SetActive(false);
            field101.SetActive(false);
        }
        // Материнская плата
        else if (r == 2)
        {
            producer.Add("Производители");
            producer.Add("ASUS");
            producer.Add("Asrock");
            producer.Add("GIGABYTE");
            producer.Add("MSI");
            field51.SetActive(true);
            GetField5("Тип памяти...", "Тип_памяти", 1);
            field61.SetActive(true);
            GetField6("Объём памяти...", "Объём_памяти");
            field71.SetActive(true);
            Tf_place71.text = "Высота...";
            field81.SetActive(true);
            Tf_place81.text = "Ширина...";
            field91.SetActive(true);
            Tf_place91.text = "Количество...";
            field101.SetActive(false);
        }
        // Видеокарта
        else if (r == 3)
        {
            producer.Add("Производители");
            producer.Add("ASUS");
            producer.Add("GIGABYTE");
            producer.Add("MSI");
            producer.Add("Palit");
            field51.SetActive(true);
            GetField5("Тип памяти...", "Тип_памяти", 2);
            field61.SetActive(true);
            GetField6("Объём памяти...", "Объём_памяти");
            field71.SetActive(true);
            Tf_place71.text = "Тип охлаждения...";
            field81.SetActive(true);
            Tf_place81.text = "Длина...";
            field91.SetActive(true);
            Tf_place91.text = "Количество...";
            field101.SetActive(false);
        }
        // Оперативная память
        else if (r == 4)
        {
            producer.Add("Производители");
            producer.Add("A-DATA");
            producer.Add("AMD");
            producer.Add("Corsair");
            producer.Add("G.Skill");
            producer.Add("Patriot Memory");
            field51.SetActive(true);
            GetField5("Тип памяти...", "Тип_памяти", 1);
            field61.SetActive(true);
            GetField6("Объём памяти...", "Объём_памяти");
            field71.SetActive(true);
            Tf_place71.text = "Частота...";
            field81.SetActive(true);
            Tf_place81.text = "Радиатор...";
            field91.SetActive(true);
            Tf_place91.text = "Высота...";
            field101.SetActive(true);
            Tf_place101.text = "Количество...";
        }
        DD_prod1.ClearOptions();
        DD_prod1.AddOptions(producer);
        DD_field51.ClearOptions();
        DD_field51.AddOptions(Optionsfield5);
        DD_field61.ClearOptions();
        DD_field61.AddOptions(Optionsfield6);
    }

    public GameObject field3, field4, field5, field6, field7, field8, field9, field10;
    public Text Tf_place7, Tf_place8, Tf_place9, Tf_place10;
    public Dropdown DD_field5, DD_field6;


    public void Type2(int r)
    {
        producer.Clear();
        if (r == 0)
        {
            producer.Add("Производители");
            field5.SetActive(false);
            field6.SetActive(false);
            field7.SetActive(false);
            field8.SetActive(false);
            field9.SetActive(false);
            field10.SetActive(false);
        }
        // Процессор
        else if (r == 1)
        {
            producer.Add("AMD");
            producer.Add("Intel");
            field5.SetActive(true);
            GetField5("Число ядер...", "Число_ядер", 0);
            field6.SetActive(true);
            GetField6("Макс память...", "Объём_памяти");
            field7.SetActive(true);
            Tf_place7.text = "Частота...";
            field8.SetActive(true);
            Tf_place8.text = "Количество...";
            field9.SetActive(false);
            field10.SetActive(false);
        }
        // Материнская плата
        else if (r == 2)
        {
            producer.Add("ASUS");
            producer.Add("Asrock");
            producer.Add("GIGABYTE");
            producer.Add("MSI");
            field5.SetActive(true);
            GetField5("Тип памяти...", "Тип_памяти", 1);
            field6.SetActive(true);
            GetField6("Объём памяти...", "Объём_памяти");
            field7.SetActive(true);
            Tf_place7.text = "Высота...";
            field8.SetActive(true);
            Tf_place8.text = "Ширина...";
            field9.SetActive(true);
            Tf_place9.text = "Количество...";
            field10.SetActive(false);
        }
        // Видеокарта
        else if (r == 3)
        {
            producer.Add("ASUS");
            producer.Add("GIGABYTE");
            producer.Add("MSI");
            producer.Add("Palit");
            field5.SetActive(true);
            GetField5("Тип памяти...", "Тип_памяти", 2);
            field6.SetActive(true);
            GetField6("Объём памяти...", "Объём_памяти");
            field7.SetActive(true);
            Tf_place7.text = "Тип охлаждения...";
            field8.SetActive(true);
            Tf_place8.text = "Длина...";
            field9.SetActive(true);
            Tf_place9.text = "Количество...";
            field10.SetActive(false);
        }
        // Оперативная память
        else if (r == 4)
        {
            producer.Add("A-DATA");
            producer.Add("AMD");
            producer.Add("Corsair");
            producer.Add("G.Skill");
            producer.Add("Patriot Memory");
            field5.SetActive(true);
            GetField5("Тип памяти...", "Тип_памяти", 1);
            field6.SetActive(true);
            GetField6("Объём памяти...", "Объём_памяти");
            field7.SetActive(true);
            Tf_place7.text = "Частота...";
            field8.SetActive(true);
            Tf_place8.text = "Радиатор...";
            field9.SetActive(true);
            Tf_place9.text = "Высота...";
            field10.SetActive(true);
            Tf_place10.text = "Количество...";
        }
        DD_prod2.ClearOptions();
        DD_prod2.AddOptions(producer);
        DD_field5.ClearOptions();
        DD_field5.AddOptions(Optionsfield5);
        DD_field6.ClearOptions();
        DD_field6.AddOptions(Optionsfield6);
    }

    public void ConfirmAdd()
    {
        int If_place1 = DD_type2.value;
        string Tf_input2 = producer[DD_prod2.value];
        string Tf_input5 = Optionsfield5[DD_field5.value];
        string Tf_input6 = Optionsfield6[DD_field6.value];
        if (If_place1 == 1)
        {
            InsertINProcessor(Tf_input2,
                field3.GetComponent<InputField>().text,
                System.Convert.ToInt32(field4.GetComponent<InputField>().text),
                System.Convert.ToInt32(Tf_input5),
                System.Convert.ToInt32(Tf_input6),
                TryGetDD(field7.GetComponent<InputField>().text),
                System.Convert.ToInt32(field8.GetComponent<InputField>().text));
            ShowAllProcessor();
            DelTuggle = 1;
        }
        else if (If_place1 == 2)
        {
            InsertINMother(Tf_input2,
                field3.GetComponent<InputField>().text,
                System.Convert.ToInt32(field4.GetComponent<InputField>().text),
                Tf_input5,
                System.Convert.ToInt32(Tf_input6),
                TryGetDD(field7.GetComponent<InputField>().text),
                TryGetDD(field8.GetComponent<InputField>().text),
                System.Convert.ToInt32(field9.GetComponent<InputField>().text));
            ShowAllMother();
            DelTuggle = 2;
        }
        else if (If_place1 == 3)
        {
            InsertINVideo(Tf_input2,
                field3.GetComponent<InputField>().text,
                System.Convert.ToInt32(field4.GetComponent<InputField>().text),
                Tf_input5,
                System.Convert.ToInt32(Tf_input6),
                field7.GetComponent<InputField>().text,
                TryGetDD(field8.GetComponent<InputField>().text),
                System.Convert.ToInt32(field9.GetComponent<InputField>().text));
            ShowAllVideo();
            DelTuggle = 3;
        }
        else if (If_place1 == 4)
        {
            InsertINMemory(Tf_input2,
                field3.GetComponent<InputField>().text,
                System.Convert.ToInt32(field4.GetComponent<InputField>().text),
                Tf_input5,
                System.Convert.ToInt32(Tf_input6),
                TryGetDD(field7.GetComponent<InputField>().text),
                field8.GetComponent<InputField>().text,
                TryGetDD(field9.GetComponent<InputField>().text),
                System.Convert.ToInt32(field10.GetComponent<InputField>().text));
            ShowAllMemory();
            DelTuggle = 4;
        }
    }

    public int DelTuggle;

    public void SetDelTuggle1()
    {
        DelTuggle = 1;
    }

    public void SetDelTuggle2()
    {
        DelTuggle = 2;
    }

    public void SetDelTuggle3()
    {
        DelTuggle = 3;
    }

    public void SetDelTuggle4()
    {
        DelTuggle = 4;
    }


    public void ConfirmEdit()
    {
        if (GameObject.FindGameObjectWithTag("f1").GetComponent<Text>().text == "Процессор" )
        {
            string table = "Процессор";
            EditData(table,
            System.Convert.ToInt32(GameObject.FindGameObjectWithTag("ID").GetComponent<Text>().text),
            GameObject.FindGameObjectWithTag("f3").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f4").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f5").GetComponent<Dropdown>().options[GameObject.FindGameObjectWithTag("f5").GetComponent<Dropdown>().value].text,
            GameObject.FindGameObjectWithTag("f6").GetComponent<Dropdown>().options[GameObject.FindGameObjectWithTag("f6").GetComponent<Dropdown>().value].text,
            GameObject.FindGameObjectWithTag("f7").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f8").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f9").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f10").GetComponent<InputField>().text);
            ShowAllProcessor();
        }
        else if (GameObject.FindGameObjectWithTag("f1").GetComponent<Text>().text == "Материнская плата")
        {
            string table = "Материнская_плата";
            EditData(table,
            System.Convert.ToInt32(GameObject.FindGameObjectWithTag("ID").GetComponent<Text>().text),
            GameObject.FindGameObjectWithTag("f3").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f4").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f5").GetComponent<Dropdown>().options[GameObject.FindGameObjectWithTag("f5").GetComponent<Dropdown>().value].text,
            GameObject.FindGameObjectWithTag("f6").GetComponent<Dropdown>().options[GameObject.FindGameObjectWithTag("f6").GetComponent<Dropdown>().value].text,
            GameObject.FindGameObjectWithTag("f7").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f8").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f9").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f10").GetComponent<InputField>().text);
            ShowAllMother();
        }
        else if (GameObject.FindGameObjectWithTag("f1").GetComponent<Text>().text == "Видеокарта")
        {
            string table = "Видеокарта";
            EditData(table,
            System.Convert.ToInt32(GameObject.FindGameObjectWithTag("ID").GetComponent<Text>().text),
            GameObject.FindGameObjectWithTag("f3").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f4").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f5").GetComponent<Dropdown>().options[GameObject.FindGameObjectWithTag("f5").GetComponent<Dropdown>().value].text,
            GameObject.FindGameObjectWithTag("f6").GetComponent<Dropdown>().options[GameObject.FindGameObjectWithTag("f6").GetComponent<Dropdown>().value].text,
            GameObject.FindGameObjectWithTag("f7").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f8").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f9").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f10").GetComponent<InputField>().text);
            ShowAllVideo();
        }
        else if (GameObject.FindGameObjectWithTag("f1").GetComponent<Text>().text == "Оперативная память")
        {
            string table = "Оперативная_память";
            EditData(table,
            System.Convert.ToInt32(GameObject.FindGameObjectWithTag("ID").GetComponent<Text>().text),
            GameObject.FindGameObjectWithTag("f3").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f4").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f5").GetComponent<Dropdown>().options[GameObject.FindGameObjectWithTag("f5").GetComponent<Dropdown>().value].text,
            GameObject.FindGameObjectWithTag("f6").GetComponent<Dropdown>().options[GameObject.FindGameObjectWithTag("f6").GetComponent<Dropdown>().value].text,
            GameObject.FindGameObjectWithTag("f7").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f8").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f9").GetComponent<InputField>().text,
            GameObject.FindGameObjectWithTag("f10").GetComponent<InputField>().text);
            ShowAllMemory();
        }
    }

    public GameObject dropdown_type_s, input_model_s;

    public void B_Search()
    {
        if (dropdown_type_s.GetComponent<Dropdown>().options[dropdown_type_s.GetComponent<Dropdown>().value].text == "Процессор")
        {
            SearchNameData("Процессор", input_model_s.GetComponent<InputField>().text);
            ShowData(1);
            DelTuggle = 1;
        }
        else if (dropdown_type_s.GetComponent<Dropdown>().options[dropdown_type_s.GetComponent<Dropdown>().value].text == "Материнская плата")
        {
            SearchNameData("Материнская_плата", input_model_s.GetComponent<InputField>().text);
            ShowData(2);
            DelTuggle = 2;
        }
        else if (dropdown_type_s.GetComponent<Dropdown>().options[dropdown_type_s.GetComponent<Dropdown>().value].text == "Видеокарта")
        {
            SearchNameData("Видеокарта", input_model_s.GetComponent<InputField>().text);
            ShowData(3);
            DelTuggle = 3;
        }
        else if (dropdown_type_s.GetComponent<Dropdown>().options[dropdown_type_s.GetComponent<Dropdown>().value].text == "Оперативная память")
        {
            SearchNameData("Оперативная_память", input_model_s.GetComponent<InputField>().text);
            ShowData(4);
            DelTuggle = 4;
        }
    }

    public GameObject Toggle, b_search, S_w;

    public void ToggleExtendedSearch()
    {
        if (Toggle.GetComponent<Toggle>().isOn == true)
        {
            dropdown_type_s.GetComponent<Dropdown>().interactable = false;
            input_model_s.GetComponent<InputField>().interactable = false;
            b_search.GetComponent<Button>().interactable = false;
            S_w.SetActive(true);
        }
        else
        {
            dropdown_type_s.GetComponent<Dropdown>().interactable = true;
            input_model_s.GetComponent<InputField>().interactable = true;
            b_search.GetComponent<Button>().interactable = true;
            S_w.SetActive(false);
        }
    }

    public void B_ExtendedSearch()
    {
        if (DD_type1.options[DD_type1.value].text == "Процессор")
        {
            ExtendedSearchData("Процессор",
                DD_prod1.options[DD_prod1.value].text,
                field31.GetComponent<InputField>().text,
                field41.GetComponent<InputField>().text,
                DD_field51.options[DD_field51.value].text,
                DD_field61.options[DD_field61.value].text,
                field71.GetComponent<InputField>().text,
                field81.GetComponent<InputField>().text,
                "",
                "");
            ShowData(1);
            DelTuggle = 1;
        }
        else if (DD_type1.options[DD_type1.value].text == "Материнская плата")
        {
            ExtendedSearchData("Процессор",
                DD_prod1.options[DD_prod1.value].text,
                field31.GetComponent<InputField>().text,
                field41.GetComponent<InputField>().text,
                DD_field51.options[DD_field51.value].text,
                DD_field61.options[DD_field61.value].text,
                field71.GetComponent<InputField>().text,
                field81.GetComponent<InputField>().text,
                field91.GetComponent<InputField>().text,
                "");
            ShowData(2);
            DelTuggle = 2;
        }
        else if (DD_type1.options[DD_type1.value].text == "Видеокарта")
        {
            ExtendedSearchData("Процессор",
                DD_prod1.options[DD_prod1.value].text,
                field31.GetComponent<InputField>().text,
                field41.GetComponent<InputField>().text,
                DD_field51.options[DD_field51.value].text,
                DD_field61.options[DD_field61.value].text,
                field71.GetComponent<InputField>().text,
                field81.GetComponent<InputField>().text,
                field91.GetComponent<InputField>().text,
                "");
            ShowData(3);
            DelTuggle = 3;
        }
        else if (DD_type1.options[DD_type1.value].text == "Оперативная память")
        {
            ExtendedSearchData("Процессор",
                DD_prod1.options[DD_prod1.value].text,
                field31.GetComponent<InputField>().text,
                field41.GetComponent<InputField>().text,
                DD_field51.options[DD_field51.value].text,
                DD_field61.options[DD_field61.value].text,
                field71.GetComponent<InputField>().text,
                field81.GetComponent<InputField>().text,
                field91.GetComponent<InputField>().text,
                field101.GetComponent<InputField>().text);
            ShowData(4);
            DelTuggle = 4;
        }
    }
}
