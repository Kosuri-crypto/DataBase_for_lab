using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class CellScript : MonoBehaviour
{
    public GameObject Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10;
    public GameObject curObj;
    public GameObject Scripts, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, EditWindow, Main, ID;

    void Start()
    {
        Scripts = GameObject.FindGameObjectWithTag("Scripts");
        EditWindow = GameObject.FindGameObjectWithTag("EditWindow");
        Main = GameObject.FindGameObjectWithTag("Main");
        f1 = GameObject.FindGameObjectWithTag("f1");
        f2 = GameObject.FindGameObjectWithTag("f2");
        f3 = GameObject.FindGameObjectWithTag("f3");
        f4 = GameObject.FindGameObjectWithTag("f4");
        f5 = GameObject.FindGameObjectWithTag("f5");
        f6 = GameObject.FindGameObjectWithTag("f6");
        f7 = GameObject.FindGameObjectWithTag("f7");
        f8 = GameObject.FindGameObjectWithTag("f8");
        f9 = GameObject.FindGameObjectWithTag("f9");
        f10 = GameObject.FindGameObjectWithTag("f10");
        ID = GameObject.FindGameObjectWithTag("ID");
        Optionsfield5 = new List<string>();
        Optionsfield6 = new List<string>();
    }

    public void SetData(string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string Field8, string Field9, string Field10)
    {
        this.Field1.GetComponent<Text>().text = Field1;
        this.Field2.GetComponent<Text>().text = Field2;
        this.Field3.GetComponent<Text>().text = Field3;
        this.Field4.GetComponent<Text>().text = Field4;
        this.Field5.GetComponent<Text>().text = Field5;
        this.Field6.GetComponent<Text>().text = Field6;
        this.Field7.GetComponent<Text>().text = Field7;
        this.Field8.GetComponent<Text>().text = Field8;
        this.Field9.GetComponent<Text>().text = Field9;
        this.Field10.GetComponent<Text>().text = Field10;
    }

    private string table;

    public void DeleteThisDataCell()
    {
        if (Scripts.GetComponent<DB_Manager>().DelTuggle == 1) { table = "Процессор"; }
        else if (Scripts.GetComponent<DB_Manager>().DelTuggle == 2) { table = "Материнская_плата"; }
        else if (Scripts.GetComponent<DB_Manager>().DelTuggle == 3) { table = "Видеокарта"; }
        else if (Scripts.GetComponent<DB_Manager>().DelTuggle == 4) { table = "Оперативная_память"; }
        Scripts.GetComponent<DB_Manager>().DeleteData(table, System.Convert.ToInt32(curObj.GetComponentInChildren<Text>().text));
        Destroy(curObj);
    }

    List<string> Optionsfield5, Optionsfield6;

    private void GetField5(string table, int tuggle)
    {
        Optionsfield5.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(DB_Manager.connectionString))
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

    private void GetField6(string table)
    {
        Optionsfield6.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(DB_Manager.connectionString))
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
                        if (table == "Тип_памяти") { Optionsfield6.Add(reader.GetString(1)); }
                        else { Optionsfield6.Add(reader.GetInt32(1).ToString()); }
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

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

    Text[] curFields;
    public void EditThisDataCell()
    {
        EditWindow.transform.SetParent(Main.GetComponent<Transform>());
        EditWindow.GetComponent<Transform>().localPosition = new Vector3(1, 1, 1);
        curFields = curObj.GetComponentsInChildren<Text>();
        ID.GetComponent<Text>().text = curFields[0].text;
        if (Scripts.GetComponent<DB_Manager>().DelTuggle == 1)
        {
            table = "Процессор";
            f1.GetComponent<Text>().text = "Процессор";
            f2.GetComponent<Text>().text = curFields[1].text;
            f3.GetComponent<InputField>().text = curFields[2].text;
            f4.GetComponent<InputField>().text = curFields[3].text;
            f5.GetComponent<Dropdown>().ClearOptions();
            f6.GetComponent<Dropdown>().ClearOptions();
            GetField5("Число_ядер", 0);
            GetField6("Объём_памяти");
            f5.GetComponent<Dropdown>().AddOptions(Optionsfield5);
            f6.GetComponent<Dropdown>().AddOptions(Optionsfield6);
            f5.GetComponent<Dropdown>().value = Optionsfield5.IndexOf(curFields[4].text);
            f6.GetComponent<Dropdown>().value = Optionsfield6.IndexOf(curFields[5].text);
            f7.GetComponent<InputField>().text = curFields[6].text;
            f8.GetComponent<InputField>().text = curFields[7].text;
            f9.GetComponent<InputField>().interactable = false;
            f9.GetComponent<InputField>().text = "";
            f10.GetComponent<InputField>().interactable = false;
            f10.GetComponent<InputField>().text = "";
        }
        else if (Scripts.GetComponent<DB_Manager>().DelTuggle == 2)
        {
            table = "Материнская_плата";
            f1.GetComponent<Text>().text = "Материнская плата";
            f2.GetComponent<Text>().text = curFields[1].text;
            f3.GetComponent<InputField>().text = curFields[2].text;
            f4.GetComponent<InputField>().text = curFields[3].text;
            f5.GetComponent<Dropdown>().ClearOptions();
            f6.GetComponent<Dropdown>().ClearOptions();
            GetField5("Тип_памяти", 1);
            GetField6("Объём_памяти");
            f5.GetComponent<Dropdown>().AddOptions(Optionsfield5);
            f6.GetComponent<Dropdown>().AddOptions(Optionsfield6);
            f5.GetComponent<Dropdown>().value = Optionsfield5.IndexOf(curFields[4].text);
            f6.GetComponent<Dropdown>().value = Optionsfield6.IndexOf(curFields[5].text);
            f7.GetComponent<InputField>().text = curFields[6].text;
            f8.GetComponent<InputField>().text = curFields[7].text;
            f9.GetComponent<InputField>().interactable = true;
            f9.GetComponent<InputField>().text = curFields[8].text;
            f10.GetComponent<InputField>().interactable = false;
            f10.GetComponent<InputField>().text = "";
        }
        else if (Scripts.GetComponent<DB_Manager>().DelTuggle == 3)
        {
            table = "Видеокарта";
            f1.GetComponent<Text>().text = "Видеокарта";
            f2.GetComponent<Text>().text = curFields[1].text;
            f3.GetComponent<InputField>().text = curFields[2].text;
            f4.GetComponent<InputField>().text = curFields[3].text;
            f5.GetComponent<Dropdown>().ClearOptions();
            f6.GetComponent<Dropdown>().ClearOptions();
            GetField5("Тип_памяти", 2);
            GetField6("Объём_памяти");
            f5.GetComponent<Dropdown>().AddOptions(Optionsfield5);
            f6.GetComponent<Dropdown>().AddOptions(Optionsfield6);
            f5.GetComponent<Dropdown>().value = Optionsfield5.IndexOf(curFields[4].text);
            f6.GetComponent<Dropdown>().value = Optionsfield6.IndexOf(curFields[5].text);
            f7.GetComponent<InputField>().text = curFields[6].text;
            f8.GetComponent<InputField>().text = curFields[7].text;
            f9.GetComponent<InputField>().interactable = true;
            f9.GetComponent<InputField>().text = curFields[8].text;
            f10.GetComponent<InputField>().interactable = false;
            f10.GetComponent<InputField>().text = "";
        }
        else if (Scripts.GetComponent<DB_Manager>().DelTuggle == 4)
        {
            table = "Оперативная_память";
            f1.GetComponent<Text>().text = "Оперативная память";
            f2.GetComponent<Text>().text = curFields[1].text;
            f3.GetComponent<InputField>().text = curFields[2].text;
            f4.GetComponent<InputField>().text = curFields[3].text;
            f5.GetComponent<Dropdown>().ClearOptions();
            f6.GetComponent<Dropdown>().ClearOptions();
            GetField5("Тип_памяти", 1);
            GetField6("Объём_памяти");
            f5.GetComponent<Dropdown>().AddOptions(Optionsfield5);
            f6.GetComponent<Dropdown>().AddOptions(Optionsfield6);
            f5.GetComponent<Dropdown>().value = Optionsfield5.IndexOf(curFields[4].text);
            f6.GetComponent<Dropdown>().value = Optionsfield6.IndexOf(curFields[5].text);
            f7.GetComponent<InputField>().text = curFields[6].text;
            f8.GetComponent<InputField>().text = curFields[7].text;
            f9.GetComponent<InputField>().interactable = true;
            f9.GetComponent<InputField>().text = curFields[8].text;
            f10.GetComponent<InputField>().interactable = true;
            f10.GetComponent<InputField>().text = curFields[9].text;
        }
    }
}
