using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System;
public static class ScoreSaver
{
    private static string player = "";
    private static int score = 0;
    private static int amountOfRecords = 10;
    private static string[] records = new string[amountOfRecords];
    private static string scoresFilePath = Path.Combine(Application.dataPath, "StreamingAssets", "Scores.txt");
    static ScoreSaver()
    {
        EventBus.ResultGotten += SetScore;
        EventBus.PlayerGotten += SetPlayer;
        ReadFile();
    }
    public static void Initialize() { Debug.Log("ScoreSaver initialized"); }
    private static void SetPlayer(string value) 
    {
        player = value;
        SaveScore();
    }
    private static void SetScore(int value)
    {
        score = value;
    }
    private static void SaveScore()
    {
        for (int i = 0; i < amountOfRecords; i++)
        {
            int tempScore = GetScoreFromRecord(records[i]);

            if (tempScore > score)
                continue;

            string tempPlayer = GetPlayerFromRecord(records[i]);
            SetRecord(i, score, player);
            score = tempScore;
            player = tempPlayer;
        }
        File.WriteAllLines(scoresFilePath, records);
    }
    private static void SetRecord(int index, int score, string player)
    {
        if (index + 1 <= 9)
            records[index] = $"{index + 1}  | {player} -- {score}";
        else
            records[index] = $"{index + 1} | {player} -- {score}";
    }
    private static int GetScoreFromRecord(string record)
    {
        string score = "";
        int i = 1;
        while (int.TryParse(record[record.Length - i].ToString(), out int number))
        {
            score += number.ToString();
            i++;
        }
        score = new string(score.Reverse().ToArray());
        return int.Parse(score);
    }
    private static string GetPlayerFromRecord(string record)
    {
        string player = "";
        int endOfNickIndex = record.Length - 1;

        while (int.TryParse(record[endOfNickIndex].ToString(), out int i))
            endOfNickIndex--;

        endOfNickIndex -= 4;

        for (int i = 5; i <= endOfNickIndex; i++)
            player += record[i];

        return player;
    }
    private static void ReadFile()
    {
        bool recordsAreIncorrect = false;
        if (!File.Exists(scoresFilePath))
        {
            for (int k = 0; k < amountOfRecords; k++)
                SetRecord(k, 0, "None");
            recordsAreIncorrect = true;
        }
        else
        {
            string[] tempRecords = File.ReadAllLines(scoresFilePath);
            if (tempRecords.Length >= amountOfRecords)
            {
                for (int i = 0; i < amountOfRecords; i++)
                {
                    if (Regex.IsMatch(tempRecords[i], $@"^{i + 1}  ?\| .* -- \d+$"))
                    {
                        records[i] = tempRecords[i];
                    }
                    else
                    {
                        recordsAreIncorrect = true;
                        SetRecord(i, 0, "None");
                    }
                }
            }
            else
            {
                recordsAreIncorrect = true;
                int i = 0;
                for (; i < tempRecords.Length; i++)
                {
                    if (Regex.IsMatch(tempRecords[i], $@"^{i + 1}  ?\| .* -- \d+$"))
                        records[i] = tempRecords[i];
                    else
                        SetRecord(i, 0, "None");
                }
                for (; i < amountOfRecords; i++)
                {
                    SetRecord(i, 0, "None");
                }
            }
        }
        if (recordsAreIncorrect)
        {
            File.WriteAllLines(scoresFilePath, records);
        }
    }
}