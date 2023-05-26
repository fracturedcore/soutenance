using System;
public class HighScore : IComparable<HighScore>
{
    public int Score { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    
    public string Type { get; set; }
    public int Id { get; set; }
    
    public HighScore(int pId, string pName,int pScore, DateTime pDate, string pType)
    {
        Id = pId;
        Score = pScore;
        Name = pName;
        Date = pDate;
        Type = pType;
    }

    public int CompareTo(HighScore other)
    {
        if ( other.Score < this.Score)
        {
            return -1;
        }
        else if (other.Score > this.Score)
        {
            return 1;
        } 
        else if (other.Date < this.Date)
        {
            return -1;
        }
        else if (other.Date > this.Date)
        {
            return 1;
        }
        return 0;
    }
}
