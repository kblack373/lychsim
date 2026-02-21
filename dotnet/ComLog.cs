using CsvHelper;
using System;
using System.Globalization;
//csv writer
public class CombatLog
{
	// instance variables
	// or: local variables
	public int Round;
	public int Turn;
	public string UnitName;
	public int UnitIdr;
	public double UnitAccuracy;
	public bool HitSuccess;
	public double HitRollResult;
	public string TargetName;
	public int TargetIdr;
    public double TargetDodge;
    public int TargetAc;
    public int Damage;
	public int TargetRemainHp;

    public CombatLog(int inRound,
                     int inTurn,
                     string inUnitName,
                     int inUnitIdr,
                     double inUnitAccuracy,
                     bool inHitSuccess,
                     double inHitRollResult,
                     string inTargetName,
                     int inTargetIdr,
                     double inTargetDodge,
                     int inTargetAc,
                     int inDamage,
                     int inTargetRemainHp)
	{
        int Round = inRound;
        int Turn = inTurn;
        string UnitName = inUnitName;
        int UnitIdr = inUnitIdr;
        double UnitAccuracy = inUnitAccuracy;
        bool HitSuccess = inHitSuccess;
        double HitRollResult = inHitRollResult;
        string TargetName =inTargetName;
        int TargetIdr = inTargetIdr;
        double TargetDodge = inTargetDodge;
        int TargetAc = inTargetAc;
        int Damage = inDamage;
        int TargetRemainHp = inTargetRemainHp;
    }

}

public class CombatLogger()
{
	private List<CombatLog> ListLogsBuffer = new List<CombatLog>(); 
    

    public int InsertLog(CombatLog log)
    {
        this.ListLogsBuffer.Add(log);
        return ListLogsBuffer.Count;
    }

    //todo: handle file output and writing

   public void WriteCsvLog(string filePath)
    {
        using StreamWriter output = File.CreateText(filePath);
        using var csvWriter = new CsvWriter(output, CultureInfo.InvariantCulture);

        // now we can write physical

        // write header

        // write data

        // flush logs
        FlushLogBuffer();
        return;

    }

    public void FlushLogBuffer()
    {
        // dump logs
        this.ListLogsBuffer = new List<CombatLog>();
    }

}