using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
//csv writer
public class CombatLog
{
	// instance variables
	// or: local variables
	public int Round { get; set; }
	public int Turn { get; set; }
    public string UnitName { get; set; }
    public int UnitIdr { get; set; }
    public double UnitAccuracy { get; set; }
    public bool HitSuccess { get; set; }
    public double HitRollResult { get; set; }
    public string TargetName { get; set; }
    public int TargetIdr { get; set; }
    public double TargetDodge { get; set; }
    public int TargetAc { get; set; }
    public int Damage { get; set; }
    public int TargetRemainHp { get; set; }

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
        Round = inRound;
        Turn = inTurn;
        UnitName = inUnitName;
        UnitIdr = inUnitIdr;
        UnitAccuracy = inUnitAccuracy;
        HitSuccess = inHitSuccess;
        HitRollResult = inHitRollResult;
        TargetName =inTargetName;
        TargetIdr = inTargetIdr;
        TargetDodge = inTargetDodge;
        TargetAc = inTargetAc;
        Damage = inDamage;
        TargetRemainHp = inTargetRemainHp;
    }



    public CombatLog(int inRound, int inTurn, string inUnitName, int inUnitIdr, double inUnitAccuracy,
                     string inTargetName, int inTargetIdr, double inTargetDodge, int inTargetAc)
    {
         Round = inRound;
         Turn = inTurn;
         UnitName = inUnitName;
         UnitIdr = inUnitIdr;
         UnitAccuracy = inUnitAccuracy;
         TargetName = inTargetName;
         TargetIdr = inTargetIdr;
         TargetDodge = inTargetDodge;
         TargetAc = inTargetAc;
    }
}

#region csvmapper
public sealed class CombatLogMap : ClassMap<CombatLog>
{
    //created to write cleanly to csv output
    //https://joshclose.github.io/CsvHelper/examples/configuration/class-maps/mapping-properties/
    public CombatLogMap() {

        Map(m => m.Round);
        Map(m => m.Turn);
        Map(m => m.UnitName);
        Map(m => m.UnitIdr);
        Map(m => m.UnitAccuracy);
        Map(m => m.HitSuccess);
        Map(m => m.HitRollResult);
        Map(m => m.TargetName);
        Map(m => m.TargetIdr);
        Map(m => m.TargetDodge);
        Map(m => m.TargetAc);
        Map(m => m.Damage);
        Map(m => m.TargetRemainHp);

    }


}
#endregion
#region aggregateCombatLoggerClass

public class CombatLogger()
{
	private List<CombatLog> ListLogsBuffer = new List<CombatLog>();
    private static string[] CsvHeaders = { "Round", "Turn", "UnitName", "UnitIdr", "UnitAccuracy", "HitSuccess", "HitRollResult", "TargetName", "TargetIdr", "TargetDodge", "TargetAc", "Damage", "TargetRemainHp" };

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
        csvWriter.Context.RegisterClassMap<CombatLogMap>();
        // now we can write physical
        // write data

        csvWriter.WriteRecords(ListLogsBuffer);

        //foreach (CombatLog log in this.ListLogsBuffer)
        //{
        //    csvWriter.WriteRecord(log.Round, log.Turn, log.UnitName, log.UnitIdr, log.UnitAccuracy, log.HitSuccess, log.HitRollResult, log.TargetName, log.TargetIdr, log.TargetDodge, log.TargetAc, log.Damage, log.TargetRemainHp);
        //}
        //// flush logs

        // can comment out to try to boost performance. this will delete all log objects from the list.
        //FlushLogBuffer();
        return;

    }

    public void FlushLogBuffer()
    {
        // dump logs
        this.ListLogsBuffer = new List<CombatLog>();
    }

}
#endregion